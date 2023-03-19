-- Clear Hub 
DELETE FROM public."CombatCharacterCombatHubClients";
DELETE FROM public."HubCombatClient";

-- Clear Audit
DELETE FROM public."AuditLog";

-- Clear Campaigns
DELETE FROM public."UserCampaign";
DELETE FROM public."CampaignCharacterAppliedPerk";
DELETE FROM public."CampaignCampaignItem";
DELETE FROM public."CampaignCharacter";
DELETE FROM public."CampaignScenario";
DELETE FROM public."CampaignCampaign";

-- Clear Combats
DELETE FROM public."CombatMonsterActiveEffects";
DELETE FROM public."CombatCharacterActiveEffects";
DELETE FROM public."CombatObjectiveActiveEffects";
DELETE FROM public."CombatMonsters";
DELETE FROM public."CombatObjectives";
DELETE FROM public."CombatCharacters";
DELETE FROM public."CombatActiveEffects";
DELETE FROM public."CombatAttackModifierDeckCards";
DELETE FROM public."CombatAttackModifierDecks";
DELETE FROM public."CombatElements";
DELETE FROM public."CombatCombat";

-- Clear User
DELETE FROM public."User";

-- Clear Content
DELETE FROM public."ContentScenarioObjective";
DELETE FROM public."ContentScenarioMonster";
DELETE FROM public."ContentObjective";
DELETE FROM public."ContentScenario";
DELETE FROM public."ContentMonsterBaseStatImmunity";
DELETE FROM public."ContentMonsterAttackEffect";
DELETE FROM public."ContentMonsterDefenseEffect";
DELETE FROM public."ContentMonsterDeathEffect";
DELETE FROM public."ContentMonsterStatSet";
DELETE FROM public."ContentMonster";
DELETE FROM public."ContentPerkAction";
DELETE FROM public."ContentPerk";
DELETE FROM public."ContentCharacterBaseStat";
DELETE FROM public."ContentCharacter";
DELETE FROM public."ContentGameBaseAttackModifiers";
DELETE FROM public."ContentAttackModifierEffect";
DELETE FROM public."ContentAttackModifier";
DELETE FROM public."ContentEffect";
DELETE FROM public."ContentItem";
DELETE FROM public."ContentGame";