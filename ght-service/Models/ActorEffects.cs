
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Service.Models;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ACTOR_EFFECT_TYPE
{
    strength,
    poison,
    wound,
    stun,
    shield,
}


[Serializable]
public class ActorEffect
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ACTOR_EFFECT_TYPE Type { get; set; }

    public int Value { get; set; } = -1;

    public int Duration { get; set; } = -1;
}


public class ActorEffects
{
    private readonly Dictionary<ACTOR_EFFECT_TYPE, ActorEffect> _effects = new Dictionary<ACTOR_EFFECT_TYPE, ActorEffect>();
    public ActorEffects(List<ActorEffect> initial)
    {
        initial.ForEach(a => _effects.TryAdd(a.Type, a));
    }
    public ActorEffects() {}

    public List<ActorEffect> ActiveEffects => _effects.Select(a => a.Value).ToList();

    public List<ActorEffect> ReduceEffectDurations(int roundCount = 1)
    {
        _effects.Where(a => a.Value.Duration > 0).Select(a => a.Value).ToList().ForEach(effect =>
        {
            effect.Duration -= roundCount;
            if (effect.Duration < 0)
                _effects.Remove(effect.Type);
        });

        return ActiveEffects;

    }

    public List<ActorEffect> ApplyEffects(List<ActorEffect> effectsToApply)
    {
        effectsToApply.ForEach(ne =>
        {
            var effectToUpdate = _effects.GetValueOrDefault(ne.Type);
            if (effectToUpdate != null)
            {
                effectToUpdate.Duration = ne.Duration;
                effectToUpdate.Value = ne.Value;
            }
            else
            {
                _effects.Add(ne.Type, ne);
            }
        });

        return ActiveEffects;
    }

    public List<ActorEffect> RemoveEffect(ACTOR_EFFECT_TYPE type)
    {
        _effects.Remove(type);
        return ActiveEffects;
    }

    public ActorEffect? HasEffect(ACTOR_EFFECT_TYPE type)
    {
        return _effects.GetValueOrDefault(type);
    }

    public void ClearAll()
    {
        _effects.Clear();
    }



}