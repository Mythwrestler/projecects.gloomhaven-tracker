using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Service.Models.Campaign;

public class Scenario {
    public Scenario(Guid id, Content.Scenario contentScenario, bool isClosed, bool isCompleted)
    {
        Id = id;
        ContentScenario = contentScenario;
        IsClosed = isClosed;
        IsCompleted = isCompleted;
    }
    public Guid Id { get; set; }
    public Content.Scenario ContentScenario { get; set; }
    public bool IsClosed { get; set; }
    public bool IsCompleted { get; set; }
}

[Serializable]
public struct ScenarioDTO
{
    public ScenarioDTO(string scenarioContentCode, int scenarioNumber, bool isClosed, bool isCompleted)
    {
        ScenarioContentCode = scenarioContentCode;
        ScenarioNumber = scenarioNumber;
        IsClosed = isClosed;
        IsCompleted = isCompleted;
    }

    [JsonPropertyName("scenarioContentCode")]
    public string ScenarioContentCode { get; }

    [JsonPropertyName("scenarioNumber")]
    public int ScenarioNumber { get; }
    
    [JsonPropertyName("isClosed")]
    public bool IsClosed { get; }

    [JsonPropertyName("isCompleted")]
    public bool IsCompleted { get; }
}

public struct ScenarioRequestBody
{
    public string ScenarioContentCode { get; set; }
    public bool? isClosed { get; set; }
    public bool? isCompleted { get; set; }
}