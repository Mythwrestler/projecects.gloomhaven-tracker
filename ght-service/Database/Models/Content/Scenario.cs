using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace GloomhavenTracker.Database.Models.Content;

public static partial class EntityDefinitions
{
    public static void DefineScenarioContentEntities(this ModelBuilder builder)
    {
        builder.Entity<ScenarioDAO>(scenarioTable =>
        {
            scenarioTable.HasIndex(scenarioTable => new { scenarioTable.GameId, scenarioTable.ContentCode });
            scenarioTable.HasIndex(scenarioTable => new { scenarioTable.GameId, scenarioTable.ScenarioNumber });
            scenarioTable.HasMany(sm => sm.Monsters).WithOne(sm => sm.Scenario).OnDelete(DeleteBehavior.Restrict);
            scenarioTable.HasMany(so => so.Objectives).WithOne(so => so.Scenario).OnDelete(DeleteBehavior.Restrict);
            scenarioTable.HasMany(sc => sc.ScenarioCampaigns).WithOne(sc => sc.ScenarioContent).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<ScenarioMonsterDAO>(scenarioMonsterTable =>
        {
            scenarioMonsterTable.HasKey(sm => new { sm.ScenarioId, sm.MonsterId });
        });

        builder.Entity<ScenarioObjectiveDAO>(scenarioObjectiveTable =>
        {
            scenarioObjectiveTable.HasKey(so => new { so.ScenarioId, so.ObjectiveId });
        });

    }
}

public class ScenarioDAO
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
    public ICollection<ScenarioMonsterDAO> Monsters { get; set; } = new HashSet<ScenarioMonsterDAO>();
    public ICollection<ScenarioObjectiveDAO> Objectives { get; set; } = new HashSet<ScenarioObjectiveDAO>();
    public virtual ICollection<Campaign.ScenarioDAO> ScenarioCampaigns { get; set; } = new HashSet<Campaign.ScenarioDAO>();
    [Required]
    public Guid GameId { get; set; }
    public GameDAO Game { get; set; } = new GameDAO();
}

public class ScenarioMonsterDAO
{
    [Required]
    public Guid ScenarioId { get; set; }
    public ScenarioDAO Scenario { get; set; } = new ScenarioDAO();
    [Required]
    public Guid MonsterId { get; set; }
    public MonsterDAO Monster { get; set; } = new MonsterDAO();
}

public class ScenarioObjectiveDAO
{
    [Required]
    public Guid ScenarioId { get; set; }
    public ScenarioDAO Scenario { get; set; } = new ScenarioDAO();
    [Required]
    public Guid ObjectiveId { get; set; }
    public ObjectiveDAO Objective { get; set; } = new ObjectiveDAO();
}