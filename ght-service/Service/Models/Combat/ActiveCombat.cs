using System;
using System.Collections.Generic;
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


[Serializable]
public class ParticipantsDTO
{
    [JsonPropertyName("combatId")]
    public Guid CombatId { get; set; }
    
    [JsonPropertyName("participants")]
    public List<ParticipantDTO> Participants { get; set; } = new List<ParticipantDTO>();
}

[Serializable]

public class ParticipantDTO
{
    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("isObserver")]
    public bool IsObserver { get; set; } = false;

    [JsonPropertyName("characterCodes")]
    public List<string> CharacterCodes { get; set; } = new List<string>();
}