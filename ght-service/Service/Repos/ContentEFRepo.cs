using System.Collections.Generic;
using System.Linq;
using GloomhavenTracker.Database;
using GloomhavenTracker.Service.Models.Content;
using Microsoft.EntityFrameworkCore;

namespace GloomhavenTracker.Service.Repos;

public class ContentEFRepo : ContentRepo
{
    private readonly ContentContext context;

    public ContentEFRepo (ContentContext context)
    {
        this.context = context;
    }

    public List<AttackModifier> GetBaseModifierDeck(GAME_TYPE gameCode)
    {
        throw new System.NotImplementedException();
    }

    public Character GetCharacterDefaults(GAME_TYPE gameCode, string contentCode)
    {
        throw new System.NotImplementedException();
    }

    public List<ContentSummary> GetContentSummary(CONTENT_TYPE contentType, GAME_TYPE? gameCode)
    {
        throw new System.NotImplementedException();
    }

    public Game GetGameDefaults(GAME_TYPE gameCode)
    {
        throw new System.NotImplementedException();
    }

    public Monster GetMonsterDefaults(GAME_TYPE gameCode, string contentCode)
    {
        var gameString = GameUtils.GameTypeString(gameCode);
        Database.Models.Content.Monster? monster = context.Monster
            .Where(monster => monster.Game.ContentCode == gameString && monster.ContentCode == contentCode)
            .Include(monster => monster.BaseStats).ThenInclude(bs => bs.AttackEffects)
            .Include(monster => monster.BaseStats).ThenInclude(bs => bs.DefenseEffects)
            .Include(monster => monster.BaseStats).ThenInclude(bs => bs.Immunity)
            .FirstOrDefault();
        if(monster is null) throw new KeyNotFoundException("Monster Content Code Not Found");
        return new Monster()
        {
            ContentCode = monster.ContentCode,
            Name = monster.Name,
            Description = monster.Description,
            BaseStats = new BaseMonsterStatSet()
            {
                Elite = monster.BaseStats.Where(bs => bs.IsElite).Select(bs => new MonsterStatSet(){Level = bs.Level, Attack = bs.Attack, Health = bs.Health, Movement = bs.Movement}).ToList(),
                Standard = monster.BaseStats.Where(bs => !bs.IsElite).Select(bs => new MonsterStatSet(){Level = bs.Level, Attack = bs.Attack, Health = bs.Health, Movement = bs.Movement}).ToList()
            }
        };
    }

    public Scenario GetScenarioDefaults(GAME_TYPE gameCode, string contentCode)
    {
        throw new System.NotImplementedException();
    }

    public List<ScenarioSummary> GetScenarioSummary(GAME_TYPE gameCode)
    {
        throw new System.NotImplementedException();
    }

    public bool IsValidCode(string kind, string contentCode, string? gameCode)
    {
        throw new System.NotImplementedException();
    }
}