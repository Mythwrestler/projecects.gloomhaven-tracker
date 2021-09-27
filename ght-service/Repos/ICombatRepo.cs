using GloomhavenTracker.Service.Models;

namespace GloomhavenTracker.Service.Repos
{

    public interface ICombatRepo
    {
        public bool CombatExists(Guid combatId);

        public List<Guid> GetCombats();

        public CombatTrackerDO GetCombat(Guid combatId);

        public void SaveCombat(CombatTrackerDO combat);

        public void DisposeCombat(Guid combatId);

    }
}

