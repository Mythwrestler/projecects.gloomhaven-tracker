using GloomhavenTracker.Database.Models.Content;

namespace GloomhavenTracker.Database.DataSeed;

public static class ContentData
{
    public static async void Seed(ContentContextImplementation context)
    {
        using (context)
        {
            context.Database.EnsureCreated();
            var monsters = GetMonsterSeed();
            await context.Monster.AddRangeAsync(monsters);
            context.SaveChanges();
        }
    }

    private static List<Monster> GetMonsterSeed()
    {
        List<MonsterEffect> monsterEffects = new List<MonsterEffect>();

        List<Monster> seedData = new List<Monster>();

        var testMonster = BuildMonster("e36e568f-4ec1-450d-9fc0-217a758a2407", "test", "test", "test");
        testMonster.BaseStats.Add(
            BuildStatSet("50c37515-e696-4565-b557-decdcdbdc8b1", 1, false, "1", "1", "1", testMonster,
                new List<MonsterEffect>() { BuildMonsterEffect("4220e32a-ba6a-4635-9ce1-1851d6d724e2", EFFECT_TYPE.wound, 1, 0) },
                new List<MonsterEffect>() { BuildMonsterEffect("9cd18dd6-c662-4320-96eb-d1e2dab6e9ca", EFFECT_TYPE.shield, 1, 0) }
        ));
        seedData.Add(testMonster);

        return seedData;
    }

    private static Monster BuildMonster(string id, string code, string name, string description) => new Monster() { Id = new Guid(id), ContentCode = code, Name = name, Description = description };
    private static MonsterStatSet BuildStatSet(string id, int level, bool isElite, string attack, string movement, string health, Monster monster, List<MonsterEffect> attackEffects, List<MonsterEffect> defenseEffects) => new MonsterStatSet() { Id = new Guid(id), Level = level, IsElite = isElite, Attack = attack, Movement = movement, Health = health, Monster = monster, MonsterId = monster.Id, AttackEffects = attackEffects, DefenseEffects = defenseEffects };
    private static MonsterEffect BuildMonsterEffect(string id, EFFECT_TYPE type, int value, int duration) => new MonsterEffect() { Id = new Guid(id), Type = type, Value = value, Duration = duration };


    private static List<Effect> GetEffectSeed()
    {
        List<Effect> seedData = new List<Effect>();

        seedData.Add(new Effect() { Id = new Guid("d186a6c4-90f6-41c7-ba3b-eef99b43982e"), Type = EFFECT_TYPE.stun, Duration = 0, Value = 0 });

        return seedData;
    }
    private static Effect BuildEffect(string id, EFFECT_TYPE type, int value, int duration) => new Effect() { Id = new Guid(id), Type = type, Value = value, Duration = duration };
}
