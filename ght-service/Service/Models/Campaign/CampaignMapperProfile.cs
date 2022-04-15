using AutoMapper;
using GloomhavenTracker.Database.Models.Campaign;
using GloomhavenTracker.Service.Models.Campaign;

public class CampaignMapperProfile : Profile
{
    public CampaignMapperProfile()
    {

        CreateMap<CampaignDAO, Campaign>();

        

    }
}