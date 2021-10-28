using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Service.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum GAME_CODES
{
    [EnumMember(Value = "jawsOfTheLion")]
    JawsOfTheLion,

    [EnumMember(Value = "original")]
    Original
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum CONTENT_TYPE
{
    [EnumMember(Value = "game")]
    game,

    [EnumMember(Value = "player")]
    player,

    [EnumMember(Value = "monster")]
    monster,

}

public static class GameUtils
{
    public static string codeString(GAME_CODES? gameCode)
    {
        switch (gameCode)
        {
            case GAME_CODES.JawsOfTheLion:
                return "jawsOfTheLion";
            case GAME_CODES.Original:
                return "original";
            default:
                return "";
        }
    }

    public static string kindString(CONTENT_TYPE? type)
    {
        switch (type)
        {
            case CONTENT_TYPE.game:
                return "game";
            case CONTENT_TYPE.monster:
                return "monster";
            case CONTENT_TYPE.player:
                return "player";
            default:
                return "";
        }
    }
}

[Serializable]
public class GameDefaults
{
    [JsonPropertyName("name")]
    public string Name {get; set;} = string.Empty;

    [JsonPropertyName("code")]
    public GAME_CODES Code { get; set; }
    
    [JsonPropertyName("kind")]
    public string Kind {get; set;} = string.Empty;

    // public List<AttackModifier> BaseModifierDeck = new List<AttackModifier>();
    [JsonPropertyName("baseModifierDeck")]
    public List<AttackModifier> BaseModifierDeck {get; set;} = new List<AttackModifier>();
}
public class ContentItemSummary {
    [JsonPropertyName("contentId")]
    public Guid ContentId {get; set;} = new Guid();

    [JsonPropertyName("name")]
    public string Name {get; set;} = string.Empty;

    [JsonPropertyName("code")]
    public string Code { get; set; } = string.Empty;
}

[Serializable]
public class BasePlayerStats
{
    [JsonPropertyName("levels")]
    public List<PlayerLevel> Levels {get; set;} = new List<PlayerLevel>();
    [JsonPropertyName("health")]
    public List<PlayerBaseHealth> Health {get; set;} = new List<PlayerBaseHealth>();
}

public class PlayerSummary
{
    [JsonPropertyName("contentId")]
    public Guid ContentId {get; set;} = new Guid();
    
    [JsonPropertyName("name")]
    public string Name {get; set;} = string.Empty;

    [JsonPropertyName("code")]
    public GAME_CODES Code { get; set; }
}

[Serializable]
public class PlayerDefaults
{

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("code")]
    public string Code { get; set; } = string.Empty;

    [JsonPropertyName("baseStats")]
    public BasePlayerStats BaseStats {get; set;} = new BasePlayerStats();
}


[Serializable]
public class MonsterDefaults
{   
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonPropertyName("code")]
    public string Code { get; set; } = string.Empty;
    
    [JsonPropertyName("baseStats")]
    public BaseMonsterStatSet BaseStats { get; set; } = new BaseMonsterStatSet();
}

