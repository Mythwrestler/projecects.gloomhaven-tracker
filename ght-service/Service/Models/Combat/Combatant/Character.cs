using System;
using CampaignCharacter = GloomhavenTracker.Service.Models.Campaign.Character;

namespace GloomhavenTracker.Service.Models.Combat.Combatant;

public class Character : Combatant
{
    public CampaignCharacter CampaignCharacter { get; }
    // public AttackModifierDeck ModifierDeck { get; }

    public Character(
        Guid id,
        int level,
        int health,
        int? initiative,
        CampaignCharacter campaignCharacter//,
        //AttackModifierDeck modifierDeck
    ) :
        base(id, level, health, initiative)
    {
        CampaignCharacter = campaignCharacter;
        //ModifierDeck = modifierDeck;
    }
}

public class CharacterDTO : CombatantDTO
{
    public string CharacterContentCode { get; set; }
    //public AttackModifierDeckDTO ModifierDeck { get; set; } = new AttackModifierDeckDTO();
}