using GloomhavenTracker.Service.Services;
using GloomhavenTracker.Service.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GloomhavenTracker.Service.Controllers
{
    [Route("api/content/games")]
    public class ContentController : Controller
    {
        private readonly IContentService _service;

        public ContentController(IContentService service) => _service = service;

        [HttpGet]
        public IActionResult GetGameSummaries()
        {
            try
            {
                var gameDefaults = _service.GetContentSummary(CONTENT_TYPE.game, null);
                return new OkObjectResult(gameDefaults);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }

        [Route("{gameCode}")]
        [HttpGet]
        public IActionResult GetGame(GAME_CODES gameCode)
        {
            try
            {
                var gameDefaults = _service.GetContentSummary(CONTENT_TYPE.game, gameCode);
                return new OkObjectResult(gameDefaults);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }

        [Route("{gameCode}/default")]
        [HttpGet]
        public IActionResult GetGameDefaults(GAME_CODES gameCode)
        {
            try
            {
                var gameDefaults = _service.GetGameDefaults(gameCode);
                return new OkObjectResult(gameDefaults);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }

        [Route("{gameCode}/players")]
        [HttpGet]
        public IActionResult GetPlayersForGame(GAME_CODES gameCode)
        {
            try
            {
                var playerList = _service.GetContentSummary(CONTENT_TYPE.player, gameCode);
                return new OkObjectResult(playerList);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }

        [Route("{gameCode}/players/{contentCode}")]
        [HttpGet]
        public IActionResult GetPlayerDefaults(GAME_CODES gameCode, string contentCode)
        {
            try
            {
                var player = _service.GetPlayerDefaults(gameCode, contentCode);
                return new OkObjectResult(player);
            }
            catch (Exception ex)
            {
                return new BadRequestResult();
            }
        }

        [Route("{gameCode}/monsters")]
        [HttpGet]
        public IActionResult GetMonstersForGame(GAME_CODES gameCode)
        {
            try
            {
                var monsterList = _service.GetContentSummary(CONTENT_TYPE.monster, gameCode);
                return new OkObjectResult(monsterList);
            }
            catch(Exception ex)
            {
                return new BadRequestResult();
            }
        }

        [Route("{gameCode}/monsters/{contentCode}")]
        [HttpGet]
        public IActionResult GetMonsterDefaults(GAME_CODES gameCode, string contentCode)
        {
            try
            {
                var monster = _service.GetMonsterDefaults(gameCode, contentCode);
                return new OkObjectResult(monster);
            }
            catch(Exception ex)
            {
                return new BadRequestResult();
            }
        }

        [Route("{gameCode}/scenarios")]
        [HttpGet]
        public IActionResult GetScenariosForGame(GAME_CODES gameCode)
        {
            try
            {
                var scenarioList = _service.GetContentSummary(CONTENT_TYPE.scenario, gameCode);
                return new OkObjectResult(scenarioList);
            }
            catch(Exception ex)
            {
                return new BadRequestResult();
            }
        }


        [Route("{gameCode}/scenarios/{contentCode}")]
        [HttpGet]
        public IActionResult GetScenarioDefaults(GAME_CODES gameCode, string contentCode)
        {
            try
            {
                var scenario = _service.GetScenarioDefaults(gameCode, contentCode).ToDTO();
                return new OkObjectResult(scenario);
            }
            catch(Exception ex)
            {
                return new BadRequestResult();
            }
        }


    }
}