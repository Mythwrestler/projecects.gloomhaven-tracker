-- User Fake User 01
INSERT INTO public."User" ("UserId", "UserName", "FirstName", "LastName", "Email") VALUES 
 ('a335dbb1-84d6-487f-896a-2555c76c8799', 'fake-user-01', 'Fake', 'User', 'fake01@gmail.com')
,('0acca7b2-527e-4f34-bd97-d9fc4d74c898','fake-user-02','Fake','User02','fake-user-02@fake.com');

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
INSERT INTO public."UserCampaign" ("UserId","CampaignId", "IsOwner") VALUES
 ('a335dbb1-84d6-487f-896a-2555c76c8799','5459c417-c100-407e-b402-e2c087493ec5', TRUE)
,('0acca7b2-527e-4f34-bd97-d9fc4d74c898','5459c417-c100-407e-b402-e2c087493ec5', TRUE);

-- Test Combat 01 - Monster Deck
INSERT INTO public."CombatAttackModifierDecks" ("Id","CreatedBy","UpdatedBy","CreatedOnUTC","UpdatedOnUTC","Positions") VALUES
 ('360b783c-c83d-4b8f-9aa1-e3ea9d4096bd','00000000-0000-0000-0000-000000000000','00000000-0000-0000-0000-000000000000','2022-08-02 22:42:28.200','2022-08-02 22:42:28.200','{}');

-- Test Combat 01 - Monster Deck Cards
INSERT INTO public."CombatAttackModifierDeckCards" ("DeckId","AttackModifierId","position") VALUES
  ('360b783c-c83d-4b8f-9aa1-e3ea9d4096bd','945471f8-21c5-4e4b-b2ab-a376d7718fb0',0)
 ,('360b783c-c83d-4b8f-9aa1-e3ea9d4096bd','c13d7849-d333-4b21-a2a5-06d7beb9b06b',1)
 ,('360b783c-c83d-4b8f-9aa1-e3ea9d4096bd','9730bd5f-8093-4a47-854c-4e47753e73a3',2)
 ,('360b783c-c83d-4b8f-9aa1-e3ea9d4096bd','d8b7bdac-346e-471c-83b7-9ff2edf5e41a',3)
 ,('360b783c-c83d-4b8f-9aa1-e3ea9d4096bd','03d414d0-e186-48f3-87dc-b109f989e79b',4)
 ,('360b783c-c83d-4b8f-9aa1-e3ea9d4096bd','9730bd5f-8093-4a47-854c-4e47753e73a3',5)
 ,('360b783c-c83d-4b8f-9aa1-e3ea9d4096bd','945471f8-21c5-4e4b-b2ab-a376d7718fb0',6)
 ,('360b783c-c83d-4b8f-9aa1-e3ea9d4096bd','48077f71-7ac2-4298-82cf-ab2c8ce9198f',7)
 ,('360b783c-c83d-4b8f-9aa1-e3ea9d4096bd','c13d7849-d333-4b21-a2a5-06d7beb9b06b',8)
 ,('360b783c-c83d-4b8f-9aa1-e3ea9d4096bd','c13d7849-d333-4b21-a2a5-06d7beb9b06b',9)
 ,('360b783c-c83d-4b8f-9aa1-e3ea9d4096bd','c13d7849-d333-4b21-a2a5-06d7beb9b06b',10)
 ,('360b783c-c83d-4b8f-9aa1-e3ea9d4096bd','d8b7bdac-346e-471c-83b7-9ff2edf5e41a',11)
 ,('360b783c-c83d-4b8f-9aa1-e3ea9d4096bd','428e5822-dd63-4daf-b947-e9835348f955',12)
 ,('360b783c-c83d-4b8f-9aa1-e3ea9d4096bd','945471f8-21c5-4e4b-b2ab-a376d7718fb0',13)
 ,('360b783c-c83d-4b8f-9aa1-e3ea9d4096bd','945471f8-21c5-4e4b-b2ab-a376d7718fb0',14)
 ,('360b783c-c83d-4b8f-9aa1-e3ea9d4096bd','48077f71-7ac2-4298-82cf-ab2c8ce9198f',15)
 ,('360b783c-c83d-4b8f-9aa1-e3ea9d4096bd','48077f71-7ac2-4298-82cf-ab2c8ce9198f',16)
 ,('360b783c-c83d-4b8f-9aa1-e3ea9d4096bd','48077f71-7ac2-4298-82cf-ab2c8ce9198f',17);

-- Test Combat 01
INSERT INTO public."CombatCombat" ("Id","CampaignId","ScenarioId","CreatedBy","UpdatedBy","CreatedOnUTC","UpdatedOnUTC","MonsterModifierDeckId","ScenarioLevel") VALUES
 ('6235d6b1-38dc-4f9d-81cd-7b97b02fd7dd','5459c417-c100-407e-b402-e2c087493ec5','5d0c2238-19de-7aeb-f8db-971289354a38','00000000-0000-0000-0000-000000000000','00000000-0000-0000-0000-000000000000','2022-08-02 22:42:28.200','2022-08-02 22:42:28.200','360b783c-c83d-4b8f-9aa1-e3ea9d4096bd', 2);

-- Test Combat 01 - Characters
INSERT INTO public."CombatCharacters" ("Id", "CombatId", "CampaignCharacterId", "Level", "Health", "CreatedBy", "UpdatedBy", "CreatedOnUTC", "UpdatedOnUTC") VALUES
 ('3edff481-8c35-4493-9703-7fb98cad2eec', '6235d6b1-38dc-4f9d-81cd-7b97b02fd7dd', '2341ff54-1d05-4bf2-ac78-9bc5465b039d', 2, 12, '00000000-0000-0000-0000-000000000000','00000000-0000-0000-0000-000000000000','2022-08-02 22:42:28.200','2022-08-02 22:42:28.200')
,('9b6c6ae3-0e87-438a-a695-02ba20443b20', '6235d6b1-38dc-4f9d-81cd-7b97b02fd7dd', '0760555c-a240-46a2-a672-99d93957b77f', 3, 10, '00000000-0000-0000-0000-000000000000','00000000-0000-0000-0000-000000000000','2022-08-02 22:42:28.200','2022-08-02 22:42:28.200');

-- Test Combat Hub Client
INSERT INTO public."HubCombatClient" ("Id", "CombatId", "UserId", "ClientId", "LastSeen") VALUES
 ('cc06d62b-8a07-4c6d-bbed-0dca175e6303', '6235d6b1-38dc-4f9d-81cd-7b97b02fd7dd', 'a335dbb1-84d6-487f-896a-2555c76c8799', 'z5Lli8nWedtRvOrtyNMXug', Current_timestamp);

