using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GloomhavenTracker.Service.Models.Campaign;
using GloomhavenTracker.Service.Models.Content;
using GloomhavenTracker.Service.Repos;
using Microsoft.Extensions.Logging;

namespace GloomhavenTracker.Service.Services;

public interface CampaignService
{
    public List<CampaignSummary> GetCampaignList();
    public CampaignDTO NewCampaign(NewCampaignRequestBody requestBody);
    public CampaignDTO GetCampaign(Guid campaignId);
    public CharacterDTO AddCharacterToCampaign(Guid campaignId, CharacterRequestBody newCharacterRequest);
    public CharacterDTO GetCharacterFromCampaign(Guid campaignId, Guid characterId);
    public CharacterDTO UpdateCharacterInCampaign(Guid campaignId, Guid characterId, CharacterRequestBody updateCharacterRequest);
}

public class CampaignServiceImplementation : CampaignService
{
    private readonly CampaignRepo repo;
    private readonly ContentService contentService;
    private readonly IMapper mapper;
    private readonly ILogger<CampaignServiceImplementation> logger;

    public CampaignServiceImplementation(CampaignRepo repo, ContentService contentService, IMapper mapper, ILogger<CampaignServiceImplementation> logger)
    {
        this.logger = logger;
        this.repo = repo;
        this.mapper = mapper;
        this.contentService = contentService;
    }

    private Campaign GetCampaignById(Guid campaignId)
    {
        return repo.GetCampaign(campaignId);
    }

    public List<CampaignSummary> GetCampaignList()
    {
        List<Campaign> campaigns = repo.GetCampaignList();
        return mapper.Map<List<CampaignSummary>>(campaigns);
    }

    public CampaignDTO GetCampaign(Guid campaignId)
    {
        Campaign campaign = GetCampaignById(campaignId);
        return mapper.Map<CampaignDTO>(campaign);
    }

    public CampaignDTO NewCampaign(NewCampaignRequestBody requestBody)
    {

        Models.Content.Game game = contentService.GetGameDefaults(
            GameUtils.GameType(requestBody.GameContentCode)
        );

        Dictionary<Guid, Models.Campaign.Scenario> scenarios = new Dictionary<Guid, Models.Campaign.Scenario>();
        game.Scenarios.ForEach(contentScenario => {
            Models.Campaign.Scenario scenario = new Models.Campaign.Scenario(
                Guid.NewGuid(),
                contentScenario,
                false,
                false
            );
            scenarios[scenario.Id] = scenario;
        });

        var campaign = new Campaign
        (
            Guid.NewGuid(),
            requestBody.Name,
            requestBody.Description,
            game,
            scenarios,
            new Dictionary<Guid, Models.Campaign.Character>()
        );

        var savedCampaign = repo.SaveCampaign(campaign);

        return mapper.Map<CampaignDTO>(savedCampaign);

    }

    public CharacterDTO AddCharacterToCampaign(Guid campaignId, CharacterRequestBody newCharacterRequest)
    {

        if(newCharacterRequest.CharacterContentCode is null)
            throw new ArgumentException("Character Content Code is in null");

        if(newCharacterRequest.Name is null)
            throw new ArgumentException("Character Name is in null");

        if(newCharacterRequest.Experience is null)
            throw new ArgumentException("Character Experience is in null");

        if(newCharacterRequest.Gold is null)
            throw new ArgumentException("Character Gold is in null");

        if(newCharacterRequest.PerkPoints is null)
            throw new ArgumentException("Character Perk Points is in null");

        var campaign = GetCampaignById(campaignId);

        if(campaign.Party.Select(kvp => kvp.Value.CharacterContent.ContentCode).ToList().Contains(newCharacterRequest.CharacterContentCode))
            throw new ArgumentException("Character Content Code is in use");
        
        var characterContent = contentService.GetCharacterDefaults(GameUtils.GameType(campaign.Game.ContentCode), newCharacterRequest.CharacterContentCode);

        var character = new Models.Campaign.Character(
            id: Guid.NewGuid(),
            newCharacterRequest.Name,
            characterContent: characterContent,
            experience: newCharacterRequest.Experience ?? 0,
            gold: newCharacterRequest.Gold ?? 0,
            perkPoints: newCharacterRequest.PerkPoints ?? 0
        );

        var savedCampaign = repo.CreateCharacterForCampaign(campaignId, character);

        var savedCharacter = savedCampaign.Party
            .Select(kvp => kvp.Value)
            .Where(chr => chr.CharacterContent.ContentCode == newCharacterRequest.CharacterContentCode)
            .First();

        return mapper.Map<CharacterDTO>(character);
    }
    
    public CharacterDTO GetCharacterFromCampaign(Guid campaignId, Guid characterId)
    {
        var campaign = GetCampaignById(campaignId);
        
        var character = campaign.Party.GetValueOrDefault(characterId);

        if(character is null) 
            throw new ArgumentException("Character Id was not found");

        return mapper.Map<CharacterDTO>(character);
    }

    public CharacterDTO UpdateCharacterInCampaign(Guid campaignId, Guid characterId, CharacterRequestBody updateCharacterRequest)
    {

        if(updateCharacterRequest.CharacterContentCode is not null)
            throw new ArgumentException("Character Content Code cannot be updated");

        if(updateCharacterRequest.Name is not null)
            throw new ArgumentException("Character Name cannot be updated");
            
        var campaign = GetCampaignById(campaignId);

        var character = campaign.Party.GetValueOrDefault(characterId);

        if(character is null) 
            throw new ArgumentException("Character Id was not found");

        character.Experience = updateCharacterRequest.Experience ?? character.Experience;
        character.Gold = updateCharacterRequest.Gold ?? character.Gold;
        character.PerkPoints = updateCharacterRequest.PerkPoints ?? character.PerkPoints;

        var savedCampaign = repo.UpdateCharacterForCampaign(campaignId, character);

        var savedCharacter = savedCampaign.Party
            .Select(kvp => kvp.Value)
            .Where(chr => chr.Id == characterId)
            .First();

        return mapper.Map<CharacterDTO>(character);
    }


    // public void UpdateCampaign(Guid campaignId, CampaignDTO campaign)
    // {
    //     throw new NotImplementedException();
    //     // ValidateCampaign(campaignId, campaign);
    //     // ValidateCampaignCharacters(campaign);
    //     // CampaignDO updatedCampaignDO = new CampaignDO()
    //     // {
    //     //     Game = GameUtils.GameTypeString(GameUtils.GameType(campaign.Game)),
    //     //     Description = campaign.Description,
    //     //     Id = campaign.Id,
    //     //     Scenarios = campaign.Scenarios.Select(s => new ScenarioDO() {
    //     //                 ContentCode = s.ContentCode,
    //     //                 IsClosed = s.IsClosed,
    //     //                 IsCompleted = s.IsCompleted,
    //     //             }).ToList()
    //     //     ,
    //     //     Party = campaign.Party.Select(c => new CharacterDO() {
    //     //                 Id = c.Id ?? Guid.NewGuid().ToString(),
    //     //                 CharacterContentCode = c.CharacterContentCode,
    //     //                 AppliedPerks = c.AppliedPerks,
    //     //                 Experience = c.Experience,
    //     //                 Gold = c.Gold,
    //     //                 Items = c.Items,
    //     //                 Name = c.Name,
    //     //                 PerkPoints = c.PerkPoints
    //     //             }).ToList()
    //     // };

    //     // var updatedCampaign = new Campaign(updatedCampaignDO);
    //     // campaigns[campaignId] = updatedCampaign;

    //     // repo.UpdateCampaign(updatedCampaignDO);

    // }

    // public void ValidateCampaign(Guid campaignId, CampaignDTO campaign)
    // {
    //     throw new NotImplementedException();
    //     // if(campaign.Id != campaignId.ToString()) throw new ArgumentException("Campaign Id Mismatch");
    //     // var campaignToReplace = GetCampaignById(campaignId);
    // }

    // public void ValidateCampaignCharacters(CampaignDTO campaign)
    // {
    //     throw new NotImplementedException();
    //     // var gameCode = GameUtils.GameType(campaign.Game);
    //     // campaign.Party.ForEach(character => {
    //     //     if(!contentService.IsValidCharacterCode(gameCode, character.CharacterContentCode))
    //     //         throw new ArgumentException("Character type is not valid for game");
    //     // });

    //     // if(campaign.Party.Select(c => c.CharacterContentCode).Distinct().Count() != campaign.Party.Count())
    //     //     throw new ArgumentException("Character appears more than once in a campaign");
    // }

    // public void ValidateCampaignScenarios(CampaignDTO campaign)
    // {
    // // throw new NotImplementedException();
    // // var gameCode = GameUtils.GameType(campaign.Game);
    // // campaign.Scenarios.ForEach(scenario => {
    // //     if(!contentService.IsValidScenarioCode(gameCode, scenario.ContentCode))
    // //         throw new ArgumentException("Scenario type is not valid for game");
    // // });

    // // if(campaign.Scenarios.Select(c => c.ContentCode).Distinct().Count() != campaign.Scenarios.Count())
    // //     throw new ArgumentException("Scenario appears more than once in a campaign");
    // }
}