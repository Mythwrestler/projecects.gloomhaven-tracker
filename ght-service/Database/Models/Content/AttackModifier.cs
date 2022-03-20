using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace GloomhavenTracker.Database.Models.Content;

public static partial class EntityDefinitions
{
    public static void DefineAttackModifierEntities(this ModelBuilder builder)
    {
        builder.Entity<AttackModifier>(attackModifierTable =>
        {
            attackModifierTable.HasIndex(attackMod => new { attackMod.GameId, attackMod.ContentCode }).IsUnique();
            attackModifierTable.HasMany(ae => ae.Effects).WithOne(ae => ae.AttackModifier).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<AttackModifierEffect>(attackModifierEffectTable =>
        {
            attackModifierEffectTable.HasKey(me => new { me.EffectId, me.AttackModifierId });
        });
    }
}


public class AttackModifier
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ContentCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsCurse { get; set; } = false;
    public bool IsBlessing { get; set; } = false;
    public bool TriggerShuffle { get; set; } = false;
    public string Value { get; set; } = string.Empty;
    public ICollection<AttackModifierEffect> Effects { get; set; } = new HashSet<AttackModifierEffect>();
    [Required]
    public Guid GameId { get; set; }
    public Game? Game { get; set; }
}

public class AttackModifierEffect
{
    [Required]
    public Guid EffectId { get; set; }
    public Effect? Effect { get; set; }
    [Required]
    public Guid AttackModifierId { get; set; }
    public AttackModifier? AttackModifier { get; set; }
}