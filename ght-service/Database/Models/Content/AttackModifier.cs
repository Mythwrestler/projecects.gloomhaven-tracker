using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Database.Models.Content;

public enum ATTACK_MODIFIER_TYPE
{
    Add,
    Multiply,
    Cancel,
}

public class AttackModifier
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ContentCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ATTACK_MODIFIER_TYPE Type { get; set; }
    public bool IsCurse { get; set; } = false;
    public bool IsBlessing { get; set; } = false;
    public bool TriggerShuffle { get; set; } = false;
    public int? Value { get; set; }
    public ICollection<AttackModifierEffect>? Effects { get; set; }
    public Guid? GameId { get; set; }
    public Game? Game { get; set; }
}

public class AttackModifierEffect
{
    public Guid? EffectId {get; set;}
    public Effect? Effect {get; set;}
    public Guid? AttackModifierId { get; set; }
    public AttackModifier? AttackModifier { get; set; }
}