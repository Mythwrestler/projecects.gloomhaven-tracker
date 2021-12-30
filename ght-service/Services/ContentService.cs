using System.Collections.Generic;
using GloomhavenTracker.Service.Models.Content;
using GloomhavenTracker.Service.Repos;

namespace GloomhavenTracker.Service.Services;

public interface ContentService
{
    public List<ContentSummary> GetContentSummary(CONTENT_TYPE kind, GAME_TYPE? gameCode);
    public Game GetGameDefaults(GAME_TYPE gameCode);
    public Character GetCharacterDefaults(GAME_TYPE gameCode, string contentCode);
    public Monster GetMonsterDefaults(GAME_TYPE gameCode, string contentCode);
    public Scenario GetScenarioDefaults(GAME_TYPE gameCode, string contentCode);
    public bool IsValidGameCode(string gameCode);
    public bool IsValidCharacterCode(string gameCode, string characterCode);
    public bool IsValidScenarioCode(string gameCode, string scenarioCode);
}

public partial class ContentServiceImplementation : ContentService
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

    public bool IsValidGameCode(string gameCode)
    {
        return _repo.IsValidCode("game", gameCode, null);
    }

    public bool IsValidCharacterCode(string gameCode, string characterCode)
    {
        return _repo.IsValidCode("character", characterCode, gameCode);
    }
    public bool IsValidScenarioCode(string gameCode, string scenarioCode)
    {
        return _repo.IsValidCode("scenario", scenarioCode, gameCode);
    }
}