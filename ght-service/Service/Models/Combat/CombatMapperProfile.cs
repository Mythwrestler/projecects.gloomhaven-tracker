using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GloomhavenTracker.Database.Models;
using GloomhavenTracker.Database.Models.Combat;
using GloomhavenTracker.Service.Models.Hub;

namespace GloomhavenTracker.Service.Models.Combat;

public class CombatMapperProfile : Profile
{
    public CombatMapperProfile()
    {
      #region Combat Users

       CreateMap<User, CombatUser>().ConvertUsing((src, dst, ctx) =>
       {
        return new CombatUser()
        {
          UserId = src.UserId,
          Username = src.UserName
        };
       });

      #endregion


      #region Attack Modifier Deck
        CreateMap<AttackModifierDeckDAO, AttackModifierDeck>().ConvertUsing((src, dst, ctx) =>
        {
          Dictionary<int, Content.AttackModifier> deck;
          
          if(src.Cards is null)
            deck = new Dictionary<int, Content.AttackModifier>();
          else
            deck = src.Cards.ToDictionary(
                card => card.position,
                card => ctx.Mapper.Map<Content.AttackModifier>(card.AttackModifier)
            );

          return new AttackModifierDeck(
            src.Id,
            deck,
            src.Positions ?? new List<int>()
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
        CreateMap<CombatHubClientDAO, HubClient>().ConvertUsing((src, dst, ctx) => {
          return new HubClient(
            id: src.Id,
            clientId: src.ClientId,
            groupId: src.CombatId.ToString(),
            user: ctx.Mapper.Map<User>(src.User),
            lastSeen: src.LastSeen
          );
        });

        CreateMap<HubClient, CombatHubClientDAO>().ConvertUsing((src, dst, ctx) => {
          return new CombatHubClientDAO()
          {
            UserId = src.User.UserId,
            CombatId = Guid.Parse(src.GroupId),
            ClientId = src.ClientId,
            LastSeen = src.LastSeen
          };
        });

        CreateMap<CombatDAO, Combat>().ConvertUsing((src, dst, ctx) => {
          return new Combat(
            src.Id,
            ctx.Mapper.Map<Campaign.Campaign>(src.Campaign),
            ctx.Mapper.Map<Content.Scenario>(src.Scenario),
            src.ScenarioLevel,
            ctx.Mapper.Map<AttackModifierDeck>(src.MonsterModifierDeck),
            ctx.Mapper.Map<List<HubClient>>(src.HubClients.ToList())
          );
        });

        CreateMap<Combat, CombatDAO>().ConvertUsing((src, dst, ctx) => {
          var hubClients = ctx.Mapper.Map<List<CombatHubClientDAO>>(src.RegisteredClients);
          hubClients.ForEach(client => client.CombatId = src.Id);

          return new CombatDAO()
          {
            Id = src.Id,
            CampaignId = src.Campaign.Id,
            ScenarioId = src.Scenario.Id,
            ScenarioLevel = src.ScenarioLevel,
            MonsterModifierDeck = ctx.Mapper.Map<AttackModifierDeckDAO>(src.MonsterModifierDeck),
            HubClients = hubClients
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