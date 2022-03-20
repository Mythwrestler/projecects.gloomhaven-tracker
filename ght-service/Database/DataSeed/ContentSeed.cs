using GloomhavenTracker.Database.Models.Content;

namespace GloomhavenTracker.Database.DataSeed;

public static partial class ContentSeedData
{
    public static void Seed(ContentContextImplementation context)
    {
        using (context)
        {
            context.Database.EnsureCreated();
            SeedMonsters(context);
            context.SaveChanges();
        }
    }








    
    private static List<Effect> GetEffectSeed()
    {
        List<Effect> seedData = new List<Effect>();

        seedData.Add(new Effect() { Id = new Guid("d186a6c4-90f6-41c7-ba3b-eef99b43982e"), Type = EFFECT_TYPE.stun, Duration = 0, Value = 0 });

        return seedData;
    }
    
    // private static MonsterDefenseEffect BuildMonsterDefenseEffect(MonsterStatSet stats, string id, EFFECT_TYPE type, int value, int duration) => new MonsterDefenseEffect() {};
    // private static MonsterAttackEffect BuildMonsterAttackEffect(MonsterStatSet stats, string id, EFFECT_TYPE type, int value, int duration) => new Effect() { Id = new Guid(id), Type = type, Value = value, Duration = duration };
    
    private static Effect CheckAddEffect(ContentContextImplementation context, EFFECT_TYPE type, int value, int duration)
    {
        var effect = context.Effect.Local.FirstOrDefault(e => e.Type == type && e.Value == value && e.Duration == duration);
        if(effect is null) 
            effect = context.Effect.FirstOrDefault(e => e.Type == type && e.Value == value && e.Duration == duration);
        if(effect is null){
            effect = BuildEffect(type, value, duration);
            context.Effect.Add(effect);
        }
        return effect;
    }
    private static Effect BuildEffect(EFFECT_TYPE type, int value, int duration) => new Effect() {Type = type, Value = value, Duration = duration };





}
