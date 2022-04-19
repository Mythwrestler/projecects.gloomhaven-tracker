-- Clear Audit
DELETE FROM public."AuditLog";

-- Clear Campaigns
DELETE FROM public."CampaignCharacter";
DELETE FROM public."CampaignScenario";
DELETE FROM public."CampaignCampaign";

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



-- DROP INDEX IF EXISTS public.IDX_game_content_gin_contentjson_code;
-- DROP INDEX IF EXISTS public.IDX_game_content_game_contentJson_code;
-- DROP TABLE IF EXISTS public."Game Content";

-- CREATE TABLE public."Game Content" (
-- 	id bigint NOT NULL GENERATED ALWAYS AS IDENTITY,
-- 	contentId uuid NOT NULL,
-- 	game varchar NOT NULL,
-- 	description varchar NOT NULL,
-- 	contentJson jsonb NOT NULL
-- );

-- ALTER TABLE public."Game Content" ADD CONSTRAINT game_content_unique_identifier_contentid UNIQUE (contentId);

-- -- Column comments
-- COMMENT ON COLUMN public."Game Content".id IS 'Identifier';
-- COMMENT ON COLUMN public."Game Content".contentId IS 'Content Id';
-- COMMENT ON COLUMN public."Game Content".game IS 'Game Version';
-- COMMENT ON COLUMN public."Game Content".description IS 'Description Of The Default Content';
-- COMMENT ON COLUMN public."Game Content".contentJson IS 'Content Object';


-- CREATE INDEX IF NOT EXISTS IDX_game_content_gin_contentjson_code ON "Game Content" USING gin ((contentJson->'code') jsonb_path_ops);
-- CREATE UNIQUE INDEX  IF NOT EXISTS IDX_game_content_game_contentJson_code on "Game Content"(game, (contentJson->>'code'));



-- DROP TABLE IF EXISTS public."Campaign";

-- CREATE TABLE public."Campaign" (
-- 	id bigint NOT NULL GENERATED ALWAYS AS IDENTITY,
-- 	campaignId uuid NOT NULL,
-- 	game varchar NOT NULL,
-- 	description varchar NOT NULL,
-- 	campaignJson jsonb NOT NULL
-- );

-- ALTER TABLE public."Campaign" ADD CONSTRAINT campaign_unique_identifier_campaignid UNIQUE (campaignId);


-- DROP TABLE IF EXISTS public."Combat";

-- CREATE TABLE public."Combat" (
-- 	id bigint NOT NULL GENERATED ALWAYS AS IDENTITY,
-- 	combatId uuid NOT NULL,
-- 	game varchar NOT NULL,
-- 	description varchar NOT NULL,
-- 	combatJson jsonb NOT NULL
-- );

-- ALTER TABLE public."Combat" ADD CONSTRAINT combat_unique_identifier_combatid UNIQUE (combatId);

