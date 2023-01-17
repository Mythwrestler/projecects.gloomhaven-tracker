using System;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Service.Models.Combat;

[Serializable]
public class CombatRequestDTO
{
    [JsonPropertyName("combatId")]
    public Guid CombatId { get; set; }

    [JsonPropertyName("combatantId")]
    public string? CombatantId { get; set; }

    [JsonPropertyName("isObserver")]
    public bool IsObserver { get; set; }
}