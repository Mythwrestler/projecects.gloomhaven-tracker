using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Service.Models.Campaign;

// public class Scenarios {
//     private Dictionary<string, Scenario> ScenarioDictionary;

//     public Scenarios(List<ScenarioDO> scenarios) {
//         this.ScenarioDictionary = scenarios.ToDictionary(scn => scn.ContentCode, scn => new Scenario(scn));
//     }
    
//     public Scenarios(List<ScenarioDTO> scenarios) {
//         this.ScenarioDictionary = scenarios.ToDictionary(scn => scn.ContentCode, scn => new Scenario(scn));
//     }

//     public Scenario AddScenario(Scenario newScenario)
//     {
//         if(ScenarioDictionary.ContainsKey(newScenario.ContentCode)) throw new ArgumentException("Scenario Already In Campaign");
//         ScenarioDictionary.Add(newScenario.ContentCode, newScenario);
//         return ScenarioDictionary[newScenario.ContentCode];
//     }

//     public Scenario UpdateScenario(Scenario updateScenario)
//     {
//         if(!ScenarioDictionary.ContainsKey(updateScenario.ContentCode)) throw new ArgumentException("Scenario Not In Campaign");
//         ScenarioDictionary[updateScenario.ContentCode] = updateScenario;
//         return ScenarioDictionary[updateScenario.ContentCode];
//     }

//     public List<ScenarioDTO> ToDTO()
//     {
//         return this.ScenarioDictionary.Select(kvp => kvp.Value.ToDTO()).ToList();
//     }

//     public List<ScenarioDO> ToDO()
//     {
//         return this.ScenarioDictionary.Select(kvp => kvp.Value.ToDO()).ToList();
//     }
// }


public class Scenario {
    public Scenario(Guid id, Content.Scenario contentScenario, bool isClosed, bool isCompleted)
    {
        Id = id;
        ContentScenario = contentScenario;
        IsClosed = isClosed;
        IsCompleted = isCompleted;
    }
    public Guid Id { get; }
    public Content.Scenario ContentScenario { get; }
    public bool IsClosed {get;}
    public bool IsCompleted {get;}
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

public struct ScenarioUpdateRequestBody
{
    public string ScenarioContentCode { get; set; }
    public bool? isClosed { get; set; }
    public bool? isCompleted { get; set; }
}