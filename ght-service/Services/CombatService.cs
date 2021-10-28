using System;
using System.Collections.Generic;
using System.Linq;
using GloomhavenTracker.Service.Models;
using GloomhavenTracker.Service.Repos;
using Microsoft.Extensions.Caching.Memory;

namespace GloomhavenTracker.Service.Services;
public interface ICombatService
{

    public bool CombatExists(Guid combatId);

    public List<CombatTrackerSummaryDTO> GetCombatList();

    public bool PlayerBelongsToCombat(Guid combatId, Guid playerId);

    public CombatTrackerDTO GetCombat(Guid combatId);

    public CombatActionResult ProcessCombatAction(Guid combatId, CombatAction action);

    public Dictionary<Guid, int> ProcessActorInitiative(Guid combatId, CombatInitiative initiative);

    public CombatTrackerDTO NextRound(Guid combatId);

    public CombatTrackerSummaryDTO NewCombat(string description);

    public CombatTrackerDTO AddActors(Guid combatId, ActorsDTO actors);

    public bool CombatRoundReady(Guid combatId);

    public Guid? CurrentTurnInCombat(Guid combatId);

    public List<string> GetHubClients(Guid combatId);
    public void RegisterHubClient(Guid combatId, string clientId);
    public void RemoveHubClient(Guid combatId, string clientId);

}

public class CombatService : ICombatService
{
    private readonly IMemoryCache _memCache;
    private readonly ICombatRepo _repo;

    private readonly Dictionary<Guid, CombatTracker> _combatSpaces = new Dictionary<Guid, CombatTracker>();

    public CombatService(IMemoryCache memCache, ICombatRepo repo)
    {
        _memCache = memCache;
        _repo = repo;
    }

    public List<CombatTrackerSummaryDTO> GetCombatList()
    {
        List<Guid> ids = _repo.GetCombats();
        List<CombatTrackerSummaryDTO> summaries = ids.Select(id => _repo.GetCombat(id).SummaryDTO).ToList();

        return summaries;
    }

    public bool CombatExists(Guid combatId)
    {
        if (_combatSpaces.ContainsKey(combatId)) return true;
        if (_repo.CombatExists(combatId)) return true;
        return false;
    }

    public CombatTrackerDTO GetCombat(Guid combatId)
    {
        if (!CombatExists(combatId)) throw new ArgumentException("Invalid Combat Id");
        return GetCombatTracker(combatId).DTO;
    }

    private CombatTracker GetCombatTracker(Guid combatId)
    {
        if (!_combatSpaces.ContainsKey(combatId))
        {
            _combatSpaces.Add(combatId, new CombatTracker(_repo.GetCombat(combatId)));
        }
        return _combatSpaces[combatId];
    }

    public CombatTrackerDTO NextRound(Guid combatId)
    {
        if (!CombatExists(combatId)) throw new ArgumentException("Invalid Combat Id");
        CombatTracker combat = GetCombatTracker(combatId);
        combat.NewRound();

        _repo.SaveCombat(combat.State);
        return combat.DTO;
    }

    public bool PlayerBelongsToCombat(Guid combatId, Guid playerId)
    {
        throw new NotImplementedException();
    }

    public Dictionary<int, Guid>? ProcessActorInitiative(Guid combatId, CombatInitiative initiative)
    {
        if (!CombatExists(combatId)) throw new ArgumentException("Invalid Combat Id");
        CombatTracker combat = GetCombatTracker(combatId);
        combat.SetCombatInitiative(initiative);

        _repo.SaveCombat(combat.State);

        return combat.TurnOrder;

    }

    public CombatActionResult ProcessCombatAction(Guid combatId, CombatAction action)
    {
        if (!CombatExists(combatId)) throw new ArgumentException("Invalid Combat Id");
        CombatTracker combat = GetCombatTracker(combatId);

        var result = combat.ProcessBattleAction(action);

        _repo.SaveCombat(combat.State);

        return result;

    }

    public CombatTrackerDTO AddActors(Guid combatId, ActorsDTO actors)
    {
        if (!CombatExists(combatId)) throw new ArgumentException("Invalid Combat Id");
        CombatTracker combat = GetCombatTracker(combatId);

        AddPlayers(combat, actors.Players);
        AddMonsters(combat, actors.Monsters);
        AddObjectives(combat, actors.Objectives);

        _repo.SaveCombat(combat.State);
        return combat.DTO;
    }


    private void AddMonsters(CombatTracker combat, List<MonsterDTO> actors)
    {
        actors.ForEach(monsterDTO =>
        {
            var baseStats = monsterDTO.BaseStats ?? new BaseMonsterStatSet();
            Monster monster = new Monster()
            {
                Effects = new ActorEffects(monsterDTO.Effects),
                Health = 0,
                Id = monsterDTO.Id,
                Level = monsterDTO.Level,
                Name = monsterDTO.Name,
                BaseStats = baseStats,
                MonsterId = monsterDTO.MonsterId,
                IsElite = monsterDTO.IsElite,
                Type = monsterDTO.Type
            };
            monster.Health = monsterDTO.Health > 0 ? monsterDTO.Health : monster.BaseHealth;
            var effects = monster.IsElite ? monster.BaseStats.Elite[monster.Level].DefenseEffects : monster.BaseStats.Standard[monster.Level].DefenseEffects;
            monster.Effects = new ActorEffects(effects);
            combat.AddMonster(monster);
        });

    }


    private void AddPlayers(CombatTracker combat, List<PlayerDTO> actors)
    {
        actors.ForEach(playerDTO =>
        {
            Player player = new Player()
            {
                BaseHealthStats = playerDTO.BaseHealthStats ?? new List<PlayerBaseHealth>(),
                BaseModifierDeck = playerDTO.BaseModifierDeck ?? new List<AttackModifier>(),
                Effects = new ActorEffects(playerDTO.Effects),
                Experience = playerDTO.Experience,
                Health = 0,
                Id = playerDTO.Id,
                Levels = playerDTO.Levels ?? new List<PlayerLevel>(),
                ModifierDeck = new AttackModifierDeck(playerDTO.BaseModifierDeck ?? new List<AttackModifier>()),
                Name = playerDTO.Name
            };
            player.Health = playerDTO.Health > 0 ? playerDTO.Health : player.BaseHealth;
            combat.AddPlayer(player);
        });
    }

    private void AddObjectives(CombatTracker combat, List<ObjectiveDTO> actors)
    {
        actors.ForEach(objectiveDTO =>
        {
            if (objectiveDTO.BaseHealth == null) throw new ArgumentException("Objectvie Base Health must be provided.");
            Objective objective = new Objective(objectiveDTO.BaseHealth ?? 1)
            {
                Effects = new ActorEffects(objectiveDTO.Effects),
                Health = 0,
                Id = objectiveDTO.Id,
                Name = objectiveDTO.Name,
                ObjectiveId = objectiveDTO.ObjectiveId
            };
            objective.Health = objectiveDTO.Health > 0 ? objectiveDTO.Health : objective.BaseHealth;
            combat.AddObjective(objective);
        });
    }


    public CombatTrackerSummaryDTO NewCombat(string description)
    {
        CombatTracker combat = new CombatTracker(description, new List<Player>(), new List<Monster>(), CombatBuilder.BaseModDeck);

        _combatSpaces.Add(combat.CombatId, combat);

        _repo.SaveCombat(combat.State);
        return combat.SummaryDTO;
    }

    Dictionary<Guid, int> ICombatService.ProcessActorInitiative(Guid combatId, CombatInitiative initiative)
    {
        if (!CombatExists(combatId)) throw new ArgumentException("Invalid Combat Id");
        CombatTracker combat = GetCombatTracker(combatId);

        combat.SetCombatInitiative(initiative);

        _repo.SaveCombat(combat.State);
        return combat.State.Initiative;
    }

    public bool CombatRoundReady(Guid combatId)
    {
        if (!CombatExists(combatId)) throw new ArgumentException("Invalid Combat Id");
        CombatTracker combat = GetCombatTracker(combatId);

        return (combat.CurrentActor != null);
    }

    public Guid? CurrentTurnInCombat(Guid combatId)
    {
        if (!CombatExists(combatId)) throw new ArgumentException("Invalid Combat Id");
        CombatTracker combat = GetCombatTracker(combatId);

        return combat.CurrentActor;
    }

    public List<string> GetHubClients(Guid combatId)
    {
        if (!CombatExists(combatId)) throw new ArgumentException("Invalid Combat Id");
        CombatTracker combat = GetCombatTracker(combatId);

        return combat.HubClients;
    }

    public void RegisterHubClient(Guid combatId, string clientId)
    {
        if (!CombatExists(combatId)) throw new ArgumentException("Invalid Combat Id");
        CombatTracker combat = GetCombatTracker(combatId);

        combat.RegisterHubClient(clientId);

        _repo.SaveCombat(combat.State);
    }

    public void RemoveHubClient(Guid combatId, string clientId)
    {
        if (!CombatExists(combatId)) throw new ArgumentException("Invalid Combat Id");
        CombatTracker combat = GetCombatTracker(combatId);

        combat.RemoveHubClient(clientId);

        _repo.SaveCombat(combat.State);
    }
}