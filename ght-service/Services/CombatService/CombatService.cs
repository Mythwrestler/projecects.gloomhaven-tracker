
using System;
using System.Collections.Generic;
using GloomhavenTracker.Service.Models.Combat;
using GloomhavenTracker.Service.Models.Content;
using GloomhavenTracker.Service.Repos;
using Microsoft.Extensions.Logging;

namespace GloomhavenTracker.Service.Services;
public interface CombatService
{
    #region Find or Start Combat Interface

    public bool CombatExists(Guid combatId);
    public Guid NewCombat(Guid campaingId, string ScenarioContentCode);
    public CombatTrackerSummary GetCombatSummary(Guid combatId);
    public List<CombatTrackerSummary> GetCombatList();
    public List<CombatTrackerSummary> GetCombatListForScenario(string scenarioCode);

    #endregion

    #region Combat Hub Clients Interface

    public void RegisterHubClient(Guid combatId, string clientId);
    public void RemoveHubClient(Guid combatId, string clientId);

    #endregion
}

public partial class CombatServiceImplentation : CombatService
{
    private CombatRepo combatRepo;
    private ContentService contentService;
    private CampaignService campaignService;
    private ILogger<CombatServiceImplentation> logger;
    public CombatServiceImplentation(
        CombatRepo combatRepo,
        ContentService contentService,
        CampaignService campaignService,
        ILogger<CombatServiceImplentation> logger)
    {
        this.combatRepo = combatRepo;
        this.contentService = contentService;
        this.logger = logger;
        this.campaignService = campaignService;
    }

    public bool CombatExists(Guid combatId)
    {
        throw new NotImplementedException();
    }

    private CombatTracker GetCombat(Guid combatId)
    {
        var combatDO = combatRepo.GetCombat(combatId);
        return new CombatTracker(combatDO);
    }

    public CombatTrackerSummary GetCombatSummary(Guid combatId)
    {
        throw new NotImplementedException();
    }

    public List<CombatTrackerSummary> GetCombatList()
    {
        throw new NotImplementedException();
    }

    public List<CombatTrackerSummary> GetCombatListForScenario(string scenarioCode)
    {
        throw new NotImplementedException();
    }

    public void RegisterHubClient(Guid combatId, string clientId)
    {
        throw new NotImplementedException();
    }

    public void RemoveHubClient(Guid combatId, string clientId)
    {
        throw new NotImplementedException();
    }
}
