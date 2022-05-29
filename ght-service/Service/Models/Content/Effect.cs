using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Service.Models.Content;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum EFFECT_TYPE
{
    [EnumMember(Value = "strength")]
    strength,

    [EnumMember(Value = "poison")]
    poison,

    [EnumMember(Value = "wound")]
    wound,

    [EnumMember(Value = "stun")]
    stun,

    [EnumMember(Value = "shield")]
    shield,

    [EnumMember(Value = "disarm")]
    disarm,

    [EnumMember(Value = "muddle")]
    muddle,

    [EnumMember(Value = "immobilize")]
    immobilize,

    [EnumMember(Value = "curse")]
    curse,

    [EnumMember(Value = "disadvantage")]
    disadvantage,
    
    [EnumMember(Value = "advantage")]
    advantage,
    
    [EnumMember(Value = "damage")]
    damage,
    
    [EnumMember(Value = "healAlly")]
    healAlly,
    
    [EnumMember(Value = "chargeElement")]
    chargeElement,
    
    [EnumMember(Value = "spendElement")]
    spendElement,
    
    [EnumMember(Value = "push")]
    push
}


[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ELEMENT
{
    [EnumMember(Value = "fire")]
    fire,
    
    [EnumMember(Value = "ice")]
    ice,
    
    [EnumMember(Value = "air")]
    air,
    
    [EnumMember(Value = "earth")]
    earth,
    
    [EnumMember(Value = "light")]
    light,
    
    [EnumMember(Value = "dark")]
    dark
}

[Serializable]
public struct Effect
{
    public Effect(Guid id, EFFECT_TYPE type, int? value, int? duration, int? range, ELEMENT? element)
    {
        Id = id;
        Type = type;
        Value = value;
        Duration = duration;
        Range = range;
        Element = element;
    }

    [JsonIgnore]
    public Guid Id { get; }

    [JsonPropertyName("type")]
    public EFFECT_TYPE Type { get; }

    [JsonPropertyName("value")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Value { get; }

    [JsonPropertyName("duration")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Duration { get; }

    [JsonPropertyName("range")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Range { get; }

    [JsonPropertyName("element")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public ELEMENT? Element { get; }
}