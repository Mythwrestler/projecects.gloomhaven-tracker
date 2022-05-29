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
    public List<AttackModifier> GetBaseModDeck(GAME_TYPE gameCode);
    public bool IsValidGameCode(string contentCode);
    public bool IsValidCharacterCode(GAME_TYPE gameCode, string characterCode);
    public bool IsValidScenarioCode(GAME_TYPE gameCode, string scenarioCode);
}

public partial class ContentServiceImplementation : ContentService
{
    private readonly ContentRepo repo;

    public ContentServiceImplementation(ContentRepo repo)
    {
        this.repo = repo;
    }

    public List<ContentSummary> GetContentSummary(CONTENT_TYPE kind, GAME_TYPE? gameCode = null)
    {
        return repo.GetContentSummary(kind, gameCode);
    }

    public Game GetGameDefaults(GAME_TYPE gameCode)
    {
        return repo.GetGameDefaults(gameCode);
    }

    public Character GetCharacterDefaults(GAME_TYPE gameCode, string contentCode)
    {
        return repo.GetCharacterDefaults(gameCode, contentCode);
    }

    public Monster GetMonsterDefaults(GAME_TYPE gameCode, string contentCode)
    {
        return repo.GetMonsterDefaults(gameCode, contentCode);
    }

    public Scenario GetScenarioDefaults(GAME_TYPE gameCode, string contentCode)
    {
        return repo.GetScenarioDefaults(gameCode, contentCode);
    }

    public List<AttackModifier> GetBaseModDeck(GAME_TYPE gameCode)
    {
        return repo.GetBaseModifierDeck(gameCode);
    }

    public bool IsValidGameCode(string contentCode)
    {
        return repo.IsValidCode(CONTENT_TYPE.game, contentCode, null);
    }

    public bool IsValidCharacterCode(GAME_TYPE gameCode, string characterCode)
    {
        return repo.IsValidCode(CONTENT_TYPE.character, characterCode, gameCode);
    }
    public bool IsValidScenarioCode(GAME_TYPE gameCode, string scenarioCode)
    {
        return repo.IsValidCode(CONTENT_TYPE.scenario, scenarioCode, gameCode);
    }
}