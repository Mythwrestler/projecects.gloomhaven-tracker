using System;
using System.Collections.Generic;
using GloomhavenTracker.Service.Models.Campaign;

namespace GloomhavenTracker.Service.Repos;

public interface CampaignRepo
{
    public List<CampaignSummary> GetCampaignList();
    public CampaignDO GetCampaign(Guid campaignId);
    public void SaveCampaign(CampaignDO campaign);
}

public class CampaignRepoImplementation : CampaignRepo
{
    public List<CampaignSummary> GetCampaignList()
    {
        throw new NotImplementedException();
    }

    public CampaignDO GetCampaign(Guid campaignId)
    {
        throw new NotImplementedException();
    }

    public void SaveCampaign(CampaignDO campaign)
    {
        throw new NotImplementedException();
    }

}