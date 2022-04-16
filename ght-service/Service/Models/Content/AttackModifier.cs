using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Service.Models.Content;


[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ATTACK_MODIFIER_TYPE
{
    [EnumMember(Value = "add")]
    Add,

    [EnumMember(Value = "multiply")]
    Multiply,

    [EnumMember(Value = "cancel")]
    Cancel,
}

[Serializable]
public struct AttackModifier : ContentItem
{
    public AttackModifier(Guid id, string contentCode, string name, string description, bool isCurse, bool isBlessing, bool triggerShuffle, string value, List<Effect> effects)
    {
        Id = id;
        ContentCode = contentCode;
        Name = name;
        Description = description;
        IsCurse = isCurse;
        IsBlessing = isBlessing;
        TriggerShuffle = triggerShuffle;
        Value = value;
        Effects = effects;
    }

    [JsonIgnore]
    public Guid Id { get; }

    [JsonPropertyName("contentCode")]
    public string ContentCode { get; }

    [JsonPropertyName("name")]
    public string Name { get; }

    [JsonPropertyName("description")]
    public string Description { get; }

    [JsonPropertyName("isCurse")]
    public bool IsCurse { get; }

    [JsonPropertyName("isBlessing")]
    public bool IsBlessing { get; }

    [JsonPropertyName("triggerShuffle")]
    public bool TriggerShuffle { get; }

    [JsonPropertyName("value")]
    public string Value { get; }

    [JsonPropertyName("effects")]
    public List<Effect> Effects { get; }

    [JsonIgnore]
    public ContentSummary Summary
    {
        get
        {
            return new ContentSummary(
                ContentCode,
                Name,
                Description
            );
        }
    }
}