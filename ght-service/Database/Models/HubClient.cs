
using System.ComponentModel.DataAnnotations;
using GloomhavenTracker.Database.Models.Combat;
using Microsoft.EntityFrameworkCore;

namespace GloomhavenTracker.Database.Models;

public static partial class EntityDefinitions
{
    public static void DefineHubEntities(this ModelBuilder builder)
    {
        builder.Entity<CombatHubClientDAO>(combatHubTable => {
            combatHubTable.HasOne(hub => hub.User).WithMany(user => user.CombatHubClients).OnDelete(DeleteBehavior.Restrict);
            combatHubTable.HasOne(hub => hub.Combat).WithMany(combat => combat.HubClients).OnDelete(DeleteBehavior.Restrict);
            combatHubTable.HasIndex(hub => hub.ClientId).IsUnique();
        });
    }
}

public class HubClientDAO {
    [Key]
    public Guid Id { get; set; }
    [Required]
    public Guid UserId { get; set; }
    public UserDAO? User { get; set; }
    [Required]
    public string ClientId { get; set; } = string.Empty;
    [Required]
    public DateTime LastSeen { get; set; }
}


public class CombatHubClientDAO : HubClientDAO 
{
    [Required]
    public Guid CombatId { get; set; }
    public CombatDAO? Combat { get; set; }
}