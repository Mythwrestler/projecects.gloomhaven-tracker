using System;
using System.Collections.Generic;
using GloomhavenTracker.Service.Models.Campaign;
using GloomhavenTracker.Service.Repos;

namespace GloomhavenTracker.Service.Services;

public interface CampaignService
{
    public List<CampaignSummary> GetCampaignList();
}

public class CampaignServiceImplementation : CampaignService
{
    private CampaignRepo repo;

    public CampaignServiceImplementation(CampaignRepo repo)
    {
        this.repo = repo;
    }

    public List<CampaignSummary> GetCampaignList()
    {
        throw new NotImplementedException();
    }

}