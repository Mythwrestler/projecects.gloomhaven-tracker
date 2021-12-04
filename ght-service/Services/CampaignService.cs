using System;
using System.Collections.Generic;
using GloomhavenTracker.Service.Models.Campaign;
using GloomhavenTracker.Service.Models.Content;
using GloomhavenTracker.Service.Repos;
using Microsoft.Extensions.Logging;

namespace GloomhavenTracker.Service.Services;

public interface CampaignService
{
    public List<CampaignSummary> GetCampaignList();
    public CampaignDTO GetCampaign(Guid campaignId);
    public CampaignSummary NewCampaign(string game, string Description, List<string> availableScenarios, List<string> closedScenarios, List<string> completedScenarios);
    public CharacterSummary AddCharacterToCampaign(Guid campaignId, CharacterDTO character);
    public List<CharacterDTO> GetCharactersForCampaign(Guid campaignId);
}

public class CampaignServiceImplementation : CampaignService
{
    private Dictionary<Guid, Campaign> campaigns = new Dictionary<Guid, Campaign>();
    private ContentService contentService;
    private ILogger<CampaignServiceImplementation> logger;
    private CampaignRepo repo;

    public CampaignServiceImplementation(CampaignRepo repo, ContentService contentService, ILogger<CampaignServiceImplementation> logger)
    {
        this.logger = logger;
        this.contentService = contentService;
        this.repo = repo;
    }

    private Campaign GetCampaignById(Guid campaignId)
    {
        if (!campaigns.ContainsKey(campaignId))
        {
            CampaignDO? campaignFromRepo = repo.GetCampaign(campaignId);
            if (campaignFromRepo == null)
            {
                throw new ArgumentException("Could not find campaign with that id.");
            }
            else
            {
                campaigns.Add(campaignId, new Campaign(campaignFromRepo ?? new CampaignDO()));
            }
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
        return repo.GetCampaignList();
    }

    public CampaignDTO GetCampaign(Guid campaignId)
    {
        return GetCampaignById(campaignId).ToDTO();
    }


    public CampaignSummary NewCampaign(string game, string Description, List<string> availableScenarios, List<string> closedScenarios, List<string> completedScenarios)
    {

        CampaignDO newCampaignDO = new CampaignDO()
        {
            Game = GameUtils.GameTypeString(GameUtils.GameType(game)),
            Description = Description,
            Id = Guid.NewGuid().ToString(),
            AvailableScenarios = availableScenarios,
            ClosedScenarios = closedScenarios,
            CompletedScenarios = completedScenarios,
            Party = new PartyDO() { Characters = new List<CharacterDO>() }
        };

        Campaign campaign = new Campaign(newCampaignDO);

        campaigns.Add(campaign.Id, campaign);

        repo.NewCampaign(campaign.ToDO());

        return campaign.Summary;
    }

    public CharacterSummary AddCharacterToCampaign(Guid campaignId, CharacterDTO newCharacter)
    {
        Campaign campaign = GetCampaignById(campaignId);

        if(!contentService.IsValidCharacterCode(campaign.Game, newCharacter.CharacterContentCode))
            throw new ArgumentException("Character type is not valid for game");

        if(campaign.Party.Characters.ContainsKey(newCharacter.CharacterContentCode))
            throw new ArgumentException("Character already in campaign");

        CharacterDO characterDO = new CharacterDO() {
            Id = Guid.NewGuid().ToString(),
            AppliedPerks = newCharacter.AppliedPerks,
            CharacterContentCode = newCharacter.CharacterContentCode,
            Experience = newCharacter.Experience,
            Gold = newCharacter.Gold,
            Items = newCharacter.Items,
            Name = newCharacter.Name,
            PerkPoints = newCharacter.PerkPoints
        };

        campaign.Party.AddCharacter(characterDO);

        repo.UpdateCampaign(campaign.ToDO());

        var characterSummary = campaign.Party.Characters.GetValueOrDefault(newCharacter.CharacterContentCode)?.Summary;
        if(characterSummary == null)
            throw new Exception("could not find character");
        return characterSummary ?? new CharacterSummary();
    }


    public List<CharacterDTO> GetCharactersForCampaign(Guid campaignId)
    {
        Campaign campaign = GetCampaignById(campaignId);

        return campaign.Party.ToDTO().Characters;
    }

}