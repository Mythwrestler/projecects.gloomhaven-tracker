using GloomhavenTracker.Service.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using GloomhavenTracker.Service.Models.Content;
using Microsoft.Extensions.Logging;

namespace GloomhavenTracker.Service.Controllers
{
    [Route("api/content/games")]
    public class ContentController : Controller
    {
        private readonly ContentService service;
        private readonly ILogger<ContentController> logger;

        public ContentController(ContentService service, ILogger<ContentController> logger)
        {
            this.service = service;
            this.logger = logger;
        }

        [HttpGet]
        public IActionResult GetGameSummaries()
        {
            try
            {
                var gameDefaults = service.GetContentSummary(CONTENT_TYPE.game, null);
                return new OkObjectResult(gameDefaults);
            }
            catch (Exception ex)
            {
                logger.LogError(new EventId(), ex, "Things");
                return UnprocessableEntity("Issue processing request");
            }
        }

        [Route("{gameCode}")]
        [HttpGet]
        public IActionResult GetGame(GAME_TYPE gameCode)
        {
            try
            {
                var gameDefaults = service.GetContentSummary(CONTENT_TYPE.game, gameCode);
                return new OkObjectResult(gameDefaults);
            }
            catch (Exception ex)
            {
                logger.LogError(new EventId(), ex, "Things");
                return UnprocessableEntity("Issue processing request");
            }
        }

        [Route("{gameCode}/default")]
        [HttpGet]
        public IActionResult GetGameDefaults(GAME_TYPE gameCode)
        {
            try
            {
                var gameDefaults = service.GetGameDefaults(gameCode);
                return new OkObjectResult(gameDefaults);
            }
            catch (Exception ex)
            {
                logger.LogError(new EventId(), ex, "Things");
                return UnprocessableEntity("Issue processing request");
            }
        }

        [Route("{gameCode}/characters")]
        [HttpGet]
        public IActionResult GetCharactersForGame(GAME_TYPE gameCode)
        {
            try
            {
                var characterList = service.GetContentSummary(CONTENT_TYPE.character, gameCode);
                return new OkObjectResult(characterList);
            }
            catch (Exception ex)
            {
                logger.LogError(new EventId(), ex, "Things");
                return UnprocessableEntity("Issue processing request");
            }
        }

        [Route("{gameCode}/characters/{contentCode}")]
        [HttpGet]
        public IActionResult GetCharacterDefaults(GAME_TYPE gameCode, string contentCode)
        {
            try
            {
                var player = service.GetCharacterDefaults(gameCode, contentCode);
                return new OkObjectResult(player);
            }
            catch (Exception ex)
            {
                logger.LogError(new EventId(), ex, "Things");
                return UnprocessableEntity("Issue processing request");
            }
        }

        [Route("{gameCode}/monsters")]
        [HttpGet]
        public IActionResult GetMonstersForGame(GAME_TYPE gameCode)
        {
            try
            {
                var monsterList = service.GetContentSummary(CONTENT_TYPE.monster, gameCode);
                return new OkObjectResult(monsterList);
            }
            catch(Exception ex)
            {
                logger.LogError(new EventId(), ex, "Things");
                return UnprocessableEntity("Issue processing request");
            }
        }

        [Route("{gameCode}/monsters/{contentCode}")]
        [HttpGet]
        public IActionResult GetMonsterDefaults(GAME_TYPE gameCode, string contentCode)
        {
            try
            {
                var monster = service.GetMonsterDefaults(gameCode, contentCode);
                return new OkObjectResult(monster);
            }
            catch(Exception ex)
            {
                logger.LogError(new EventId(), ex, "Things");
                return UnprocessableEntity("Issue processing request");
            }
        }

        [Route("{gameCode}/scenarios")]
        [HttpGet]
        public IActionResult GetScenariosForGame(GAME_TYPE gameCode)
        {
            try
            {
                var scenarioList = service.GetContentSummary(CONTENT_TYPE.scenario, gameCode);
                return new OkObjectResult(scenarioList);
            }
            catch(Exception ex)
            {
                logger.LogError(new EventId(), ex, "Things");
                return UnprocessableEntity("Issue processing request");
            }
        }


        [Route("{gameCode}/scenarios/{contentCode}")]
        [HttpGet]
        public IActionResult GetScenarioDefaults(GAME_TYPE gameCode, string contentCode)
        {
            try
            {
                var scenario = service.GetScenarioDefaults(gameCode, contentCode).Summary;
                return new OkObjectResult(scenario);
            }
            catch(Exception ex)
            {
                logger.LogError(new EventId(), ex, "Things");
                return UnprocessableEntity("Issue processing request");
            }
        }


    }
}