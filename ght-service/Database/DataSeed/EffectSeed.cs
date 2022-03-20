using GloomhavenTracker.Database.Models.Content;

namespace GloomhavenTracker.Database.DataSeed;

public static partial class ContentSeedData
{
    private static Effect CheckAddEffect(ContentContext context, EFFECT_TYPE type, int value, int duration)
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