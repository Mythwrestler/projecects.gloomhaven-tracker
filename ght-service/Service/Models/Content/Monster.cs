using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Service.Models.Content;

[Serializable]
public struct MonsterStatSet
{
    public MonsterStatSet(int level, string health, string movement, string attack, bool rangeAttackable, bool meleeAttackable, List<Effect> defenseEffects, List<Effect> attackEffects, List<Effect> deathEffects, List<EFFECT_TYPE> immunity)
    {
        Level = level;
        Health = health;
        Movement = movement;
        Attack = attack;
        RangeAttackable = rangeAttackable;
        MeleeAttackable = meleeAttackable;
        DefenseEffects = defenseEffects;
        AttackEffects = attackEffects;
        DeathEffects = deathEffects;
        Immunity = immunity;
    }

    [JsonPropertyName("level")]
    public int Level { get; }

    [JsonPropertyName("health")]
    public string Health { get; }

    [JsonPropertyName("movement")]
    public string Movement { get; }

    [JsonPropertyName("attack")]
    public string Attack { get; }

    [JsonPropertyName("rangeAttackable")]
    public bool RangeAttackable { get; }

    [JsonPropertyName("meleeAttackable")]
    public bool MeleeAttackable { get; }

    [JsonPropertyName("defenseEffects")]
    public List<Effect> DefenseEffects { get; }

    [JsonPropertyName("attackEffects")]
    public List<Effect> AttackEffects { get; }

    [JsonPropertyName("deathEffects")]
    public List<Effect> DeathEffects { get; }

    [JsonPropertyName("immunity")]
    public List<EFFECT_TYPE> Immunity { get; }

}

[Serializable]
public struct BaseMonsterStatSet
{
    public BaseMonsterStatSet(List<MonsterStatSet> elite, List<MonsterStatSet> standard)
    {
        Elite = elite;
        Standard = standard;
    }

    [JsonPropertyName("elite")]
    public List<MonsterStatSet> Elite { get; }
    // Monster level, Monster Stats
    [JsonPropertyName("standard")]
    public List<MonsterStatSet> Standard { get; }
}


[Serializable]
public struct Monster : ContentItem
{
    public Monster(Guid id, string contentCode, string name, string description, BaseMonsterStatSet baseStats)
    {
        Id = id;
        ContentCode = contentCode;
        Name = name;
        Description = description;
        BaseStats = baseStats;
    }

    [JsonIgnore]
    public Guid Id { get; }

    [JsonPropertyName("contentCode")]
    public string ContentCode { get; }

    [JsonPropertyName("name")]
    public string Name { get; }

    [JsonPropertyName("description")]
    public string Description { get; }

    [JsonPropertyName("baseStats")]
    public BaseMonsterStatSet BaseStats { get; }

    [JsonIgnore]
    public ContentSummary Summary
    {
        get
        {
            return new ContentSummary(
                ContentCode,
                Name,
                Description
            );
        }
    }
}