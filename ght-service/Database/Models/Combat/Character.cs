using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace GloomhavenTracker.Database.Models.Combat;

public static partial class EntityDefinitions
{
    public static void DefineCombatCharacterEntities(this ModelBuilder builder)
    {
        builder.Entity<CharacterDAO>(characterTable => {
            characterTable.HasMany(character => character.ActiveEffects).WithOne(effect => effect.Character).OnDelete(DeleteBehavior.Restrict);
        });
        builder.Entity<CharacterActiveEffectDAO>(characterActiveEffectsTable => {
            characterActiveEffectsTable.HasKey(activeEffect => new {activeEffect.CharacterId, activeEffect.ActiveEffectId});
            characterActiveEffectsTable.HasIndex(activeEffect => new {activeEffect.ActiveEffectId, activeEffect.CharacterId}).IsUnique();
        });
    }
}

public class CharacterDAO : Combatant
{
    [Required]
    public Guid CampaignCharacterId { get; set; }
    public Campaign.CharacterDAO? CampaignCharacter { get; set; }
    public int Health { get; set; }
    public ICollection<CharacterActiveEffectDAO> ActiveEffects { get; set; } = new HashSet<CharacterActiveEffectDAO>();
}

public class CharacterActiveEffectDAO
{
    [Required]
    public Guid CharacterId { get; set; }
    public CharacterDAO? Character { get; set; }
    [Required]
    public Guid ActiveEffectId { get; set; }
    public ActiveEffectDAO? ActiveEffect { get; set; }
}