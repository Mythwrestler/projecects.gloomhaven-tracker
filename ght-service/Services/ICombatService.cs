using GloomhavenTracker.Service.Models;

namespace GloomhavenTracker.Service.Services
{
    public interface ICombatService {

        public bool CombatExists(Guid combatId);

        public List<Guid> GetCombatList();

        public bool PlayerBelongsToCombat(Guid combatId, Guid playerId);

        public CombatTrackerDTO GetCombat(Guid combatId);

        public CombatActionResult ProcessCombatAction(Guid combatId, CombatAction action);

        public Dictionary<Guid, int> ProcessActorInitiative(Guid combatId, CombatInitiative initiative);

        public CombatTrackerDTO NextRound(Guid combatId);

        public Guid NewCombat();

        public CombatTrackerDTO AddActors(Guid combatId, ActorsDTO actors);

        public bool CombatRoundReady(Guid combatId);

        public Guid? CurrentTurnInCombat(Guid combatId);

        public List<string> GetHubClients (Guid combatId);
        public void RegisterHubClient(Guid combatId, string clientId);
        public void RemoveHubClient(Guid combatId, string clientId);

    }
}