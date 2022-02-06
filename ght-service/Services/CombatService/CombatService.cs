
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
    public List<CombatTrackerSummary> GetCombatList();
    public CombatTrackerDTO GetCombatDTO(Guid combatId);

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
    private Dictionary<Guid, CombatTracker> combatTrackers = new Dictionary<Guid, CombatTracker>();

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

    private CombatTracker GetCombatById(Guid combatId)
    {
        CombatTracker combat;
        
        if(!combatTrackers.ContainsKey(combatId)){
            combat = new CombatTracker(combatRepo.GetCombatTracker(combatId));
            combatTrackers.Add(combat.Id, combat);
        }
        combat = combatTrackers[combatId];

        return combat;
    }

    public List<CombatTrackerSummary> GetCombatList()
    {
        return combatRepo.GetCombatTrackerListing();
    }

    public CombatTrackerDTO GetCombatDTO(Guid combatId)
    {
        CombatTracker combat = GetCombatById(combatId);
        return combat.DataTransferObject;
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
