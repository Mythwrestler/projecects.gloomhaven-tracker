using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace GloomhavenTracker.Database.Models.Content;

public static partial class EntityDefinitions
{
    public static void DefineAttackModifierEntities(this ModelBuilder builder)
    {
        builder.Entity<AttackModifierDAO>(attackModifierTable =>
        {
            attackModifierTable.HasIndex(attackMod => new { attackMod.GameId, attackMod.ContentCode }).IsUnique();
            attackModifierTable.HasMany(ae => ae.Effects).WithOne(ae => ae.AttackModifier).OnDelete(DeleteBehavior.Restrict);
            attackModifierTable.HasMany(am => am.GameBaseAttackModifiers).WithOne(gbm => gbm.AttackModifier).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<AttackModifierEffectDAO>(attackModifierEffectTable =>
        {
            attackModifierEffectTable.HasKey(me => new { me.EffectId, me.AttackModifierId });
        });

        
    }
}


public class AttackModifierDAO
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
    public ICollection<AttackModifierEffectDAO> Effects { get; set; } = new HashSet<AttackModifierEffectDAO>();
    public ICollection<GameBaseAttackModifierDAO> GameBaseAttackModifiers { get; set; } = new HashSet<GameBaseAttackModifierDAO>();
    [Required]
    public Guid GameId { get; set; }
    public GameDAO? Game { get; set; }
}

public class AttackModifierEffectDAO
{
    [Required]
    public Guid EffectId { get; set; }
    public EffectDAO? Effect { get; set; }
    [Required]
    public Guid AttackModifierId { get; set; }
    public AttackModifierDAO? AttackModifier { get; set; }
}