using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace GloomhavenTracker.Database.Models.Content;

public static partial class EntityDefinitions
{
    public static void DefineScenarioEntities(this ModelBuilder builder)
    {
        builder.Entity<Scenario>(scenarioTable =>
        {
            scenarioTable.HasIndex(scenarioTable => new { scenarioTable.GameId, scenarioTable.ContentCode });
            scenarioTable.HasIndex(scenarioTable => new { scenarioTable.GameId, scenarioTable.ScenarioNumber });
            scenarioTable.HasMany(sm => sm.Monsters).WithOne(sm => sm.Scenario).OnDelete(DeleteBehavior.Restrict);
            scenarioTable.HasMany(so => so.Objectives).WithOne(so => so.Scenario).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<ScenarioMonster>(scenarioMonsterTable =>
        {
            scenarioMonsterTable.HasKey(sm => new { sm.ScenarioId, sm.MonsterId });
        });

        builder.Entity<ScenarioObjective>(scenarioObjectiveTable =>
        {
            scenarioObjectiveTable.HasKey(so => new { so.ScenarioId, so.ObjectiveId });
        });

    }
}

public class Scenario
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ContentCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ScenarioNumber { get; set; }
    public string Goal { get; set; } = string.Empty;
    public string CityMapLocation { get; set; } = string.Empty;
    public List<int> ScenarioBookPages { get; set; } = new List<int>();
    public List<int> SupplementalBookPages { get; set; } = new List<int>();
    public virtual ICollection<ScenarioMonster> Monsters { get; set; } = new HashSet<ScenarioMonster>();
    public virtual ICollection<ScenarioObjective> Objectives { get; set; } = new HashSet<ScenarioObjective>();
    [Required]
    public Guid GameId { get; set; }
    public Game? Game { get; set; }
}

public class ScenarioMonster
{
    [Required]
    public Guid ScenarioId { get; set; }
    public Scenario? Scenario { get; set; }
    [Required]
    public Guid MonsterId { get; set; }
    public Monster? Monster { get; set; }
}

public class ScenarioObjective
{
    [Required]
    public Guid ScenarioId { get; set; }
    public Scenario? Scenario { get; set; }
    [Required]
    public Guid ObjectiveId { get; set; }
    public Objective? Objective { get; set; }
}