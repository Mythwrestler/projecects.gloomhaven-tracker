using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace GloomhavenTracker.Database.Models.Combat;

public static partial class EntityDefinitions
{
    public static void DefineCombatObjectiveEntities(this ModelBuilder builder)
    {
        builder.Entity<ObjectiveDAO>(ObjectiveTable => {
            ObjectiveTable.HasMany(Objective => Objective.ActiveEffects).WithOne(effect => effect.Objective).OnDelete(DeleteBehavior.Restrict);
        });
        builder.Entity<ObjectiveActiveEffectDAO>(objectiveActiveEffectsTable => {
            objectiveActiveEffectsTable.HasKey(activeEffect => new {activeEffect.ObjectiveId, activeEffect.ActiveEffectId});
            objectiveActiveEffectsTable.HasIndex(activeEffect => new {activeEffect.ActiveEffectId, activeEffect.ObjectiveId}).IsUnique();
        });
    }
}

public class ObjectiveDAO : Combatant
{
    [Required]
    public Guid ContentObjectiveId { get; set; }
    public Content.ObjectiveDAO Objective { get; set; } = null!;
    public int Health { get; set; }
    public ICollection<ObjectiveActiveEffectDAO> ActiveEffects { get; set; } = new HashSet<ObjectiveActiveEffectDAO>();
}

public class ObjectiveActiveEffectDAO
{
    [Required]
    public Guid ObjectiveId { get; set; }
    public ObjectiveDAO Objective { get; set; } = null!;
    [Required]
    public Guid ActiveEffectId { get; set; }
    public ActiveEffectDAO ActiveEffect { get; set; } = null!;
}