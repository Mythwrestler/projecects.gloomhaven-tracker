using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace GloomhavenTracker.Database.Models.Content;


public static partial class EntityDefinitions
{
    public static void DefineGameEntities(this ModelBuilder builder)
    {
        builder.Entity<Game>(gameTable =>
        {
            gameTable.HasAlternateKey(game => game.ContentCode);
            gameTable.HasMany(game => game.AttackModifiers).WithOne(dep => dep.Game).OnDelete(DeleteBehavior.Restrict);
            gameTable.HasMany(game => game.Monsters).WithOne(dep => dep.Game).OnDelete(DeleteBehavior.Restrict);
            gameTable.HasMany(game => game.Objectives).WithOne(dep => dep.Game).OnDelete(DeleteBehavior.Restrict);
            gameTable.HasMany(game => game.Scenarios).WithOne(dep => dep.Game).OnDelete(DeleteBehavior.Restrict);
            gameTable.HasMany(game => game.Characters).WithOne(dep => dep.Game).OnDelete(DeleteBehavior.Restrict);
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
    public virtual ICollection<AttackModifier> AttackModifiers { get; set; } = new HashSet<AttackModifier>();
    public virtual ICollection<Monster> Monsters { get; set; } = new HashSet<Monster>();
    public virtual ICollection<Objective> Objectives { get; set; } = new HashSet<Objective>();
    public virtual ICollection<Scenario> Scenarios { get; set; } = new HashSet<Scenario>();
    public virtual ICollection<Character> Characters { get; set; } = new HashSet<Character>();
};