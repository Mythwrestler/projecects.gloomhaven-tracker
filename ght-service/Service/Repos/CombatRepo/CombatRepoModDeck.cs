
using System;
using System.Collections.Generic;
using System.Linq;
using GloomhavenTracker.Database.Models.Combat;
using GloomhavenTracker.Service.Models.Combat;
using Microsoft.EntityFrameworkCore;

namespace GloomhavenTracker.Service.Repos;

public partial interface CombatRepo
{
    public void UpdateModDeckState (AttackModifierDeck deckToSave);
}

public partial class CombatRepoImplementation : CombatRepo
{

    public void UpdateModDeckState (AttackModifierDeck deckToSave)
    {
        var deckToSaveDAO = mapper.Map<AttackModifierDeckDAO>(deckToSave);
        UpdateModDeckState(deckToSaveDAO);
        context.SaveChanges();
    }

    private void UpdateModDeckState (AttackModifierDeckDAO deckToSave)
    {
        AttackModifierDeckDAO deckToUpdate = GetAttackModifierDeckDAO(deckToSave.Id);
        deckToUpdate.Cards.ToList().ForEach(card => {
            context.CombatAttackModifierDeckCards.Remove(card);
        });
        deckToUpdate.Positions = deckToSave.Positions;
        context.CombatAttackModifierDeckCards.AddRange(deckToSave.Cards);
    }
    
    private AttackModifierDeckDAO GetAttackModifierDeckDAO (Guid deckId)
    {
        return this.context.CombatAttackModifierDecks
            .Include(deck => deck.Cards).ThenInclude(card => card.AttackModifier)
            .First(deck => deck.Id == deckId);
    }

    
    private void CreateModDeck (AttackModifierDeckDAO deckToCreate)
    {
        context.CombatAttackModifierDecks.Add(deckToCreate);
        context.CombatAttackModifierDeckCards.AddRange(deckToCreate.Cards);
    }

}