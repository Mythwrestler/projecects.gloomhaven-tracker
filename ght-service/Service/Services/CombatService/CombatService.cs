
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GloomhavenTracker.Service.Models;
using GloomhavenTracker.Service.Models.Campaign;
using GloomhavenTracker.Service.Models.Combat;
using GloomhavenTracker.Service.Models.Content;
using GloomhavenTracker.Service.Models.Hub;
using GloomhavenTracker.Service.Repos;
using Microsoft.Extensions.Logging;

namespace GloomhavenTracker.Service.Services;
public partial interface CombatService
{
    public bool CombatExists(Guid combatId);
    public List<CombatDTO> GetCombatListing();
    public CombatDTO NewCombat(Guid campaignId, string scenarioContentCode);
    public CombatDTO GetCombatDTO(Guid combatId);
}

public partial class CombatServiceImplantation : CombatService
{
    private readonly ContentRepo contentRepo;
    private readonly CampaignRepo campaignRepo;
    private readonly CombatRepo combatRepo;
    private readonly UserService userService;
    private readonly CombatHubClientTracker clientTracker;
    private readonly IMapper mapper;
    private readonly Dictionary<Guid, Combat> combats = new Dictionary<Guid, Combat>();

    public CombatServiceImplantation 
    (
        ContentRepo contentRepo,
        CampaignRepo campaignRepo,
        CombatRepo combatRepo,
        UserService userService,
        CombatHubClientTracker clientTracker,
        IMapper mapper
    )
    {
        this.contentRepo = contentRepo;
        this.campaignRepo = campaignRepo;
        this.combatRepo = combatRepo;
        this.userService = userService;
        this.clientTracker = clientTracker;
        this.mapper = mapper;
    }

    public List<CombatDTO> GetCombatListing()
    {
        var listing = combatRepo.GetCombatListing();
        return mapper.Map<List<CombatDTO>>(listing);
    }

    public bool CombatExists (Guid combatId)
    {
        return combatRepo.CombatExists(combatId);
    }

    public CombatDTO GetCombatDTO(Guid combatId)
    {
        return mapper.Map<CombatDTO>(GetCombat(combatId));
    }

    private Combat GetCombat(Guid combatId) 
    {
        return combatRepo.GetCombatById(combatId);
    }

    public CombatDTO NewCombat(Guid campaignId, string scenarioContentCode)
    {
        Campaign campaign = campaignRepo.GetCampaign(campaignId);
        GAME_TYPE gameType = GameUtils.GameType(campaign.Game.ContentCode);
        Game game = contentRepo.GetGameDefaults(gameType);
        Models.Content.Scenario scenario = contentRepo.GetScenarioDefaults(gameType, scenarioContentCode);
        int scenarioLevel =  (int)Math.Floor(campaign.Party.Select(kvp => kvp.Value.Level).Average());
        Combat newCombat = new Combat(
            id: Guid.NewGuid(),
            campaign: campaign,
            scenario: scenario,
            scenarioLevel: scenarioLevel,
            monsterModifierDeck: new AttackModifierDeck(game.BaseModifierDeck),
            new List<HubClient>()
        );

        combatRepo.CreateCombat(newCombat);

        combats.Add(newCombat.Id, newCombat);

        return mapper.Map<CombatDTO>(newCombat);
    }
    
}
