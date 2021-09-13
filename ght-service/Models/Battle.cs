using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Service.Models;

public enum ACTOR_EFFECT_TYPE
{
    [EnumMember(Value = "strength")]
    Strength,

    [EnumMember(Value = "poision")]
    Poision,

    [EnumMember(Value = "stun")]
    Stun,

    [EnumMember(Value = "shield")]
    Shield,
}

public enum ATTACK_MODIFIER_TYPE 
{
    [EnumMember(Value = "add")]
    Add,

    [EnumMember(Value = "multiply")]
    Multiply,

    [EnumMember(Value = "cancel")]
    Cancel,
}

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


public enum MONSTER_TYPES
{
    [EnumMember(Value = "giantViper")]
    GiantViper,

    [EnumMember(Value = "cultist")]
    Cultist,

    [EnumMember(Value = "stoneGolem")]
    StoneGolem,
}

[Serializable]
public class AttackModifier
{
    public ATTACK_MODIFIER_TYPE Type {get; set;}

    public bool isCurse {get; set;} = false;

    public bool isBlessing {get; set;} = false;

    public int? Value {get; set;}
}

[Serializable]
public class ActorEffect
{
    public ACTOR_EFFECT_TYPE Type {get; set;}

    public int Value {get; set;} = -1;

    public int Duration {get; set;} = -1;
}

[Serializable]
public abstract class Actor
{
    public Guid Id {get; set;}

    public string Name {get; set;} = String.Empty;
}

[Serializable]
public class Player : Actor
{
    public int CurrentHealth {get; set;}

    public List<AttackModifier> BaseModifierDeck {get; set;} = new List<AttackModifier>();

    public List<AttackModifier>? ModifierDeck {get; set;}

    public Dictionary<int, int> BaseHealth {get; set;} = new Dictionary<int, int>();

    public Dictionary<int, int> Level {get; set;} = new Dictionary<int, int>();
    
    public List<ActorEffect> Effects {get; set;} = new List<ActorEffect>();
}


[Serializable]
public class MonsterStatSet
{
    public int Health {get; set;}

    public int Movement {get; set;}

    public int Attack {get; set;}

    public List<ActorEffect> Effects {get; set;} = new List<ActorEffect>();

}

[Serializable]
public class BaseMonsterStatSet 
{
    public MonsterStatSet Elite {get; set;} = new MonsterStatSet();

    public MonsterStatSet Standard {get; set;} = new MonsterStatSet();
}

[Serializable]
public class Monster : Actor 
{
    public MONSTER_TYPES Type {get; set;}
    public int level {get; set;}
    public bool IsElite {get; set;}
    public int MonsterId {get; set;}
    public Dictionary<int, BaseMonsterStatSet> BaseStats {get; set;} = new Dictionary<int, BaseMonsterStatSet>();
    public int Health {get; set;}
    public int Attack {get; set;}
    public int Movement {get; set;}
    public List<ActorEffect> Effects {get; set;} = new List<ActorEffect>();
}

[Serializable]
public class Battle
{
    public List<Actor> Actors {get; set;} = new List<Actor>();

    public List<AttackModifier> MonsterDeck {get; set;} = new List<AttackModifier>();

    public Dictionary<int, Guid> Initiative {get; set;} = new Dictionary<int, Guid>();
}

public class BattleDTO
{
    
    public List<object> Actors {get; set;} = new List<object>();

    public List<AttackModifier> MonsterDeck {get; set;} = new List<AttackModifier>();

    public Dictionary<int, Guid> Initiative {get; set;} = new Dictionary<int, Guid>();
}

[Serializable]
public class BattleAction 
{

    public Guid Source {get; set;}

    public Guid Target {get; set;}

    public int Damage {get; set;}
    
    public List<ActorEffect> Effects {get; set;} = new List<ActorEffect>();
}

[Serializable]
public class BattleActionResult 
{
    public string Affect {get; set;} = string.Empty;

    public string Affected {get; set;} = string.Empty;
}