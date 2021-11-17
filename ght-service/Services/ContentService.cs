using System.Collections.Generic;
using GloomhavenTracker.Service.Models.Content;
using GloomhavenTracker.Service.Repos;

namespace GloomhavenTracker.Service.Services;

public interface ContentService
{
    public List<ContentSummary> GetContentSummary(CONTENT_TYPE kind, GAME_TYPE? gameCode);
    public Game GetGameDefaults(GAME_TYPE gameCode);
    public Character GetPlayerDefaults(GAME_TYPE gameCode, string contentCode);
    public Monster GetMonsterDefaults(GAME_TYPE gameCode, string contentCode);
    public ScenarioContent GetScenarioDefaults(GAME_TYPE gameCode, string contentCode);

}

public class ContentServiceImplementation : ContentService
{
    private readonly ContentRepo _repo;

    public ContentServiceImplementation(ContentRepo repo) => _repo = repo;

    public List<ContentSummary> GetContentSummary(CONTENT_TYPE kind, GAME_TYPE? gameCode = null)
    {
        return _repo.GetContentSummary(kind, gameCode);
    }

    public Game GetGameDefaults(GAME_TYPE gameCode)
    {
        return _repo.GetGameDefaults(gameCode);
    }

    public Character GetPlayerDefaults(GAME_TYPE gameCode, string contentCode)
    {
        return _repo.GetPlayerDefaults(gameCode, contentCode);
    }

    public Monster GetMonsterDefaults(GAME_TYPE gameCode, string contentCode)
    {
        return _repo.GetMonsterDefaults(gameCode, contentCode);
    }

    public ScenarioContent GetScenarioDefaults(GAME_TYPE gameCode, string contentCode)
    {
        return _repo.GetScenarioDefaults(gameCode, contentCode);
    }
}