using System.Collections.Generic;
using GloomhavenTracker.Service.Models;
using GloomhavenTracker.Service.Repos;

namespace GloomhavenTracker.Service.Services;

public interface IContentService
{
    public List<ContentItemSummary> GetContentSummary(CONTENT_TYPE kind, GAME_CODES? gameCode);
    public ContentItemSummary GetDefaultsForGame(GAME_CODES gameCode);
    public List<ContentItemSummary> GetPlayerDefaultsForGame(GAME_CODES gameCode);
    public List<ContentItemSummary> GetMonsterDefaultsForGame(GAME_CODES gameCode);

}

public class ContentService : IContentService
{
    private readonly IContentRepo _repo;

    public ContentService(IContentRepo repo) => _repo = repo;

    public List<ContentItemSummary> GetContentSummary(CONTENT_TYPE kind, GAME_CODES? gameCode = null)
    {
        return _repo.GetContentSummary(kind, gameCode);
    }

    public ContentItemSummary GetDefaultsForGame(GAME_CODES gameCode)
    {
        return _repo.GetDefaultsForGame(gameCode);
    }

    public List<ContentItemSummary> GetMonsterDefaultsForGame(GAME_CODES gameCode)
    {
        return _repo.GeMonsterDefaultsForGame(gameCode);
    }

    public List<ContentItemSummary> GetPlayerDefaultsForGame(GAME_CODES gameCode)
    {
        return _repo.GetPlayerDefaultsForGame(gameCode);
    }
}