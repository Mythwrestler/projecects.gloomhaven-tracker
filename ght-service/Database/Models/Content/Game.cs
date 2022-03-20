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

            gameTable.HasMany(game => game.BaseModifierDeck).WithOne(bmd => bmd.Game).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<GameBaseAttackModifier>(gameBaseAttackModifierTable => {
            gameBaseAttackModifierTable.HasOne(gbm => gbm.AttackModifier).WithMany(am => am.GameBaseAttackModifiers).OnDelete(DeleteBehavior.Restrict);
            gameBaseAttackModifierTable.HasOne(gbm => gbm.Game).WithMany(am => am.BaseModifierDeck).OnDelete(DeleteBehavior.Restrict);
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
    public virtual ICollection<GameBaseAttackModifier> BaseModifierDeck { get; set; } = new HashSet<GameBaseAttackModifier>();
};

public class GameBaseAttackModifier
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public Guid GameId { get; set; }
    public Game? Game { get; set; }
    [Required]
    public Guid AttackModifierId { get; set; }
    public AttackModifier? AttackModifier { get; set; }
}