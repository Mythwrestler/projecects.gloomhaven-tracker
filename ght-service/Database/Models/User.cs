using System.ComponentModel.DataAnnotations;
using GloomhavenTracker.Database.Models.Combat;
using Microsoft.EntityFrameworkCore;

namespace GloomhavenTracker.Database.Models;

public static partial class EntityDefinitions
{
    public static void DefineUserEntities(this ModelBuilder builder)
    {
        builder.Entity<UserDAO>(userTable => {
            userTable.HasMany(user => user.Campaigns).WithOne(uc => uc.User);
            userTable.HasMany(user => user.CombatHubClients).WithOne(hub => hub.User);
        });

        builder.Entity<UserCampaignDAO>(userCampaignTable => {
            userCampaignTable.HasKey(uc => new {uc.UserId, uc.CampaignId});
        });
    }
}

public class UserDAO
{
    [Key]
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public ICollection<UserCampaignDAO> Campaigns { get; set; } = new HashSet<UserCampaignDAO>();
    public ICollection<CombatHubClientDAO> CombatHubClients { get; set; } = new HashSet<CombatHubClientDAO>();
}

public class UserCampaignDAO
{
    [Required]
    public Guid UserId { get; set; }
    public UserDAO? User { get; set; }
    [Required]
    public Guid CampaignId { get; set; }
    public Campaign.CampaignDAO? Campaign { get; set; }
    public bool IsOwner { get; set;}
}