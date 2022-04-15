using System;
using System.Collections.Generic;
using System.Linq;
using GloomhavenTracker.Service.Models.Campaign;
using GloomhavenTracker.Service.Models.Content;
using GloomhavenTracker.Service.Repos;
using Microsoft.Extensions.Logging;

namespace GloomhavenTracker.Service.Services;

public interface CampaignService
{
    public List<CampaignSummary> GetCampaignList();
    public Campaign GetCampaignById(Guid campaignId);
    public CampaignDTO GetCampaignDTO(Guid campaignId);
    public CampaignDTO NewCampaign(Guid id, string game, string Description);
    public void UpdateCampaign(Guid campaignId, CampaignDTO campaign);

}

public class CampaignServiceImplementation : CampaignService
{
    private Dictionary<Guid, Campaign> campaigns = new Dictionary<Guid, Campaign>();
    private ContentService contentService;
    private ILogger<CampaignServiceImplementation> logger;
    // private CampaignRepo repo;

    public CampaignServiceImplementation(ContentService contentService, ILogger<CampaignServiceImplementation> logger)
    {
        this.logger = logger;
        this.contentService = contentService;
        // this.repo = repo;
    }

    public Campaign GetCampaignById(Guid campaignId)
    {
        if (!campaigns.ContainsKey(campaignId))
        {
            // CampaignDO? campaignFromRepo = repo.GetCampaign(campaignId);
            // if (campaignFromRepo == null)
            // {
            //     throw new ArgumentException("Could not find campaign with that id.");
            // }
            // else
            // {
            //     campaigns.Add(campaignId, new Campaign(campaignFromRepo ?? new CampaignDO()));
            // }
        }

        var campaign = campaigns.GetValueOrDefault(campaignId);
        if (campaign != null)
        {
            return campaign;
        }
        throw new ArgumentException("Could not find campaign with that id.");
    }


    public List<CampaignSummary> GetCampaignList()
    {
        throw new NotImplementedException();
        // return repo.GetCampaignList();
    }

    public CampaignDTO GetCampaignDTO(Guid campaignId)
    {
        throw new NotImplementedException();
        // return GetCampaignById(campaignId).ToDTO();
    }


    public CampaignDTO NewCampaign(Guid id, string game, string Description)
    {
        throw new NotImplementedException();
        // CampaignDO newCampaignDO = new CampaignDO()
        // {
        //     Game = GameUtils.GameTypeString(GameUtils.GameType(game)),
        //     Description = Description,
        //     Id = id.ToString(),
        //     Scenarios = new List<ScenarioDO>(),
        //     Party = new List<CharacterDO>()
        // };

        // Campaign campaign = new Campaign(newCampaignDO);

        // campaigns.Add(campaign.Id, campaign);

        // repo.NewCampaign(campaign.ToDO());

        // return campaign.ToDTO();
    }

    public void UpdateCampaign(Guid campaignId, CampaignDTO campaign)
    {
        throw new NotImplementedException();
        // ValidateCampaign(campaignId, campaign);
        // ValidateCampaignCharacters(campaign);
        // CampaignDO updatedCampaignDO = new CampaignDO()
        // {
        //     Game = GameUtils.GameTypeString(GameUtils.GameType(campaign.Game)),
        //     Description = campaign.Description,
        //     Id = campaign.Id,
        //     Scenarios = campaign.Scenarios.Select(s => new ScenarioDO() {
        //                 ContentCode = s.ContentCode,
        //                 IsClosed = s.IsClosed,
        //                 IsCompleted = s.IsCompleted,
        //             }).ToList()
        //     ,
        //     Party = campaign.Party.Select(c => new CharacterDO() {
        //                 Id = c.Id ?? Guid.NewGuid().ToString(),
        //                 CharacterContentCode = c.CharacterContentCode,
        //                 AppliedPerks = c.AppliedPerks,
        //                 Experience = c.Experience,
        //                 Gold = c.Gold,
        //                 Items = c.Items,
        //                 Name = c.Name,
        //                 PerkPoints = c.PerkPoints
        //             }).ToList()
        // };

        // var updatedCampaign = new Campaign(updatedCampaignDO);
        // campaigns[campaignId] = updatedCampaign;

        // repo.UpdateCampaign(updatedCampaignDO);

    }

    public void ValidateCampaign(Guid campaignId, CampaignDTO campaign)
    {
        if(campaign.Id != campaignId.ToString()) throw new ArgumentException("Campaign Id Mismatch");
        var campaignToReplace = GetCampaignById(campaignId);
    }

    public void ValidateCampaignCharacters(CampaignDTO campaign)
    {
        var gameCode = GameUtils.GameType(campaign.Game);
        campaign.Party.ForEach(character => {
            if(!contentService.IsValidCharacterCode(gameCode, character.CharacterContentCode))
                throw new ArgumentException("Character type is not valid for game");
        });

        if(campaign.Party.Select(c => c.CharacterContentCode).Distinct().Count() != campaign.Party.Count())
            throw new ArgumentException("Character appears more than once in a campaign");
    }

    public void ValidateCampaignScenarios(CampaignDTO campaign)
    {
        var gameCode = GameUtils.GameType(campaign.Game);
        campaign.Scenarios.ForEach(scenario => {
            if(!contentService.IsValidScenarioCode(gameCode, scenario.ContentCode))
                throw new ArgumentException("Scenario type is not valid for game");
        });

        if(campaign.Scenarios.Select(c => c.ContentCode).Distinct().Count() != campaign.Scenarios.Count())
            throw new ArgumentException("Scenario appears more than once in a campaign");
    }
}