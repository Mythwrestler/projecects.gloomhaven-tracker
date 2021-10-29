using System.Collections.Generic;
using GloomhavenTracker.Service.Models;
using GloomhavenTracker.Service.Repos;

namespace GloomhavenTracker.Service.Services;

public interface IContentService
{
    public List<ContentItemSummary> GetContentSummary(CONTENT_TYPE kind, GAME_CODES? gameCode);
    public GameContent GetGameDefaults(GAME_CODES gameCode);
    public PlayerContent GetPlayerDefaults(GAME_CODES gameCode, string contentCode);
    public MonsterContent GetMonsterDefaults(GAME_CODES gameCode, string contentCode);
    public ScenarioContent GetScenarioDefaults(GAME_CODES gameCode, string contentCode);

}

public class ContentService : IContentService
{
    private readonly IContentRepo _repo;

    public ContentService(IContentRepo repo) => _repo = repo;

    public List<ContentItemSummary> GetContentSummary(CONTENT_TYPE kind, GAME_CODES? gameCode = null)
    {
        return _repo.GetContentSummary(kind, gameCode);
    }

    public GameContent GetGameDefaults(GAME_CODES gameCode)
    {
        return _repo.GetGameDefaults(gameCode);
    }

    public PlayerContent GetPlayerDefaults(GAME_CODES gameCode, string contentCode)
    {
        return _repo.GetPlayerDefaults(gameCode, contentCode);
    }

    public MonsterContent GetMonsterDefaults(GAME_CODES gameCode, string contentCode)
    {
        return _repo.GetMonsterDefaults(gameCode, contentCode);
    }

    public ScenarioContent GetScenarioDefaults(GAME_CODES gameCode, string contentCode)
    {
        return _repo.GetScenarioDefaults(gameCode, contentCode);
    }
}