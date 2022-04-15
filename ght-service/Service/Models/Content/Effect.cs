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
    spendElement
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
public class Effect
{

    [JsonPropertyName("type")]
    public EFFECT_TYPE Type { get; set; }

    [JsonPropertyName("value")]
    public int? Value { get; set; }

    [JsonPropertyName("duration")]
    public int? Duration { get; set; }

    public int? Range { get; set; }

    public ELEMENT? Element { get; set; }
}