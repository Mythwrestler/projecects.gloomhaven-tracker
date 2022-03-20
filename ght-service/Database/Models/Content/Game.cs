using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace GloomhavenTracker.Database.Models.Content;


public static partial class EntityDefinitions
{
    public static void DefineGameEntities(this ModelBuilder builder)
    {
        builder.Entity<Game>(gameTable => {
            gameTable.HasAlternateKey(game => game.ContentCode);
        });
    }
}

public class Game
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ContentCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<Monster> Monsters { get; set; } = new HashSet<Monster>();
    public ICollection<AttackModifier> BaseModifierDeck { get; set; } = new HashSet<AttackModifier>();
};