using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Service.Models.Campaign;

public class Campaign
{
    public Guid Id {get;}
    public string Description {get;}
    public List<Guid> CompletedScenarios {get;}
    public List<Guid> AvailableScenarios {get;}
    public Party Party {get;}



    public CampaignSummary Summary => new CampaignSummary()
    {
        Id = this.Id.ToString(),
        Description = this.Description
    };

    public Campaign(CampaignDO campaign)
    {
        this.Id = new Guid(campaign.Id);
        this.Description = campaign.Description;
        this.CompletedScenarios = campaign.CompletedScenarios.Select(scenarioId => new Guid(scenarioId)).ToList();
        this.AvailableScenarios = campaign.AvailableScenarios.Select(scenarioId => new Guid(scenarioId)).ToList();
        this.Party = new Party(campaign.Party);
    }

    public CampaignDO ToDO()
    {
        return new CampaignDO()
        {
            Id = this.Id.ToString(),
            Description = this.Description,
            CompletedScenarios = this.CompletedScenarios.Select(scenarioId => scenarioId.ToString()).ToList(),
            AvailableScenarios = this.AvailableScenarios.Select(scenarioId => scenarioId.ToString()).ToList(),
            Party = this.Party.ToDO()
        };
    }

}

[Serializable]
public struct CampaignDO
{
    [JsonPropertyName("id")]
    public string Id {get; set;}

    [JsonPropertyName("description")]
    public string Description {get; set;}

    [JsonPropertyName("completedScenarios")]
    public List<string> CompletedScenarios {get; set;}

    [JsonPropertyName("availableScenarios")]
    public List<string> AvailableScenarios {get; set;}

    [JsonPropertyName("party")]
    public PartyDO Party {get; set;}
}


[Serializable]
public struct CampaignSummary
{
    [JsonPropertyName("id")]
    public string Id {get; set;}

    [JsonPropertyName("description")]
    public string Description {get; set;}
}