using System;
using System.Collections.Generic;
using GloomhavenTracker.Service.Models;
using Microsoft.Extensions.Caching.Memory;

namespace GloomhavenTracker.Service.Repos;


public interface ICombatRepo
{
    public bool CombatExists(Guid combatId);

    public List<Guid> GetCombats();

    public CombatTrackerDO GetCombat(Guid combatId);

    public void SaveCombat(CombatTrackerDO combat);

    public void DisposeCombat(Guid combatId);

}

public class CombatRepo : ICombatRepo
{
    private readonly IMemoryCache _memCache;
    private readonly string _connectionString;

    private List<Guid>? _combatListing;

    private static class CACHE_KEYS
    {
        public static string Combat { get; } = "combat_";
        public static string CombatListing { get; } = "combat_listing";
    }

    public CombatRepo(IMemoryCache memCache, string connectionString)
    {
        _memCache = memCache;
        _connectionString = connectionString;
    }

    public bool CombatExists(Guid combatId)
    {
        return CombatIdInListing(combatId);
    }

    public List<Guid> GetCombats()
    {
        return _combatListing ?? new List<Guid>();
    }

    public CombatTrackerDO GetCombat(Guid combatId)
    {
        CombatTrackerDO? combat;
        _memCache.TryGetValue<CombatTrackerDO>($"{CACHE_KEYS.Combat}{combatId.ToString()}", out combat);
        return combat;
    }

    public void SaveCombat(CombatTrackerDO combat)
    {
        _memCache.Set<CombatTrackerDO>($"{CACHE_KEYS.Combat}{combat.CombatId.ToString()}", combat);
        if (!CombatIdInListing(combat.CombatId)) AddNewCombatToListing(combat.CombatId);
    }

    public void DisposeCombat(Guid combatId)
    {
        throw new NotImplementedException();
    }


    private bool CombatIdInListing(Guid combatId)
    {
        if (_combatListing == null)
        {
            _combatListing = new List<Guid>();
            List<Guid>? listingFromMemory;
            _memCache.TryGetValue(CACHE_KEYS.CombatListing, out listingFromMemory);
            if (listingFromMemory != null) _combatListing.AddRange(listingFromMemory);
        }

        return _combatListing.Contains(combatId);
    }


    private void AddNewCombatToListing(Guid combatId)
    {
        if (_combatListing == null)
        {
            _combatListing = new List<Guid>();
            List<Guid>? listingFromMemory;
            _memCache.TryGetValue(CACHE_KEYS.CombatListing, out listingFromMemory);
            if (listingFromMemory != null) _combatListing.AddRange(listingFromMemory);
        }

        if (!_combatListing.Contains(combatId)) _combatListing.Add(combatId);
        _memCache.Set<List<Guid>>(CACHE_KEYS.CombatListing, _combatListing);
    }











    // public Battle GetBattle()
    // {
    //     Battle? battle = _cache.Get<Battle>(CACHE_KEYS.Battle);

    //     if(battle == null)
    //     {
    //         battle = GetTestData();
    //         _cache.Set(CACHE_KEYS.Battle, battle);
    //     }

    //     return battle;
    // }

    // public void SaveBattle()
    // {
    //     throw new NotImplementedException();
    // }


    // private Battle GetTestData()
    // {
    //     var baseMonsterDeck = GetTestModifierDeck();
    //     var monsterDeck = new List<AttackModifier>();
    //     monsterDeck.AddRange(baseMonsterDeck);

    //     Player player1 = GetTestPlayer(new Guid("1f11e875-d93f-4002-854f-579b90e3c736"), "Test Player 01");
    //     Player player2 = GetTestPlayer(new Guid("ac6b070a-5625-45cd-80a0-6c05beb0510b"), "Test Player 02");
    //     Player player3 = GetTestPlayer(new Guid("59502422-8b4f-49c4-a1c9-2b5c684186c6"), "Test Player 03");
    //     Player player4 = GetTestPlayer(new Guid("d972f2a8-0d94-4842-b8c2-1f8e4fec7b14"), "Test Player 04");

    //     Monster monster1 = GetTestMonster(new Guid("55387201-8a08-441b-b558-9227d16009c8"), MONSTER_TYPES.Cultist, 3, false);
    //     Monster monster2 = GetTestMonster(new Guid("a6ca21ab-ccc8-43f0-83f7-37c865ea5db5"), MONSTER_TYPES.StoneGolem, 2, true);

    //     var actors = new List<Actor>(){
    //         player1,
    //         player2,
    //         player3,
    //         player4,
    //         monster1,
    //         monster2
    //     };

    //     return new Battle(){
    //         MonsterDeck = monsterDeck,
    //         Actors = actors,
    //         Initiative = new Dictionary<int, Guid> {
    //             {1, player1.Id}, {2, monster1.Id}, {3, player2.Id}, {4, player3.Id}, {5, monster2.Id}, {6, player4.Id}
    //         }
    //     };
    // }


    // private Monster GetTestMonster(Guid id, MONSTER_TYPES type, int monsterId, bool isElite)
    // {

    //     var baseStats = new Dictionary<int, BaseMonsterStatSet>(){
    //         {1, new BaseMonsterStatSet(){
    //                 Elite = new MonsterStatSet(){Health = 10, Movement = 3, Attack = 2},
    //                 Standard = new MonsterStatSet(){Health = 8, Movement = 2, Attack = 1}
    //             }}
    //     };

    //     return new Monster(){
    //         Id = id,
    //         Type = type,
    //         BaseStats = baseStats,
    //         IsElite = isElite,
    //         MonsterId = monsterId,
    //         Health = isElite ? baseStats.GetValueOrDefault(1).Elite.Health : baseStats.GetValueOrDefault(1).Standard.Health,
    //         Attack = isElite ? baseStats.GetValueOrDefault(1).Elite.Attack : baseStats.GetValueOrDefault(1).Standard.Attack,
    //         Movement = isElite ? baseStats.GetValueOrDefault(1).Elite.Movement : baseStats.GetValueOrDefault(1).Standard.Movement
    //     };
    // }


    // private Player GetTestPlayer(Guid id, string name) 
    // {

    //     var baseModDeck = GetTestModifierDeck();
    //     var modDeck = new List<AttackModifier>();
    //     modDeck.AddRange(baseModDeck);

    //     return new Player()
    //     {
    //         Id = id,
    //         BaseHealth = new Dictionary<int, int>(){
    //             {0, 8},
    //             {1, 12},
    //             {3, 14}
    //         },
    //         Level = new Dictionary<int, int>() {
    //             {0, 0},
    //             {1, 0},
    //             {2, 45}
    //         },
    //         CurrentHealth = 6,
    //         Name = name,
    //         ModifierDeck = modDeck,
    //         BaseModifierDeck = baseModDeck
    //     };

    // }


    // public List<AttackModifier> GetTestModifierDeck(){
    //     return new List<AttackModifier>{
    //         new AttackModifier(){Type = ATTACK_MODIFIER_TYPE.Add, Value = 1},
    //         new AttackModifier(){Type = ATTACK_MODIFIER_TYPE.Add, Value = 1},
    //         new AttackModifier(){Type = ATTACK_MODIFIER_TYPE.Add, Value = 1},
    //         new AttackModifier(){Type = ATTACK_MODIFIER_TYPE.Add, Value = 1},
    //         new AttackModifier(){Type = ATTACK_MODIFIER_TYPE.Add, Value = 2},
    //         new AttackModifier(){Type = ATTACK_MODIFIER_TYPE.Add, Value = 2},
    //         new AttackModifier(){Type = ATTACK_MODIFIER_TYPE.Multiply, Value = 2},
    //         new AttackModifier(){Type = ATTACK_MODIFIER_TYPE.Add, Value = -1},
    //         new AttackModifier(){Type = ATTACK_MODIFIER_TYPE.Add, Value = -1},
    //         new AttackModifier(){Type = ATTACK_MODIFIER_TYPE.Add, Value = -1},
    //         new AttackModifier(){Type = ATTACK_MODIFIER_TYPE.Add, Value = -1},
    //         new AttackModifier(){Type = ATTACK_MODIFIER_TYPE.Add, Value = -2},
    //         new AttackModifier(){Type = ATTACK_MODIFIER_TYPE.Add, Value = -2},
    //         new AttackModifier(){Type = ATTACK_MODIFIER_TYPE.Cancel},
    //         new AttackModifier(){Type = ATTACK_MODIFIER_TYPE.Cancel},
    //     };
    // }

    // public CombatTracker GetBattle(Guid BattleId)
    // {
    //     throw new NotImplementedException();
    // }

    // public List<CombatTracker> GetActiveBattles()
    // {
    //     throw new NotImplementedException();
    // }

    // public void SaveBattle(CombatTracker battle)
    // {
    //     throw new NotImplementedException();
    // }

    // public bool BattleExists(Guid BattleId)
    // {
    //     throw new NotImplementedException();
    // }
}