using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace GloomhavenTracker.Database.Models.Content;

public static partial class EntityDefinitions
{
    public static void DefineObjectiveEntities(this ModelBuilder builder)
    {
        builder.Entity<Objective>(objectiveTable =>
        {
            objectiveTable.HasIndex(objectiveTable => new { objectiveTable.GameId, objectiveTable.ContentCode });
            objectiveTable.HasMany(objective => objective.ScenarioObjectives).WithOne(so => so.Objective).OnDelete(DeleteBehavior.Restrict);
        });
    }
}

public class Objective
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ContentCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Health { get; set; } = string.Empty;
    public bool RangeAttackable { get; set; } = true;
    public bool MeleeAttackable { get; set; } = true;
    public ICollection<ScenarioObjective> ScenarioObjectives { get; set; } = new HashSet<ScenarioObjective>();
    [Required]
    public Guid GameId { get; set; }
    public Game? Game { get; set; }
}