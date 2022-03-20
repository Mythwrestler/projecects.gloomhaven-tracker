using GloomhavenTracker.Database.Models.Content;

namespace GloomhavenTracker.Database.DataSeed;

public static partial class ContentSeedData
{

    private static void SeedMonsters(ContentContext context)
    {
        //Bosses
        Jaws_BloodHorror(context);
        Jaws_BloodTumor(context);
        Jaws_FirstOfTheOrder(context);

        //Monsters
        Jaws_VermlingRaider(context);
        Jaws_VermlingScout(context);
        Jaws_GiantViper(context);
        Jaws_Zealot(context);
        Jaws_BlackSludge(context);

    }

    #region Helper Methods
    private static bool MonsterExists(ContentContext context, string contentCode, Game game)
    {
        var check = context.Monster.Local.FirstOrDefault(monster => monster.ContentCode == contentCode && monster.Game.Id == game.Id);
        if(check is null) check = context.Monster.FirstOrDefault(monster => monster.ContentCode == contentCode && monster.Game.Id == game.Id);
        if(check is null) return false;
        return true;
    }

    private static Monster BuildMonster(string code, string name, string description, Game game) => new Monster() {ContentCode = code, Name = name, Description = description, GameId = game.Id, Game = game };
    private static MonsterStatSet BuildMonsterStatSet(int level, bool isElite, string attack, string health, string movement, List<EFFECT_TYPE> immunity, Monster monster, List<Effect> defenseEffects, List<Effect> attackEffects)
    {
        var statSet = BuildBaseMonsterStatSet(level, isElite, attack, health, movement, immunity, monster);
        if(defenseEffects.Count() > 0) statSet.DefenseEffects = BuildDefenseEffects(statSet, defenseEffects);
        if(attackEffects.Count() > 0)statSet.AttackEffects = BuildAttackEffects(statSet, attackEffects);
        return statSet;
    }
    private static MonsterStatSet BuildBaseMonsterStatSet(int level, bool isElite, string attack, string health, string movement, List<EFFECT_TYPE> immunity, Monster monster) => new MonsterStatSet() {Level = level, IsElite = isElite, Attack = attack, Movement = movement, Health = health, Immunity=immunity, Monster = monster, MonsterId = monster.Id};
    private static List<MonsterAttackEffect> BuildAttackEffects(MonsterStatSet stats, List<Effect> effects) => effects.Select(effect => new MonsterAttackEffect(){EffectId=effect.Id, Effect=effect, MonsterStatSetId=stats.Id, MonsterStatSet=stats}).ToList();
    private static List<MonsterDefenseEffect> BuildDefenseEffects(MonsterStatSet stats, List<Effect> effects) => effects.Select(effect => new MonsterDefenseEffect(){EffectId=effect.Id, Effect=effect, MonsterStatSetId=stats.Id, MonsterStatSet=stats}).ToList();

    #endregion

    #region Bosses

    private static void Jaws_BloodHorror(ContentContext context)
    {
        var game = GetGame(context, "jawsOfTheLion");
        if(MonsterExists(context, "blood_horror", game)) return;

        Monster monster = BuildMonster("blood_horror", "Blood Horror", "Boss: Blood Horror", game);

        List<EFFECT_TYPE> immunity = new List<EFFECT_TYPE>(){EFFECT_TYPE.disarm, EFFECT_TYPE.muddle, EFFECT_TYPE.immobilize, EFFECT_TYPE.stun};
        
        monster.BaseStats = new List<MonsterStatSet>() {
            BuildMonsterStatSet(0, false, "C - 1", "C * 7", "3", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(1, false, "C", "C * 10", "3", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(2, false, "C", "C * 12", "4", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(3, false, "C + 1", "C * 15", "4", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(4, false, "C + 1", "C * 17", "5", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(5, false, "C + 2", "C * 20", "5", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(6, false, "C + 3", "C * 23", "5", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(7, false, "C + 4", "C * 28", "5", immunity, monster, new List<Effect>(), new List<Effect>())
        };

        context.Monster.Add(monster);
    }

    private static void Jaws_BloodTumor(ContentContext context)
    {
        var game = GetGame(context, "jawsOfTheLion");
        if(MonsterExists(context, "blood_tumor", game)) return;

        Monster monster = BuildMonster("blood_tumor", "Blood Tumor", "Boss: Blood Tumor", game);
        List<EFFECT_TYPE> immunity = new List<EFFECT_TYPE>(){EFFECT_TYPE.disarm, EFFECT_TYPE.muddle, EFFECT_TYPE.immobilize, EFFECT_TYPE.stun, EFFECT_TYPE.curse};

        monster.BaseStats = new List<MonsterStatSet>() {
            BuildMonsterStatSet(0, false, "C - 1", "C + 7", "0", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(1, false, "C", "C * 10", "0", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(2, false, "C", "C * 12", "0", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(3, false, "C + 1", "C * 15", "0", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(4, false, "C + 1", "C * 17", "0", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(5, false, "C + 2", "C * 20", "0", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(6, false, "C + 2", "C * 23", "0", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(7, false, "C + 3", "C * 28", "0", immunity, monster, new List<Effect>(), new List<Effect>())
        };

        context.Monster.Add(monster);
    }

    private static void Jaws_FirstOfTheOrder(ContentContext context)
    {
        var game = GetGame(context, "jawsOfTheLion");
        if(MonsterExists(context, "first_of_the_order", game)) return;

        Monster monster = BuildMonster("first_of_the_order", "First Of The Order", "Boss: First Of The Order", game);
        List<EFFECT_TYPE> immunity = new List<EFFECT_TYPE>(){EFFECT_TYPE.disarm, EFFECT_TYPE.muddle, EFFECT_TYPE.immobilize, EFFECT_TYPE.stun, EFFECT_TYPE.curse};

        monster.BaseStats = new List<MonsterStatSet>() {
            BuildMonsterStatSet(0, false, "3", "C * 10", "2", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(1, false, "4", "C * 14", "2", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(2, false, "4", "C * 17", "2", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(3, false, "5", "C * 20", "2", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(4, false, "5", "C * 24", "3", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(5, false, "6", "C * 28", "3", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(6, false, "7", "C * 32", "3", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(7, false, "8", "C * 36", "3", immunity, monster, new List<Effect>(), new List<Effect>())
        };

        context.Monster.Add(monster);
    }



    #endregion

    #region Monsters

    private static void Jaws_VermlingRaider(ContentContext context)
    {
        var game = GetGame(context, "jawsOfTheLion");
        if(MonsterExists(context, "vermling_raider", game)) return;

        Monster monster = BuildMonster("vermling_raider", "Vermling Raider", "Monster: Vermling Raider", game);
        List<EFFECT_TYPE> immunity = new List<EFFECT_TYPE>();

        monster.BaseStats = new List<MonsterStatSet>() {
            BuildMonsterStatSet(0, false, "2", "4", "1", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(1, false, "2", "5", "1", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(2, false, "2", "9", "2", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(3, false, "2", "11", "3", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(4, false, "3", "12", "3", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(5, false, "3", "15", "3", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(6, false, "3", "17", "4", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(7, false, "3", "17", "4", immunity, monster, new List<Effect>(), new List<Effect>()),

            BuildMonsterStatSet(0, true, "2", "8", "1", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(1, true, "2", "10", "1", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(2, true, "3", "14", "3", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(3, true, "4", "16", "3", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(4, true, "4", "19", "4", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(5, true, "4", "23", "4", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(6, true, "5", "27", "4", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(7, true, "6", "31", "4", immunity, monster, new List<Effect>(), new List<Effect>())
        };
        
        context.Monster.Add(monster);
    }

    private static void Jaws_VermlingScout(ContentContext context)
    {
        var game = GetGame(context, "jawsOfTheLion");
        if(MonsterExists(context, "vermling_scout", game)) return;

        Monster monster = BuildMonster("vermling_scout", "Vermling Scout", "Monsster: Vermling Scout", game);
        List<EFFECT_TYPE> immunity = new List<EFFECT_TYPE>();

        monster.BaseStats = new List<MonsterStatSet>() {
            BuildMonsterStatSet(0, false, "1", "2", "3", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(1, false, "1", "3", "3", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(2, false, "2", "3", "3", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(3, false, "2", "5", "3", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(4, false, "3", "6", "3", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(5, false, "3", "8", "3", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(6, false, "3", "10", "4", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(7, false, "3", "13", "4", immunity, monster, new List<Effect>(), new List<Effect>()),

            BuildMonsterStatSet(0, true, "2", "4", "3", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(1, true, "2", "5", "3", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(2, true, "3", "5", "4", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(3, true, "3", "7", "4", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(4, true, "4", "8", "4", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(5, true, "4", "11", "4", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(6, true, "4", "13", "5", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(7, true, "4", "17", "5", immunity, monster, new List<Effect>(), new List<Effect>())
        };
        
        context.Monster.Add(monster);
    }

    private static void Jaws_GiantViper(ContentContext context)
    {
        var game = GetGame(context, "jawsOfTheLion");
        if(MonsterExists(context, "giant_viper", game)) return;

        Monster monster = BuildMonster("giant_viper", "Giant Viper", "Monster: Giant Viper", game);
        List<EFFECT_TYPE> immunity = new List<EFFECT_TYPE>();

        monster.BaseStats = new List<MonsterStatSet>() {
            BuildMonsterStatSet(0, false, "1", "2", "2", immunity, monster, new List<Effect>(), new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.poison, -1, -1)}),
            BuildMonsterStatSet(1, false, "1", "3", "2", immunity, monster, new List<Effect>(), new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.poison, -1, -1)}),
            BuildMonsterStatSet(2, false, "1", "4", "3", immunity, monster, new List<Effect>(), new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.poison, -1, -1)}),
            BuildMonsterStatSet(3, false, "2", "4", "3", immunity, monster, new List<Effect>(), new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.poison, -1, -1)}),
            BuildMonsterStatSet(4, false, "2", "6", "3", immunity, monster, new List<Effect>(), new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.poison, -1, -1)}),
            BuildMonsterStatSet(5, false, "3", "7", "3", immunity, monster, new List<Effect>(), new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.poison, -1, -1)}),
            BuildMonsterStatSet(6, false, "3", "8", "4", immunity, monster, new List<Effect>(), new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.poison, -1, -1)}),
            BuildMonsterStatSet(7, false, "3", "10", "4", immunity, monster, new List<Effect>(), new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.poison, -1, -1)}),

            BuildMonsterStatSet(0, true, "2", "3", "2", immunity, monster, new List<Effect>(), new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.poison, -1, -1)}),
            BuildMonsterStatSet(1, true, "2", "5", "2", immunity, monster, new List<Effect>(), new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.poison, -1, -1)}),
            BuildMonsterStatSet(2, true, "2", "7", "3", immunity, monster, new List<Effect>(), new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.poison, -1, -1)}),
            BuildMonsterStatSet(3, true, "3", "8", "3", immunity, monster, new List<Effect>(), new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.poison, -1, -1)}),
            BuildMonsterStatSet(4, true, "3", "11", "3", immunity, monster, new List<Effect>(), new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.poison, -1, -1)}),
            BuildMonsterStatSet(5, true, "3", "13", "4", immunity, monster, new List<Effect>(), new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.poison, -1, -1)}),
            BuildMonsterStatSet(6, true, "4", "14", "4", immunity, monster, new List<Effect>(), new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.poison, -1, -1)}),
            BuildMonsterStatSet(7, true, "4", "18", "4", immunity, monster, new List<Effect>(), new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.poison, -1, -1)})
        };
        
        context.Monster.Add(monster);
    }

    private static void Jaws_Zealot(ContentContext context)
    {
        var game = GetGame(context, "jawsOfTheLion");
        if(MonsterExists(context, "zealot", game)) return;

        Monster monster = BuildMonster("zealot", "Zealot", "Monster: Zealot", game);
        List<EFFECT_TYPE> immunity = new List<EFFECT_TYPE>();

        monster.BaseStats = new List<MonsterStatSet>() {
            BuildMonsterStatSet(0, false, "2", "4", "2", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(1, false, "2", "6", "2", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(2, false, "3", "7", "3", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(3, false, "3", "8", "3", immunity, monster, new List<Effect>(), new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.wound, -1, -1)}),
            BuildMonsterStatSet(4, false, "3", "10", "3", immunity, monster, new List<Effect>(), new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.wound, -1, -1)}),
            BuildMonsterStatSet(5, false, "3", "12", "4", immunity, monster, new List<Effect>(), new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.wound, -1, -1)}),
            BuildMonsterStatSet(6, false, "4", "14", "4", immunity, monster, new List<Effect>(), new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.wound, -1, -1)}),
            BuildMonsterStatSet(7, false, "5", "16", "4", immunity, monster, new List<Effect>(), new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.wound, -1, -1)}),

            BuildMonsterStatSet(0, true, "3", "7", "2", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(1, true, "3", "8", "2", immunity, monster, new List<Effect>(), new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.wound, -1, -1)}),
            BuildMonsterStatSet(2, true, "3", "11", "3", immunity, monster, new List<Effect>(), new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.wound, -1, -1)}),
            BuildMonsterStatSet(3, true, "4", "13", "3", immunity, monster, new List<Effect>(), new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.wound, -1, -1)}),
            BuildMonsterStatSet(4, true, "4", "17", "3", immunity, monster, new List<Effect>(), new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.wound, -1, -1)}),
            BuildMonsterStatSet(5, true, "5", "18", "4", immunity, monster, new List<Effect>(), new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.wound, -1, -1)}),
            BuildMonsterStatSet(6, true, "6", "22", "4", immunity, monster, new List<Effect>(), new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.wound, -1, -1)}),
            BuildMonsterStatSet(7, true, "7", "26", "4", immunity, monster, new List<Effect>(), new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.wound, -1, -1)})
        };
        
        context.Monster.Add(monster);
    }

    private static void Jaws_BlackSludge(ContentContext context)
    {
        var game = GetGame(context, "jawsOfTheLion");
        if(MonsterExists(context, "black_sludge", game)) return;

        // Add Monster
        List<EFFECT_TYPE> immunity = new List<EFFECT_TYPE>();

        Monster monster = BuildMonster("black_sludge", "Black Sludge", "Normal Monster", game);
        monster.BaseStats = new List<MonsterStatSet>() {
            BuildMonsterStatSet(0, false, "2", "4", "1", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(1, false, "2", "5", "1", immunity, monster, new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.shield, 1, -1)}, new List<Effect>()),
            BuildMonsterStatSet(2, false, "2", "7", "1", immunity, monster, new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.shield, 1, -1)}, new List<Effect>()),
            BuildMonsterStatSet(3, false, "3", "8", "1", immunity, monster, new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.shield, 1, -1)}, new List<Effect>()),
            BuildMonsterStatSet(4, false, "3", "9", "2", immunity, monster, new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.shield, 1, -1)}, new List<Effect>()),
            BuildMonsterStatSet(5, false, "3", "10", "2", immunity, monster, new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.shield, 1, -1)}, new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.poison, -1, -1)}),
            BuildMonsterStatSet(6, false, "4", "12", "2", immunity, monster, new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.shield, 1, -1)}, new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.poison, -1, -1)}),
            BuildMonsterStatSet(7, false, "4", "16", "2", immunity, monster, new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.shield, 1, -1)}, new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.poison, -1, -1)}),

            BuildMonsterStatSet(0, true, "2", "8", "1", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(1, true, "2", "9", "1", immunity, monster, new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.shield, 1, -1)}, new List<Effect>()),
            BuildMonsterStatSet(2, true, "2", "11", "1", immunity, monster, new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.shield, 1, -1)}, new List<Effect>()),
            BuildMonsterStatSet(3, true, "3", "11", "2", immunity, monster, new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.shield, 1, -1)}, new List<Effect>()),
            BuildMonsterStatSet(4, true, "4", "13", "2", immunity, monster, new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.shield, 1, -1)}, new List<Effect>()),
            BuildMonsterStatSet(5, true, "4", "15", "3", immunity, monster, new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.shield, 1, -1)}, new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.poison, -1, -1)}),
            BuildMonsterStatSet(6, true, "4", "16", "3", immunity, monster, new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.shield, 1, -1)}, new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.poison, -1, -1)}),
            BuildMonsterStatSet(7, true, "5", "18", "3", immunity, monster, new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.shield, 1, -1)}, new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.poison, -1, -1)})
        };
        
        context.Monster.Add(monster);
    }

    #endregion

    private static void PlaceHolderMonsterAdd(ContentContext context)
    {
        var game = GetGame(context, "game_content_code");
        if(MonsterExists(context, "content_code", game)) return;

        Monster monster = BuildMonster("content_code", "Name", "Description", game);
        List<EFFECT_TYPE> immunity = new List<EFFECT_TYPE>();

        monster.BaseStats = new List<MonsterStatSet>() {
            BuildMonsterStatSet(0, false, "0", "0", "0", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(1, false, "0", "0", "0", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(2, false, "0", "0", "0", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(3, false, "0", "0", "0", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(4, false, "0", "0", "0", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(5, false, "0", "0", "0", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(6, false, "0", "0", "0", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(7, false, "0", "0", "0", immunity, monster, new List<Effect>(), new List<Effect>()),

            BuildMonsterStatSet(0, true, "0", "0", "0", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(1, true, "0", "0", "0", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(2, true, "0", "0", "0", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(3, true, "0", "0", "0", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(4, true, "0", "0", "0", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(5, true, "0", "0", "0", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(6, true, "0", "0", "0", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(7, true, "0", "0", "0", immunity, monster, new List<Effect>(), new List<Effect>())
        };
        
        context.Monster.Add(monster);
    }

}