-- User Fake User 01
INSERT INTO public."User" ("UserId", "UserName", "FirstName", "LastName", "Email") VALUES 
('a335dbb1-84d6-487f-896a-2555c76c8799', 'fake-user-01', 'Fake', 'User', 'fake01@gmail.com');

-- Test Campaign 01
INSERT INTO public."CampaignCampaign" ("Id", "Name", "Description", "GameId", "CreatedBy", "UpdatedBy", "CreatedOnUTC", "UpdatedOnUTC") VALUES
('5459c417-c100-407e-b402-e2c087493ec5', 'Test Campaign', 'Test Campaign Description', '153dad18-1725-4a91-b337-521c52aaccd1', '00000000-0000-0000-0000-000000000000', '00000000-0000-0000-0000-000000000000', '2022-07-17 12:48:57.165 -0400', '2022-07-27 13:37:36.666 -0400');

-- Test Campaign 01 Scenarios
INSERT INTO public."CampaignScenario" ("Id", "IsClosed", "IsCompleted", "ScenarioContentId", "CampaignId", "CreatedBy", "UpdatedBy", "CreatedOnUTC", "UpdatedOnUTC") VALUES
 ('659c1578-6c5f-45cd-a050-4b79cf8a71ca', FALSE, TRUE, '5397e29d-9eb5-ed44-9579-d0846a51a40d', '5459c417-c100-407e-b402-e2c087493ec5', '00000000-0000-0000-0000-000000000000', '00000000-0000-0000-0000-000000000000', '2022-07-27 14:53:36.156 -0400', '2022-07-27 14:53:36.156 -0400')
,('bc21345e-d0d2-48ae-8c99-af7b101b6e95', TRUE, FALSE, '5d0c2238-19de-7aeb-f8db-971289354a38', '5459c417-c100-407e-b402-e2c087493ec5', '00000000-0000-0000-0000-000000000000', '00000000-0000-0000-0000-000000000000', '2022-07-27 14:53:23.749 -0400', '2022-07-27 14:53:23.749 -0400');

-- Test Campaign 01 Characters
INSERT INTO public."CampaignCharacter" ("Id","Name","Experience","Gold","PerkPoints","CharacterContentId","CampaignId","CreatedBy","UpdatedBy","CreatedOnUTC","UpdatedOnUTC") VALUES
 ('2341ff54-1d05-4bf2-ac78-9bc5465b039d','Test Hatchet',25,86,12,'a198b73c-a605-032b-dba5-fcf73b494a3d','5459c417-c100-407e-b402-e2c087493ec5','00000000-0000-0000-0000-000000000000','00000000-0000-0000-0000-000000000000','2022-07-27 13:38:04.990','2022-07-27 13:38:04.990')
,('0760555c-a240-46a2-a672-99d93957b77f','Test Voidwarden',90,1000,2,'219fc817-afce-5ffb-fac3-0534a9af5bc7','5459c417-c100-407e-b402-e2c087493ec5','00000000-0000-0000-0000-000000000000','00000000-0000-0000-0000-000000000000','2022-07-27 13:40:20.991','2022-07-27 13:40:20.991');

INSERT INTO public."CampaignCampaignItem" ("ItemId", "CampaignId", "CreatedOnUTC", "UpdatedOnUTC", "CharacterId") VALUES
 ('7a08d269-21f0-f69d-cc59-5191f3089647', '5459c417-c100-407e-b402-e2c087493ec5', '2022-07-27 13:38:04.990','2022-07-27 13:38:04.990', '2341ff54-1d05-4bf2-ac78-9bc5465b039d');

-- Test Campaign 01 Users
INSERT INTO public."UserCampaign" ("UserId","CampaignId") VALUES
 ('a335dbb1-84d6-487f-896a-2555c76c8799','5459c417-c100-407e-b402-e2c087493ec5');
