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
}


[Serializable]
public class Effect
{

    [JsonPropertyName("type")]
    public EFFECT_TYPE Type { get; set; }

    [JsonPropertyName("value")]
    public int Value { get; set; } = -1;

    [JsonPropertyName("duration")]
    public int Duration { get; set; } = -1;
}