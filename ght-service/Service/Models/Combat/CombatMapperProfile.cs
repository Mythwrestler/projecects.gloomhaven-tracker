using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GloomhavenTracker.Database.Models.Combat;

namespace GloomhavenTracker.Service.Models.Combat;

public class CombatMapperProfile : Profile
{
    public CombatMapperProfile()
    {
      #region Attack Modifier Deck
        CreateMap<AttackModifierDeckDAO, AttackModifierDeck>().ConvertUsing((src, dst, ctx) =>
        {
          
          Dictionary<int, Content.AttackModifier> deck = src.Cards.ToDictionary(
              card => card.position,
              card => ctx.Mapper.Map<Content.AttackModifier>(card.AttackModifier)
          );

          return new AttackModifierDeck(
            src.Id,
            deck,
            src.Positions
          );
        });

        CreateMap<AttackModifierDeck, AttackModifierDeckDAO>().ConvertUsing((src, dst, ctx) => {

          List<AttackModifierDeckCardDAO> cards = src.Deck.Select(kvp => new AttackModifierDeckCardDAO()
          {
            DeckId = src.Id,
            AttackModifierId = kvp.Value.Id,
            position = kvp.Key
          }).ToList();

          return new AttackModifierDeckDAO() {
            Id = src.Id,
            Cards = cards,
            Positions = src.Positions
          };
        });

        CreateMap<AttackModifierDeck, AttackModifierDeckDTO>().ConvertUsing((src, dst, ctx) => {
          return new AttackModifierDeckDTO()
          {
            DiscardPileCount = src.DiscardPile.Count(),
            DrawPileCount = src.DrawPile.Count(),
            ShownCards = src.ShownCards
          };
        });

        #endregion


      #region Combat
        CreateMap<CombatDAO, Combat>().ConvertUsing((src, dst, ctx) => {
          return new Combat(
            src.Id,
            ctx.Mapper.Map<Campaign.Campaign>(src.Campaign),
            ctx.Mapper.Map<Content.Scenario>(src.Scenario),
            src.ScenarioLevel,
            ctx.Mapper.Map<AttackModifierDeck>(src.MonsterModifierDeck)
          );
        });

        CreateMap<Combat, CombatDAO>().ConvertUsing((src, dst, ctx) => {
          return new CombatDAO()
          {
            Id = src.Id,
            CampaignId = src.Campaign.Id,
            ScenarioId = src.Scenario.Id,
            ScenarioLevel = src.ScenarioLevel,
            MonsterModifierDeck = ctx.Mapper.Map<AttackModifierDeckDAO>(src.MonsterModifierDeck)
          };
        });

        CreateMap<Combat, CombatDTO>().ConvertUsing((src, dst, ctx) => {
          return new CombatDTO()
          {
            CampaignId = src.Campaign.Id,
            Description = src.Description,
            ScenarioContentCode = src.Scenario.ContentCode,
            Id = src.Id,
            ScenarioLevel = src.ScenarioLevel,
            MonsterModifierDeck = ctx.Mapper.Map<AttackModifierDeckDTO>(src.MonsterModifierDeck)
          };
        });

      #endregion
    }
}