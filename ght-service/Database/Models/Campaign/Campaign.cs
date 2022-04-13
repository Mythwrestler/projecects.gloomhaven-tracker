using System.ComponentModel.DataAnnotations;
using GloomhavenTracker.Database.Models.Content;
using Microsoft.EntityFrameworkCore;

namespace GloomhavenTracker.Database.Models.Campaign;


public static partial class EntityDefinitions
{
    public static void DefineCampaignEntities(this ModelBuilder builder)
    {
        builder.Entity<CampaignDAO>(campaignTable => {
            campaignTable.HasMany(campaign => campaign.Scenarios).WithOne(scenario => scenario.Campaign).OnDelete(DeleteBehavior.Restrict);
            campaignTable.HasMany(campaign => campaign.Party).WithOne(character => character.Campaign).OnDelete(DeleteBehavior.Restrict);
        });
    }
}


public class CampaignDAO : AuditEntityBase<CampaignDAO>
{
    [Key]
    public Guid Id {get; set;}
    public string Description {get; set;} = string.Empty;
    public Guid GameId {get; set;}
    public GameDAO? Game {get; set;}
    public virtual ICollection<ScenarioDAO> Scenarios {get; set;} = new HashSet<ScenarioDAO>();
    public virtual ICollection<CharacterDAO> Party {get; set;} = new HashSet<CharacterDAO>();
}