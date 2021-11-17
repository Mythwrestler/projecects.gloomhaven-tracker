using System;
using GloomhavenTracker.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GloomhavenTracker.Service.Controllers;


    [Route("api/campaign")]
    public class CampaignController : Controller
    {
        private CampaignService service;
        private ILogger<CampaignController> logger;

        public CampaignController(CampaignService service, ILogger<CampaignController> logger )
        {
            this.service = service;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult GetListOfAvailableCampaigns()
        {
            try
            {
                var campaigns = service.GetCampaignList();
                return Ok(campaigns);
            }
            catch (Exception ex)
            {
                logger.LogError(new EventId(), ex, "Failed to get list of campaigns");
                return UnprocessableEntity("Issue processing request");
            }
        }



    }