using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GloomhavenTracker.Database;
using GloomhavenTracker.Database.Models.Campaign;
using GloomhavenTracker.Service.Models.Campaign;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GloomhavenTracker.Service.Repos;

public interface CampaignRepo
{
    public List<Campaign> GetCampaignList();
    public Campaign GetCampaign(Guid campaignId);
    public Campaign SaveCampaign(Campaign campaign);
    public Campaign CreateCharacterForCampaign(Guid campaignId, Character character);
    public Campaign UpdateCharacterForCampaign(Guid campaignId, Character character);
    public Campaign CreateScenarioForCampaign(Guid campaignId, Scenario scenario);
    public Campaign UpdateScenarioForCampaign(Guid campaignId, Scenario scenario);
}

public class CampaignRepoImplementation : CampaignRepo
{
    private readonly IMapper mapper;
    private readonly GloomhavenContext context;
    private readonly ILogger<CampaignRepoImplementation> logger;
    private Dictionary<Guid, CampaignDAO> campaigns = new Dictionary<Guid, CampaignDAO>();

    public CampaignRepoImplementation(GloomhavenContext context, IMapper mapper, ILogger<CampaignRepoImplementation> logger)
    {
        this.context = context;
        this.mapper = mapper;
        this.logger = logger;
    }

    public List<Campaign> GetCampaignList()
    {
        List<CampaignDAO> campaignDAOs = context.CampaignCampaign.Include(c => c.Game).ToList();
        return mapper.Map<List<Campaign>>(campaignDAOs);
    }
    
    private CampaignDAO GetCampaignDAO(Guid campaignId)
    {
        CampaignDAO? campaignDAO;

        campaignDAO = campaigns.GetValueOrDefault(campaignId);

        if(campaignDAO is null)
        {
            campaignDAO = context.CampaignCampaign
                .Where(campaign => campaign.Id == campaignId)
                .Include(campaign => campaign.Game)
                .Include(campaign => campaign.Party).ThenInclude(chr => chr.CharacterContent)
                .Include(campaign => campaign.Scenarios).ThenInclude(scn => scn.ScenarioContent)
                .FirstOrDefault();
            
            if(campaignDAO is null) throw new KeyNotFoundException("Could not find campaign.");

            campaigns[campaignDAO.Id] = campaignDAO;
        }

        return campaignDAO;
    }

    public Campaign GetCampaign(Guid campaignId)
    {
        return mapper.Map<Campaign>(GetCampaignDAO(campaignId));
    }

    public Campaign SaveCampaign(Campaign campaign)
    {
        var campaignToSave = mapper.Map<CampaignDAO>(campaign);

        context.CampaignCampaign.Add(campaignToSave);

        context.SaveChanges();

        return GetCampaign(campaign.Id);
    }

    public Campaign CreateCharacterForCampaign(Guid campaignId, Character character)
    {
        CampaignDAO campaign = GetCampaignDAO(campaignId);

        CharacterDAO characterForUpdate = mapper.Map<CharacterDAO>(character);
        characterForUpdate.CampaignId = campaignId;

        campaign.Party.Add(characterForUpdate);

        context.CampaignCharacter.Add(characterForUpdate);

        context.SaveChanges();

        return GetCampaign(campaignId);
    }

    public Campaign UpdateCharacterForCampaign(Guid campaignId, Character character)
    {
        CharacterDAO characterToUpdate = context.CampaignCharacter.Where(chr => chr.Id == character.Id).First();

        if(characterToUpdate.Experience != character.Experience)
            characterToUpdate.Experience = character.Experience;

        if(characterToUpdate.Gold != character.Gold)
            characterToUpdate.Gold = character.Gold;
            
        if(characterToUpdate.PerkPoints != character.PerkPoints)
            characterToUpdate.PerkPoints = character.PerkPoints;
        
        context.SaveChanges();

        return GetCampaign(campaignId);
    }

    public Campaign CreateScenarioForCampaign(Guid campaignId, Scenario scenario)
    {
        CampaignDAO campaign = GetCampaignDAO(campaignId);

        ScenarioDAO scenarioForUpdate = mapper.Map<ScenarioDAO>(scenario);
        scenarioForUpdate.CampaignId = campaignId;

        campaign.Scenarios.Add(scenarioForUpdate);

        context.CampaignScenario.Add(scenarioForUpdate);

        context.SaveChanges();

        return GetCampaign(campaignId);
    }

    public Campaign UpdateScenarioForCampaign(Guid campaignId, Scenario scenario)
    {
        ScenarioDAO scenarioToUpdate = context.CampaignScenario.Where(scn => scn.Id == scenario.Id).First();

        if(scenarioToUpdate.IsClosed != scenario.IsClosed)
            scenarioToUpdate.IsClosed = scenario.IsClosed;

        if(scenarioToUpdate.IsCompleted != scenario.IsCompleted)
            scenarioToUpdate.IsCompleted = scenario.IsCompleted;

        context.SaveChanges();

        return GetCampaign(campaignId);
    }
}