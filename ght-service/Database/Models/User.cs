using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace GloomhavenTracker.Database.Models;

public static partial class EntityDefinitions
{
    public static void DefineUserEntities(this ModelBuilder builder)
    {
        builder.Entity<UserDAO>(userTable => {
            userTable.HasMany(user => user.Campaigns).WithOne(uc => uc.User);
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
}

public class UserCampaignDAO
{
    [Required]
    public Guid UserId { get; set; }
    public UserDAO? User { get; set; }
    [Required]
    public Guid CampaignId { get; set; }
    public Campaign.CampaignDAO? Campaign { get; set; }

}