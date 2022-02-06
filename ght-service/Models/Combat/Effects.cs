

using System.Collections.Generic;
using System.Linq;
using GloomhavenTracker.Service.Models.Content;

namespace GloomhavenTracker.Service.Models.Combat;


public class Effects
{
    private readonly Dictionary<EFFECT_TYPE, Effect> effects = new Dictionary<EFFECT_TYPE, Effect>();
    public Effects(List<Effect> effects)
    {
        this.effects = effects.ToDictionary(effect => effect.Type, effect => effect);
    }
    public Effects() { }

    public List<Effect> ActiveEffects => effects.Select(a => a.Value).ToList();

    public List<Effect> ReduceEffectDurations(int roundCount = 1)
    {
        effects.Where(a => a.Value.Duration > 0).Select(a => a.Value).ToList().ForEach(effect =>
        {
            effect.Duration -= roundCount;
            if (effect.Duration < 0)
                effects.Remove(effect.Type);
        });

        return ActiveEffects;

    }

    public List<Effect> ApplyEffects(List<Effect> effectsToApply)
    {
        effectsToApply.ForEach(ne =>
        {
            var effectToUpdate = effects.GetValueOrDefault(ne.Type);
            if (effectToUpdate != null)
            {
                effectToUpdate.Duration = ne.Duration;
                effectToUpdate.Value = ne.Value;
            }
            else
            {
                effects.Add(ne.Type, ne);
            }
        });

        return ActiveEffects;
    }

    public List<Effect> RemoveEffect(EFFECT_TYPE type)
    {
        effects.Remove(type);
        return ActiveEffects;
    }

    public Effect? HasEffect(EFFECT_TYPE type)
    {
        return effects.GetValueOrDefault(type);
    }

    public void ClearAll()
    {
        effects.Clear();
    }

    public List<Effect> DataObject
    {
        get
        {
            return effects.Values.ToList();
        }
    }

    public List<Effect> DataTransferObject
    {
        get
        {
            return this.DataObject;
        }
    }

}