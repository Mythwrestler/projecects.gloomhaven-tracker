using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Service.Models.Content;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum MONSTER_STAT_MOD_TYPE
{

    [EnumMember(Value = "add")]
    add,
    [EnumMember(Value = "multi")]
    multi
}

[Serializable]
public class MonsterStatSet
{
    [JsonPropertyName("level")]
    public int Level { get; set; }

    [JsonPropertyName("health")]
    public string Health { get; set; } = string.Empty;

    [JsonPropertyName("movement")]
    public string Movement { get; set; } = string.Empty;

    [JsonPropertyName("attack")]
    public string Attack { get; set; } = string.Empty;

    [JsonPropertyName("defenseEffects")]
    public List<Effect> DefenseEffects { get; set; } = new List<Effect>();

    [JsonPropertyName("attackEffects")]
    public List<Effect> AttackEffects { get; set; } = new List<Effect>();

    [JsonPropertyName("immunity")]
    public List<EFFECT_TYPE> Immunity { get; set; } = new List<EFFECT_TYPE>();

}

[Serializable]
public class BaseMonsterStatSet
{
    [JsonPropertyName("elite")]
    public List<MonsterStatSet> Elite { get; set; } = new List<MonsterStatSet>();
    // Monster level, Monster Stats
    [JsonPropertyName("standard")]
    public List<MonsterStatSet> Standard { get; set; } = new List<MonsterStatSet>();
}


[Serializable]
public class Monster : ContentItem
{

    [JsonPropertyName("baseStats")]
    public BaseMonsterStatSet BaseStats { get; set; } = new BaseMonsterStatSet();

    [JsonIgnore]
    public ContentSummary Summary
    {
        get
        {
            return new ContentSummary()
            {
                ContentCode = ContentCode,
                Name = Name,
                Description = Description
            };
        }
    }
}