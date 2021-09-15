using System.Text.Json;
using System.Text.Json.Serialization;
using GloomhavenTracker.Service.Models;
using GloomhavenTracker.Service.Repos;

namespace GloomhavenTracker.Service.Services
{
    public class BattleService : IBattleService {

        private IBattleRepo _repo;

        public BattleService (IBattleRepo repo) => _repo = repo;

        public Battle GetBattle() => _repo.GetBattle();

    }
}