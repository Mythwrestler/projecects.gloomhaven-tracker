
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GloomhavenTracker.Database;
using GloomhavenTracker.Database.Models.Combat;
using GloomhavenTracker.Service.Models.Combat;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GloomhavenTracker.Service.Repos;

public partial interface CombatRepo
{
    public bool CombatExists(Guid combatId);
    public void CreateCombat(Combat combatToSave);
    public Combat GetCombatById(Guid combatId);
    public List<Combat> GetCombatListing();
}

public partial class CombatRepoImplementation : CombatRepo
{
    private readonly IMapper mapper;
    private readonly GloomhavenContext context;
    private readonly ILogger<CampaignRepoImplementation> logger;

    public CombatRepoImplementation(GloomhavenContext context, IMapper mapper, ILogger<CampaignRepoImplementation> logger)
    {
        this.context = context;
        this.mapper = mapper;
        this.logger = logger;
    }

    public Combat GetCombatById(Guid combatId)
    {
        return mapper.Map<Combat>(GetCombatDAOById(combatId));
    }

    public List<Combat> GetCombatListing()
    {
        return mapper.Map<List<Combat>>(context.CombatCombat.ToList());
    }

    public bool CombatExists(Guid combatId)
    {
        if(context.CombatCombat.Where(combat => combat.Id == combatId).Count() > 1)
            return true;
        if(context.CombatCombat.Local.Where(combat => combat.Id == combatId).Count() > 1)
            return true;
        return false;
    }

    private CombatDAO GetCombatDAOById(Guid combatId)
    {
        return this.context.CombatCombat.Where(combat => combat.Id == combatId)
            // Campaign
            .Include(combat => combat.Campaign).ThenInclude(campaign => campaign.Party)

            // Monster Mod Deck
            .Include(combat => combat.MonsterModifierDeck).ThenInclude(modDeck => modDeck.Cards).ThenInclude(card => card.AttackModifier)

            // Scenario
            .Include(combat => combat.Scenario).ThenInclude(scenario => scenario.Monsters).ThenInclude(ms => ms.Monster)
            .Include(combat => combat.Scenario).ThenInclude(scenario => scenario.Objectives).ThenInclude(os => os.Objective)

            // Characters
            //.Include(combat => combat.Characters).ThenInclude(character => character.ActiveEffects)
            .First();
    }

    public void CreateCombat(Combat combatToSave)
    {;
        CombatDAO combatDAO = mapper.Map<CombatDAO>(combatToSave);
        context.CombatCombat.Add(combatDAO);
        if(combatDAO.MonsterModifierDeck != null)
            CreateModDeck(combatDAO.MonsterModifierDeck);
        
        context.SaveChanges();
    }
}