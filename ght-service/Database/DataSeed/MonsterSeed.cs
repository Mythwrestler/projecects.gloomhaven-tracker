using GloomhavenTracker.Database.Models.Content;

namespace GloomhavenTracker.Database.DataSeed;

public static partial class ContentSeedData
{

    private static void SeedMonsters(ContentContextImplementation context)
    {
        //Bosses
        Boss_BloodHorror(context);

        //Monsters
        Monster_BlackSludge(context);

    }

    private static Monster BuildMonster(string code, string name, string description) => new Monster() {ContentCode = code, Name = name, Description = description };
    private static List<MonsterAttackEffect> BuildAttackEffects(MonsterStatSet stats, List<Effect> effects) => effects.Select(effect => new MonsterAttackEffect(){EffectId=effect.Id, Effect=effect, MonsterStatSetId=stats.Id, MonsterStatSet=stats}).ToList();
    private static List<MonsterDefenseEffect> BuildDefenseEffects(MonsterStatSet stats, List<Effect> effects) => effects.Select(effect => new MonsterDefenseEffect(){EffectId=effect.Id, Effect=effect, MonsterStatSetId=stats.Id, MonsterStatSet=stats}).ToList();


    private static void Boss_BloodHorror(ContentContextImplementation context)
    {
        var check = context.Monster.FirstOrDefault(monster => monster.ContentCode == "blood_horror");
        if(check is not null) return;

        Monster monster = BuildMonster("blood_horror", "Blood Horror", "Monstrous Boss");

        List<EFFECT_TYPE> immunity = new List<EFFECT_TYPE>(){EFFECT_TYPE.disarm, EFFECT_TYPE.muddle, EFFECT_TYPE.immobilize, EFFECT_TYPE.stun};
        
        MonsterStatSet statLvlS0 = BuildBaseMonsterStatSet(0, false, "C - 1", "C * 7", "3", immunity, monster);
        monster.BaseStats.Add(statLvlS0);
        MonsterStatSet statLvlS1 = BuildBaseMonsterStatSet(1, false, "C", "C * 10", "3", immunity, monster);
        monster.BaseStats.Add(statLvlS1);
        MonsterStatSet statLvlS2 = BuildBaseMonsterStatSet(2, false, "C", "C * 12", "4", immunity, monster);
        monster.BaseStats.Add(statLvlS2);
        MonsterStatSet statLvlS3 = BuildBaseMonsterStatSet(3, false, "C + 1", "C * 15", "4", immunity, monster);
        monster.BaseStats.Add(statLvlS3);
        MonsterStatSet statLvlS4 = BuildBaseMonsterStatSet(4, false, "C + 1", "C * 17", "5", immunity, monster);
        monster.BaseStats.Add(statLvlS4);
        MonsterStatSet statLvlS5 = BuildBaseMonsterStatSet(5, false, "C + 2", "C * 20", "5", immunity, monster);
        monster.BaseStats.Add(statLvlS5);
        MonsterStatSet statLvlS6 = BuildBaseMonsterStatSet(6, false, "C + 3", "C * 23", "5", immunity, monster);
        monster.BaseStats.Add(statLvlS6);
        MonsterStatSet statLvlS7 = BuildBaseMonsterStatSet(7, false, "C + 4", "C * 28", "5", immunity, monster);
        monster.BaseStats.Add(statLvlS7);

        context.Monster.Add(monster);

    }


    private static MonsterStatSet BuildMonsterStatSet(int level, bool isElite, string attack, string health, string movement, List<EFFECT_TYPE> immunity, Monster monster, List<Effect> defenseEffects, List<Effect> attackEffects)
    {
        var statSet = BuildBaseMonsterStatSet(level, isElite, attack, health, movement, immunity, monster);
        if(defenseEffects.Count() > 0) statSet.DefenseEffects = BuildDefenseEffects(statSet, defenseEffects);
        if(attackEffects.Count() > 0)statSet.AttackEffects = BuildAttackEffects(statSet, attackEffects);
        return statSet;
    }
    private static MonsterStatSet BuildBaseMonsterStatSet(int level, bool isElite, string attack, string health, string movement, List<EFFECT_TYPE> immunity, Monster monster) => new MonsterStatSet() {Level = level, IsElite = isElite, Attack = attack, Movement = movement, Health = health, Immunity=immunity, Monster = monster, MonsterId = monster.Id};

    private static void Monster_BlackSludge(ContentContextImplementation context)
    {
        var check = context.Monster.FirstOrDefault(monster => monster.ContentCode == "black_sludge");
        if(check is not null) return;

        // Add Monster
        List<EFFECT_TYPE> immunity = new List<EFFECT_TYPE>();

        Monster monster = BuildMonster("black_sludge", "Black Sludge", "Normal Monster");
        monster.BaseStats = new List<MonsterStatSet>() {
            BuildMonsterStatSet(0, false, "2", "4", "1", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(1, false, "2", "5", "1", immunity, monster, new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.shield, 1, -1)}, new List<Effect>()),
            BuildMonsterStatSet(2, false, "2", "7", "1", immunity, monster, new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.shield, 1, -1)}, new List<Effect>()),
            BuildMonsterStatSet(3, false, "3", "8", "1", immunity, monster, new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.shield, 1, -1)}, new List<Effect>()),
            BuildMonsterStatSet(4, false, "3", "9", "2", immunity, monster, new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.shield, 1, -1)}, new List<Effect>()),
            BuildMonsterStatSet(5, false, "3", "10", "2", immunity, monster, new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.shield, 1, -1)}, new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.poison, -1, -1)}),
            BuildMonsterStatSet(6, false, "4", "12", "2", immunity, monster, new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.shield, 1, -1)}, new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.poison, -1, -1)}),
            BuildMonsterStatSet(7, false, "4", "16", "2", immunity, monster, new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.shield, 1, -1)}, new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.poison, -1, -1)}),

            BuildMonsterStatSet(0, true, "0", "0", "0", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(0, true, "0", "0", "0", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(0, true, "0", "0", "0", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(0, true, "0", "0", "0", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(0, true, "0", "0", "0", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(0, true, "0", "0", "0", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(0, true, "0", "0", "0", immunity, monster, new List<Effect>(), new List<Effect>()),
            BuildMonsterStatSet(0, true, "0", "0", "0", immunity, monster, new List<Effect>(), new List<Effect>())
        };
        
        context.Monster.Add(monster);
    }




    private static void PlaceHolderMonsterAdd(ContentContextImplementation context)
    {

        var check = context.Monster.FirstOrDefault(monster => monster.ContentCode == "content_code");
        if(check is not null) return;
        
        Monster monster = BuildMonster("content_code", "Name", "Description");
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
    }


}