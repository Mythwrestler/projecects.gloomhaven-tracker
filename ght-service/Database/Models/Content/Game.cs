using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace GloomhavenTracker.Database.Models.Content;


public static partial class EntityDefinitions
{
    public static void DefineGameEntities(this ModelBuilder builder)
    {
        builder.Entity<GameDAO>(gameTable =>
        {
            gameTable.HasAlternateKey(game => game.ContentCode);
            gameTable.HasMany(game => game.AttackModifiers).WithOne(dep => dep.Game).OnDelete(DeleteBehavior.Restrict);
            gameTable.HasMany(game => game.Monsters).WithOne(dep => dep.Game).OnDelete(DeleteBehavior.Restrict);
            gameTable.HasMany(game => game.Objectives).WithOne(dep => dep.Game).OnDelete(DeleteBehavior.Restrict);
            gameTable.HasMany(game => game.Scenarios).WithOne(dep => dep.Game).OnDelete(DeleteBehavior.Restrict);
            gameTable.HasMany(game => game.Characters).WithOne(dep => dep.Game).OnDelete(DeleteBehavior.Restrict);

            gameTable.HasMany(game => game.BaseModifierDeck).WithOne(bmd => bmd.Game).OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<GameBaseAttackModifierDAO>(gameBaseAttackModifierTable => {
            gameBaseAttackModifierTable.HasOne(gbm => gbm.AttackModifier).WithMany(am => am.GameBaseAttackModifiers).OnDelete(DeleteBehavior.Restrict);
            gameBaseAttackModifierTable.HasOne(gbm => gbm.Game).WithMany(am => am.BaseModifierDeck).OnDelete(DeleteBehavior.Restrict);
        });
    }
}

public class GameDAO
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ContentCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public virtual ICollection<AttackModifierDAO> AttackModifiers { get; set; } = new HashSet<AttackModifierDAO>();
    public virtual ICollection<MonsterDAO> Monsters { get; set; } = new HashSet<MonsterDAO>();
    public virtual ICollection<ObjectiveDAO> Objectives { get; set; } = new HashSet<ObjectiveDAO>();
    public virtual ICollection<ScenarioDAO> Scenarios { get; set; } = new HashSet<ScenarioDAO>();
    public virtual ICollection<CharacterDAO> Characters { get; set; } = new HashSet<CharacterDAO>();
    public virtual ICollection<GameBaseAttackModifierDAO> BaseModifierDeck { get; set; } = new HashSet<GameBaseAttackModifierDAO>();
};

public class GameBaseAttackModifierDAO
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public Guid GameId { get; set; }
    public GameDAO? Game { get; set; }
    [Required]
    public Guid AttackModifierId { get; set; }
    public AttackModifierDAO? AttackModifier { get; set; }
}