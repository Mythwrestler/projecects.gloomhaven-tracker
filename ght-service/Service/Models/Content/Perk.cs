using System;
using System.Text.Json.Serialization;

[Serializable]
public class Perk
{   
    [JsonPropertyName("contentCode")]
    public string ContentCode { get; set; } = string.Empty;
    
    [JsonPropertyName("Name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonPropertyName("Description")]
    public string Description { get; set; } = string.Empty;
}