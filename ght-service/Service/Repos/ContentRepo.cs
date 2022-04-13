using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GloomhavenTracker.Database;
using GloomhavenTracker.Database.Models.Content;
using GloomhavenTracker.Service.Models.Content;
using Microsoft.EntityFrameworkCore;

namespace GloomhavenTracker.Service.Repos;

public interface ContentRepo
{
    public List<ContentSummary> GetContentSummary(CONTENT_TYPE contentType, GAME_TYPE? gameCode);
    public List<ScenarioSummary> GetScenarios(GAME_TYPE gameCode);
    public Game GetGameDefaults(GAME_TYPE gameCode);
    public Character GetCharacterDefaults(GAME_TYPE gameCode, string contentCode);
    public Monster GetMonsterDefaults(GAME_TYPE gameCode, string contentCode);
    public List<AttackModifier> GetBaseModifierDeck (GAME_TYPE gameCode);
    public Scenario GetScenarioDefaults(GAME_TYPE gameCode, string contentCode);
    public bool IsValidCode(CONTENT_TYPE contentType, string contentCode, GAME_TYPE? gameCode);
}

public class ContentRepoImplementation : ContentRepo
{
    private readonly GloomhavenContext context;
    private readonly IMapper mapper;

    public ContentRepoImplementation (GloomhavenContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public List<AttackModifier> GetBaseModifierDeck(GAME_TYPE gameCode)
    {
        var gameString = GameUtils.GameTypeString(gameCode);
        List<GameBaseAttackModifierDAO> baseDeck = context.GameBaseAttackModifiers
            .Where(baseDeck => baseDeck.Game.ContentCode == gameString)
            .Include(baseDeck => baseDeck.AttackModifier)
            .ToList();
        return mapper.Map<List<AttackModifier>>(baseDeck);
    }

    public Character GetCharacterDefaults(GAME_TYPE gameCode, string contentCode)
    {
        var gameString = GameUtils.GameTypeString(gameCode);
        CharacterDAO? character = context.CharacterContent
            .Where(character => character.Game.ContentCode == gameString && character.ContentCode == contentCode)
            .Include(character => character.BaseStats)
            .FirstOrDefault();
        if(character is null) throw new KeyNotFoundException("Character Content Code Not Found");
        
        return mapper.Map<Character>(character);
    }

    public List<ContentSummary> GetContentSummary(CONTENT_TYPE contentType, GAME_TYPE? gameCode)
    {
        var gameString = GameUtils.GameTypeString(gameCode);
        switch (contentType)
        {
            case(CONTENT_TYPE.game):
            {
                List<GameDAO> gameDAOs = context.Game.ToList();
                return mapper.Map<List<Game>>(gameDAOs).Select(g => g.Summary).ToList();
            }
            case(CONTENT_TYPE.attackModifier):
            {
                List<AttackModifierDAO> modifierDAOs = context.AttackModifier
                    .Where(modifier => modifier.Game.ContentCode == gameString)
                    .ToList();
                return mapper.Map<List<AttackModifier>>(modifierDAOs).Select(am => am.Summary).ToList();
            }
            case(CONTENT_TYPE.character):
            {
                List<CharacterDAO> characterDAOs = context.CharacterContent
                    .Where(character => character.Game.ContentCode == gameString)
                    .ToList();
                return mapper.Map<List<Character>>(characterDAOs).Select(c => c.Summary).ToList();
            }
            case(CONTENT_TYPE.item):
            {
                return new List<ContentSummary>();
            }
            case(CONTENT_TYPE.monster):
            {
                List<MonsterDAO> monsterDAOs = context.Monster
                    .Where(monster => monster.Game.ContentCode == gameString)
                    .ToList();
                return mapper.Map<List<Monster>>(monsterDAOs).Select(m => m.Summary).ToList();
            }
            case(CONTENT_TYPE.objective):
            {
                List<ObjectiveDAO> objectiveDAOs = context.Objective
                    .Where(monster => monster.Game.ContentCode == gameString)
                    .ToList();
                return mapper.Map<List<Objective>>(objectiveDAOs).Select(m => m.Summary).ToList();
            }
            default:
            {
                return new List<ContentSummary>();
            }
        }
    }

    public Game GetGameDefaults(GAME_TYPE gameCode)
    {
        var gameString = GameUtils.GameTypeString(gameCode);
        GameDAO? game = context.Game
            .Where(game => game.ContentCode == gameString)
            .Include(game => game.BaseModifierDeck)
            .FirstOrDefault();
        if(game is null) throw new KeyNotFoundException("Game Content Code Not Found");
        return mapper.Map<Game>(game);
    }

    public Monster GetMonsterDefaults(GAME_TYPE gameCode, string contentCode)
    {
        var gameString = GameUtils.GameTypeString(gameCode);
        MonsterDAO? monster = context.Monster
            .Where(monster => monster.Game.ContentCode == gameString && monster.ContentCode == contentCode)
            .Include(monster => monster.BaseStats).ThenInclude(bs => bs.AttackEffects).ThenInclude(ae => ae.Effect)
            .Include(monster => monster.BaseStats).ThenInclude(bs => bs.DefenseEffects).ThenInclude(de => de.Effect)
            .Include(monster => monster.BaseStats).ThenInclude(bs => bs.DeathEffects).ThenInclude(de => de.Effect)
            .Include(monster => monster.BaseStats).ThenInclude(bs => bs.Immunity)
            .FirstOrDefault();
        if(monster is null) throw new KeyNotFoundException("Monster Content Code Not Found");

        return mapper.Map<Monster>(monster);
    }

    public List<ScenarioSummary> GetScenarios(GAME_TYPE gameCode)
    {
        var gameString = GameUtils.GameTypeString(gameCode);
        List<ScenarioDAO> scenarios = context.ScenarioContent
            .Where(scenario => scenario.Game.ContentCode == gameString)
            .ToList();
        if(scenarios.Count() == 0) throw new KeyNotFoundException("Game Content Code Not Found");

        return mapper.Map<List<Scenario>>(scenarios).Select(scenario => scenario.Summary).ToList();
    }
    
    public Scenario GetScenarioDefaults(GAME_TYPE gameCode, string contentCode)
    {
        var gameString = GameUtils.GameTypeString(gameCode);
        ScenarioDAO? scenario = context.ScenarioContent
            .Where(scenario => scenario.Game.ContentCode == gameString && scenario.ContentCode == contentCode)
            .Include(scenario => scenario.Monsters).ThenInclude(sm => sm.Monster).ThenInclude(monster => monster.BaseStats).ThenInclude(bs => bs.AttackEffects).ThenInclude(ae => ae.Effect)
            .Include(scenario => scenario.Monsters).ThenInclude(sm => sm.Monster).ThenInclude(monster => monster.BaseStats).ThenInclude(bs => bs.DefenseEffects).ThenInclude(de => de.Effect)
            .Include(scenario => scenario.Monsters).ThenInclude(sm => sm.Monster).ThenInclude(monster => monster.BaseStats).ThenInclude(bs => bs.DeathEffects).ThenInclude(de => de.Effect)
            .Include(scenario => scenario.Monsters).ThenInclude(sm => sm.Monster).ThenInclude(monster => monster.BaseStats).ThenInclude(bs => bs.Immunity)
            .FirstOrDefault();

        if(scenario is null) throw new KeyNotFoundException("Scenario Content Code Not Found");

        return mapper.Map<Scenario>(scenario);
    }

    public bool IsValidCode(CONTENT_TYPE contentType, string contentCode, GAME_TYPE? gameCode)
    {
        var gameString = GameUtils.GameTypeString(gameCode);
        switch (contentType)
        {
            case(CONTENT_TYPE.game):
            {
                var content = context.Game.Where(c => c.ContentCode == contentCode).FirstOrDefault();
                return content is not null ? true : false;
            }
            case(CONTENT_TYPE.attackModifier):
            {
                var content = context.AttackModifier
                    .Where(c => c.Game.ContentCode == gameString && c.ContentCode == contentCode)
                    .FirstOrDefault();
                return content is not null ? true : false;
            }
            case(CONTENT_TYPE.character):
            {
                var content = context.CharacterContent
                    .Where(c => c.Game.ContentCode == gameString && c.ContentCode == contentCode)
                    .FirstOrDefault();
                return content is not null ? true : false;
            }
            case(CONTENT_TYPE.item):
            {
                return false;
            }
            case(CONTENT_TYPE.monster):
            {
                var content = context.Monster
                    .Where(c => c.Game.ContentCode == gameString && c.ContentCode == contentCode)
                    .FirstOrDefault();
                return content is not null ? true : false;
            }
            case(CONTENT_TYPE.objective):
            {
                var content = context.Objective
                    .Where(c => c.Game.ContentCode == gameString && c.ContentCode == contentCode)
                    .FirstOrDefault();
                return content is not null ? true : false;
            }
            case(CONTENT_TYPE.scenario):
            {
                var content = context.ScenarioContent
                    .Where(c => c.Game.ContentCode == gameString && c.ContentCode == contentCode)
                    .FirstOrDefault();
                return content is not null ? true : false;
            }
            default:
            {
                return false;
            }
        }
    }
}