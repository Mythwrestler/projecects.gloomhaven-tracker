
using System;
using System.Collections.Generic;
using GloomhavenTracker.Service.Models.Combat;
using GloomhavenTracker.Service.Models.Content;

namespace GloomhavenTracker.Service.Services;
public interface ICombatService
{

    #region Find or Start Combat Interface

    public bool CombatExists(Guid combatId);
    public Guid NewCombat(GAME_TYPE gameCode, string scenarioCode, string description);
    public CombatTrackerSummary GetCombat(Guid combatId);
    public List<CombatTrackerSummary> GetCombatList();
    public List<CombatTrackerSummary> GetCombatListForScenario(string scenarioCode);

    #endregion

    #region Combat Hub Clients Interface
    
    public void RegisterHubClient(Guid combatId, string clientId);
    public void RemoveHubClient(Guid combatId, string clientId);
    
    #endregion
}

public partial class CombatService : ICombatService
{
    public bool CombatExists(Guid combatId)
    {
        throw new NotImplementedException();
    }

    public CombatTrackerSummary GetCombat(Guid combatId)
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
