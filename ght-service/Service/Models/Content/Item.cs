using System;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Service.Models.Content;

[Serializable]
public struct Item : ContentItem
{
    public Item(Guid id, string contentCode, string name, string description)
    {
        Id = id;
        ContentCode = contentCode;
        Name = name;
        Description = description;
    }

    [JsonIgnore]
    public Guid Id { get; }

    [JsonPropertyName("contentCode")]
    public string ContentCode { get; }
    
    [JsonPropertyName("Name")]
    public string Name { get; }
    
    [JsonPropertyName("Description")]
    public string Description { get; }

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