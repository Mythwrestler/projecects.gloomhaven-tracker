using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;


namespace GloomhavenTracker.Service.Models.Content;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PERK_ACTION
{
    [EnumMember(Value = "remove")]
    remove,
    
    [EnumMember(Value = "add")]
    add
}

[Serializable]
public struct PerkAction
{
    public PerkAction(PERK_ACTION action, int count, AttackModifier attackModifier)
    {
        Action = action;
        Count = count;
        AttackModifier = attackModifier;
    }

    [JsonPropertyName("action")]
    public PERK_ACTION Action { get; }

    [JsonPropertyName("count")]
    public int Count { get; }

    [JsonPropertyName("attackModifier")]
    public AttackModifier AttackModifier { get; }
}

[Serializable]
public struct Perk : ContentItem
{
    public Perk(Guid id, string contentCode, string name, string description, List<PerkAction> actions)
    {
        Id = id;
        ContentCode = contentCode;
        Name = name;
        Description = description;
        Actions = actions;
    }

    [JsonIgnore]
    public Guid Id { get; }

    [JsonPropertyName("contentCode")]
    public string ContentCode { get; }
    
    [JsonPropertyName("Name")]
    public string Name { get; }
    
    [JsonPropertyName("description")]
    public string Description { get; }

    [JsonPropertyName("actions")]
    public List<PerkAction> Actions { get; }

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