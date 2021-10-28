using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Service.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PLAYER_CLASS
{
    [EnumMember(Value = "hatchet")]
    Hatchet,

    [EnumMember(Value = "demolitionist")]
    Demolitionist,

    [EnumMember(Value = "voidwarden")]
    VoidWarden,

    [EnumMember(Value = "redguard")]
    RedGuard,
}


[Serializable]
public abstract class Actor
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = String.Empty;

    public abstract int BaseHealth { get; }

    public int Health { get; set; }

    public ActorEffects Effects { get; set; } = new ActorEffects();

}


[Serializable]
public class PlayerBaseHealth
{
    [JsonPropertyName("level")]
    public int Level {get; set;}
    [JsonPropertyName("health")]
    public int Health {get; set;}
}

[Serializable]
public class PlayerLevel
{
    [JsonPropertyName("experience")]
    public int Experience {get; set;}
    [JsonPropertyName("level")]
    public int Level {get; set;}
}


[Serializable]
public class Player : Actor
{
    public List<AttackModifier> BaseModifierDeck { get; set; } = new List<AttackModifier>();

    public AttackModifierDeck ModifierDeck { get; set; } = new AttackModifierDeck(new List<AttackModifier>());

    public List<PlayerBaseHealth> BaseHealthStats { get; set; } = new List<PlayerBaseHealth>();

    override public int BaseHealth => BaseHealthStats.First(hs => hs.Level == CurrentLevel)?.Health ?? 0;

    public List<PlayerLevel> Levels { get; set; } = new List<PlayerLevel>();

    public int Experience { get; set; } = 0;

    public int CurrentLevel
    {
        get
        {
            return Levels.OrderBy(lvl => lvl.Experience).Where(lvl => lvl.Experience >= Experience).First()?.Level ?? 0;
        }
    }

    public PlayerDO State => new PlayerDO
    {
        Id = Id,
        Name = Name,
        BaseHealthStats = BaseHealthStats,
        BaseModifierDeck = BaseModifierDeck,
        Effects = Effects.ActiveEffects,
        Experience = Experience,
        Health = Health,
        Levels = Levels,
        modifierDeck = ModifierDeck.State
    };

    public PlayerDTO DTO => new PlayerDTO
    {
        Id = Id,
        Name = Name,
        BaseHealth = BaseHealth,
        Effects = Effects.ActiveEffects,
        Experience = Experience,
        Health = Health,
        Levels = Levels,
        modifierDeck = ModifierDeck.DTO,
        flippedModifierCards = ModifierDeck.GetFlippedCards()
    };
}


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
    public int Health { get; set; }

    [JsonPropertyName("healthMod")]
    public MONSTER_STAT_MOD_TYPE? HealthMod {get; set;}

    [JsonPropertyName("movement")]
    public int Movement { get; set; }

    [JsonPropertyName("moveMod")]
    public MONSTER_STAT_MOD_TYPE? MoveMod {get; set;}

    [JsonPropertyName("attack")]
    public int Attack { get; set; }

    [JsonPropertyName("attackMod")]
    public MONSTER_STAT_MOD_TYPE? attackMod {get; set;}
    
    [JsonPropertyName("defenseEffects")]
    public List<ActorEffect> DefenseEffects { get; set; } = new List<ActorEffect>();
    
    [JsonPropertyName("attackEffects")]
    public List<ActorEffect> AttackEffects { get; set; } = new List<ActorEffect>();
    
    [JsonPropertyName("immunity")]
    public List<ACTOR_EFFECT_TYPE> Immunity { get; set; } = new List<ACTOR_EFFECT_TYPE>();

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
public class Monster : Actor
{
    public string Type { get; set; } = string.Empty;
    public BaseMonsterStatSet BaseStats { get; set; } = new BaseMonsterStatSet();
    public int Level { get; set; }
    public bool IsElite { get; set; }
    public int MonsterId { get; set; }
    public int Attack
    {
        get
        {
            return IsElite ? BaseStats.Elite.First(bs => bs.Level == Level).Attack : BaseStats.Standard.First(bs => bs.Level == Level).Attack;
        }
    }
    public int Movement
    {
        get
        {
            return IsElite ? BaseStats.Elite.First(bs => bs.Level == Level).Movement : BaseStats.Standard.First(bs => bs.Level == Level).Movement;
        }
    }
    override public int BaseHealth
    {
        get
        {
            return IsElite ? BaseStats.Elite.First(bs => bs.Level == Level).Health : BaseStats.Standard.First(bs => bs.Level == Level).Health;
        }
    }
    public MonsterDO State => new MonsterDO
    {
        Id = Id,
        BaseStats = BaseStats,
        Level = Level,
        IsElite = IsElite,
        Effects = Effects.ActiveEffects,
        MonsterId = MonsterId,
        Health = Health,
        Attack = Attack,
        Movement = Movement,
        Name = Name,
        Type = Type,
    };
    public MonsterDTO DTO => new MonsterDTO
    {
        Id = Id,
        Level = Level,
        IsElite = IsElite,
        Effects = Effects.ActiveEffects,
        MonsterId = MonsterId,
        BaseHealth = BaseHealth,
        Health = Health,
        Attack = Attack,
        Movement = Movement,
        Name = Name,
        Type = Type,
    };
}

[Serializable]
public class Objective : Actor
{
    private int _baseHealth;
    public Objective(int baseHealth = 0) { _baseHealth = baseHealth; }
    public string ObjectiveId { get; set; } = string.Empty;
    override public int BaseHealth => _baseHealth;
    public ObjectiveDO State => new ObjectiveDO()
    {
        Id = Id,
        BaseHealth = BaseHealth,
        Effects = Effects.ActiveEffects,
        ObjectiveId = ObjectiveId,
        Health = Health,
        Name = Name
    };
    public ObjectiveDTO DTO => new ObjectiveDTO()
    {
        Id = Id,
        BaseHealth = BaseHealth,
        Effects = Effects.ActiveEffects,
        ObjectiveId = ObjectiveId,
        Health = Health,
        Name = Name
    };
}



[Serializable]
public class ActorDO
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = String.Empty;

    public int Health { get; set; }

    public List<ActorEffect> Effects { get; set; } = new List<ActorEffect>();
}

[Serializable]
public class PlayerDO : ActorDO
{
    public List<AttackModifier> BaseModifierDeck { get; set; } = new List<AttackModifier>();

    public AttackModifierDeckDO modifierDeck { get; set; } = new AttackModifierDeckDO();

    public List<PlayerBaseHealth> BaseHealthStats { get; set; } = new List<PlayerBaseHealth>();

    public List<PlayerLevel> Levels { get; set; } = new List<PlayerLevel>();

    public int Experience { get; set; } = 0;
}

[Serializable]
public class MonsterDO : ActorDO
{
    public string Type { get; set; } = string.Empty;
    public BaseMonsterStatSet BaseStats { get; set; } = new BaseMonsterStatSet();
    public int Level { get; set; }
    public bool IsElite { get; set; }
    public int MonsterId { get; set; }
    public int Attack { get; set; }
    public int Movement { get; set; }
}

[Serializable]
public class ObjectiveDO : ActorDO
{
    public int BaseHealth { get; set; }
    public string ObjectiveId { get; set; } = string.Empty;
}


public class ActorDTO
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = String.Empty;
    public int? BaseHealth { get; set; }
    public int Health { get; set; }
    public List<ActorEffect> Effects { get; set; } = new List<ActorEffect>();
}
public class MonsterDTO : ActorDTO
{
    public string Type { get; set; } = string.Empty;
    public BaseMonsterStatSet? BaseStats { get; set; }
    public int Level { get; set; }
    public bool IsElite { get; set; }
    public int MonsterId { get; set; }
    public int Attack { get; set; }
    public int Movement { get; set; }
}
public class ObjectiveDTO : ActorDTO
{
    public string ObjectiveId { get; set; } = string.Empty;
}

public class PlayerDTO : ActorDTO
{
    public List<AttackModifier>? BaseModifierDeck { get; set; }

    public AttackModifierDeckDTO? modifierDeck { get; set; }

    public List<AttackModifier>? flippedModifierCards { get; set; }

    public List<PlayerBaseHealth>? BaseHealthStats { get; set; } = new List<PlayerBaseHealth>();

    public List<PlayerLevel>? Levels { get; set; } = new List<PlayerLevel>();

    public int Experience { get; set; } = 0;
}

[Serializable]
public class ActorsDTO
{
    public List<PlayerDTO> Players { get; set; } = new List<PlayerDTO>();
    public List<MonsterDTO> Monsters { get; set; } = new List<MonsterDTO>();
    public List<ObjectiveDTO> Objectives { get; set; } = new List<ObjectiveDTO>();
}

[Serializable]
public class ActorsDO
{
    public List<PlayerDO> Players { get; set; } = new List<PlayerDO>();
    public List<MonsterDO> Monsters { get; set; } = new List<MonsterDO>();
    public List<ObjectiveDO> Objectives { get; set; } = new List<ObjectiveDO>();
}