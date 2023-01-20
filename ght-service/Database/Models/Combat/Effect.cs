using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace GloomhavenTracker.Database.Models.Combat;


public static partial class EntityDefinitions
{
    public static void DefineActiveEffectsEntities(this ModelBuilder builder)
    {
        builder.Entity<ActiveEffectDAO>(activeEffectTable => {
            activeEffectTable.HasMany(effect => effect.MonsterActiveEffects).WithOne(monsterEffect => monsterEffect.ActiveEffect).OnDelete(DeleteBehavior.Restrict);
            activeEffectTable.HasMany(effect => effect.CharacterActiveEffectsDAOs).WithOne(characterEffect => characterEffect.ActiveEffect).OnDelete(DeleteBehavior.Restrict);
        });
    }
}

public class ActiveEffectDAO : AuditableEntityBase
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public Guid EffectId { get; set; }
    public Content.EffectDAO Effect { get; set; } = null!;
    public int RoundsRemaining { get; set; }
    public ICollection<MonsterActiveEffectDAO> MonsterActiveEffects = new HashSet<MonsterActiveEffectDAO>();
    public ICollection<CharacterActiveEffectDAO> CharacterActiveEffectsDAOs = new HashSet<CharacterActiveEffectDAO>();
    public Guid CreatedBy { get; set; }
    public Guid UpdatedBy { get; set; }
}