using System.Collections.Generic;
using GloomhavenTracker.Service.Models.Content;
using GloomhavenTracker.Service.Repos;

namespace GloomhavenTracker.Service.Services;

public interface IContentService
{
    public List<ContentSummary> GetContentSummary(CONTENT_TYPE kind, GAME_TYPE? gameCode);
    public Game GetGameDefaults(GAME_TYPE gameCode);
    public Character GetCharacterDefaults(GAME_TYPE gameCode, string contentCode);
    public Monster GetMonsterDefaults(GAME_TYPE gameCode, string contentCode);
    public Scenario GetScenarioDefaults(GAME_TYPE gameCode, string contentCode);

}

public class ContentService : IContentService
{
    private readonly IContentRepo _repo;

    public ContentService(IContentRepo repo) => _repo = repo;

    public List<ContentSummary> GetContentSummary(CONTENT_TYPE kind, GAME_TYPE? gameCode = null)
    {
        return _repo.GetContentSummary(kind, gameCode);
    }

    public Game GetGameDefaults(GAME_TYPE gameCode)
    {
        return _repo.GetGameDefaults(gameCode);
    }

    public Character GetCharacterDefaults(GAME_TYPE gameCode, string contentCode)
    {
        return _repo.GetCharacterDefaults(gameCode, contentCode);
    }

    public Monster GetMonsterDefaults(GAME_TYPE gameCode, string contentCode)
    {
        return _repo.GetMonsterDefaults(gameCode, contentCode);
    }

    public Scenario GetScenarioDefaults(GAME_TYPE gameCode, string contentCode)
    {
        return _repo.GetScenarioDefaults(gameCode, contentCode);
    }
}