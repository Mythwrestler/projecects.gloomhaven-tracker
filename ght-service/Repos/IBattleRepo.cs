using GloomhavenTracker.Service.Models;

namespace GloomhavenTracker.Service.Repos
{

    public interface IBattleRepo
    {
        public Battle GetBattle();

        public void SaveBattle();
    }
}

