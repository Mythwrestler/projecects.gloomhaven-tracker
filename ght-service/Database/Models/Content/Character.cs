using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace GloomhavenTracker.Database.Models.Content;

public static partial class EntityDefinitions
{
    public static void DefineCharacterEntities(this ModelBuilder builder)
    {
        builder.Entity<CharacterDAO>(characterTable =>
        {
            characterTable.HasIndex(characterTable => new { characterTable.GameId, characterTable.ContentCode });
            characterTable.HasMany(character => character.BaseStats).WithOne(stat => stat.Character).OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<CharacterBaseStatsDAO>(characterBaseStatsTable =>
        {
            characterBaseStatsTable.HasIndex(stat => new { stat.CharacterId, stat.level });
        });

    }
}

public class CharacterDAO
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ContentCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<CharacterBaseStatsDAO> BaseStats { get; set; } = new HashSet<CharacterBaseStatsDAO>();
    [Required]
    public Guid GameId { get; set; }
    public GameDAO? Game { get; set; }
}

public class CharacterBaseStatsDAO
{
    [Key]
    public Guid Id { get; set; }
    public int level { get; set; }
    public int experience { get; set; }
    public int health { get; set; }
    [Required]
    public Guid CharacterId { get; set; }
    public CharacterDAO? Character { get; set; }
}