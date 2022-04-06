using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GloomhavenTracker.Database.Models.Content;

public static partial class EntityDefinitions
{
    private static EnumToStringConverter<EFFECT_TYPE> effectType = new EnumToStringConverter<EFFECT_TYPE>();
    public static void DefineEffectEntities(this ModelBuilder builder)
    {
        builder.Entity<EffectDAO>(effectTable =>
        {
            effectTable.HasIndex(effect => new { effect.Type, effect.Value, effect.Duration }).IsUnique();
            effectTable.Property(effect => effect.Type).HasConversion(effectType);
            effectTable.HasMany(effect => effect.AttackModifierEffects).WithOne(ame => ame.Effect).OnDelete(DeleteBehavior.Restrict);
            effectTable.HasMany(effect => effect.MonsterDefenseEffects).WithOne(ame => ame.Effect).OnDelete(DeleteBehavior.Restrict);
            effectTable.HasMany(effect => effect.MonsterAttackEffects).WithOne(ame => ame.Effect).OnDelete(DeleteBehavior.Restrict);
            effectTable.HasMany(effect => effect.MonsterDeathEffects).WithOne(ame => ame.Effect).OnDelete(DeleteBehavior.Restrict);
        });
    }
}

public enum EFFECT_TYPE
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
}

public class EffectDAO
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public EFFECT_TYPE Type { get; set; }
    public int Value { get; set; } = -1;
    public int Duration { get; set; } = -1;
    public int Range { get; set; } = -1;
    public virtual ICollection<AttackModifierEffectDAO> AttackModifierEffects { get; set; } = new HashSet<AttackModifierEffectDAO>();
    public virtual ICollection<MonsterDefenseEffectDAO> MonsterDefenseEffects { get; set; } = new HashSet<MonsterDefenseEffectDAO>();
    public virtual ICollection<MonsterAttackEffectDAO> MonsterAttackEffects { get; set; } = new HashSet<MonsterAttackEffectDAO>();
    public virtual ICollection<MonsterDeathEffectDAO> MonsterDeathEffects { get; set; } = new HashSet<MonsterDeathEffectDAO>();
}