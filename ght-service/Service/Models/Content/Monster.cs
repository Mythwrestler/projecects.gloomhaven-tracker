using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Service.Models.Content;

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

    [JsonPropertyName("rangeAttackable")]
    public bool RangeAttackable { get; set; } = true;

    [JsonPropertyName("meleeAttackable")]
    public bool MeleeAttackable { get; set; } = true;

    [JsonPropertyName("defenseEffects")]
    public List<Effect> DefenseEffects { get; set; } = new List<Effect>();

    [JsonPropertyName("attackEffects")]
    public List<Effect> AttackEffects { get; set; } = new List<Effect>();

    [JsonPropertyName("deathEffects")]
    public List<Effect> DeathEffects { get; set; } = new List<Effect>();

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
public class Monster
{

    [JsonPropertyName("contentCode")]
    public string ContentCode { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

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