using System;
using System.Collections.Generic;
using GloomhavenTracker.Database;
using GloomhavenTracker.Service.Models.Campaign;
using Microsoft.Extensions.Logging;

namespace GloomhavenTracker.Service.Repos;

public interface CampaignEFRepo
{
    public List<CampaignSummary> GetCampaignList();
    public Campaign? GetCampaign(Guid campaignId);
    public void NewCampaign(Campaign campaign);
    public void UpdateCampaign(Campaign campaign);
}

public class CampaignEFRepoImplementation : CampaignEFRepo
{
    private readonly GloomhavenContext context;
    private readonly ILogger<CampaignEFRepoImplementation> logger;

    public CampaignEFRepoImplementation(GloomhavenContext context, ILogger<CampaignEFRepoImplementation> logger)
    {
        this.context = context;
        this.logger = logger;
    }

    public List<CampaignSummary> GetCampaignList()
    {
        throw new NotImplementedException();
        // return context.Campaign
        //     .Select(campaign => new CampaignSummary(){
        //         Id = campaign.Id.ToString(),
        //         Description = campaign.Description,
        //         Game = campaign.Game.ContentCode
        //     })
        //     .ToList();
    }
    
    public Campaign? GetCampaign(Guid campaignId)
    {
        throw new NotImplementedException();
    }

    public void NewCampaign(Campaign campaign)
    {
        throw new NotImplementedException();
    }

    public void UpdateCampaign(Campaign campaign)
    {
        throw new NotImplementedException();
    }
}