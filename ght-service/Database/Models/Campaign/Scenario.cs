
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace GloomhavenTracker.Database.Models.Campaign;

public static partial class EntityDefinitions
{
    public static void DefineScenarioCampaignEntities(this ModelBuilder builder)
    {
        builder.Entity<ScenarioDAO>(scenarioTable =>
        {
            scenarioTable.HasOne(scenario => scenario.ScenarioContent).WithMany(scenario => scenario.ScenarioCampaigns).OnDelete(DeleteBehavior.Restrict);
        });
    }
}

public class ScenarioDAO
{
    [Key]
    public Guid Id { get; set; }
    public bool IsClosed { get; set; }
    public bool IsCompleted { get; set; }
    public Guid ScenarioContentId { get; set; }
    public Content.ScenarioDAO? ScenarioContent { get; set; }
}