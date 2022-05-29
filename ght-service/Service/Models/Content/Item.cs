using System;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Service.Models.Content;

[Serializable]
public struct Item : ContentItem
{
    public Item(Guid id, string contentCode, string name, string description, string gameContentCode)
    {
        Id = id;
        ContentCode = contentCode;
        Name = name;
        Description = description;
        GameContentCode = gameContentCode;
    }

    [JsonIgnore]
    public Guid Id { get; }

    [JsonPropertyName("contentCode")]
    public string ContentCode { get; }
    
    [JsonPropertyName("Name")]
    public string Name { get; }
    
    [JsonPropertyName("Description")]
    public string Description { get; }

    [JsonPropertyName("game")]
    public string GameContentCode { get; }

    [JsonIgnore]
    public ContentSummary Summary
    {
        get
        {
            return new ContentSummary(
                ContentCode,
                Name,
                Description,
                Game: GameContentCode
            );
        }
    }
}