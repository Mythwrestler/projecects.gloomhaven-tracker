using System;
using System.Collections.Generic;
using System.Linq;
using GloomhavenTracker.Service.Models.Campaign;
using GloomhavenTracker.Service.Models.Combat;
using GloomhavenTracker.Service.Models.Content;

namespace GloomhavenTracker.Service.Services;

public partial interface CombatService
{
    public void ShuffleMonsterModDeck(Guid combatId);
    public List<AttackModifier> DrawFromMonsterModDeck(Guid combatId, int numberOfCards);
}

public partial class CombatServiceImplantation : CombatService
{
    public void ShuffleMonsterModDeck(Guid combatId)
    {
        Combat combat = GetCombat(combatId);
        combat.MonsterModifierDeck.ShuffleDeck();
        combatRepo.UpdateModDeckState(combat.MonsterModifierDeck);
    }

    public List<AttackModifier> DrawFromMonsterModDeck(Guid combatId, int numberOfCards)
    {
        Combat combat = GetCombat(combatId);
        List<AttackModifier> drawnCards = combat.MonsterModifierDeck.DrawCards(numberOfCards);
        combatRepo.UpdateModDeckState(combat.MonsterModifierDeck);
        return drawnCards;
    }
}