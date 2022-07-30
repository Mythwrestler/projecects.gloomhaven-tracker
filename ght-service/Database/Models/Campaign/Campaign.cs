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
            campaignTable.HasMany(campaign => campaign.Items).WithOne(ci => ci.Campaign).OnDelete(DeleteBehavior.Restrict);
            campaignTable.HasMany(campaign => campaign.Managers).WithOne(manager => manager.Campaign).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<CampaignItemDAO>(campaignItemTable => {
            campaignItemTable.HasKey(ci => new {ci.CampaignId, ci.ItemId});
        });

    }
}


public class CampaignDAO : AuditableEntityBase
{
    [Key]
    public Guid Id {get; set;}
    public string Name {get; set;} = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid GameId {get; set;}
    public GameDAO? Game {get; set;}
    public ICollection<ScenarioDAO> Scenarios {get; set;} = new HashSet<ScenarioDAO>();
    public ICollection<CharacterDAO> Party {get; set;} = new HashSet<CharacterDAO>();
    public ICollection<CampaignItemDAO> Items {get; set;} = new HashSet<CampaignItemDAO>();
    public Guid CreatedBy { get; set; }
    public Guid UpdatedBy { get; set; }
    public ICollection<UserCampaignDAO> Managers {get; set;} = new HashSet<UserCampaignDAO>();
}


public class CampaignItemDAO : AuditableEntityBase
{
    [Required]
    public Guid ItemId { get; set; }
    public ItemDAO? Item { get; set; }
    [Required]
    public Guid CampaignId { get; set; }
    public CampaignDAO? Campaign { get; set; }
    public Guid? CharacterId { get; set; }
    public CharacterDAO? Character { get; set; }
}