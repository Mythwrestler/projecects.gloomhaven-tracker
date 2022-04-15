using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GloomhavenTracker.Database.Models.Content;

public static partial class EntityDefinitions
{
    private static EnumToStringConverter<EFFECT_TYPE_DAO> effectTypeConverter = new EnumToStringConverter<EFFECT_TYPE_DAO>();
    private static EnumToStringConverter<ELEMENT_DAO> elementConverter = new EnumToStringConverter<ELEMENT_DAO>();
    public static void DefineEffectEntities(this ModelBuilder builder)
    {
        builder.Entity<EffectDAO>(effectTable =>
        {
            effectTable.HasIndex(effect => new { effect.Type, effect.Value, effect.Duration }).IsUnique();
            effectTable.Property(effect => effect.Type).HasConversion(effectTypeConverter);
            effectTable.Property(effect => effect.Element).HasConversion(elementConverter);
            effectTable.HasMany(effect => effect.AttackModifierEffects).WithOne(ame => ame.Effect).OnDelete(DeleteBehavior.Restrict);
            effectTable.HasMany(effect => effect.MonsterDefenseEffects).WithOne(ame => ame.Effect).OnDelete(DeleteBehavior.Restrict);
            effectTable.HasMany(effect => effect.MonsterAttackEffects).WithOne(ame => ame.Effect).OnDelete(DeleteBehavior.Restrict);
            effectTable.HasMany(effect => effect.MonsterDeathEffects).WithOne(ame => ame.Effect).OnDelete(DeleteBehavior.Restrict);
        });
    }
}

public enum EFFECT_TYPE_DAO
{
    strength,
    poison,
    wound,
    stun,
    shield,
    disarm,
    muddle,
    immobilize,
    curse,
    disadvantage,
    advantage,
    damage,
    healAlly,
    chargeElement,
    spendElement
}

public enum ELEMENT_DAO
{
    fire,
    ice,
    air,
    earth,
    light,
    dark
}

public class EffectDAO
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public EFFECT_TYPE_DAO Type { get; set; }
    public int? Value { get; set; }
    public int? Duration { get; set; }
    public int? Range { get; set; }
    public ELEMENT_DAO? Element { get; set; }
    public virtual ICollection<AttackModifierEffectDAO> AttackModifierEffects { get; set; } = new HashSet<AttackModifierEffectDAO>();
    public virtual ICollection<MonsterDefenseEffectDAO> MonsterDefenseEffects { get; set; } = new HashSet<MonsterDefenseEffectDAO>();
    public virtual ICollection<MonsterAttackEffectDAO> MonsterAttackEffects { get; set; } = new HashSet<MonsterAttackEffectDAO>();
    public virtual ICollection<MonsterDeathEffectDAO> MonsterDeathEffects { get; set; } = new HashSet<MonsterDeathEffectDAO>();
}