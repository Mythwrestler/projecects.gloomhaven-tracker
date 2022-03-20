using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace GloomhavenTracker.Database.Models.Content;

public static partial class EntityDefinitions
{
    public static void DefineCharacterEntities(this ModelBuilder builder)
    {
        builder.Entity<Character>(characterTable =>
        {
            characterTable.HasIndex(characterTable => new { characterTable.GameId, characterTable.ContentCode });
            characterTable.HasMany(character => character.BaseStats).WithOne(stat => stat.Character).OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<CharacterBaseStats>(characterBaseStatsTable =>
        {
            characterBaseStatsTable.HasIndex(stat => new { stat.CharacterId, stat.level });
        });

    }
}

public class Character
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ContentCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<CharacterBaseStats> BaseStats { get; set; } = new HashSet<CharacterBaseStats>();
    [Required]
    public Guid GameId { get; set; }
    public Game? Game { get; set; }
}

public class CharacterBaseStats
{
    [Key]
    public Guid Id { get; set; }
    public int level { get; set; }
    public int experience { get; set; }
    public int health { get; set; }
    [Required]
    public Guid CharacterId { get; set; }
    public Character? Character { get; set; }
}