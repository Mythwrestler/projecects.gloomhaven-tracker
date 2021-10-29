using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using GloomhavenTracker.Service.Models;

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

    [EnumMember(Value = "scenario")]
    scenario,

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
            case CONTENT_TYPE.scenario:
                return "scenario";
            default:
                return "";
        }
    }
}


public class ContentItemSummary {
    [JsonPropertyName("name")]
    public string Name {get; set;} = string.Empty;

    [JsonPropertyName("code")]
    public string Code { get; set; } = string.Empty;
}

[Serializable]
public class GameContent
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
    public ContentItemSummary ToDTO()
    {
        return new ContentItemSummary()
        {
            Name = Name,
            Code = GameUtils.codeString(Code)
        };
    }
}


[Serializable]
public class BasePlayerStats
{
    [JsonPropertyName("levels")]
    public List<PlayerLevel> Levels {get; set;} = new List<PlayerLevel>();
    [JsonPropertyName("health")]
    public List<PlayerBaseHealth> Health {get; set;} = new List<PlayerBaseHealth>();
}

[Serializable]
public class PlayerContent
{

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("code")]
    public string Code { get; set; } = string.Empty;

    [JsonPropertyName("baseStats")]
    public BasePlayerStats BaseStats {get; set;} = new BasePlayerStats();

    public ContentItemSummary ToDTO()
    {
        return new ContentItemSummary()
        {
            Name = Name,
            Code = Code
        };
    }

}




[Serializable]
public class MonsterContent
{   
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonPropertyName("code")]
    public string Code { get; set; } = string.Empty;
    
    [JsonPropertyName("baseStats")]
    public BaseMonsterStatSet BaseStats { get; set; } = new BaseMonsterStatSet();
    
    public ContentItemSummary ToDTO()
    {
        return new ContentItemSummary()
        {
            Name = Name,
            Code = Code
        };
    }
}

public class ScenarioContent
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("code")]
    public string Code { get; set; } = string.Empty;
    
    [JsonPropertyName("scenarioNumber")]
    public int ScenarioNumber {get; set;}

    [JsonPropertyName("monsters")]
    public List<MonsterContent> Monsters {get; set;} = new List<MonsterContent>();

    public ScenarioContentDTO ToDTO()
    {
        return new ScenarioContentDTO()
        {
            Code = Code,
            Name = Name,
            ScenarioNumber = ScenarioNumber,
            Monsters = Monsters.Select(mon => new ContentItemSummary(){Code = mon.Code, Name = mon.Name}).ToList()
        };
    }
}

public class ScenarioContentDTO
{
    public string Code {get; set;} = string.Empty;
    public string Name {get; set;} = string.Empty;
    public int ScenarioNumber {get; set;}
    public List<ContentItemSummary> Monsters { get; set; } = new List<ContentItemSummary>();
}
