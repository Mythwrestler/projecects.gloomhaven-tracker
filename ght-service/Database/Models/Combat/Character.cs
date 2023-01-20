using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace GloomhavenTracker.Database.Models.Combat;

public static partial class EntityDefinitions
{
    public static void DefineCombatCharacterEntities(this ModelBuilder builder)
    {
        builder.Entity<CharacterDAO>(characterTable => {
            characterTable.HasMany(character => character.ActiveEffects).WithOne(effect => effect.Character).OnDelete(DeleteBehavior.Restrict);
            characterTable.HasOne(character => character.CombatHubClient).WithOne(hubClient => hubClient.Character).OnDelete(DeleteBehavior.Cascade);
        });
        builder.Entity<CharacterActiveEffectDAO>(characterActiveEffectsTable => {
            characterActiveEffectsTable.HasKey(activeEffect => new {activeEffect.CharacterId, activeEffect.ActiveEffectId});
            characterActiveEffectsTable.HasIndex(activeEffect => new {activeEffect.ActiveEffectId, activeEffect.CharacterId}).IsUnique();
        });
        builder.Entity<CharacterCombatHubClientDAO>(characterCombatHubClientTable => {
            characterCombatHubClientTable.HasKey(charClient => new {charClient.CharacterId, charClient.CombatHubClientId});
            characterCombatHubClientTable.HasOne(charClient => charClient.Character).WithOne(character => character.CombatHubClient);
            characterCombatHubClientTable.HasOne(charClient => charClient.CombatHubClient).WithMany(hub => hub.Characters);
        });
    }
}

public class CharacterDAO : Combatant
{
    [Required]
    public Guid CampaignCharacterId { get; set; }
    public Campaign.CharacterDAO CampaignCharacter { get; set; } = null!;
    public int Health { get; set; }
    public int Level { get; set; }
    public CharacterCombatHubClientDAO CombatHubClient { get; set; } = null!;
    public ICollection<CharacterActiveEffectDAO> ActiveEffects { get; set; } = new HashSet<CharacterActiveEffectDAO>();
}

public class CharacterActiveEffectDAO
{
    [Required]
    public Guid CharacterId { get; set; }
    public CharacterDAO? Character { get; set; } = null!;
    [Required]
    public Guid ActiveEffectId { get; set; }
    public ActiveEffectDAO? ActiveEffect { get; set; } = null!;
}

public class CharacterCombatHubClientDAO
{
    [Required]
    public Guid CharacterId { get; set; }
    public CharacterDAO Character { get; set; } = null!;
    [Required]
    public Guid CombatHubClientId { get; set; }
    public CombatHubClientDAO CombatHubClient { get; set; } = null!;
}