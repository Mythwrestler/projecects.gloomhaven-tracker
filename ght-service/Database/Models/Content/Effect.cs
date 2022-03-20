using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GloomhavenTracker.Database.Models.Content;

public static partial class EntityDefinitions
{
    private static EnumToStringConverter<EFFECT_TYPE> effectType = new EnumToStringConverter<EFFECT_TYPE>();
    public static void DefineEffectEntities(this ModelBuilder builder)
    {
        builder.Entity<Effect>(effectTable => {
            effectTable.HasIndex(effect => new { effect.Type, effect.Value, effect.Duration }).IsUnique();
            effectTable.Property(effect => effect.Type).HasConversion(effectType);
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
}

public class Effect
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public EFFECT_TYPE Type { get; set; }
    public int Value { get; set; } = -1;
    public int Duration { get; set; } = -1;
}