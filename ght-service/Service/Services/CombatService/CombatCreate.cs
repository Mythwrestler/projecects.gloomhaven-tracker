using System;
using System.Collections.Generic;
using System.Linq;
using GloomhavenTracker.Service.Models.Campaign;
using GloomhavenTracker.Service.Models.Combat;
using GloomhavenTracker.Service.Models.Content;

namespace GloomhavenTracker.Service.Services;

public partial class CombatServiceImplentation
{
    public Guid NewCombat(Guid campaingId, string scenarioContentCode)
    {
        throw new NotImplementedException();
        // if(CombatExists(campaingId, scenarioContentCode))
        //     throw new ArgumentException("Combat already exists for the scenario on the campaign.");
        // Campaign campaign;
        // try
        // {
        //     campaign = campaignService.GetCampaignById(campaingId);
        // }
        // catch
        // {
        //     throw new ArgumentException("Could not find campaign");
        // }
        // Models.Content.Scenario scenario;
        // try
        // {
        //     scenario = contentService.GetScenarioDefaults(campaign.Game, scenarioContentCode);
        // }
        // catch
        // {
        //     throw new ArgumentException("Could not find scenario");
        // }

        // var baseModDeck = contentService.GetBaseModDeck(campaign.Game);
        // var attackModifierDeckDO = new AttackModifierDeckDO()
        // {
        //     DiscardPile = new List<AttackModifier>(),
        //     DrawPile = baseModDeck
        // };

        // var monsterGroups = scenario.Monsters.Select(sm => new MonsterGroupDO(){
        //     ContentCode = sm.ContentCode,
        //     Monsters = new List<MonsterDO>()
        // }).ToList();

        // var levels = new List<int>();
        // var characters = campaign.Party.Characters.Select(cc => {
        //     var defaults = contentService.GetCharacterDefaults(campaign.Game, cc.Value.CharacterContentCode);
        //     levels.Add(GameUtils.GetPlayerLevel(defaults, cc.Value.Experience));
        //     return new Models.Combat.CharacterDO(){
        //         ContentCode = cc.Value.CharacterContentCode,
        //         Health = GameUtils.GetPlayerBaseHealth(defaults, cc.Value.Experience),
        //         ActiveEffects = new List<Effect>(),
        //         CombatantCode = "",
        //         ModifierDeck = attackModifierDeckDO
        //     };
        // }).ToList();

        // var intitiativeDO = new InitiativeDO()
        // {
        //     Combatants = campaign.Party.Characters.Select(kvp => new CombatantInitiativeDO(){
        //         CombatantCode = kvp.Value.CharacterContentCode,
        //         ContentCode = kvp.Value.CharacterContentCode,
        //         Initiative = -1,
        //         InstanceNumber = 1,
        //         IsElite = false,
        //     }).ToList(),
        //     CompletedCombatants = new List<string>(),
        //     CurrentCombatant = ""
        // };

        // var scenarioLevel = Math.Floor(levels.Average());

        // var newCombatForSave = new CombatTrackerDO()
        // {
        //     Id = Guid.NewGuid().ToString(),
        //     CampaignId = campaign.Id.ToString(),
        //     GameCode = GameUtils.GameTypeString(campaign.Game),
        //     ScenarioContentCode = scenarioContentCode,
        //     Description = $"{campaign.Description} - {scenario.Name}",
        //     Elements = new List<ElementDO>(),
        //     MonsterModifierDeck = attackModifierDeckDO,
        //     Monsters = monsterGroups,
        //     Characters = characters,
        //     Initiative = intitiativeDO,
        //     ScenarioLevel = ((int)scenarioLevel)
        // };


        // try
        // {
        //     combatRepo.NewCombat(newCombatForSave);
        //     var combatFromRepo = GetCombatById(new Guid(newCombatForSave.Id));
        //     if(combatFromRepo == null) throw new Exception("Could not save new combat");
        //     return combatFromRepo.Id;
        // }
        // catch {
        //     throw;
        // }

    }

    private bool CombatExists(Guid campaignId, string scenarioContentCode)
    {
        return combatRepo.CombatTrackerExists(campaignId, scenarioContentCode);
    }
}