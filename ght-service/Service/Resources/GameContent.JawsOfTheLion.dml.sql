DELETE FROM public."ScenarioObjective";
DELETE FROM public."ScenarioMonster";
DELETE FROM public."Objective";
DELETE FROM public."Scenario";
DELETE FROM public."MonsterBaseStatImmunity";
DELETE FROM public."MonsterAttackEffect";
DELETE FROM public."MonsterDefenseEffect";
DELETE FROM public."MonsterDeathEffect";
DELETE FROM public."MonsterStatSet";
DELETE FROM public."Monster";
DELETE FROM public."CharacterBaseStats";
DELETE FROM public."Character";
DELETE FROM public."GameBaseAttackModifiers";
DELETE FROM public."AttackModifierEffect";
DELETE FROM public."AttackModifier";
DELETE FROM public."Effect";
DELETE FROM public."Game";


INSERT INTO public."Game" ("Id","ContentCode","Name","Description") VALUES
 ('153dad18-1725-4a91-b337-521c52aaccd1', 'jawsOfTheLion', 'Jaws of The Lion', 'Game: Jaws of The Lion') --Game Jaws of the lion
;

INSERT INTO public."Effect" ("Id","Type","Value","Duration","Range") VALUES
 ('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', 'poison', -1, -1, -1) --Type: poison - Value: -1 - Duration: -1 - Range: -1
,('58b68f6b-c976-442d-9b15-c654d6f42a7b', 'muddle', -1, 1, -1) --Type: muddle - Value: -1 - Duration: 1 - Range: -1
,('6c407028-6e98-47da-bcc1-47b3e7cb8066', 'shield', 1, -1, -1) --Type: shield - Value: 1 - Duration: -1 - Range: -1
,('b41f91aa-5d40-0935-6545-876f4c6213e7', 'shield', 2, -1, -1) --Type: shield - Value: 2 - Duration: -1 - Range: -1
,('15e43b68-8db9-5781-559e-24c6bee9fbf1', 'shield', 3, -1, -1) --Type: shield - Value: 3 - Duration: -1 - Range: -1
,('84d679fa-3745-577a-4351-d0edd839d327', 'shield', 4, -1, -1) --Type: shield - Value: 4 - Duration: -1 - Range: -1
,('eb92eff2-35ed-4e1a-9480-f527445f3ef4', 'wound', -1, -1, -1) --Type: wound - Value: -1 - Duration: -1 - Range: -1
,('0ac1f965-80e8-bbec-69b4-128f0a6fb320', 'disadvantage', -1, -1, -1) --Type: disadvantage - Value: -1 - Duration: -1 - Range: -1
,('67119d3e-d9fd-3da2-7981-c1c82d3935e3', 'advantage', -1, -1, -1) --Type: advantage - Value: -1 - Duration: -1 - Range: -1
,('ae850bb6-26fb-0d22-684d-0ab3d0f43280', 'damage', 1, -1, 1) --Type: damage - Value: 1 - Duration: -1 - Range: 1
,('04977de7-6ea7-fff7-4e69-de45cbac49b5', 'damage', 2, -1, 1) --Type: damage - Value: 2 - Duration: -1 - Range: 1
,('fbbb6c2c-10f1-879f-edbb-2fb95d7bd7a5', 'damage', 3, -1, 1) --Type: damage - Value: 3 - Duration: -1 - Range: 1
,('2d692b7f-9f97-37ff-b622-1a2071027053', 'damage', 4, -1, 1) --Type: damage - Value: 4 - Duration: -1 - Range: 1
,('26f9d0a7-f71e-e17b-0cf6-af7cb914d29d', 'damage', 5, -1, 1) --Type: damage - Value: 5 - Duration: -1 - Range: 1
;

INSERT INTO public."AttackModifier" ("Id","ContentCode","Name","Description","IsCurse","IsBlessing","TriggerShuffle","Value","GameId") VALUES
 ('03d414d0-e186-48f3-87dc-b109f989e79b', 'mod_cancel', 'Attack Cancel', 'Attack Modifier: Cancel', FALSE, FALSE, TRUE, 'A * 0', '153dad18-1725-4a91-b337-521c52aaccd1') --Attack Modifier: Cancel
,('428e5822-dd63-4daf-b947-e9835348f955', 'mod_*2', 'Attack * 2', 'Attack Modifier: *2', FALSE, FALSE, TRUE, 'A * 2', '153dad18-1725-4a91-b337-521c52aaccd1') --Attack Modifier: *2
,('42ef417c-31d0-4981-97fe-931a36bddfa1', 'mod_curse', 'Attack Cancel (Curse)', 'Attack Modifier: Cancel (Curse)', TRUE, FALSE, TRUE, 'A * 0', '153dad18-1725-4a91-b337-521c52aaccd1') --Attack Modifier: Cancel (Curse)
,('48077f71-7ac2-4298-82cf-ab2c8ce9198f', 'mod_+1', 'Attack + 1', 'Attack Modifier: +1', FALSE, FALSE, FALSE, 'A + 1', '153dad18-1725-4a91-b337-521c52aaccd1') --Attack Modifier: +1
,('945471f8-21c5-4e4b-b2ab-a376d7718fb0', 'mod_+0', 'Attack + 0', 'Attack Modifier: +0', FALSE, FALSE, FALSE, 'A + 0', '153dad18-1725-4a91-b337-521c52aaccd1') --Attack Modifier: +0
,('9730bd5f-8093-4a47-854c-4e47753e73a3', 'mod_+2', 'Attack + 2', 'Attack Modifier: +2', FALSE, FALSE, FALSE, 'A + 2', '153dad18-1725-4a91-b337-521c52aaccd1') --Attack Modifier: +2
,('c13d7849-d333-4b21-a2a5-06d7beb9b06b', 'mod_-1', 'Attack - 1', 'Attack Modifier: -1', FALSE, FALSE, FALSE, 'A - 1', '153dad18-1725-4a91-b337-521c52aaccd1') --Attack Modifier: -1
,('cc557a2d-89a9-4cfa-9ec0-b5d64815495e', 'mod_blessing', 'Attack * 2 (Blessing)', 'Attack Modifier: *2 (Blessing)', FALSE, TRUE, TRUE, 'A * 2', '153dad18-1725-4a91-b337-521c52aaccd1') --Attack Modifier: *2 (Blessing)
,('d8b7bdac-346e-471c-83b7-9ff2edf5e41a', 'mod_-2', 'Attack - 2', 'Attack Modifier: -2', FALSE, FALSE, FALSE, 'A - 2', '153dad18-1725-4a91-b337-521c52aaccd1') --Attack Modifier: -2
,('71291998-346d-4ed5-a770-e8ea78661187', 'mod_+1_muddle', 'Attack + 1 | Muddle', 'Attack Modifier: + 1 | Muddle', FALSE, FALSE, FALSE, 'A + 1', '153dad18-1725-4a91-b337-521c52aaccd1') --Attack Modifier: + 1 | Muddle
;

INSERT INTO public."AttackModifierEffect" ("EffectId","AttackModifierId") VALUES
 ('58b68f6b-c976-442d-9b15-c654d6f42a7b', '71291998-346d-4ed5-a770-e8ea78661187') --Modifier: Attack Modifier: + 1 | Muddle - Effect: Type: muddle - Value: -1 - Duration: 1
;

INSERT INTO public."GameBaseAttackModifiers" ("Id", "GameId", "AttackModifierId") VALUES
 ('e43dce52-6b17-47ab-bea7-1816075691fe', '153dad18-1725-4a91-b337-521c52aaccd1', '428e5822-dd63-4daf-b947-e9835348f955') --Jaws of The Lion - Base Deck - Attack * 2
,('6a3ff216-36e7-1a8d-92c5-bd1482bf0992', '153dad18-1725-4a91-b337-521c52aaccd1', '9730bd5f-8093-4a47-854c-4e47753e73a3') --Jaws of The Lion - Base Deck - Attack + 2
,('7bc951a2-79d8-1136-acb6-f452c2967daf', '153dad18-1725-4a91-b337-521c52aaccd1', '9730bd5f-8093-4a47-854c-4e47753e73a3') --Jaws of The Lion - Base Deck - Attack + 2
,('4fd2bf06-1b06-626c-7018-91ba43c4b57f', '153dad18-1725-4a91-b337-521c52aaccd1', '48077f71-7ac2-4298-82cf-ab2c8ce9198f') --Jaws of The Lion - Base Deck - Attack + 1
,('24992d91-c2a0-0e52-1d98-a7e795b58c78', '153dad18-1725-4a91-b337-521c52aaccd1', '48077f71-7ac2-4298-82cf-ab2c8ce9198f') --Jaws of The Lion - Base Deck - Attack + 1
,('64189c12-d1a8-e7c9-57fe-9986b231b80e', '153dad18-1725-4a91-b337-521c52aaccd1', '48077f71-7ac2-4298-82cf-ab2c8ce9198f') --Jaws of The Lion - Base Deck - Attack + 1
,('8f06016e-0045-b7c3-c4e6-940fcc573305', '153dad18-1725-4a91-b337-521c52aaccd1', '48077f71-7ac2-4298-82cf-ab2c8ce9198f') --Jaws of The Lion - Base Deck - Attack + 1
,('05170925-82da-a03b-bb80-d44fe414f9c6', '153dad18-1725-4a91-b337-521c52aaccd1', '945471f8-21c5-4e4b-b2ab-a376d7718fb0') --Jaws of The Lion - Base Deck - Attack + 0
,('a9e25710-26ce-d6b7-bed5-4779bfb16686', '153dad18-1725-4a91-b337-521c52aaccd1', '945471f8-21c5-4e4b-b2ab-a376d7718fb0') --Jaws of The Lion - Base Deck - Attack + 0
,('347c7381-fa85-cd5f-945a-6d8760b026b4', '153dad18-1725-4a91-b337-521c52aaccd1', '945471f8-21c5-4e4b-b2ab-a376d7718fb0') --Jaws of The Lion - Base Deck - Attack + 0
,('2824f7d2-d151-84e1-b583-05c9370ed934', '153dad18-1725-4a91-b337-521c52aaccd1', '945471f8-21c5-4e4b-b2ab-a376d7718fb0') --Jaws of The Lion - Base Deck - Attack + 0
,('2a99292b-7527-376e-5b7a-9acaf0a7ca1a', '153dad18-1725-4a91-b337-521c52aaccd1', 'c13d7849-d333-4b21-a2a5-06d7beb9b06b') --Jaws of The Lion - Base Deck - Attack - 1
,('edbb6d3f-8138-1274-5ade-b99043dc9777', '153dad18-1725-4a91-b337-521c52aaccd1', 'c13d7849-d333-4b21-a2a5-06d7beb9b06b') --Jaws of The Lion - Base Deck - Attack - 1
,('be99d52b-95c0-4dc5-a5a2-84555aee2494', '153dad18-1725-4a91-b337-521c52aaccd1', 'c13d7849-d333-4b21-a2a5-06d7beb9b06b') --Jaws of The Lion - Base Deck - Attack - 1
,('0bbf2154-f2fc-f97e-a293-aec8a2a93a72', '153dad18-1725-4a91-b337-521c52aaccd1', 'c13d7849-d333-4b21-a2a5-06d7beb9b06b') --Jaws of The Lion - Base Deck - Attack - 1
,('637ec13f-ec97-4d19-3572-d6a68e3f7ec1', '153dad18-1725-4a91-b337-521c52aaccd1', 'd8b7bdac-346e-471c-83b7-9ff2edf5e41a') --Jaws of The Lion - Base Deck - Attack - 2
,('c032812e-0a03-cbaa-3fbf-feb29a007085', '153dad18-1725-4a91-b337-521c52aaccd1', 'd8b7bdac-346e-471c-83b7-9ff2edf5e41a') --Jaws of The Lion - Base Deck - Attack - 2
,('f6659877-b9f6-d4f2-d0da-a82de40fb9ff', '153dad18-1725-4a91-b337-521c52aaccd1', '03d414d0-e186-48f3-87dc-b109f989e79b') --Jaws of The Lion - Base Deck - Attack Cancel
;

INSERT INTO public."Character" ("Id", "ContentCode", "Name", "Description", "GameId") VALUES
 ('219fc817-afce-5ffb-fac3-0534a9af5bc7', 'voidwarden', 'Voidwarden', 'Character - Void Warden', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Voidwarden
,('450631fd-d6a1-946b-0745-2332b4152567', 'red_guard', 'Red Guard', 'Character - Red Guard', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Red Guard
,('a198b73c-a605-032b-dba5-fcf73b494a3d', 'hatchet', 'Hatchet', 'Character - Hatchet', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Hatchet
,('c2bf284b-b41a-3ab8-dee7-d51510668b84', 'demolitionist', 'Demolitionist', 'Character - Demolitionist', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Demolitionist
;

INSERT INTO public."CharacterBaseStats" ("Id", "CharacterId", "Level", "Experience", "Health") VALUES
 ('9d9f358e-0d7e-df45-7011-51e64f730121', '219fc817-afce-5ffb-fac3-0534a9af5bc7', 1, 0, 6) --Voidwarden - Level 1
,('b0ee1d9f-ddc4-f303-cb84-9c831befe080', '219fc817-afce-5ffb-fac3-0534a9af5bc7', 2, 45, 7) --Voidwarden - Level 2
,('e6cc43b9-fffd-d2a0-2772-1e99acd8b877', '219fc817-afce-5ffb-fac3-0534a9af5bc7', 3, 95, 8) --Voidwarden - Level 3
,('7017cc2a-65e3-1ae6-1845-a10e6f827782', '219fc817-afce-5ffb-fac3-0534a9af5bc7', 4, 150, 9) --Voidwarden - Level 4
,('32ef1260-b312-cf1b-d21e-bead3960f484', '219fc817-afce-5ffb-fac3-0534a9af5bc7', 5, 210, 10) --Voidwarden - Level 5
,('850552d1-3ea0-347e-a509-a5315fb54015', '219fc817-afce-5ffb-fac3-0534a9af5bc7', 6, 275, 11) --Voidwarden - Level 6
,('a7befe5c-121e-adb0-acae-4979b60df2c2', '219fc817-afce-5ffb-fac3-0534a9af5bc7', 7, 345, 12) --Voidwarden - Level 7
,('49c2f978-272a-91f1-e78c-00fbc18ac02f', '219fc817-afce-5ffb-fac3-0534a9af5bc7', 8, 420, 13) --Voidwarden - Level 8
,('2cf13e91-f26c-36d3-eac9-a1f29daf7c9a', '219fc817-afce-5ffb-fac3-0534a9af5bc7', 9, 500, 14) --Voidwarden - Level 9
,('62b63b21-72f9-19fa-9ac1-1cdf9f3afa39', '450631fd-d6a1-946b-0745-2332b4152567', 1, 0, 10) --Red Guard - Level 1
,('c4c6e1cb-6d5e-0d2a-301e-27839a11eca3', '450631fd-d6a1-946b-0745-2332b4152567', 2, 45, 12) --Red Guard - Level 2
,('d0e85c6f-c6cb-750b-e39b-cabcb3859e57', '450631fd-d6a1-946b-0745-2332b4152567', 3, 95, 14) --Red Guard - Level 3
,('6f717508-74b7-0dbd-e986-04d9d5b2794c', '450631fd-d6a1-946b-0745-2332b4152567', 4, 150, 16) --Red Guard - Level 4
,('76f0daf3-cdcc-ad05-cdbe-1c33836e3827', '450631fd-d6a1-946b-0745-2332b4152567', 5, 210, 18) --Red Guard - Level 5
,('53c213ce-b550-400e-5c84-86160d0df72d', '450631fd-d6a1-946b-0745-2332b4152567', 6, 275, 20) --Red Guard - Level 6
,('74361a7c-da10-aeb6-5c52-ea61030311d5', '450631fd-d6a1-946b-0745-2332b4152567', 7, 345, 22) --Red Guard - Level 7
,('e599247e-7b1a-00ba-716c-0f509eee7791', '450631fd-d6a1-946b-0745-2332b4152567', 8, 420, 24) --Red Guard - Level 8
,('61c1c988-a368-ab2e-e394-bb3529fee7e6', '450631fd-d6a1-946b-0745-2332b4152567', 9, 500, 26) --Red Guard - Level 9
,('0a9da8bb-ef4d-b115-f59f-1a1909767e30', 'a198b73c-a605-032b-dba5-fcf73b494a3d', 1, 0, 8) --Hatchet - Level 1
,('2f620f22-b9be-b67f-05a9-aa2305dc2fe8', 'a198b73c-a605-032b-dba5-fcf73b494a3d', 2, 45, 9) --Hatchet - Level 2
,('47c3d296-0f4e-1005-8e06-c80196732ed6', 'a198b73c-a605-032b-dba5-fcf73b494a3d', 3, 95, 11) --Hatchet - Level 3
,('091b0b6e-5d68-caa7-7c3f-e48186afa654', 'a198b73c-a605-032b-dba5-fcf73b494a3d', 4, 150, 12) --Hatchet - Level 4
,('8d32c406-679b-1f38-2a1f-2bc67be0f007', 'a198b73c-a605-032b-dba5-fcf73b494a3d', 5, 210, 14) --Hatchet - Level 5
,('b2c265bd-b2b6-56c8-c9bd-a68b88788028', 'a198b73c-a605-032b-dba5-fcf73b494a3d', 6, 275, 15) --Hatchet - Level 6
,('13c4b36a-727e-3505-a1b0-c1e0a8170dd8', 'a198b73c-a605-032b-dba5-fcf73b494a3d', 7, 345, 17) --Hatchet - Level 7
,('f593c0ec-c8da-fedd-d9a5-f576fec48d5d', 'a198b73c-a605-032b-dba5-fcf73b494a3d', 8, 420, 18) --Hatchet - Level 8
,('9d362c1a-7124-b3b5-bed6-b9dc75f3eead', 'a198b73c-a605-032b-dba5-fcf73b494a3d', 9, 500, 20) --Hatchet - Level 9
,('990206b2-3314-3918-5166-212ee033e635', 'c2bf284b-b41a-3ab8-dee7-d51510668b84', 1, 0, 8) --Demolitionist - Level 1
,('7434fa8c-2831-1770-d551-bcc31464151a', 'c2bf284b-b41a-3ab8-dee7-d51510668b84', 2, 45, 9) --Demolitionist - Level 2
,('59e3b3d6-ea94-7bae-a8d2-75e6915db044', 'c2bf284b-b41a-3ab8-dee7-d51510668b84', 3, 95, 11) --Demolitionist - Level 3
,('119dc8bd-4100-2232-520f-3730317b32cf', 'c2bf284b-b41a-3ab8-dee7-d51510668b84', 4, 150, 12) --Demolitionist - Level 4
,('42fea39b-4888-f05e-cd10-bd0ce7bdc0c8', 'c2bf284b-b41a-3ab8-dee7-d51510668b84', 5, 210, 14) --Demolitionist - Level 5
,('77857c81-cc34-3be9-9d78-0e8a0db4756a', 'c2bf284b-b41a-3ab8-dee7-d51510668b84', 6, 275, 15) --Demolitionist - Level 6
,('6ef58722-ec81-774d-1a0b-c05e63399ebe', 'c2bf284b-b41a-3ab8-dee7-d51510668b84', 7, 345, 17) --Demolitionist - Level 7
,('b6e1e00b-722e-3376-bafa-0a67b4310fe9', 'c2bf284b-b41a-3ab8-dee7-d51510668b84', 8, 420, 18) --Demolitionist - Level 8
,('7c5032a5-4603-f96f-a58f-ab023de2e237', 'c2bf284b-b41a-3ab8-dee7-d51510668b84', 9, 500, 20) --Demolitionist - Level 9
;

INSERT INTO public."Monster" ("Id", "ContentCode", "Name", "Description", "GameId") VALUES
 ('15fb31da-55d3-48cc-8dcc-e98c329f8bee', 'blood_horror', 'Blood Horror', 'Boss: Blood Horror', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Blood Horror
,('3c5ee40c-1118-4f09-a733-c19bb73e9f69', 'blood_tumor', 'Blood Tumor', 'Boss: Blood Tumor', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Blood Tumor
,('4775e3b4-3ee3-4d66-8d9b-c250700962f3', 'first_of_the_order', 'First Of The Order', 'Boss: First Of The Order', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - First Of The Order
,('7e7f370b-eab5-ded7-11e8-227a04a4f283', 'black_imp', 'Black Imp', 'Monster: Black Imp', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Black Imp
,('9462d916-3512-4692-a797-d2861d42c4b9', 'black_sludge', 'Black Sludge', 'Normal Monster', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Black Sludge
,('c18c7770-0c09-9e46-3b56-8d7015eed6e4', 'blood_imp', 'Blood Imp', 'Monster: Blood Imp', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Blood Imp
,('91610c35-e291-8e88-f7ba-f487649ca947', 'blood_monstrosity', 'Blood Monstrosity', 'Monster: Blood Monstrosity', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Blood Monstrosity
,('a3d6ad55-ba4c-ab9b-83bd-1dd2aa8c1b3f', 'chaos_demon', 'Chaos Demon', 'Monster: Chaos Demon', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Chaos Demon
,('75b4da15-43a4-46da-a410-6f90e8f9e9db', 'giant_viper', 'Giant Viper', 'Monster: Giant Viper', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Giant Viper
,('1c153051-3d56-693a-d5a3-153bf86d1315', 'living_corpse', 'Living Corpse', 'Monster: Living Corpse', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Living Corpse
,('fa9f27a0-b6ca-60a2-327f-bf700a4e5d29', 'living_spirit', 'Living Spirit', 'Monster: Living Spirit', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Living Spirit
,('89d6080f-3ff1-ca9d-900c-70651091a11a', 'rat_monstrosity', 'Rat Monstrosity', 'Monster: Rat Monstrosity', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Rat Monstrosity
,('c22bac94-b488-924f-a8d6-8770fc530d62', 'stone_golem', 'Stone Golem', 'Monster: Stone Golem', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Stone Golem
,('abc857ec-2b99-4189-a2df-b5cfb98c3c31', 'vermling_raider', 'Vermling Raider', 'Monster: Vermling Raider', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Vermling Raider
,('1b1c44d4-417d-4548-ab5c-87116956d51e', 'vermling_scout', 'Vermling Scout', 'Monsster: Vermling Scout', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Vermling Scout
,('01d2606d-8269-4e83-a6c4-4fb870e37ca0', 'zealot', 'Zealot', 'Monster: Zealot', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Zealot
;

INSERT INTO public."MonsterStatSet" ("Id",  "MonsterId", "IsElite", "Level", "Health", "Movement", "Attack", "RangeAttackable", "MeleeAttackable") VALUES
 ('f512e56e-2067-4df3-9894-1650664d8a93', '15fb31da-55d3-48cc-8dcc-e98c329f8bee', FALSE, '0', 'C * 7', '3', 'C - 1', TRUE, TRUE) --Jaws of The Lion - Blood Horror - Standard - Level 0
,('384e42c4-66c1-48d0-af7f-6ed6f53de3fd', '15fb31da-55d3-48cc-8dcc-e98c329f8bee', FALSE, '1', 'C * 10', '3', 'C', TRUE, TRUE) --Jaws of The Lion - Blood Horror - Standard - Level 1
,('446be22b-3d69-4ac4-9941-71b28e7fca39', '15fb31da-55d3-48cc-8dcc-e98c329f8bee', FALSE, '2', 'C * 12', '4', 'C', TRUE, TRUE) --Jaws of The Lion - Blood Horror - Standard - Level 2
,('e5323bc1-e33b-45d3-8a00-cfb5e1759a09', '15fb31da-55d3-48cc-8dcc-e98c329f8bee', FALSE, '3', 'C * 15', '4', 'C + 1', TRUE, TRUE) --Jaws of The Lion - Blood Horror - Standard - Level 3
,('16d4b2f7-b917-48f3-8a83-6950e4257c9a', '15fb31da-55d3-48cc-8dcc-e98c329f8bee', FALSE, '4', 'C * 17', '5', 'C + 1', TRUE, TRUE) --Jaws of The Lion - Blood Horror - Standard - Level 4
,('5efd0b48-fa3a-45b6-a65e-0ff0eb33ee64', '15fb31da-55d3-48cc-8dcc-e98c329f8bee', FALSE, '5', 'C * 20', '5', 'C + 2', TRUE, TRUE) --Jaws of The Lion - Blood Horror - Standard - Level 5
,('8c5eec8f-c206-4d4d-a395-b978f9a5783f', '15fb31da-55d3-48cc-8dcc-e98c329f8bee', FALSE, '6', 'C * 23', '5', 'C + 3', TRUE, TRUE) --Jaws of The Lion - Blood Horror - Standard - Level 6
,('62beed40-2295-4371-ac9d-e71c17d56ef8', '15fb31da-55d3-48cc-8dcc-e98c329f8bee', FALSE, '7', 'C * 28', '5', 'C + 4', TRUE, TRUE) --Jaws of The Lion - Blood Horror - Standard - Level 7
,('fd7d54ff-6e25-43ae-bc8c-3ea0a01eb3b3', '3c5ee40c-1118-4f09-a733-c19bb73e9f69', FALSE, '0', 'C + 7', '0', 'C - 1', TRUE, TRUE) --Jaws of The Lion - Blood Tumor - Standard - Level 0
,('fd539ed4-abcd-4788-827b-86b9345f7c2d', '3c5ee40c-1118-4f09-a733-c19bb73e9f69', FALSE, '1', 'C * 10', '0', 'C', TRUE, TRUE) --Jaws of The Lion - Blood Tumor - Standard - Level 1
,('67fdb1d5-d476-4969-8688-3d660f6cc7f5', '3c5ee40c-1118-4f09-a733-c19bb73e9f69', FALSE, '2', 'C * 12', '0', 'C', TRUE, TRUE) --Jaws of The Lion - Blood Tumor - Standard - Level 2
,('8d94619f-428c-4603-8102-917c2bd4a984', '3c5ee40c-1118-4f09-a733-c19bb73e9f69', FALSE, '3', 'C * 15', '0', 'C + 1', TRUE, TRUE) --Jaws of The Lion - Blood Tumor - Standard - Level 3
,('3eabd5dc-0162-4a7b-9ce6-7132b3253215', '3c5ee40c-1118-4f09-a733-c19bb73e9f69', FALSE, '4', 'C * 17', '0', 'C + 1', TRUE, TRUE) --Jaws of The Lion - Blood Tumor - Standard - Level 4
,('f67915e5-2235-49ab-9921-23f02439c94e', '3c5ee40c-1118-4f09-a733-c19bb73e9f69', FALSE, '5', 'C * 20', '0', 'C + 2', TRUE, TRUE) --Jaws of The Lion - Blood Tumor - Standard - Level 5
,('9d5a78ee-9d90-4c77-808a-dd87401b0162', '3c5ee40c-1118-4f09-a733-c19bb73e9f69', FALSE, '6', 'C * 23', '0', 'C + 2', TRUE, TRUE) --Jaws of The Lion - Blood Tumor - Standard - Level 6
,('3ddf6d79-e8f7-4e18-bf2f-386925942bde', '3c5ee40c-1118-4f09-a733-c19bb73e9f69', FALSE, '7', 'C * 28', '0', 'C + 3', TRUE, TRUE) --Jaws of The Lion - Blood Tumor - Standard - Level 7
,('baf330e0-c3d8-439e-8a8f-1fdeec170eea', '4775e3b4-3ee3-4d66-8d9b-c250700962f3', FALSE, '0', 'C * 10', '2', '3', TRUE, TRUE) --Jaws of The Lion - First Of The Order - Standard - Level 0
,('9c643ddf-a888-4a25-9805-66e54cbcf9eb', '4775e3b4-3ee3-4d66-8d9b-c250700962f3', FALSE, '1', 'C * 14', '2', '4', TRUE, TRUE) --Jaws of The Lion - First Of The Order - Standard - Level 1
,('83c868bf-9e0b-411b-80ff-f750f74a7029', '4775e3b4-3ee3-4d66-8d9b-c250700962f3', FALSE, '2', 'C * 17', '2', '4', TRUE, TRUE) --Jaws of The Lion - First Of The Order - Standard - Level 2
,('ad3cf584-4c76-48d4-a630-b264ec8a9b2a', '4775e3b4-3ee3-4d66-8d9b-c250700962f3', FALSE, '3', 'C * 20', '2', '5', TRUE, TRUE) --Jaws of The Lion - First Of The Order - Standard - Level 3
,('3526fd3b-6c0a-4abc-a19e-ec966745cfc3', '4775e3b4-3ee3-4d66-8d9b-c250700962f3', FALSE, '4', 'C * 24', '3', '5', TRUE, TRUE) --Jaws of The Lion - First Of The Order - Standard - Level 4
,('69e52834-f7b0-48ec-9293-080182f42e3b', '4775e3b4-3ee3-4d66-8d9b-c250700962f3', FALSE, '5', 'C * 28', '3', '6', TRUE, TRUE) --Jaws of The Lion - First Of The Order - Standard - Level 5
,('cc2b9a36-6518-48f5-ad4f-f3e7ff6a5a89', '4775e3b4-3ee3-4d66-8d9b-c250700962f3', FALSE, '6', 'C * 32', '3', '7', TRUE, TRUE) --Jaws of The Lion - First Of The Order - Standard - Level 6
,('dec4b990-fb0d-4e6b-a5df-9296d00325e5', '4775e3b4-3ee3-4d66-8d9b-c250700962f3', FALSE, '7', 'C * 36', '3', '8', TRUE, TRUE) --Jaws of The Lion - First Of The Order - Standard - Level 7
,('05ac4103-b890-f93a-bace-a528a49b4e61', '7e7f370b-eab5-ded7-11e8-227a04a4f283', FALSE, '0', '3', '1', '1', TRUE, TRUE) --Jaws of The Lion - Black Imp - Standard - Level 0
,('213b19ce-caf9-3c5d-842c-28b712a18b13', '7e7f370b-eab5-ded7-11e8-227a04a4f283', FALSE, '1', '4', '1', '1', TRUE, TRUE) --Jaws of The Lion - Black Imp - Standard - Level 1
,('e981b583-563b-e41e-23be-8a78afad3e70', '7e7f370b-eab5-ded7-11e8-227a04a4f283', FALSE, '2', '5', '1', '1', TRUE, TRUE) --Jaws of The Lion - Black Imp - Standard - Level 2
,('b03e0c27-6cbd-6da6-d1bb-025dd267ddd9', '7e7f370b-eab5-ded7-11e8-227a04a4f283', FALSE, '3', '5', '1', '2', TRUE, TRUE) --Jaws of The Lion - Black Imp - Standard - Level 3
,('5af26922-1e1f-965e-2eca-644b112c4415', '7e7f370b-eab5-ded7-11e8-227a04a4f283', FALSE, '4', '74', '1', '2', TRUE, TRUE) --Jaws of The Lion - Black Imp - Standard - Level 4
,('f1d84540-a7a5-2c0b-c39b-4a8b3e3bb39a', '7e7f370b-eab5-ded7-11e8-227a04a4f283', FALSE, '5', '9', '1', '2', TRUE, TRUE) --Jaws of The Lion - Black Imp - Standard - Level 5
,('729186cf-cd3f-6ddf-3841-b409081fa274', '7e7f370b-eab5-ded7-11e8-227a04a4f283', FALSE, '6', '10', '1', '3', TRUE, TRUE) --Jaws of The Lion - Black Imp - Standard - Level 6
,('861ec268-9726-b12d-db06-2574bfbef56a', '7e7f370b-eab5-ded7-11e8-227a04a4f283', FALSE, '7', '13', '1', '3', TRUE, TRUE) --Jaws of The Lion - Black Imp - Standard - Level 7
,('466c942b-2646-fd20-b64e-0daf748ab3d6', '7e7f370b-eab5-ded7-11e8-227a04a4f283', TRUE, '0', '4', '1', '2', TRUE, TRUE) --Jaws of The Lion - Black Imp - Elite - Level 0
,('b3b6ff71-c904-5f37-819e-8c9f00ac0763', '7e7f370b-eab5-ded7-11e8-227a04a4f283', TRUE, '1', '6', '1', '2', TRUE, TRUE) --Jaws of The Lion - Black Imp - Elite - Level 1
,('9650b42a-345d-160e-de14-ba807edaa72b', '7e7f370b-eab5-ded7-11e8-227a04a4f283', TRUE, '2', '8', '1', '2', TRUE, TRUE) --Jaws of The Lion - Black Imp - Elite - Level 2
,('26300d09-a752-2561-e7c3-ed012446a4b5', '7e7f370b-eab5-ded7-11e8-227a04a4f283', TRUE, '3', '8', '1', '3', TRUE, TRUE) --Jaws of The Lion - Black Imp - Elite - Level 3
,('72ac5762-defd-0acf-af37-a525e65491bd', '7e7f370b-eab5-ded7-11e8-227a04a4f283', TRUE, '4', '11', '1', '3', TRUE, TRUE) --Jaws of The Lion - Black Imp - Elite - Level 4
,('e4530315-2031-a83b-a024-6c1f5f503b4e', '7e7f370b-eab5-ded7-11e8-227a04a4f283', TRUE, '5', '14', '1', '3', TRUE, TRUE) --Jaws of The Lion - Black Imp - Elite - Level 5
,('da937599-a7f0-c06f-7a12-0321d801fb94', '7e7f370b-eab5-ded7-11e8-227a04a4f283', TRUE, '6', '15', '1', '4', TRUE, TRUE) --Jaws of The Lion - Black Imp - Elite - Level 6
,('ec943b8b-f0a3-0981-1bb7-31edfab2b02f', '7e7f370b-eab5-ded7-11e8-227a04a4f283', TRUE, '7', '19', '1', '4', TRUE, TRUE) --Jaws of The Lion - Black Imp - Elite - Level 7
,('a8b3927c-3ad7-4109-9d08-8e5fa0987b45', '9462d916-3512-4692-a797-d2861d42c4b9', FALSE, '0', '4', '1', '2', TRUE, TRUE) --Jaws of The Lion - Black Sludge - Standard - Level 0
,('6c2decba-31f5-4029-bbd5-f9ce17f998da', '9462d916-3512-4692-a797-d2861d42c4b9', FALSE, '1', '5', '1', '2', TRUE, TRUE) --Jaws of The Lion - Black Sludge - Standard - Level 1
,('22eef8ba-9775-4859-aa22-98e0f9f57ccc', '9462d916-3512-4692-a797-d2861d42c4b9', FALSE, '2', '7', '1', '2', TRUE, TRUE) --Jaws of The Lion - Black Sludge - Standard - Level 2
,('ac7934c8-a614-48a3-8907-501f1cc303e2', '9462d916-3512-4692-a797-d2861d42c4b9', FALSE, '3', '8', '1', '3', TRUE, TRUE) --Jaws of The Lion - Black Sludge - Standard - Level 3
,('daba4a73-d718-492d-bb51-c0093e195d71', '9462d916-3512-4692-a797-d2861d42c4b9', FALSE, '4', '9', '2', '3', TRUE, TRUE) --Jaws of The Lion - Black Sludge - Standard - Level 4
,('725e2771-83ec-4079-a242-13d0d5764cb9', '9462d916-3512-4692-a797-d2861d42c4b9', FALSE, '5', '10', '2', '3', TRUE, TRUE) --Jaws of The Lion - Black Sludge - Standard - Level 5
,('0b1a0437-f9c5-48bf-a7ca-44753816edf5', '9462d916-3512-4692-a797-d2861d42c4b9', FALSE, '6', '12', '2', '4', TRUE, TRUE) --Jaws of The Lion - Black Sludge - Standard - Level 6
,('64c31849-040f-4b05-a2e2-60701b7191a0', '9462d916-3512-4692-a797-d2861d42c4b9', FALSE, '7', '16', '2', '4', TRUE, TRUE) --Jaws of The Lion - Black Sludge - Standard - Level 7
,('703ab4aa-141e-4ddf-a11b-304c7de64b23', '9462d916-3512-4692-a797-d2861d42c4b9', TRUE, '0', '8', '1', '2', TRUE, TRUE) --Jaws of The Lion - Black Sludge - Elite - Level 0
,('2f49c845-c624-4243-a117-54b007580dd7', '9462d916-3512-4692-a797-d2861d42c4b9', TRUE, '1', '9', '1', '2', TRUE, TRUE) --Jaws of The Lion - Black Sludge - Elite - Level 1
,('f7268cf2-9d86-4d41-8958-906bf98d7b28', '9462d916-3512-4692-a797-d2861d42c4b9', TRUE, '2', '11', '1', '2', TRUE, TRUE) --Jaws of The Lion - Black Sludge - Elite - Level 2
,('03791852-b149-4c3e-b163-4cc8be39172d', '9462d916-3512-4692-a797-d2861d42c4b9', TRUE, '3', '11', '2', '3', TRUE, TRUE) --Jaws of The Lion - Black Sludge - Elite - Level 3
,('02cbda8d-a850-46df-84a3-888fe7b1f3d5', '9462d916-3512-4692-a797-d2861d42c4b9', TRUE, '4', '13', '2', '4', TRUE, TRUE) --Jaws of The Lion - Black Sludge - Elite - Level 4
,('fa028ad0-5937-4d12-b586-806a92e2ebc2', '9462d916-3512-4692-a797-d2861d42c4b9', TRUE, '5', '15', '3', '4', TRUE, TRUE) --Jaws of The Lion - Black Sludge - Elite - Level 5
,('334dc3de-4926-4970-ac8f-09325af9f031', '9462d916-3512-4692-a797-d2861d42c4b9', TRUE, '6', '16', '3', '4', TRUE, TRUE) --Jaws of The Lion - Black Sludge - Elite - Level 6
,('655dc49b-b24e-4b6f-81f0-9aeb7db5d4d0', '9462d916-3512-4692-a797-d2861d42c4b9', TRUE, '7', '18', '3', '5', TRUE, TRUE) --Jaws of The Lion - Black Sludge - Elite - Level 7
,('c7888f48-c491-77b8-4411-22e33c362be6', 'c18c7770-0c09-9e46-3b56-8d7015eed6e4', FALSE, '0', '3', '2', '1', TRUE, TRUE) --Jaws of The Lion - Blood Imp - Standard - Level 0
,('86e46e9f-f336-6996-25a1-61b9b9ea06dc', 'c18c7770-0c09-9e46-3b56-8d7015eed6e4', FALSE, '1', '4', '2', '1', TRUE, TRUE) --Jaws of The Lion - Blood Imp - Standard - Level 1
,('95cd864f-16bd-d64a-b529-e4660654f6d5', 'c18c7770-0c09-9e46-3b56-8d7015eed6e4', FALSE, '2', '5', '3', '1', TRUE, TRUE) --Jaws of The Lion - Blood Imp - Standard - Level 2
,('e79eb90d-e293-1073-870d-2b4e9905e9e0', 'c18c7770-0c09-9e46-3b56-8d7015eed6e4', FALSE, '3', '5', '3', '2', TRUE, TRUE) --Jaws of The Lion - Blood Imp - Standard - Level 3
,('cdd64a26-2021-22e7-9902-dd4503767033', 'c18c7770-0c09-9e46-3b56-8d7015eed6e4', FALSE, '4', '7', '3', '2', TRUE, TRUE) --Jaws of The Lion - Blood Imp - Standard - Level 4
,('3d6ed739-03c1-c7e6-80a8-a4a43e57de18', 'c18c7770-0c09-9e46-3b56-8d7015eed6e4', FALSE, '5', '8', '4', '2', TRUE, TRUE) --Jaws of The Lion - Blood Imp - Standard - Level 5
,('995ec60c-abc6-d30a-17c9-2551cc2a48aa', 'c18c7770-0c09-9e46-3b56-8d7015eed6e4', FALSE, '6', '9', '4', '3', TRUE, TRUE) --Jaws of The Lion - Blood Imp - Standard - Level 6
,('ad344817-5be8-ae1b-675e-558a19c34ae7', 'c18c7770-0c09-9e46-3b56-8d7015eed6e4', FALSE, '7', '12', '4', '3', TRUE, TRUE) --Jaws of The Lion - Blood Imp - Standard - Level 7
,('8364cdb0-9c6e-d42c-2d26-9c4973a30a12', 'c18c7770-0c09-9e46-3b56-8d7015eed6e4', TRUE, '0', '4', '2', '2', TRUE, TRUE) --Jaws of The Lion - Blood Imp - Elite - Level 0
,('568d8b5a-1169-ed0e-9571-c80ac1dcc598', 'c18c7770-0c09-9e46-3b56-8d7015eed6e4', TRUE, '1', '6', '2', '2', TRUE, TRUE) --Jaws of The Lion - Blood Imp - Elite - Level 1
,('65f09f40-05d8-f222-6406-584540868c0e', 'c18c7770-0c09-9e46-3b56-8d7015eed6e4', TRUE, '2', '7', '3', '2', TRUE, TRUE) --Jaws of The Lion - Blood Imp - Elite - Level 2
,('566e9ca6-3985-4a8b-6fda-59363e2aef05', 'c18c7770-0c09-9e46-3b56-8d7015eed6e4', TRUE, '3', '10', '3', '2', TRUE, TRUE) --Jaws of The Lion - Blood Imp - Elite - Level 3
,('7983e416-58c4-13b2-9e66-aa879090f5e3', 'c18c7770-0c09-9e46-3b56-8d7015eed6e4', TRUE, '4', '11', '3', '3', TRUE, TRUE) --Jaws of The Lion - Blood Imp - Elite - Level 4
,('c3032197-b6c4-72a3-0030-f8ef751bad62', 'c18c7770-0c09-9e46-3b56-8d7015eed6e4', TRUE, '5', '13', '4', '3', TRUE, TRUE) --Jaws of The Lion - Blood Imp - Elite - Level 5
,('da03f0c1-999e-7900-8acf-5edb62bf5ff5', 'c18c7770-0c09-9e46-3b56-8d7015eed6e4', TRUE, '6', '17', '4', '3', TRUE, TRUE) --Jaws of The Lion - Blood Imp - Elite - Level 6
,('703addb5-d906-ecae-5791-2770708e7602', 'c18c7770-0c09-9e46-3b56-8d7015eed6e4', TRUE, '7', '21', '4', '4', TRUE, TRUE) --Jaws of The Lion - Blood Imp - Elite - Level 7
,('04d14299-955f-8c38-8709-2553cddaee83', '91610c35-e291-8e88-f7ba-f487649ca947', FALSE, '0', '7', '2', '2', TRUE, TRUE) --Jaws of The Lion - Blood Monstrosity - Standard - Level 0
,('b6d5836c-6df6-cd9f-7ac6-a2cb91bef6fc', '91610c35-e291-8e88-f7ba-f487649ca947', FALSE, '1', '9', '2', '2', TRUE, TRUE) --Jaws of The Lion - Blood Monstrosity - Standard - Level 1
,('da937dc6-6711-8fc0-999f-571fcd43b335', '91610c35-e291-8e88-f7ba-f487649ca947', FALSE, '2', '10', '2', '3', TRUE, TRUE) --Jaws of The Lion - Blood Monstrosity - Standard - Level 2
,('611186c0-eaa4-0f3a-6cd4-e6fecd9fe40d', '91610c35-e291-8e88-f7ba-f487649ca947', FALSE, '3', '12', '3', '3', TRUE, TRUE) --Jaws of The Lion - Blood Monstrosity - Standard - Level 3
,('2bb5367e-8a12-8f7d-5495-465136b53e7f', '91610c35-e291-8e88-f7ba-f487649ca947', FALSE, '4', '12', '3', '3', TRUE, TRUE) --Jaws of The Lion - Blood Monstrosity - Standard - Level 4
,('84d60ec9-2bd6-dcd0-4e9a-595b15876937', '91610c35-e291-8e88-f7ba-f487649ca947', FALSE, '5', '13', '3', '4', TRUE, TRUE) --Jaws of The Lion - Blood Monstrosity - Standard - Level 5
,('101ad56f-2202-77ed-8eb3-3edde9a1d01a', '91610c35-e291-8e88-f7ba-f487649ca947', FALSE, '6', '17', '3', '4', TRUE, TRUE) --Jaws of The Lion - Blood Monstrosity - Standard - Level 6
,('3e79d278-4ab5-f7db-33a7-9c51a731101b', '91610c35-e291-8e88-f7ba-f487649ca947', FALSE, '7', '20', '3', '5', TRUE, TRUE) --Jaws of The Lion - Blood Monstrosity - Standard - Level 7
,('9fafddb1-b700-4cf0-d783-06ea4f4fb44c', '91610c35-e291-8e88-f7ba-f487649ca947', TRUE, '0', '12', '2', '3', TRUE, TRUE) --Jaws of The Lion - Blood Monstrosity - Elite - Level 0
,('2ba16433-a5aa-ba33-2e6a-ebc38f47b226', '91610c35-e291-8e88-f7ba-f487649ca947', TRUE, '1', '12', '2', '3', TRUE, TRUE) --Jaws of The Lion - Blood Monstrosity - Elite - Level 1
,('64f58e8b-80f2-f625-d99e-fd15247e8f57', '91610c35-e291-8e88-f7ba-f487649ca947', TRUE, '2', '15', '2', '4', TRUE, TRUE) --Jaws of The Lion - Blood Monstrosity - Elite - Level 2
,('495ae278-5184-47b3-cc7c-82832c2f75ec', '91610c35-e291-8e88-f7ba-f487649ca947', TRUE, '3', '18', '3', '4', TRUE, TRUE) --Jaws of The Lion - Blood Monstrosity - Elite - Level 3
,('fbf4e487-45d6-0e10-b1b3-a743f22f134f', '91610c35-e291-8e88-f7ba-f487649ca947', TRUE, '4', '18', '3', '4', TRUE, TRUE) --Jaws of The Lion - Blood Monstrosity - Elite - Level 4
,('4a444dbc-01c3-a651-6e4d-349de6d90afd', '91610c35-e291-8e88-f7ba-f487649ca947', TRUE, '5', '20', '3', '5', TRUE, TRUE) --Jaws of The Lion - Blood Monstrosity - Elite - Level 5
,('81306dea-f035-0d1c-84d1-fabb038fdd07', '91610c35-e291-8e88-f7ba-f487649ca947', TRUE, '6', '23', '3', '6', TRUE, TRUE) --Jaws of The Lion - Blood Monstrosity - Elite - Level 6
,('5ee01bf2-ceff-3fdd-c4ef-32754c458303', '91610c35-e291-8e88-f7ba-f487649ca947', TRUE, '7', '23', '3', '6', TRUE, TRUE) --Jaws of The Lion - Blood Monstrosity - Elite - Level 7
,('f5580f7a-0d1a-0551-f821-1b4c72a20b0a', 'a3d6ad55-ba4c-ab9b-83bd-1dd2aa8c1b3f', FALSE, '0', '7', '3', '2', TRUE, TRUE) --Jaws of The Lion - Chaos Demon - Standard - Level 0
,('06d975a9-c866-af97-ccd1-39af332c6a9e', 'a3d6ad55-ba4c-ab9b-83bd-1dd2aa8c1b3f', FALSE, '1', '8', '3', '3', TRUE, TRUE) --Jaws of The Lion - Chaos Demon - Standard - Level 1
,('9a3ec149-3a14-e6c6-3e35-49fb505fa7e4', 'a3d6ad55-ba4c-ab9b-83bd-1dd2aa8c1b3f', FALSE, '2', '11', '3', '3', TRUE, TRUE) --Jaws of The Lion - Chaos Demon - Standard - Level 2
,('8171c1eb-9fbd-77dd-9cc8-346aec57dc3d', 'a3d6ad55-ba4c-ab9b-83bd-1dd2aa8c1b3f', FALSE, '3', '12', '3', '4', TRUE, TRUE) --Jaws of The Lion - Chaos Demon - Standard - Level 3
,('3bff934f-ccbd-feab-ae63-5e6dd42bcf56', 'a3d6ad55-ba4c-ab9b-83bd-1dd2aa8c1b3f', FALSE, '4', '15', '4', '4', TRUE, TRUE) --Jaws of The Lion - Chaos Demon - Standard - Level 4
,('e61f5084-19c3-2f4a-f810-a74a4b76acc3', 'a3d6ad55-ba4c-ab9b-83bd-1dd2aa8c1b3f', FALSE, '5', '16', '4', '5', TRUE, TRUE) --Jaws of The Lion - Chaos Demon - Standard - Level 5
,('8e578566-bcee-7755-5e18-335ec2ece224', 'a3d6ad55-ba4c-ab9b-83bd-1dd2aa8c1b3f', FALSE, '6', '20', '4', '5', TRUE, TRUE) --Jaws of The Lion - Chaos Demon - Standard - Level 6
,('b4c028b6-505a-7cdf-320d-f1965720585c', 'a3d6ad55-ba4c-ab9b-83bd-1dd2aa8c1b3f', FALSE, '7', '22', '4', '6', TRUE, TRUE) --Jaws of The Lion - Chaos Demon - Standard - Level 7
,('c3b19dde-134d-b1df-5594-a8ffd022fcba', 'a3d6ad55-ba4c-ab9b-83bd-1dd2aa8c1b3f', TRUE, '0', '10', '4', '3', TRUE, TRUE) --Jaws of The Lion - Chaos Demon - Elite - Level 0
,('b89c8f08-0382-a763-549e-3a3f99cf0a43', 'a3d6ad55-ba4c-ab9b-83bd-1dd2aa8c1b3f', TRUE, '1', '12', '4', '4', TRUE, TRUE) --Jaws of The Lion - Chaos Demon - Elite - Level 1
,('ec8c81c5-27db-afba-a99f-1ecbff039282', 'a3d6ad55-ba4c-ab9b-83bd-1dd2aa8c1b3f', TRUE, '2', '14', '4', '4', TRUE, TRUE) --Jaws of The Lion - Chaos Demon - Elite - Level 2
,('fce5b4c9-444b-9f09-4b38-bd8c978f59bc', 'a3d6ad55-ba4c-ab9b-83bd-1dd2aa8c1b3f', TRUE, '3', '18', '5', '5', TRUE, TRUE) --Jaws of The Lion - Chaos Demon - Elite - Level 3
,('052933ac-7607-8b87-2ced-78cf41e6b1a9', 'a3d6ad55-ba4c-ab9b-83bd-1dd2aa8c1b3f', TRUE, '4', '21', '5', '6', TRUE, TRUE) --Jaws of The Lion - Chaos Demon - Elite - Level 4
,('2a4e2e1e-5248-79bb-cce6-be44bed38bc5', 'a3d6ad55-ba4c-ab9b-83bd-1dd2aa8c1b3f', TRUE, '5', '26', '5', '6', TRUE, TRUE) --Jaws of The Lion - Chaos Demon - Elite - Level 5
,('3950f9cc-68c9-f652-0a7f-f1c89ba56bdf', 'a3d6ad55-ba4c-ab9b-83bd-1dd2aa8c1b3f', TRUE, '6', '30', '5', '7', TRUE, TRUE) --Jaws of The Lion - Chaos Demon - Elite - Level 6
,('c73c1be5-eaf6-04a3-bb97-ec9ae16d2fa2', 'a3d6ad55-ba4c-ab9b-83bd-1dd2aa8c1b3f', TRUE, '7', '35', '5', '8', TRUE, TRUE) --Jaws of The Lion - Chaos Demon - Elite - Level 7
,('cec73721-3c25-4dec-baf8-5a8572f690b7', '75b4da15-43a4-46da-a410-6f90e8f9e9db', FALSE, '0', '2', '2', '1', TRUE, TRUE) --Jaws of The Lion - Giant Viper - Standard - Level 0
,('886716df-8792-4738-8732-3d427b24e655', '75b4da15-43a4-46da-a410-6f90e8f9e9db', FALSE, '1', '3', '2', '1', TRUE, TRUE) --Jaws of The Lion - Giant Viper - Standard - Level 1
,('696aeb13-b176-4e7f-af59-ec9ac739a7ea', '75b4da15-43a4-46da-a410-6f90e8f9e9db', FALSE, '2', '4', '3', '1', TRUE, TRUE) --Jaws of The Lion - Giant Viper - Standard - Level 2
,('ab18e350-f139-42a7-ad7e-89db32bd384f', '75b4da15-43a4-46da-a410-6f90e8f9e9db', FALSE, '3', '4', '3', '2', TRUE, TRUE) --Jaws of The Lion - Giant Viper - Standard - Level 3
,('f74c3482-2a4a-4e34-8834-d75e309b024d', '75b4da15-43a4-46da-a410-6f90e8f9e9db', FALSE, '4', '6', '3', '2', TRUE, TRUE) --Jaws of The Lion - Giant Viper - Standard - Level 4
,('fd5a799a-3dd1-4498-9143-0a7493ce45df', '75b4da15-43a4-46da-a410-6f90e8f9e9db', FALSE, '5', '7', '3', '3', TRUE, TRUE) --Jaws of The Lion - Giant Viper - Standard - Level 5
,('309aa084-7b3a-45f1-aa93-38efcd4abead', '75b4da15-43a4-46da-a410-6f90e8f9e9db', FALSE, '6', '8', '4', '3', TRUE, TRUE) --Jaws of The Lion - Giant Viper - Standard - Level 6
,('4f70db4f-6d01-49c5-9e49-cbe5188aa4ff', '75b4da15-43a4-46da-a410-6f90e8f9e9db', FALSE, '7', '10', '4', '3', TRUE, TRUE) --Jaws of The Lion - Giant Viper - Standard - Level 7
,('e57351ea-ada0-4070-9038-d36cd85baa62', '75b4da15-43a4-46da-a410-6f90e8f9e9db', TRUE, '0', '3', '2', '2', TRUE, TRUE) --Jaws of The Lion - Giant Viper - Elite - Level 0
,('05e328af-b0ce-46bb-9bc5-864377bf3c63', '75b4da15-43a4-46da-a410-6f90e8f9e9db', TRUE, '1', '5', '2', '2', TRUE, TRUE) --Jaws of The Lion - Giant Viper - Elite - Level 1
,('36eebd0c-e40d-4216-a00c-902bcc8634b5', '75b4da15-43a4-46da-a410-6f90e8f9e9db', TRUE, '2', '7', '3', '2', TRUE, TRUE) --Jaws of The Lion - Giant Viper - Elite - Level 2
,('09ad2203-2505-4ab7-88ad-bab52dcc1eeb', '75b4da15-43a4-46da-a410-6f90e8f9e9db', TRUE, '3', '8', '3', '3', TRUE, TRUE) --Jaws of The Lion - Giant Viper - Elite - Level 3
,('4f41c2cb-0ad2-4469-bda4-d8bc9ec0d032', '75b4da15-43a4-46da-a410-6f90e8f9e9db', TRUE, '4', '11', '3', '3', TRUE, TRUE) --Jaws of The Lion - Giant Viper - Elite - Level 4
,('f9741e49-a9f4-4e82-b6ba-7c776cf09e9e', '75b4da15-43a4-46da-a410-6f90e8f9e9db', TRUE, '5', '13', '4', '3', TRUE, TRUE) --Jaws of The Lion - Giant Viper - Elite - Level 5
,('2a546c99-9ab4-4f75-ac20-0c966d9ee786', '75b4da15-43a4-46da-a410-6f90e8f9e9db', TRUE, '6', '14', '4', '4', TRUE, TRUE) --Jaws of The Lion - Giant Viper - Elite - Level 6
,('12250e57-0598-4535-b8ca-65c96c586b02', '75b4da15-43a4-46da-a410-6f90e8f9e9db', TRUE, '7', '18', '4', '4', TRUE, TRUE) --Jaws of The Lion - Giant Viper - Elite - Level 7
,('ec1792eb-ec03-f782-f29c-4dbd86128c17', '1c153051-3d56-693a-d5a3-153bf86d1315', FALSE, '0', '5', '1', '3', TRUE, TRUE) --Jaws of The Lion - Living Corpse - Standard - Level 0
,('9ee4ac44-d9a3-b576-9a4c-7758139a5e2d', '1c153051-3d56-693a-d5a3-153bf86d1315', FALSE, '1', '7', '1', '3', TRUE, TRUE) --Jaws of The Lion - Living Corpse - Standard - Level 1
,('5d2a90d9-59fb-b49d-0c92-8a6d1258eed1', '1c153051-3d56-693a-d5a3-153bf86d1315', FALSE, '2', '9', '1', '3', TRUE, TRUE) --Jaws of The Lion - Living Corpse - Standard - Level 2
,('99728f62-a308-40bd-6570-964cc4a9e62a', '1c153051-3d56-693a-d5a3-153bf86d1315', FALSE, '3', '10', '1', '4', TRUE, TRUE) --Jaws of The Lion - Living Corpse - Standard - Level 3
,('6f9b2f88-d9c5-57dc-0871-052142c60b73', '1c153051-3d56-693a-d5a3-153bf86d1315', FALSE, '4', '11', '2', '4', TRUE, TRUE) --Jaws of The Lion - Living Corpse - Standard - Level 4
,('58eb9cf6-1faa-28a9-9aad-a0f2aeee4998', '1c153051-3d56-693a-d5a3-153bf86d1315', FALSE, '5', '13', '2', '4', TRUE, TRUE) --Jaws of The Lion - Living Corpse - Standard - Level 5
,('b79bebac-00ef-873b-5965-a9fe60964390', '1c153051-3d56-693a-d5a3-153bf86d1315', FALSE, '6', '14', '2', '4', TRUE, TRUE) --Jaws of The Lion - Living Corpse - Standard - Level 6
,('b58f993d-719b-7435-a8c5-8deb315d94e2', '1c153051-3d56-693a-d5a3-153bf86d1315', FALSE, '7', '16', '2', '5', TRUE, TRUE) --Jaws of The Lion - Living Corpse - Standard - Level 7
,('bdd21241-1a88-44cc-7e13-8a0938819c73', '1c153051-3d56-693a-d5a3-153bf86d1315', TRUE, '0', '10', '1', '3', TRUE, TRUE) --Jaws of The Lion - Living Corpse - Elite - Level 0
,('b4a37902-374a-a89e-d8c3-3dc09a40ca79', '1c153051-3d56-693a-d5a3-153bf86d1315', TRUE, '1', '11', '1', '4', TRUE, TRUE) --Jaws of The Lion - Living Corpse - Elite - Level 1
,('6fa75695-2efd-b0bd-0602-b36703b8e7f9', '1c153051-3d56-693a-d5a3-153bf86d1315', TRUE, '2', '14', '1', '4', TRUE, TRUE) --Jaws of The Lion - Living Corpse - Elite - Level 2
,('05767b0b-b381-5995-6ad0-afe2889ab8db', '1c153051-3d56-693a-d5a3-153bf86d1315', TRUE, '3', '14', '2', '5', TRUE, TRUE) --Jaws of The Lion - Living Corpse - Elite - Level 3
,('e66b14b3-e3bb-e39e-e442-a89ea41c6c83', '1c153051-3d56-693a-d5a3-153bf86d1315', TRUE, '4', '16', '2', '5', TRUE, TRUE) --Jaws of The Lion - Living Corpse - Elite - Level 4
,('0ef7b0a8-3782-1454-6e56-c1771ff76771', '1c153051-3d56-693a-d5a3-153bf86d1315', TRUE, '5', '18', '2', '6', TRUE, TRUE) --Jaws of The Lion - Living Corpse - Elite - Level 5
,('72ccf30d-fb8c-c04f-2831-167166b92c60', '1c153051-3d56-693a-d5a3-153bf86d1315', TRUE, '6', '23', '2', '6', TRUE, TRUE) --Jaws of The Lion - Living Corpse - Elite - Level 6
,('0c25406f-923e-60e2-94d9-3ac68687411b', '1c153051-3d56-693a-d5a3-153bf86d1315', TRUE, '7', '29', '2', '6', TRUE, TRUE) --Jaws of The Lion - Living Corpse - Elite - Level 7
,('71cf7cf3-c3d4-53b3-df15-cdcf971c8629', 'fa9f27a0-b6ca-60a2-327f-bf700a4e5d29', FALSE, '0', '3', '2', '2', TRUE, TRUE) --Jaws of The Lion - Living Spirit - Standard - Level 0
,('dd9ebc9a-c26b-a359-470a-3941959649a4', 'fa9f27a0-b6ca-60a2-327f-bf700a4e5d29', FALSE, '1', '4', '2', '2', TRUE, TRUE) --Jaws of The Lion - Living Spirit - Standard - Level 1
,('561c380e-4745-f68d-8ccd-d73d3d67a621', 'fa9f27a0-b6ca-60a2-327f-bf700a4e5d29', FALSE, '2', '5', '3', '2', TRUE, TRUE) --Jaws of The Lion - Living Spirit - Standard - Level 2
,('b5585874-2b84-47bd-c76c-90451c45b3e8', 'fa9f27a0-b6ca-60a2-327f-bf700a4e5d29', FALSE, '3', '6', '3', '3', TRUE, TRUE) --Jaws of The Lion - Living Spirit - Standard - Level 3
,('5a8a19ab-5958-8744-49e6-587e4a212c52', 'fa9f27a0-b6ca-60a2-327f-bf700a4e5d29', FALSE, '4', '6', '3', '3', TRUE, TRUE) --Jaws of The Lion - Living Spirit - Standard - Level 4
,('67fea66b-ea33-c98c-a889-b0809ee76997', 'fa9f27a0-b6ca-60a2-327f-bf700a4e5d29', FALSE, '5', '8', '3', '3', TRUE, TRUE) --Jaws of The Lion - Living Spirit - Standard - Level 5
,('0ba50875-d314-5fbd-1ee7-da61a4e90ddd', 'fa9f27a0-b6ca-60a2-327f-bf700a4e5d29', FALSE, '6', '9', '3', '4', TRUE, TRUE) --Jaws of The Lion - Living Spirit - Standard - Level 6
,('9c2ffc85-651f-0a50-700f-056b3e3f3568', 'fa9f27a0-b6ca-60a2-327f-bf700a4e5d29', FALSE, '7', '12', '3', '4', TRUE, TRUE) --Jaws of The Lion - Living Spirit - Standard - Level 7
,('c184e538-d448-165f-6732-625ff84e6c22', 'fa9f27a0-b6ca-60a2-327f-bf700a4e5d29', TRUE, '0', '5', '3', '3', TRUE, TRUE) --Jaws of The Lion - Living Spirit - Elite - Level 0
,('1f570c4d-d73c-0166-556c-c5632a0aeac9', 'fa9f27a0-b6ca-60a2-327f-bf700a4e5d29', TRUE, '1', '5', '3', '3', TRUE, TRUE) --Jaws of The Lion - Living Spirit - Elite - Level 1
,('54161a46-d97f-6e96-c3bf-0fca9fd3570f', 'fa9f27a0-b6ca-60a2-327f-bf700a4e5d29', TRUE, '2', '7', '4', '3', TRUE, TRUE) --Jaws of The Lion - Living Spirit - Elite - Level 2
,('524a14e8-7d9d-e0fc-fa2b-c853c240747d', 'fa9f27a0-b6ca-60a2-327f-bf700a4e5d29', TRUE, '3', '8', '4', '4', TRUE, TRUE) --Jaws of The Lion - Living Spirit - Elite - Level 3
,('e06d9a0b-9a7e-927d-b8be-644e11a7f0cc', 'fa9f27a0-b6ca-60a2-327f-bf700a4e5d29', TRUE, '4', '8', '4', '4', TRUE, TRUE) --Jaws of The Lion - Living Spirit - Elite - Level 4
,('bbc76362-0e0f-f76a-64c4-7983987caaf7', 'fa9f27a0-b6ca-60a2-327f-bf700a4e5d29', TRUE, '5', '11', '4', '4', TRUE, TRUE) --Jaws of The Lion - Living Spirit - Elite - Level 5
,('2f624227-5679-5a5d-dc2c-6a50bb47fa78', 'fa9f27a0-b6ca-60a2-327f-bf700a4e5d29', TRUE, '6', '13', '4', '5', TRUE, TRUE) --Jaws of The Lion - Living Spirit - Elite - Level 6
,('031f7089-ac23-6568-4304-a1d98caf21b8', 'fa9f27a0-b6ca-60a2-327f-bf700a4e5d29', TRUE, '7', '17', '4', '5', TRUE, TRUE) --Jaws of The Lion - Living Spirit - Elite - Level 7
,('30aac56c-9d4f-2716-437e-4ca73d0e1a9d', '89d6080f-3ff1-ca9d-900c-70651091a11a', FALSE, '0', '4', '1', '1', TRUE, TRUE) --Jaws of The Lion - Rat Monstrosity - Standard - Level 0
,('adfc81b1-3c4e-f0fe-ee51-d77e248d9f48', '89d6080f-3ff1-ca9d-900c-70651091a11a', FALSE, '1', '4', '1', '2', TRUE, TRUE) --Jaws of The Lion - Rat Monstrosity - Standard - Level 1
,('cc2e6eb5-67ce-a800-2ab5-90be793bf7a5', '89d6080f-3ff1-ca9d-900c-70651091a11a', FALSE, '2', '5', '2', '2', TRUE, TRUE) --Jaws of The Lion - Rat Monstrosity - Standard - Level 2
,('5e1ccc50-bec7-8070-1d6f-2f06c21f95bd', '89d6080f-3ff1-ca9d-900c-70651091a11a', FALSE, '3', '6', '2', '3', TRUE, TRUE) --Jaws of The Lion - Rat Monstrosity - Standard - Level 3
,('3eb95261-628f-c422-bd2e-d42b31f5f74c', '89d6080f-3ff1-ca9d-900c-70651091a11a', FALSE, '4', '8', '2', '3', TRUE, TRUE) --Jaws of The Lion - Rat Monstrosity - Standard - Level 4
,('5298d826-21d1-8ae9-1071-dff47dccf12b', '89d6080f-3ff1-ca9d-900c-70651091a11a', FALSE, '5', '10', '3', '3', TRUE, TRUE) --Jaws of The Lion - Rat Monstrosity - Standard - Level 5
,('f55b9e84-bae6-dbb0-8d59-863505580810', '89d6080f-3ff1-ca9d-900c-70651091a11a', FALSE, '6', '12', '3', '3', TRUE, TRUE) --Jaws of The Lion - Rat Monstrosity - Standard - Level 6
,('48e15926-ea48-2067-42ac-dc6566498cc2', '89d6080f-3ff1-ca9d-900c-70651091a11a', FALSE, '7', '12', '3', '4', TRUE, TRUE) --Jaws of The Lion - Rat Monstrosity - Standard - Level 7
,('39911167-d318-8dc6-c56b-0e378f337d32', '89d6080f-3ff1-ca9d-900c-70651091a11a', TRUE, '0', '6', '1', '2', TRUE, TRUE) --Jaws of The Lion - Rat Monstrosity - Elite - Level 0
,('abfc9083-a51d-ceb4-2cd0-63a00037df6b', '89d6080f-3ff1-ca9d-900c-70651091a11a', TRUE, '1', '7', '1', '2', TRUE, TRUE) --Jaws of The Lion - Rat Monstrosity - Elite - Level 1
,('a331d9fb-514b-e837-502f-e9c63f71e8d9', '89d6080f-3ff1-ca9d-900c-70651091a11a', TRUE, '2', '8', '1', '3', TRUE, TRUE) --Jaws of The Lion - Rat Monstrosity - Elite - Level 2
,('f79109df-51db-ac00-f6ef-fe4b7070030b', '89d6080f-3ff1-ca9d-900c-70651091a11a', TRUE, '3', '10', '2', '3', TRUE, TRUE) --Jaws of The Lion - Rat Monstrosity - Elite - Level 3
,('8d92e175-9aca-0bb2-0bd1-9fe6ea517145', '89d6080f-3ff1-ca9d-900c-70651091a11a', TRUE, '4', '12', '2', '3', TRUE, TRUE) --Jaws of The Lion - Rat Monstrosity - Elite - Level 4
,('d63dc2b6-feb0-5e74-d5db-68c8840b3783', '89d6080f-3ff1-ca9d-900c-70651091a11a', TRUE, '5', '13', '2', '4', TRUE, TRUE) --Jaws of The Lion - Rat Monstrosity - Elite - Level 5
,('c8cb3cc1-c2b8-9809-3c5a-7b232b483198', '89d6080f-3ff1-ca9d-900c-70651091a11a', TRUE, '6', '14', '3', '4', TRUE, TRUE) --Jaws of The Lion - Rat Monstrosity - Elite - Level 6
,('68c70ed9-effb-5c77-27ff-17221d6e6b89', '89d6080f-3ff1-ca9d-900c-70651091a11a', TRUE, '7', '16', '3', '5', TRUE, TRUE) --Jaws of The Lion - Rat Monstrosity - Elite - Level 7
,('4b1cf00f-5637-8021-effe-0ae908fac58d', 'c22bac94-b488-924f-a8d6-8770fc530d62', FALSE, '0', '10', '1', '3', TRUE, TRUE) --Jaws of The Lion - Stone Golem - Standard - Level 0
,('996cda0b-0ee2-d22e-bf0e-10643d487d6d', 'c22bac94-b488-924f-a8d6-8770fc530d62', FALSE, '1', '10', '1', '3', TRUE, TRUE) --Jaws of The Lion - Stone Golem - Standard - Level 1
,('fba262b9-745f-2f74-f6b1-7e810b246a09', 'c22bac94-b488-924f-a8d6-8770fc530d62', FALSE, '2', '11', '1', '4', TRUE, TRUE) --Jaws of The Lion - Stone Golem - Standard - Level 2
,('8ed9a6f8-78c6-b2af-e799-9a665d087ed3', 'c22bac94-b488-924f-a8d6-8770fc530d62', FALSE, '3', '11', '1', '4', TRUE, TRUE) --Jaws of The Lion - Stone Golem - Standard - Level 3
,('573454c6-1b58-ff37-1e5c-d52150a19084', 'c22bac94-b488-924f-a8d6-8770fc530d62', FALSE, '4', '12', '2', '4', TRUE, TRUE) --Jaws of The Lion - Stone Golem - Standard - Level 4
,('80712257-505e-ecae-a197-dfd19793a705', 'c22bac94-b488-924f-a8d6-8770fc530d62', FALSE, '5', '13', '2', '5', TRUE, TRUE) --Jaws of The Lion - Stone Golem - Standard - Level 5
,('be810fee-e207-8375-c015-98897d148095', 'c22bac94-b488-924f-a8d6-8770fc530d62', FALSE, '6', '16', '2', '5', TRUE, TRUE) --Jaws of The Lion - Stone Golem - Standard - Level 6
,('2d8657d9-fa76-cab4-0f1d-2c06450d007d', 'c22bac94-b488-924f-a8d6-8770fc530d62', FALSE, '7', '16', '2', '5', TRUE, TRUE) --Jaws of The Lion - Stone Golem - Standard - Level 7
,('d12efba7-8788-445f-0284-7bd9c56ce304', 'c22bac94-b488-924f-a8d6-8770fc530d62', TRUE, '0', '10', '2', '4', TRUE, TRUE) --Jaws of The Lion - Stone Golem - Elite - Level 0
,('a39e0067-6abd-0c6a-2d4a-5972186143a5', 'c22bac94-b488-924f-a8d6-8770fc530d62', TRUE, '1', '10', '2', '4', TRUE, TRUE) --Jaws of The Lion - Stone Golem - Elite - Level 1
,('cb93b4ed-b54d-ac25-b23f-b2576a8d16dd', 'c22bac94-b488-924f-a8d6-8770fc530d62', TRUE, '2', '13', '2', '5', TRUE, TRUE) --Jaws of The Lion - Stone Golem - Elite - Level 2
,('1166924d-3846-d54e-0387-2a7f0ce07694', 'c22bac94-b488-924f-a8d6-8770fc530d62', TRUE, '3', '14', '2', '5', TRUE, TRUE) --Jaws of The Lion - Stone Golem - Elite - Level 3
,('a8ac821d-9536-7ebe-882f-f03da4059932', 'c22bac94-b488-924f-a8d6-8770fc530d62', TRUE, '4', '16', '2', '6', TRUE, TRUE) --Jaws of The Lion - Stone Golem - Elite - Level 4
,('f167c962-8f0c-c9b0-7edf-0df6db965b60', 'c22bac94-b488-924f-a8d6-8770fc530d62', TRUE, '5', '18', '3', '6', TRUE, TRUE) --Jaws of The Lion - Stone Golem - Elite - Level 5
,('90f15f6e-1531-ff43-291e-125e60e5e852', 'c22bac94-b488-924f-a8d6-8770fc530d62', TRUE, '6', '20', '3', '7', TRUE, TRUE) --Jaws of The Lion - Stone Golem - Elite - Level 6
,('f3a18f7b-2bb1-1e7a-43b1-daa1f339a77e', 'c22bac94-b488-924f-a8d6-8770fc530d62', TRUE, '7', '21', '3', '7', TRUE, TRUE) --Jaws of The Lion - Stone Golem - Elite - Level 7
,('8e6f3033-9c76-4610-a1cf-b0cc6ca92bbc', 'abc857ec-2b99-4189-a2df-b5cfb98c3c31', FALSE, '0', '4', '1', '2', TRUE, TRUE) --Jaws of The Lion - Vermling Raider - Standard - Level 0
,('3f688cce-e988-45e3-bc74-a6e0a62c9a42', 'abc857ec-2b99-4189-a2df-b5cfb98c3c31', FALSE, '1', '5', '1', '2', TRUE, TRUE) --Jaws of The Lion - Vermling Raider - Standard - Level 1
,('9487b27b-629e-4e13-b9bc-84604a4a5e82', 'abc857ec-2b99-4189-a2df-b5cfb98c3c31', FALSE, '2', '9', '2', '2', TRUE, TRUE) --Jaws of The Lion - Vermling Raider - Standard - Level 2
,('2b1bba67-6498-4650-8ba4-ece7abb02089', 'abc857ec-2b99-4189-a2df-b5cfb98c3c31', FALSE, '3', '11', '3', '2', TRUE, TRUE) --Jaws of The Lion - Vermling Raider - Standard - Level 3
,('a2fbe2c8-f0ce-46b3-a6ef-509f33c4f9c8', 'abc857ec-2b99-4189-a2df-b5cfb98c3c31', FALSE, '4', '12', '3', '3', TRUE, TRUE) --Jaws of The Lion - Vermling Raider - Standard - Level 4
,('82db9de8-2164-42c7-a583-7e44fe571c7f', 'abc857ec-2b99-4189-a2df-b5cfb98c3c31', FALSE, '5', '15', '3', '3', TRUE, TRUE) --Jaws of The Lion - Vermling Raider - Standard - Level 5
,('f25cfdb3-7fbe-4914-9b0c-3daf9f2d192a', 'abc857ec-2b99-4189-a2df-b5cfb98c3c31', FALSE, '6', '17', '4', '3', TRUE, TRUE) --Jaws of The Lion - Vermling Raider - Standard - Level 6
,('36d3f768-fbc5-4bd0-aa57-358d04bfb80a', 'abc857ec-2b99-4189-a2df-b5cfb98c3c31', FALSE, '7', '17', '4', '3', TRUE, TRUE) --Jaws of The Lion - Vermling Raider - Standard - Level 7
,('e4d85449-5e5b-47c3-954a-2be6cb9a5bd1', 'abc857ec-2b99-4189-a2df-b5cfb98c3c31', TRUE, '0', '8', '1', '2', TRUE, TRUE) --Jaws of The Lion - Vermling Raider - Elite - Level 0
,('9486a3fa-bf05-41fb-b78d-cf2ad25a9729', 'abc857ec-2b99-4189-a2df-b5cfb98c3c31', TRUE, '1', '10', '1', '2', TRUE, TRUE) --Jaws of The Lion - Vermling Raider - Elite - Level 1
,('1d61d956-0c63-4955-985c-61d8f58a0bfc', 'abc857ec-2b99-4189-a2df-b5cfb98c3c31', TRUE, '2', '14', '3', '3', TRUE, TRUE) --Jaws of The Lion - Vermling Raider - Elite - Level 2
,('de472a5e-df0d-41ca-ade0-c0dad46a6844', 'abc857ec-2b99-4189-a2df-b5cfb98c3c31', TRUE, '3', '16', '3', '4', TRUE, TRUE) --Jaws of The Lion - Vermling Raider - Elite - Level 3
,('ff484e4a-f0e8-409a-8440-3c14aba2bd94', 'abc857ec-2b99-4189-a2df-b5cfb98c3c31', TRUE, '4', '19', '4', '4', TRUE, TRUE) --Jaws of The Lion - Vermling Raider - Elite - Level 4
,('ccb178c1-d513-4d21-943d-2bddc0e0d4e8', 'abc857ec-2b99-4189-a2df-b5cfb98c3c31', TRUE, '5', '23', '4', '4', TRUE, TRUE) --Jaws of The Lion - Vermling Raider - Elite - Level 5
,('eef6d2a8-5f53-4e73-bdb3-fcd02f217cd0', 'abc857ec-2b99-4189-a2df-b5cfb98c3c31', TRUE, '6', '27', '4', '5', TRUE, TRUE) --Jaws of The Lion - Vermling Raider - Elite - Level 6
,('6e3e879b-d1a0-4f46-9622-97108a16c1bd', 'abc857ec-2b99-4189-a2df-b5cfb98c3c31', TRUE, '7', '31', '4', '6', TRUE, TRUE) --Jaws of The Lion - Vermling Raider - Elite - Level 7
,('fdab335a-04c2-45f1-bb57-8973f44876b5', '1b1c44d4-417d-4548-ab5c-87116956d51e', FALSE, '0', '2', '3', '1', TRUE, TRUE) --Jaws of The Lion - Vermling Scout - Standard - Level 0
,('d89d7678-06d0-41d9-9219-0cd4e7306e2b', '1b1c44d4-417d-4548-ab5c-87116956d51e', FALSE, '1', '3', '3', '1', TRUE, TRUE) --Jaws of The Lion - Vermling Scout - Standard - Level 1
,('a0808223-6056-4e4b-a920-d82d97e55485', '1b1c44d4-417d-4548-ab5c-87116956d51e', FALSE, '2', '3', '3', '2', TRUE, TRUE) --Jaws of The Lion - Vermling Scout - Standard - Level 2
,('46221224-350f-46d1-b052-ecfb0e9d7917', '1b1c44d4-417d-4548-ab5c-87116956d51e', FALSE, '3', '5', '3', '2', TRUE, TRUE) --Jaws of The Lion - Vermling Scout - Standard - Level 3
,('43be2a95-7c36-4ec2-8246-5fae3f79fd96', '1b1c44d4-417d-4548-ab5c-87116956d51e', FALSE, '4', '6', '3', '3', TRUE, TRUE) --Jaws of The Lion - Vermling Scout - Standard - Level 4
,('392aabea-ed5f-470a-b198-b75466c80b57', '1b1c44d4-417d-4548-ab5c-87116956d51e', FALSE, '5', '8', '3', '3', TRUE, TRUE) --Jaws of The Lion - Vermling Scout - Standard - Level 5
,('9a356363-3c0c-40e7-bd4f-fac0937a18e9', '1b1c44d4-417d-4548-ab5c-87116956d51e', FALSE, '6', '10', '4', '3', TRUE, TRUE) --Jaws of The Lion - Vermling Scout - Standard - Level 6
,('417eeaaa-c2bf-465e-a689-d81c5e5fafd0', '1b1c44d4-417d-4548-ab5c-87116956d51e', FALSE, '7', '13', '4', '3', TRUE, TRUE) --Jaws of The Lion - Vermling Scout - Standard - Level 7
,('759fce60-31a1-4425-869c-8e6bdc5fbe8e', '1b1c44d4-417d-4548-ab5c-87116956d51e', TRUE, '0', '4', '3', '2', TRUE, TRUE) --Jaws of The Lion - Vermling Scout - Elite - Level 0
,('8d77138c-8f47-46ad-af4f-fd8f7acda15c', '1b1c44d4-417d-4548-ab5c-87116956d51e', TRUE, '1', '5', '3', '2', TRUE, TRUE) --Jaws of The Lion - Vermling Scout - Elite - Level 1
,('4d12b50e-c0b5-4b81-84ff-f1310b8b5c7d', '1b1c44d4-417d-4548-ab5c-87116956d51e', TRUE, '2', '5', '4', '3', TRUE, TRUE) --Jaws of The Lion - Vermling Scout - Elite - Level 2
,('0825e8ed-9232-4959-921e-9988f2cb8971', '1b1c44d4-417d-4548-ab5c-87116956d51e', TRUE, '3', '7', '4', '3', TRUE, TRUE) --Jaws of The Lion - Vermling Scout - Elite - Level 3
,('6569f489-3fd8-49f5-bfd1-b200342145c9', '1b1c44d4-417d-4548-ab5c-87116956d51e', TRUE, '4', '8', '4', '4', TRUE, TRUE) --Jaws of The Lion - Vermling Scout - Elite - Level 4
,('640c9169-795a-4b49-9270-d94d4b76b283', '1b1c44d4-417d-4548-ab5c-87116956d51e', TRUE, '5', '11', '4', '4', TRUE, TRUE) --Jaws of The Lion - Vermling Scout - Elite - Level 5
,('6706e0ae-ffa6-479e-bbb5-6ea62d0e286b', '1b1c44d4-417d-4548-ab5c-87116956d51e', TRUE, '6', '13', '5', '4', TRUE, TRUE) --Jaws of The Lion - Vermling Scout - Elite - Level 6
,('c71467f9-53fd-472f-bf84-9d29090f388e', '1b1c44d4-417d-4548-ab5c-87116956d51e', TRUE, '7', '17', '5', '4', TRUE, TRUE) --Jaws of The Lion - Vermling Scout - Elite - Level 7
,('999c0c95-c861-495e-a0bb-d0bf2fee28c8', '01d2606d-8269-4e83-a6c4-4fb870e37ca0', FALSE, '0', '4', '2', '2', TRUE, TRUE) --Jaws of The Lion - Zealot - Standard - Level 0
,('96657015-3059-4a62-a83f-9711d3c51258', '01d2606d-8269-4e83-a6c4-4fb870e37ca0', FALSE, '1', '6', '2', '2', TRUE, TRUE) --Jaws of The Lion - Zealot - Standard - Level 1
,('50becd65-f649-40e4-848a-79950307b194', '01d2606d-8269-4e83-a6c4-4fb870e37ca0', FALSE, '2', '7', '3', '3', TRUE, TRUE) --Jaws of The Lion - Zealot - Standard - Level 2
,('56486fac-ab93-4ca0-8d4e-4a3e72f3edbe', '01d2606d-8269-4e83-a6c4-4fb870e37ca0', FALSE, '3', '8', '3', '3', TRUE, TRUE) --Jaws of The Lion - Zealot - Standard - Level 3
,('7a23e2e9-37cc-40d6-a14f-0e0941a9a237', '01d2606d-8269-4e83-a6c4-4fb870e37ca0', FALSE, '4', '10', '3', '3', TRUE, TRUE) --Jaws of The Lion - Zealot - Standard - Level 4
,('2917c938-13b8-41b1-8e0f-51487f33dae1', '01d2606d-8269-4e83-a6c4-4fb870e37ca0', FALSE, '5', '12', '4', '3', TRUE, TRUE) --Jaws of The Lion - Zealot - Standard - Level 5
,('d43259bf-9e24-4927-a527-ad055c23849d', '01d2606d-8269-4e83-a6c4-4fb870e37ca0', FALSE, '6', '14', '4', '4', TRUE, TRUE) --Jaws of The Lion - Zealot - Standard - Level 6
,('fb6cdd3c-a980-4efb-bc06-e0cfd1a0a02f', '01d2606d-8269-4e83-a6c4-4fb870e37ca0', FALSE, '7', '16', '4', '5', TRUE, TRUE) --Jaws of The Lion - Zealot - Standard - Level 7
,('e2decd41-9702-47f1-b446-8ce67a8213eb', '01d2606d-8269-4e83-a6c4-4fb870e37ca0', TRUE, '0', '7', '2', '3', TRUE, TRUE) --Jaws of The Lion - Zealot - Elite - Level 0
,('2895d7e7-19c5-4394-add4-e073cdbd272a', '01d2606d-8269-4e83-a6c4-4fb870e37ca0', TRUE, '1', '8', '2', '3', TRUE, TRUE) --Jaws of The Lion - Zealot - Elite - Level 1
,('9552e506-6387-494c-bb00-321784890beb', '01d2606d-8269-4e83-a6c4-4fb870e37ca0', TRUE, '2', '11', '3', '3', TRUE, TRUE) --Jaws of The Lion - Zealot - Elite - Level 2
,('ba221868-3db2-4b29-b540-382fc5fce010', '01d2606d-8269-4e83-a6c4-4fb870e37ca0', TRUE, '3', '13', '3', '4', TRUE, TRUE) --Jaws of The Lion - Zealot - Elite - Level 3
,('15d56d6f-26f9-43fe-91e5-f6ecccff9635', '01d2606d-8269-4e83-a6c4-4fb870e37ca0', TRUE, '4', '17', '3', '4', TRUE, TRUE) --Jaws of The Lion - Zealot - Elite - Level 4
,('96da6942-c6f9-4d24-9679-783b3f8205fc', '01d2606d-8269-4e83-a6c4-4fb870e37ca0', TRUE, '5', '18', '4', '5', TRUE, TRUE) --Jaws of The Lion - Zealot - Elite - Level 5
,('b778d7e9-61e0-4173-a383-e5aabc671dc8', '01d2606d-8269-4e83-a6c4-4fb870e37ca0', TRUE, '6', '22', '4', '6', TRUE, TRUE) --Jaws of The Lion - Zealot - Elite - Level 6
,('243dcf9e-e1bd-4e24-aa8b-8d817ad98c55', '01d2606d-8269-4e83-a6c4-4fb870e37ca0', TRUE, '7', '26', '4', '7', TRUE, TRUE) --Jaws of The Lion - Zealot - Elite - Level 7
;

INSERT INTO public."MonsterAttackEffect" ("EffectId", "MonsterStatSetId") VALUES
 ('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', '05ac4103-b890-f93a-bace-a528a49b4e61') --Jaws of The Lion - Black Imp - Standard - Level 0 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', '213b19ce-caf9-3c5d-842c-28b712a18b13') --Jaws of The Lion - Black Imp - Standard - Level 1 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', 'e981b583-563b-e41e-23be-8a78afad3e70') --Jaws of The Lion - Black Imp - Standard - Level 2 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', 'b03e0c27-6cbd-6da6-d1bb-025dd267ddd9') --Jaws of The Lion - Black Imp - Standard - Level 3 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', '5af26922-1e1f-965e-2eca-644b112c4415') --Jaws of The Lion - Black Imp - Standard - Level 4 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', 'f1d84540-a7a5-2c0b-c39b-4a8b3e3bb39a') --Jaws of The Lion - Black Imp - Standard - Level 5 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', '729186cf-cd3f-6ddf-3841-b409081fa274') --Jaws of The Lion - Black Imp - Standard - Level 6 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', '861ec268-9726-b12d-db06-2574bfbef56a') --Jaws of The Lion - Black Imp - Standard - Level 7 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', '466c942b-2646-fd20-b64e-0daf748ab3d6') --Jaws of The Lion - Black Imp - Elite - Level 0 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', 'b3b6ff71-c904-5f37-819e-8c9f00ac0763') --Jaws of The Lion - Black Imp - Elite - Level 1 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', '9650b42a-345d-160e-de14-ba807edaa72b') --Jaws of The Lion - Black Imp - Elite - Level 2 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', '26300d09-a752-2561-e7c3-ed012446a4b5') --Jaws of The Lion - Black Imp - Elite - Level 3 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', '72ac5762-defd-0acf-af37-a525e65491bd') --Jaws of The Lion - Black Imp - Elite - Level 4 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', 'e4530315-2031-a83b-a024-6c1f5f503b4e') --Jaws of The Lion - Black Imp - Elite - Level 5 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', 'da937599-a7f0-c06f-7a12-0321d801fb94') --Jaws of The Lion - Black Imp - Elite - Level 6 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', 'ec943b8b-f0a3-0981-1bb7-31edfab2b02f') --Jaws of The Lion - Black Imp - Elite - Level 7 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', 'fa028ad0-5937-4d12-b586-806a92e2ebc2') --Jaws of The Lion - Black Sludge - Elite - Level 5 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', '334dc3de-4926-4970-ac8f-09325af9f031') --Jaws of The Lion - Black Sludge - Elite - Level 6 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', '655dc49b-b24e-4b6f-81f0-9aeb7db5d4d0') --Jaws of The Lion - Black Sludge - Elite - Level 7 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', '725e2771-83ec-4079-a242-13d0d5764cb9') --Jaws of The Lion - Black Sludge - Standard - Level 5 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', '0b1a0437-f9c5-48bf-a7ca-44753816edf5') --Jaws of The Lion - Black Sludge - Standard - Level 6 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', '64c31849-040f-4b05-a2e2-60701b7191a0') --Jaws of The Lion - Black Sludge - Standard - Level 7 | Type: poison - Value: -1 - Duration: -1
,('58b68f6b-c976-442d-9b15-c654d6f42a7b', '86e46e9f-f336-6996-25a1-61b9b9ea06dc') --Jaws of The Lion - Blood Imp - Standard - Level 1 | Type: muddle - Value: -1 - Duration: 1
,('58b68f6b-c976-442d-9b15-c654d6f42a7b', '95cd864f-16bd-d64a-b529-e4660654f6d5') --Jaws of The Lion - Blood Imp - Standard - Level 2 | Type: muddle - Value: -1 - Duration: 1
,('58b68f6b-c976-442d-9b15-c654d6f42a7b', 'e79eb90d-e293-1073-870d-2b4e9905e9e0') --Jaws of The Lion - Blood Imp - Standard - Level 3 | Type: muddle - Value: -1 - Duration: 1
,('58b68f6b-c976-442d-9b15-c654d6f42a7b', 'cdd64a26-2021-22e7-9902-dd4503767033') --Jaws of The Lion - Blood Imp - Standard - Level 4 | Type: muddle - Value: -1 - Duration: 1
,('58b68f6b-c976-442d-9b15-c654d6f42a7b', '3d6ed739-03c1-c7e6-80a8-a4a43e57de18') --Jaws of The Lion - Blood Imp - Standard - Level 5 | Type: muddle - Value: -1 - Duration: 1
,('58b68f6b-c976-442d-9b15-c654d6f42a7b', '995ec60c-abc6-d30a-17c9-2551cc2a48aa') --Jaws of The Lion - Blood Imp - Standard - Level 6 | Type: muddle - Value: -1 - Duration: 1
,('58b68f6b-c976-442d-9b15-c654d6f42a7b', 'ad344817-5be8-ae1b-675e-558a19c34ae7') --Jaws of The Lion - Blood Imp - Standard - Level 7 | Type: muddle - Value: -1 - Duration: 1
,('58b68f6b-c976-442d-9b15-c654d6f42a7b', '8364cdb0-9c6e-d42c-2d26-9c4973a30a12') --Jaws of The Lion - Blood Imp - Elite - Level 0 | Type: muddle - Value: -1 - Duration: 1
,('58b68f6b-c976-442d-9b15-c654d6f42a7b', '568d8b5a-1169-ed0e-9571-c80ac1dcc598') --Jaws of The Lion - Blood Imp - Elite - Level 1 | Type: muddle - Value: -1 - Duration: 1
,('58b68f6b-c976-442d-9b15-c654d6f42a7b', '65f09f40-05d8-f222-6406-584540868c0e') --Jaws of The Lion - Blood Imp - Elite - Level 2 | Type: muddle - Value: -1 - Duration: 1
,('58b68f6b-c976-442d-9b15-c654d6f42a7b', '566e9ca6-3985-4a8b-6fda-59363e2aef05') --Jaws of The Lion - Blood Imp - Elite - Level 3 | Type: muddle - Value: -1 - Duration: 1
,('58b68f6b-c976-442d-9b15-c654d6f42a7b', '7983e416-58c4-13b2-9e66-aa879090f5e3') --Jaws of The Lion - Blood Imp - Elite - Level 4 | Type: muddle - Value: -1 - Duration: 1
,('58b68f6b-c976-442d-9b15-c654d6f42a7b', 'c3032197-b6c4-72a3-0030-f8ef751bad62') --Jaws of The Lion - Blood Imp - Elite - Level 5 | Type: muddle - Value: -1 - Duration: 1
,('58b68f6b-c976-442d-9b15-c654d6f42a7b', 'da03f0c1-999e-7900-8acf-5edb62bf5ff5') --Jaws of The Lion - Blood Imp - Elite - Level 6 | Type: muddle - Value: -1 - Duration: 1
,('58b68f6b-c976-442d-9b15-c654d6f42a7b', '703addb5-d906-ecae-5791-2770708e7602') --Jaws of The Lion - Blood Imp - Elite - Level 7 | Type: muddle - Value: -1 - Duration: 1
,('58b68f6b-c976-442d-9b15-c654d6f42a7b', 'f5580f7a-0d1a-0551-f821-1b4c72a20b0a') --Jaws of The Lion - Chaos Demon - Standard - Level 0 | Type: muddle - Value: -1 - Duration: 1
,('58b68f6b-c976-442d-9b15-c654d6f42a7b', '06d975a9-c866-af97-ccd1-39af332c6a9e') --Jaws of The Lion - Chaos Demon - Standard - Level 1 | Type: muddle - Value: -1 - Duration: 1
,('58b68f6b-c976-442d-9b15-c654d6f42a7b', '9a3ec149-3a14-e6c6-3e35-49fb505fa7e4') --Jaws of The Lion - Chaos Demon - Standard - Level 2 | Type: muddle - Value: -1 - Duration: 1
,('58b68f6b-c976-442d-9b15-c654d6f42a7b', '8171c1eb-9fbd-77dd-9cc8-346aec57dc3d') --Jaws of The Lion - Chaos Demon - Standard - Level 3 | Type: muddle - Value: -1 - Duration: 1
,('58b68f6b-c976-442d-9b15-c654d6f42a7b', '3bff934f-ccbd-feab-ae63-5e6dd42bcf56') --Jaws of The Lion - Chaos Demon - Standard - Level 4 | Type: muddle - Value: -1 - Duration: 1
,('58b68f6b-c976-442d-9b15-c654d6f42a7b', 'e61f5084-19c3-2f4a-f810-a74a4b76acc3') --Jaws of The Lion - Chaos Demon - Standard - Level 5 | Type: muddle - Value: -1 - Duration: 1
,('58b68f6b-c976-442d-9b15-c654d6f42a7b', '8e578566-bcee-7755-5e18-335ec2ece224') --Jaws of The Lion - Chaos Demon - Standard - Level 6 | Type: muddle - Value: -1 - Duration: 1
,('58b68f6b-c976-442d-9b15-c654d6f42a7b', 'b4c028b6-505a-7cdf-320d-f1965720585c') --Jaws of The Lion - Chaos Demon - Standard - Level 7 | Type: muddle - Value: -1 - Duration: 1
,('58b68f6b-c976-442d-9b15-c654d6f42a7b', 'c3b19dde-134d-b1df-5594-a8ffd022fcba') --Jaws of The Lion - Chaos Demon - Elite - Level 0 | Type: muddle - Value: -1 - Duration: 1
,('58b68f6b-c976-442d-9b15-c654d6f42a7b', 'b89c8f08-0382-a763-549e-3a3f99cf0a43') --Jaws of The Lion - Chaos Demon - Elite - Level 1 | Type: muddle - Value: -1 - Duration: 1
,('58b68f6b-c976-442d-9b15-c654d6f42a7b', 'ec8c81c5-27db-afba-a99f-1ecbff039282') --Jaws of The Lion - Chaos Demon - Elite - Level 2 | Type: muddle - Value: -1 - Duration: 1
,('58b68f6b-c976-442d-9b15-c654d6f42a7b', 'fce5b4c9-444b-9f09-4b38-bd8c978f59bc') --Jaws of The Lion - Chaos Demon - Elite - Level 3 | Type: muddle - Value: -1 - Duration: 1
,('58b68f6b-c976-442d-9b15-c654d6f42a7b', '052933ac-7607-8b87-2ced-78cf41e6b1a9') --Jaws of The Lion - Chaos Demon - Elite - Level 4 | Type: muddle - Value: -1 - Duration: 1
,('58b68f6b-c976-442d-9b15-c654d6f42a7b', '2a4e2e1e-5248-79bb-cce6-be44bed38bc5') --Jaws of The Lion - Chaos Demon - Elite - Level 5 | Type: muddle - Value: -1 - Duration: 1
,('58b68f6b-c976-442d-9b15-c654d6f42a7b', '3950f9cc-68c9-f652-0a7f-f1c89ba56bdf') --Jaws of The Lion - Chaos Demon - Elite - Level 6 | Type: muddle - Value: -1 - Duration: 1
,('58b68f6b-c976-442d-9b15-c654d6f42a7b', 'c73c1be5-eaf6-04a3-bb97-ec9ae16d2fa2') --Jaws of The Lion - Chaos Demon - Elite - Level 7 | Type: muddle - Value: -1 - Duration: 1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', 'e57351ea-ada0-4070-9038-d36cd85baa62') --Jaws of The Lion - Giant Viper - Elite - Level 0 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', '05e328af-b0ce-46bb-9bc5-864377bf3c63') --Jaws of The Lion - Giant Viper - Elite - Level 1 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', '36eebd0c-e40d-4216-a00c-902bcc8634b5') --Jaws of The Lion - Giant Viper - Elite - Level 2 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', '09ad2203-2505-4ab7-88ad-bab52dcc1eeb') --Jaws of The Lion - Giant Viper - Elite - Level 3 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', '4f41c2cb-0ad2-4469-bda4-d8bc9ec0d032') --Jaws of The Lion - Giant Viper - Elite - Level 4 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', 'f9741e49-a9f4-4e82-b6ba-7c776cf09e9e') --Jaws of The Lion - Giant Viper - Elite - Level 5 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', '2a546c99-9ab4-4f75-ac20-0c966d9ee786') --Jaws of The Lion - Giant Viper - Elite - Level 6 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', '12250e57-0598-4535-b8ca-65c96c586b02') --Jaws of The Lion - Giant Viper - Elite - Level 7 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', 'cec73721-3c25-4dec-baf8-5a8572f690b7') --Jaws of The Lion - Giant Viper - Standard - Level 0 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', '886716df-8792-4738-8732-3d427b24e655') --Jaws of The Lion - Giant Viper - Standard - Level 1 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', '696aeb13-b176-4e7f-af59-ec9ac739a7ea') --Jaws of The Lion - Giant Viper - Standard - Level 2 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', 'ab18e350-f139-42a7-ad7e-89db32bd384f') --Jaws of The Lion - Giant Viper - Standard - Level 3 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', 'f74c3482-2a4a-4e34-8834-d75e309b024d') --Jaws of The Lion - Giant Viper - Standard - Level 4 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', 'fd5a799a-3dd1-4498-9143-0a7493ce45df') --Jaws of The Lion - Giant Viper - Standard - Level 5 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', '309aa084-7b3a-45f1-aa93-38efcd4abead') --Jaws of The Lion - Giant Viper - Standard - Level 6 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', '4f70db4f-6d01-49c5-9e49-cbe5188aa4ff') --Jaws of The Lion - Giant Viper - Standard - Level 7 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', 'b79bebac-00ef-873b-5965-a9fe60964390') --Jaws of The Lion - Living Corpse - Standard - Level 6 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', 'b58f993d-719b-7435-a8c5-8deb315d94e2') --Jaws of The Lion - Living Corpse - Standard - Level 7 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', 'e66b14b3-e3bb-e39e-e442-a89ea41c6c83') --Jaws of The Lion - Living Corpse - Elite - Level 4 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', '0ef7b0a8-3782-1454-6e56-c1771ff76771') --Jaws of The Lion - Living Corpse - Elite - Level 5 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', '72ccf30d-fb8c-c04f-2831-167166b92c60') --Jaws of The Lion - Living Corpse - Elite - Level 6 | Type: poison - Value: -1 - Duration: -1
,('0b11aaf8-a41f-4c87-a1bb-8b1021220b00', '0c25406f-923e-60e2-94d9-3ac68687411b') --Jaws of The Lion - Living Corpse - Elite - Level 7 | Type: poison - Value: -1 - Duration: -1
,('eb92eff2-35ed-4e1a-9480-f527445f3ef4', '2895d7e7-19c5-4394-add4-e073cdbd272a') --Jaws of The Lion - Zealot - Elite - Level 1 | Type: wound - Value: -1 - Duration: -1
,('eb92eff2-35ed-4e1a-9480-f527445f3ef4', '9552e506-6387-494c-bb00-321784890beb') --Jaws of The Lion - Zealot - Elite - Level 2 | Type: wound - Value: -1 - Duration: -1
,('eb92eff2-35ed-4e1a-9480-f527445f3ef4', 'ba221868-3db2-4b29-b540-382fc5fce010') --Jaws of The Lion - Zealot - Elite - Level 3 | Type: wound - Value: -1 - Duration: -1
,('eb92eff2-35ed-4e1a-9480-f527445f3ef4', '15d56d6f-26f9-43fe-91e5-f6ecccff9635') --Jaws of The Lion - Zealot - Elite - Level 4 | Type: wound - Value: -1 - Duration: -1
,('eb92eff2-35ed-4e1a-9480-f527445f3ef4', '96da6942-c6f9-4d24-9679-783b3f8205fc') --Jaws of The Lion - Zealot - Elite - Level 5 | Type: wound - Value: -1 - Duration: -1
,('eb92eff2-35ed-4e1a-9480-f527445f3ef4', 'b778d7e9-61e0-4173-a383-e5aabc671dc8') --Jaws of The Lion - Zealot - Elite - Level 6 | Type: wound - Value: -1 - Duration: -1
,('eb92eff2-35ed-4e1a-9480-f527445f3ef4', '243dcf9e-e1bd-4e24-aa8b-8d817ad98c55') --Jaws of The Lion - Zealot - Elite - Level 7 | Type: wound - Value: -1 - Duration: -1
,('eb92eff2-35ed-4e1a-9480-f527445f3ef4', '56486fac-ab93-4ca0-8d4e-4a3e72f3edbe') --Jaws of The Lion - Zealot - Standard - Level 3 | Type: wound - Value: -1 - Duration: -1
,('eb92eff2-35ed-4e1a-9480-f527445f3ef4', '7a23e2e9-37cc-40d6-a14f-0e0941a9a237') --Jaws of The Lion - Zealot - Standard - Level 4 | Type: wound - Value: -1 - Duration: -1
,('eb92eff2-35ed-4e1a-9480-f527445f3ef4', '2917c938-13b8-41b1-8e0f-51487f33dae1') --Jaws of The Lion - Zealot - Standard - Level 5 | Type: wound - Value: -1 - Duration: -1
,('eb92eff2-35ed-4e1a-9480-f527445f3ef4', 'd43259bf-9e24-4927-a527-ad055c23849d') --Jaws of The Lion - Zealot - Standard - Level 6 | Type: wound - Value: -1 - Duration: -1
,('eb92eff2-35ed-4e1a-9480-f527445f3ef4', 'fb6cdd3c-a980-4efb-bc06-e0cfd1a0a02f') --Jaws of The Lion - Zealot - Standard - Level 7 | Type: wound - Value: -1 - Duration: -1
;

INSERT INTO public."MonsterDefenseEffect" ("EffectId", "MonsterStatSetId") VALUES
 ('0ac1f965-80e8-bbec-69b4-128f0a6fb320', '26300d09-a752-2561-e7c3-ed012446a4b5') --Jaws of The Lion - Black Imp - Elite - Level 3 | Type: disadvantage - Value: -1 - Duration: -1
,('0ac1f965-80e8-bbec-69b4-128f0a6fb320', '72ac5762-defd-0acf-af37-a525e65491bd') --Jaws of The Lion - Black Imp - Elite - Level 4 | Type: disadvantage - Value: -1 - Duration: -1
,('0ac1f965-80e8-bbec-69b4-128f0a6fb320', 'e4530315-2031-a83b-a024-6c1f5f503b4e') --Jaws of The Lion - Black Imp - Elite - Level 5 | Type: disadvantage - Value: -1 - Duration: -1
,('0ac1f965-80e8-bbec-69b4-128f0a6fb320', 'da937599-a7f0-c06f-7a12-0321d801fb94') --Jaws of The Lion - Black Imp - Elite - Level 6 | Type: disadvantage - Value: -1 - Duration: -1
,('0ac1f965-80e8-bbec-69b4-128f0a6fb320', 'ec943b8b-f0a3-0981-1bb7-31edfab2b02f') --Jaws of The Lion - Black Imp - Elite - Level 7 | Type: disadvantage - Value: -1 - Duration: -1
,('6c407028-6e98-47da-bcc1-47b3e7cb8066', '2f49c845-c624-4243-a117-54b007580dd7') --Jaws of The Lion - Black Sludge - Elite - Level 1 | Type: shield - Value: 1 - Duration: -1
,('6c407028-6e98-47da-bcc1-47b3e7cb8066', 'f7268cf2-9d86-4d41-8958-906bf98d7b28') --Jaws of The Lion - Black Sludge - Elite - Level 2 | Type: shield - Value: 1 - Duration: -1
,('6c407028-6e98-47da-bcc1-47b3e7cb8066', '03791852-b149-4c3e-b163-4cc8be39172d') --Jaws of The Lion - Black Sludge - Elite - Level 3 | Type: shield - Value: 1 - Duration: -1
,('6c407028-6e98-47da-bcc1-47b3e7cb8066', '02cbda8d-a850-46df-84a3-888fe7b1f3d5') --Jaws of The Lion - Black Sludge - Elite - Level 4 | Type: shield - Value: 1 - Duration: -1
,('6c407028-6e98-47da-bcc1-47b3e7cb8066', 'fa028ad0-5937-4d12-b586-806a92e2ebc2') --Jaws of The Lion - Black Sludge - Elite - Level 5 | Type: shield - Value: 1 - Duration: -1
,('6c407028-6e98-47da-bcc1-47b3e7cb8066', '334dc3de-4926-4970-ac8f-09325af9f031') --Jaws of The Lion - Black Sludge - Elite - Level 6 | Type: shield - Value: 1 - Duration: -1
,('6c407028-6e98-47da-bcc1-47b3e7cb8066', '655dc49b-b24e-4b6f-81f0-9aeb7db5d4d0') --Jaws of The Lion - Black Sludge - Elite - Level 7 | Type: shield - Value: 1 - Duration: -1
,('6c407028-6e98-47da-bcc1-47b3e7cb8066', '6c2decba-31f5-4029-bbd5-f9ce17f998da') --Jaws of The Lion - Black Sludge - Standard - Level 1 | Type: shield - Value: 1 - Duration: -1
,('6c407028-6e98-47da-bcc1-47b3e7cb8066', '22eef8ba-9775-4859-aa22-98e0f9f57ccc') --Jaws of The Lion - Black Sludge - Standard - Level 2 | Type: shield - Value: 1 - Duration: -1
,('6c407028-6e98-47da-bcc1-47b3e7cb8066', 'ac7934c8-a614-48a3-8907-501f1cc303e2') --Jaws of The Lion - Black Sludge - Standard - Level 3 | Type: shield - Value: 1 - Duration: -1
,('6c407028-6e98-47da-bcc1-47b3e7cb8066', 'daba4a73-d718-492d-bb51-c0093e195d71') --Jaws of The Lion - Black Sludge - Standard - Level 4 | Type: shield - Value: 1 - Duration: -1
,('6c407028-6e98-47da-bcc1-47b3e7cb8066', '725e2771-83ec-4079-a242-13d0d5764cb9') --Jaws of The Lion - Black Sludge - Standard - Level 5 | Type: shield - Value: 1 - Duration: -1
,('6c407028-6e98-47da-bcc1-47b3e7cb8066', '0b1a0437-f9c5-48bf-a7ca-44753816edf5') --Jaws of The Lion - Black Sludge - Standard - Level 6 | Type: shield - Value: 1 - Duration: -1
,('6c407028-6e98-47da-bcc1-47b3e7cb8066', '64c31849-040f-4b05-a2e2-60701b7191a0') --Jaws of The Lion - Black Sludge - Standard - Level 7 | Type: shield - Value: 1 - Duration: -1
,('6c407028-6e98-47da-bcc1-47b3e7cb8066', '2bb5367e-8a12-8f7d-5495-465136b53e7f') --Jaws of The Lion - Blood Monstrosity - Standard - Level 4 | Type: shield - Value: 1 - Duration: -1
,('6c407028-6e98-47da-bcc1-47b3e7cb8066', '84d60ec9-2bd6-dcd0-4e9a-595b15876937') --Jaws of The Lion - Blood Monstrosity - Standard - Level 5 | Type: shield - Value: 1 - Duration: -1
,('6c407028-6e98-47da-bcc1-47b3e7cb8066', '101ad56f-2202-77ed-8eb3-3edde9a1d01a') --Jaws of The Lion - Blood Monstrosity - Standard - Level 6 | Type: shield - Value: 1 - Duration: -1
,('6c407028-6e98-47da-bcc1-47b3e7cb8066', '3e79d278-4ab5-f7db-33a7-9c51a731101b') --Jaws of The Lion - Blood Monstrosity - Standard - Level 7 | Type: shield - Value: 1 - Duration: -1
,('6c407028-6e98-47da-bcc1-47b3e7cb8066', '2ba16433-a5aa-ba33-2e6a-ebc38f47b226') --Jaws of The Lion - Blood Monstrosity - Elite - Level 1 | Type: shield - Value: 1 - Duration: -1
,('6c407028-6e98-47da-bcc1-47b3e7cb8066', '64f58e8b-80f2-f625-d99e-fd15247e8f57') --Jaws of The Lion - Blood Monstrosity - Elite - Level 2 | Type: shield - Value: 1 - Duration: -1
,('6c407028-6e98-47da-bcc1-47b3e7cb8066', '495ae278-5184-47b3-cc7c-82832c2f75ec') --Jaws of The Lion - Blood Monstrosity - Elite - Level 3 | Type: shield - Value: 1 - Duration: -1
,('b41f91aa-5d40-0935-6545-876f4c6213e7', 'fbf4e487-45d6-0e10-b1b3-a743f22f134f') --Jaws of The Lion - Blood Monstrosity - Elite - Level 4 | Type: shield - Value: 2 - Duration: -1
,('b41f91aa-5d40-0935-6545-876f4c6213e7', '4a444dbc-01c3-a651-6e4d-349de6d90afd') --Jaws of The Lion - Blood Monstrosity - Elite - Level 5 | Type: shield - Value: 2 - Duration: -1
,('b41f91aa-5d40-0935-6545-876f4c6213e7', '81306dea-f035-0d1c-84d1-fabb038fdd07') --Jaws of The Lion - Blood Monstrosity - Elite - Level 6 | Type: shield - Value: 2 - Duration: -1
,('15e43b68-8db9-5781-559e-24c6bee9fbf1', '5ee01bf2-ceff-3fdd-c4ef-32754c458303') --Jaws of The Lion - Blood Monstrosity - Elite - Level 7 | Type: shield - Value: 3 - Duration: -1
,('6c407028-6e98-47da-bcc1-47b3e7cb8066', '71cf7cf3-c3d4-53b3-df15-cdcf971c8629') --Jaws of The Lion - Living Spirit - Standard - Level 0 | Type: shield - Value: 1 - Duration: -1
,('6c407028-6e98-47da-bcc1-47b3e7cb8066', 'dd9ebc9a-c26b-a359-470a-3941959649a4') --Jaws of The Lion - Living Spirit - Standard - Level 1 | Type: shield - Value: 1 - Duration: -1
,('6c407028-6e98-47da-bcc1-47b3e7cb8066', '561c380e-4745-f68d-8ccd-d73d3d67a621') --Jaws of The Lion - Living Spirit - Standard - Level 2 | Type: shield - Value: 1 - Duration: -1
,('6c407028-6e98-47da-bcc1-47b3e7cb8066', 'b5585874-2b84-47bd-c76c-90451c45b3e8') --Jaws of The Lion - Living Spirit - Standard - Level 3 | Type: shield - Value: 1 - Duration: -1
,('b41f91aa-5d40-0935-6545-876f4c6213e7', '5a8a19ab-5958-8744-49e6-587e4a212c52') --Jaws of The Lion - Living Spirit - Standard - Level 4 | Type: shield - Value: 2 - Duration: -1
,('b41f91aa-5d40-0935-6545-876f4c6213e7', '67fea66b-ea33-c98c-a889-b0809ee76997') --Jaws of The Lion - Living Spirit - Standard - Level 5 | Type: shield - Value: 2 - Duration: -1
,('b41f91aa-5d40-0935-6545-876f4c6213e7', '0ba50875-d314-5fbd-1ee7-da61a4e90ddd') --Jaws of The Lion - Living Spirit - Standard - Level 6 | Type: shield - Value: 2 - Duration: -1
,('b41f91aa-5d40-0935-6545-876f4c6213e7', '9c2ffc85-651f-0a50-700f-056b3e3f3568') --Jaws of The Lion - Living Spirit - Standard - Level 7 | Type: shield - Value: 2 - Duration: -1
,('6c407028-6e98-47da-bcc1-47b3e7cb8066', 'c184e538-d448-165f-6732-625ff84e6c22') --Jaws of The Lion - Living Spirit - Elite - Level 0 | Type: shield - Value: 1 - Duration: -1
,('b41f91aa-5d40-0935-6545-876f4c6213e7', '1f570c4d-d73c-0166-556c-c5632a0aeac9') --Jaws of The Lion - Living Spirit - Elite - Level 1 | Type: shield - Value: 2 - Duration: -1
,('b41f91aa-5d40-0935-6545-876f4c6213e7', '54161a46-d97f-6e96-c3bf-0fca9fd3570f') --Jaws of The Lion - Living Spirit - Elite - Level 2 | Type: shield - Value: 2 - Duration: -1
,('b41f91aa-5d40-0935-6545-876f4c6213e7', '524a14e8-7d9d-e0fc-fa2b-c853c240747d') --Jaws of The Lion - Living Spirit - Elite - Level 3 | Type: shield - Value: 2 - Duration: -1
,('15e43b68-8db9-5781-559e-24c6bee9fbf1', 'e06d9a0b-9a7e-927d-b8be-644e11a7f0cc') --Jaws of The Lion - Living Spirit - Elite - Level 4 | Type: shield - Value: 3 - Duration: -1
,('15e43b68-8db9-5781-559e-24c6bee9fbf1', 'bbc76362-0e0f-f76a-64c4-7983987caaf7') --Jaws of The Lion - Living Spirit - Elite - Level 5 | Type: shield - Value: 3 - Duration: -1
,('15e43b68-8db9-5781-559e-24c6bee9fbf1', '2f624227-5679-5a5d-dc2c-6a50bb47fa78') --Jaws of The Lion - Living Spirit - Elite - Level 6 | Type: shield - Value: 3 - Duration: -1
,('15e43b68-8db9-5781-559e-24c6bee9fbf1', '031f7089-ac23-6568-4304-a1d98caf21b8') --Jaws of The Lion - Living Spirit - Elite - Level 7 | Type: shield - Value: 3 - Duration: -1
,('6c407028-6e98-47da-bcc1-47b3e7cb8066', '996cda0b-0ee2-d22e-bf0e-10643d487d6d') --Jaws of The Lion - Stone Golem - Standard - Level 1 | Type: shield - Value: 1 - Duration: -1
,('6c407028-6e98-47da-bcc1-47b3e7cb8066', 'fba262b9-745f-2f74-f6b1-7e810b246a09') --Jaws of The Lion - Stone Golem - Standard - Level 2 | Type: shield - Value: 1 - Duration: -1
,('b41f91aa-5d40-0935-6545-876f4c6213e7', '8ed9a6f8-78c6-b2af-e799-9a665d087ed3') --Jaws of The Lion - Stone Golem - Standard - Level 3 | Type: shield - Value: 2 - Duration: -1
,('b41f91aa-5d40-0935-6545-876f4c6213e7', '573454c6-1b58-ff37-1e5c-d52150a19084') --Jaws of The Lion - Stone Golem - Standard - Level 4 | Type: shield - Value: 2 - Duration: -1
,('b41f91aa-5d40-0935-6545-876f4c6213e7', '80712257-505e-ecae-a197-dfd19793a705') --Jaws of The Lion - Stone Golem - Standard - Level 5 | Type: shield - Value: 2 - Duration: -1
,('b41f91aa-5d40-0935-6545-876f4c6213e7', 'be810fee-e207-8375-c015-98897d148095') --Jaws of The Lion - Stone Golem - Standard - Level 6 | Type: shield - Value: 2 - Duration: -1
,('15e43b68-8db9-5781-559e-24c6bee9fbf1', '2d8657d9-fa76-cab4-0f1d-2c06450d007d') --Jaws of The Lion - Stone Golem - Standard - Level 7 | Type: shield - Value: 3 - Duration: -1
,('6c407028-6e98-47da-bcc1-47b3e7cb8066', 'd12efba7-8788-445f-0284-7bd9c56ce304') --Jaws of The Lion - Stone Golem - Elite - Level 0 | Type: shield - Value: 1 - Duration: -1
,('b41f91aa-5d40-0935-6545-876f4c6213e7', 'a39e0067-6abd-0c6a-2d4a-5972186143a5') --Jaws of The Lion - Stone Golem - Elite - Level 1 | Type: shield - Value: 2 - Duration: -1
,('b41f91aa-5d40-0935-6545-876f4c6213e7', 'cb93b4ed-b54d-ac25-b23f-b2576a8d16dd') --Jaws of The Lion - Stone Golem - Elite - Level 2 | Type: shield - Value: 2 - Duration: -1
,('15e43b68-8db9-5781-559e-24c6bee9fbf1', '1166924d-3846-d54e-0387-2a7f0ce07694') --Jaws of The Lion - Stone Golem - Elite - Level 3 | Type: shield - Value: 3 - Duration: -1
,('15e43b68-8db9-5781-559e-24c6bee9fbf1', 'a8ac821d-9536-7ebe-882f-f03da4059932') --Jaws of The Lion - Stone Golem - Elite - Level 4 | Type: shield - Value: 3 - Duration: -1
,('15e43b68-8db9-5781-559e-24c6bee9fbf1', 'f167c962-8f0c-c9b0-7edf-0df6db965b60') --Jaws of The Lion - Stone Golem - Elite - Level 5 | Type: shield - Value: 3 - Duration: -1
,('15e43b68-8db9-5781-559e-24c6bee9fbf1', '90f15f6e-1531-ff43-291e-125e60e5e852') --Jaws of The Lion - Stone Golem - Elite - Level 6 | Type: shield - Value: 3 - Duration: -1
,('84d679fa-3745-577a-4351-d0edd839d327', 'f3a18f7b-2bb1-1e7a-43b1-daa1f339a77e') --Jaws of The Lion - Stone Golem - Elite - Level 7 | Type: shield - Value: 4 - Duration: -1
;

INSERT INTO public."MonsterDeathEffect" ("EffectId", "MonsterStatSetId") VALUES
 ('ae850bb6-26fb-0d22-684d-0ab3d0f43280', '04d14299-955f-8c38-8709-2553cddaee83') --Jaws of The Lion - Blood Monstrosity - Standard - Level 0 | Type: damage - Value: 1 - Duration: -1 - Range: 1
,('04977de7-6ea7-fff7-4e69-de45cbac49b5', 'b6d5836c-6df6-cd9f-7ac6-a2cb91bef6fc') --Jaws of The Lion - Blood Monstrosity - Standard - Level 1 | Type: damage - Value: 2 - Duration: -1 - Range: 1
,('04977de7-6ea7-fff7-4e69-de45cbac49b5', 'da937dc6-6711-8fc0-999f-571fcd43b335') --Jaws of The Lion - Blood Monstrosity - Standard - Level 2 | Type: damage - Value: 2 - Duration: -1 - Range: 1
,('fbbb6c2c-10f1-879f-edbb-2fb95d7bd7a5', '611186c0-eaa4-0f3a-6cd4-e6fecd9fe40d') --Jaws of The Lion - Blood Monstrosity - Standard - Level 3 | Type: damage - Value: 3 - Duration: -1 - Range: 1
,('fbbb6c2c-10f1-879f-edbb-2fb95d7bd7a5', '2bb5367e-8a12-8f7d-5495-465136b53e7f') --Jaws of The Lion - Blood Monstrosity - Standard - Level 4 | Type: damage - Value: 3 - Duration: -1 - Range: 1
,('fbbb6c2c-10f1-879f-edbb-2fb95d7bd7a5', '84d60ec9-2bd6-dcd0-4e9a-595b15876937') --Jaws of The Lion - Blood Monstrosity - Standard - Level 5 | Type: damage - Value: 3 - Duration: -1 - Range: 1
,('fbbb6c2c-10f1-879f-edbb-2fb95d7bd7a5', '101ad56f-2202-77ed-8eb3-3edde9a1d01a') --Jaws of The Lion - Blood Monstrosity - Standard - Level 6 | Type: damage - Value: 3 - Duration: -1 - Range: 1
,('2d692b7f-9f97-37ff-b622-1a2071027053', '3e79d278-4ab5-f7db-33a7-9c51a731101b') --Jaws of The Lion - Blood Monstrosity - Standard - Level 7 | Type: damage - Value: 4 - Duration: -1 - Range: 1
,('04977de7-6ea7-fff7-4e69-de45cbac49b5', '9fafddb1-b700-4cf0-d783-06ea4f4fb44c') --Jaws of The Lion - Blood Monstrosity - Elite - Level 0 | Type: damage - Value: 2 - Duration: -1 - Range: 1
,('fbbb6c2c-10f1-879f-edbb-2fb95d7bd7a5', '2ba16433-a5aa-ba33-2e6a-ebc38f47b226') --Jaws of The Lion - Blood Monstrosity - Elite - Level 1 | Type: damage - Value: 3 - Duration: -1 - Range: 1
,('fbbb6c2c-10f1-879f-edbb-2fb95d7bd7a5', '64f58e8b-80f2-f625-d99e-fd15247e8f57') --Jaws of The Lion - Blood Monstrosity - Elite - Level 2 | Type: damage - Value: 3 - Duration: -1 - Range: 1
,('2d692b7f-9f97-37ff-b622-1a2071027053', '495ae278-5184-47b3-cc7c-82832c2f75ec') --Jaws of The Lion - Blood Monstrosity - Elite - Level 3 | Type: damage - Value: 4 - Duration: -1 - Range: 1
,('2d692b7f-9f97-37ff-b622-1a2071027053', 'fbf4e487-45d6-0e10-b1b3-a743f22f134f') --Jaws of The Lion - Blood Monstrosity - Elite - Level 4 | Type: damage - Value: 4 - Duration: -1 - Range: 1
,('26f9d0a7-f71e-e17b-0cf6-af7cb914d29d', '4a444dbc-01c3-a651-6e4d-349de6d90afd') --Jaws of The Lion - Blood Monstrosity - Elite - Level 5 | Type: damage - Value: 5 - Duration: -1 - Range: 1
,('26f9d0a7-f71e-e17b-0cf6-af7cb914d29d', '81306dea-f035-0d1c-84d1-fabb038fdd07') --Jaws of The Lion - Blood Monstrosity - Elite - Level 6 | Type: damage - Value: 5 - Duration: -1 - Range: 1
,('26f9d0a7-f71e-e17b-0cf6-af7cb914d29d', '5ee01bf2-ceff-3fdd-c4ef-32754c458303') --Jaws of The Lion - Blood Monstrosity - Elite - Level 7 | Type: damage - Value: 5 - Duration: -1 - Range: 1
,('ae850bb6-26fb-0d22-684d-0ab3d0f43280', '30aac56c-9d4f-2716-437e-4ca73d0e1a9d') --Jaws of The Lion - Rat Monstrosity - Standard - Level 0 | Type: damage - Value: 1 - Duration: -1 - Range: 1
,('ae850bb6-26fb-0d22-684d-0ab3d0f43280', 'adfc81b1-3c4e-f0fe-ee51-d77e248d9f48') --Jaws of The Lion - Rat Monstrosity - Standard - Level 1 | Type: damage - Value: 1 - Duration: -1 - Range: 1
,('ae850bb6-26fb-0d22-684d-0ab3d0f43280', 'cc2e6eb5-67ce-a800-2ab5-90be793bf7a5') --Jaws of The Lion - Rat Monstrosity - Standard - Level 2 | Type: damage - Value: 1 - Duration: -1 - Range: 1
,('04977de7-6ea7-fff7-4e69-de45cbac49b5', '5e1ccc50-bec7-8070-1d6f-2f06c21f95bd') --Jaws of The Lion - Rat Monstrosity - Standard - Level 3 | Type: damage - Value: 2 - Duration: -1 - Range: 1
,('04977de7-6ea7-fff7-4e69-de45cbac49b5', '3eb95261-628f-c422-bd2e-d42b31f5f74c') --Jaws of The Lion - Rat Monstrosity - Standard - Level 4 | Type: damage - Value: 2 - Duration: -1 - Range: 1
,('04977de7-6ea7-fff7-4e69-de45cbac49b5', '5298d826-21d1-8ae9-1071-dff47dccf12b') --Jaws of The Lion - Rat Monstrosity - Standard - Level 5 | Type: damage - Value: 2 - Duration: -1 - Range: 1
,('fbbb6c2c-10f1-879f-edbb-2fb95d7bd7a5', 'f55b9e84-bae6-dbb0-8d59-863505580810') --Jaws of The Lion - Rat Monstrosity - Standard - Level 6 | Type: damage - Value: 3 - Duration: -1 - Range: 1
,('fbbb6c2c-10f1-879f-edbb-2fb95d7bd7a5', '48e15926-ea48-2067-42ac-dc6566498cc2') --Jaws of The Lion - Rat Monstrosity - Standard - Level 7 | Type: damage - Value: 3 - Duration: -1 - Range: 1
,('ae850bb6-26fb-0d22-684d-0ab3d0f43280', '39911167-d318-8dc6-c56b-0e378f337d32') --Jaws of The Lion - Rat Monstrosity - Elite - Level 0 | Type: damage - Value: 1 - Duration: -1 - Range: 1
,('04977de7-6ea7-fff7-4e69-de45cbac49b5', 'abfc9083-a51d-ceb4-2cd0-63a00037df6b') --Jaws of The Lion - Rat Monstrosity - Elite - Level 1 | Type: damage - Value: 2 - Duration: -1 - Range: 1
,('04977de7-6ea7-fff7-4e69-de45cbac49b5', 'a331d9fb-514b-e837-502f-e9c63f71e8d9') --Jaws of The Lion - Rat Monstrosity - Elite - Level 2 | Type: damage - Value: 2 - Duration: -1 - Range: 1
,('04977de7-6ea7-fff7-4e69-de45cbac49b5', 'f79109df-51db-ac00-f6ef-fe4b7070030b') --Jaws of The Lion - Rat Monstrosity - Elite - Level 3 | Type: damage - Value: 2 - Duration: -1 - Range: 1
,('fbbb6c2c-10f1-879f-edbb-2fb95d7bd7a5', '8d92e175-9aca-0bb2-0bd1-9fe6ea517145') --Jaws of The Lion - Rat Monstrosity - Elite - Level 4 | Type: damage - Value: 3 - Duration: -1 - Range: 1
,('fbbb6c2c-10f1-879f-edbb-2fb95d7bd7a5', 'd63dc2b6-feb0-5e74-d5db-68c8840b3783') --Jaws of The Lion - Rat Monstrosity - Elite - Level 5 | Type: damage - Value: 3 - Duration: -1 - Range: 1
,('fbbb6c2c-10f1-879f-edbb-2fb95d7bd7a5', 'c8cb3cc1-c2b8-9809-3c5a-7b232b483198') --Jaws of The Lion - Rat Monstrosity - Elite - Level 6 | Type: damage - Value: 3 - Duration: -1 - Range: 1
,('2d692b7f-9f97-37ff-b622-1a2071027053', '68c70ed9-effb-5c77-27ff-17221d6e6b89') --Jaws of The Lion - Rat Monstrosity - Elite - Level 7 | Type: damage - Value: 4 - Duration: -1 - Range: 1
;


INSERT INTO public."MonsterBaseStatImmunity" ("MonsterStatSetId", "Effect") VALUES
 ('f512e56e-2067-4df3-9894-1650664d8a93', 'disarm') --Jaws of The Lion - Blood Horror - Standard - Level 0 | disarm
,('f512e56e-2067-4df3-9894-1650664d8a93', 'immobilize') --Jaws of The Lion - Blood Horror - Standard - Level 0 | immobilize
,('f512e56e-2067-4df3-9894-1650664d8a93', 'muddle') --Jaws of The Lion - Blood Horror - Standard - Level 0 | muddle
,('f512e56e-2067-4df3-9894-1650664d8a93', 'stun') --Jaws of The Lion - Blood Horror - Standard - Level 0 | stun
,('384e42c4-66c1-48d0-af7f-6ed6f53de3fd', 'disarm') --Jaws of The Lion - Blood Horror - Standard - Level 1 | disarm
,('384e42c4-66c1-48d0-af7f-6ed6f53de3fd', 'immobilize') --Jaws of The Lion - Blood Horror - Standard - Level 1 | immobilize
,('384e42c4-66c1-48d0-af7f-6ed6f53de3fd', 'muddle') --Jaws of The Lion - Blood Horror - Standard - Level 1 | muddle
,('384e42c4-66c1-48d0-af7f-6ed6f53de3fd', 'stun') --Jaws of The Lion - Blood Horror - Standard - Level 1 | stun
,('446be22b-3d69-4ac4-9941-71b28e7fca39', 'disarm') --Jaws of The Lion - Blood Horror - Standard - Level 2 | disarm
,('446be22b-3d69-4ac4-9941-71b28e7fca39', 'immobilize') --Jaws of The Lion - Blood Horror - Standard - Level 2 | immobilize
,('446be22b-3d69-4ac4-9941-71b28e7fca39', 'muddle') --Jaws of The Lion - Blood Horror - Standard - Level 2 | muddle
,('446be22b-3d69-4ac4-9941-71b28e7fca39', 'stun') --Jaws of The Lion - Blood Horror - Standard - Level 2 | stun
,('e5323bc1-e33b-45d3-8a00-cfb5e1759a09', 'disarm') --Jaws of The Lion - Blood Horror - Standard - Level 3 | disarm
,('e5323bc1-e33b-45d3-8a00-cfb5e1759a09', 'immobilize') --Jaws of The Lion - Blood Horror - Standard - Level 3 | immobilize
,('e5323bc1-e33b-45d3-8a00-cfb5e1759a09', 'muddle') --Jaws of The Lion - Blood Horror - Standard - Level 3 | muddle
,('e5323bc1-e33b-45d3-8a00-cfb5e1759a09', 'stun') --Jaws of The Lion - Blood Horror - Standard - Level 3 | stun
,('16d4b2f7-b917-48f3-8a83-6950e4257c9a', 'disarm') --Jaws of The Lion - Blood Horror - Standard - Level 4 | disarm
,('16d4b2f7-b917-48f3-8a83-6950e4257c9a', 'immobilize') --Jaws of The Lion - Blood Horror - Standard - Level 4 | immobilize
,('16d4b2f7-b917-48f3-8a83-6950e4257c9a', 'muddle') --Jaws of The Lion - Blood Horror - Standard - Level 4 | muddle
,('16d4b2f7-b917-48f3-8a83-6950e4257c9a', 'stun') --Jaws of The Lion - Blood Horror - Standard - Level 4 | stun
,('5efd0b48-fa3a-45b6-a65e-0ff0eb33ee64', 'disarm') --Jaws of The Lion - Blood Horror - Standard - Level 5 | disarm
,('5efd0b48-fa3a-45b6-a65e-0ff0eb33ee64', 'immobilize') --Jaws of The Lion - Blood Horror - Standard - Level 5 | immobilize
,('5efd0b48-fa3a-45b6-a65e-0ff0eb33ee64', 'muddle') --Jaws of The Lion - Blood Horror - Standard - Level 5 | muddle
,('5efd0b48-fa3a-45b6-a65e-0ff0eb33ee64', 'stun') --Jaws of The Lion - Blood Horror - Standard - Level 5 | stun
,('8c5eec8f-c206-4d4d-a395-b978f9a5783f', 'disarm') --Jaws of The Lion - Blood Horror - Standard - Level 6 | disarm
,('8c5eec8f-c206-4d4d-a395-b978f9a5783f', 'immobilize') --Jaws of The Lion - Blood Horror - Standard - Level 6 | immobilize
,('8c5eec8f-c206-4d4d-a395-b978f9a5783f', 'muddle') --Jaws of The Lion - Blood Horror - Standard - Level 6 | muddle
,('8c5eec8f-c206-4d4d-a395-b978f9a5783f', 'stun') --Jaws of The Lion - Blood Horror - Standard - Level 6 | stun
,('62beed40-2295-4371-ac9d-e71c17d56ef8', 'disarm') --Jaws of The Lion - Blood Horror - Standard - Level 7 | disarm
,('62beed40-2295-4371-ac9d-e71c17d56ef8', 'immobilize') --Jaws of The Lion - Blood Horror - Standard - Level 7 | immobilize
,('62beed40-2295-4371-ac9d-e71c17d56ef8', 'muddle') --Jaws of The Lion - Blood Horror - Standard - Level 7 | muddle
,('62beed40-2295-4371-ac9d-e71c17d56ef8', 'stun') --Jaws of The Lion - Blood Horror - Standard - Level 7 | stun
,('fd7d54ff-6e25-43ae-bc8c-3ea0a01eb3b3', 'curse') --Jaws of The Lion - Blood Tumor - Standard - Level 0 | curse
,('fd7d54ff-6e25-43ae-bc8c-3ea0a01eb3b3', 'disarm') --Jaws of The Lion - Blood Tumor - Standard - Level 0 | disarm
,('fd7d54ff-6e25-43ae-bc8c-3ea0a01eb3b3', 'immobilize') --Jaws of The Lion - Blood Tumor - Standard - Level 0 | immobilize
,('fd7d54ff-6e25-43ae-bc8c-3ea0a01eb3b3', 'muddle') --Jaws of The Lion - Blood Tumor - Standard - Level 0 | muddle
,('fd7d54ff-6e25-43ae-bc8c-3ea0a01eb3b3', 'stun') --Jaws of The Lion - Blood Tumor - Standard - Level 0 | stun
,('fd539ed4-abcd-4788-827b-86b9345f7c2d', 'curse') --Jaws of The Lion - Blood Tumor - Standard - Level 1 | curse
,('fd539ed4-abcd-4788-827b-86b9345f7c2d', 'disarm') --Jaws of The Lion - Blood Tumor - Standard - Level 1 | disarm
,('fd539ed4-abcd-4788-827b-86b9345f7c2d', 'immobilize') --Jaws of The Lion - Blood Tumor - Standard - Level 1 | immobilize
,('fd539ed4-abcd-4788-827b-86b9345f7c2d', 'muddle') --Jaws of The Lion - Blood Tumor - Standard - Level 1 | muddle
,('fd539ed4-abcd-4788-827b-86b9345f7c2d', 'stun') --Jaws of The Lion - Blood Tumor - Standard - Level 1 | stun
,('67fdb1d5-d476-4969-8688-3d660f6cc7f5', 'curse') --Jaws of The Lion - Blood Tumor - Standard - Level 2 | curse
,('67fdb1d5-d476-4969-8688-3d660f6cc7f5', 'disarm') --Jaws of The Lion - Blood Tumor - Standard - Level 2 | disarm
,('67fdb1d5-d476-4969-8688-3d660f6cc7f5', 'immobilize') --Jaws of The Lion - Blood Tumor - Standard - Level 2 | immobilize
,('67fdb1d5-d476-4969-8688-3d660f6cc7f5', 'muddle') --Jaws of The Lion - Blood Tumor - Standard - Level 2 | muddle
,('67fdb1d5-d476-4969-8688-3d660f6cc7f5', 'stun') --Jaws of The Lion - Blood Tumor - Standard - Level 2 | stun
,('8d94619f-428c-4603-8102-917c2bd4a984', 'curse') --Jaws of The Lion - Blood Tumor - Standard - Level 3 | curse
,('8d94619f-428c-4603-8102-917c2bd4a984', 'disarm') --Jaws of The Lion - Blood Tumor - Standard - Level 3 | disarm
,('8d94619f-428c-4603-8102-917c2bd4a984', 'immobilize') --Jaws of The Lion - Blood Tumor - Standard - Level 3 | immobilize
,('8d94619f-428c-4603-8102-917c2bd4a984', 'muddle') --Jaws of The Lion - Blood Tumor - Standard - Level 3 | muddle
,('8d94619f-428c-4603-8102-917c2bd4a984', 'stun') --Jaws of The Lion - Blood Tumor - Standard - Level 3 | stun
,('3eabd5dc-0162-4a7b-9ce6-7132b3253215', 'curse') --Jaws of The Lion - Blood Tumor - Standard - Level 4 | curse
,('3eabd5dc-0162-4a7b-9ce6-7132b3253215', 'disarm') --Jaws of The Lion - Blood Tumor - Standard - Level 4 | disarm
,('3eabd5dc-0162-4a7b-9ce6-7132b3253215', 'immobilize') --Jaws of The Lion - Blood Tumor - Standard - Level 4 | immobilize
,('3eabd5dc-0162-4a7b-9ce6-7132b3253215', 'muddle') --Jaws of The Lion - Blood Tumor - Standard - Level 4 | muddle
,('3eabd5dc-0162-4a7b-9ce6-7132b3253215', 'stun') --Jaws of The Lion - Blood Tumor - Standard - Level 4 | stun
,('f67915e5-2235-49ab-9921-23f02439c94e', 'curse') --Jaws of The Lion - Blood Tumor - Standard - Level 5 | curse
,('f67915e5-2235-49ab-9921-23f02439c94e', 'disarm') --Jaws of The Lion - Blood Tumor - Standard - Level 5 | disarm
,('f67915e5-2235-49ab-9921-23f02439c94e', 'immobilize') --Jaws of The Lion - Blood Tumor - Standard - Level 5 | immobilize
,('f67915e5-2235-49ab-9921-23f02439c94e', 'muddle') --Jaws of The Lion - Blood Tumor - Standard - Level 5 | muddle
,('f67915e5-2235-49ab-9921-23f02439c94e', 'stun') --Jaws of The Lion - Blood Tumor - Standard - Level 5 | stun
,('9d5a78ee-9d90-4c77-808a-dd87401b0162', 'curse') --Jaws of The Lion - Blood Tumor - Standard - Level 6 | curse
,('9d5a78ee-9d90-4c77-808a-dd87401b0162', 'disarm') --Jaws of The Lion - Blood Tumor - Standard - Level 6 | disarm
,('9d5a78ee-9d90-4c77-808a-dd87401b0162', 'immobilize') --Jaws of The Lion - Blood Tumor - Standard - Level 6 | immobilize
,('9d5a78ee-9d90-4c77-808a-dd87401b0162', 'muddle') --Jaws of The Lion - Blood Tumor - Standard - Level 6 | muddle
,('9d5a78ee-9d90-4c77-808a-dd87401b0162', 'stun') --Jaws of The Lion - Blood Tumor - Standard - Level 6 | stun
,('3ddf6d79-e8f7-4e18-bf2f-386925942bde', 'curse') --Jaws of The Lion - Blood Tumor - Standard - Level 7 | curse
,('3ddf6d79-e8f7-4e18-bf2f-386925942bde', 'disarm') --Jaws of The Lion - Blood Tumor - Standard - Level 7 | disarm
,('3ddf6d79-e8f7-4e18-bf2f-386925942bde', 'immobilize') --Jaws of The Lion - Blood Tumor - Standard - Level 7 | immobilize
,('3ddf6d79-e8f7-4e18-bf2f-386925942bde', 'muddle') --Jaws of The Lion - Blood Tumor - Standard - Level 7 | muddle
,('3ddf6d79-e8f7-4e18-bf2f-386925942bde', 'stun') --Jaws of The Lion - Blood Tumor - Standard - Level 7 | stun
,('baf330e0-c3d8-439e-8a8f-1fdeec170eea', 'curse') --Jaws of The Lion - First Of The Order - Standard - Level 0 | curse
,('baf330e0-c3d8-439e-8a8f-1fdeec170eea', 'disarm') --Jaws of The Lion - First Of The Order - Standard - Level 0 | disarm
,('baf330e0-c3d8-439e-8a8f-1fdeec170eea', 'immobilize') --Jaws of The Lion - First Of The Order - Standard - Level 0 | immobilize
,('baf330e0-c3d8-439e-8a8f-1fdeec170eea', 'muddle') --Jaws of The Lion - First Of The Order - Standard - Level 0 | muddle
,('baf330e0-c3d8-439e-8a8f-1fdeec170eea', 'stun') --Jaws of The Lion - First Of The Order - Standard - Level 0 | stun
,('9c643ddf-a888-4a25-9805-66e54cbcf9eb', 'curse') --Jaws of The Lion - First Of The Order - Standard - Level 1 | curse
,('9c643ddf-a888-4a25-9805-66e54cbcf9eb', 'disarm') --Jaws of The Lion - First Of The Order - Standard - Level 1 | disarm
,('9c643ddf-a888-4a25-9805-66e54cbcf9eb', 'immobilize') --Jaws of The Lion - First Of The Order - Standard - Level 1 | immobilize
,('9c643ddf-a888-4a25-9805-66e54cbcf9eb', 'muddle') --Jaws of The Lion - First Of The Order - Standard - Level 1 | muddle
,('9c643ddf-a888-4a25-9805-66e54cbcf9eb', 'stun') --Jaws of The Lion - First Of The Order - Standard - Level 1 | stun
,('83c868bf-9e0b-411b-80ff-f750f74a7029', 'curse') --Jaws of The Lion - First Of The Order - Standard - Level 2 | curse
,('83c868bf-9e0b-411b-80ff-f750f74a7029', 'disarm') --Jaws of The Lion - First Of The Order - Standard - Level 2 | disarm
,('83c868bf-9e0b-411b-80ff-f750f74a7029', 'immobilize') --Jaws of The Lion - First Of The Order - Standard - Level 2 | immobilize
,('83c868bf-9e0b-411b-80ff-f750f74a7029', 'muddle') --Jaws of The Lion - First Of The Order - Standard - Level 2 | muddle
,('83c868bf-9e0b-411b-80ff-f750f74a7029', 'stun') --Jaws of The Lion - First Of The Order - Standard - Level 2 | stun
,('ad3cf584-4c76-48d4-a630-b264ec8a9b2a', 'curse') --Jaws of The Lion - First Of The Order - Standard - Level 3 | curse
,('ad3cf584-4c76-48d4-a630-b264ec8a9b2a', 'disarm') --Jaws of The Lion - First Of The Order - Standard - Level 3 | disarm
,('ad3cf584-4c76-48d4-a630-b264ec8a9b2a', 'immobilize') --Jaws of The Lion - First Of The Order - Standard - Level 3 | immobilize
,('ad3cf584-4c76-48d4-a630-b264ec8a9b2a', 'muddle') --Jaws of The Lion - First Of The Order - Standard - Level 3 | muddle
,('ad3cf584-4c76-48d4-a630-b264ec8a9b2a', 'stun') --Jaws of The Lion - First Of The Order - Standard - Level 3 | stun
,('3526fd3b-6c0a-4abc-a19e-ec966745cfc3', 'curse') --Jaws of The Lion - First Of The Order - Standard - Level 4 | curse
,('3526fd3b-6c0a-4abc-a19e-ec966745cfc3', 'disarm') --Jaws of The Lion - First Of The Order - Standard - Level 4 | disarm
,('3526fd3b-6c0a-4abc-a19e-ec966745cfc3', 'immobilize') --Jaws of The Lion - First Of The Order - Standard - Level 4 | immobilize
,('3526fd3b-6c0a-4abc-a19e-ec966745cfc3', 'muddle') --Jaws of The Lion - First Of The Order - Standard - Level 4 | muddle
,('3526fd3b-6c0a-4abc-a19e-ec966745cfc3', 'stun') --Jaws of The Lion - First Of The Order - Standard - Level 4 | stun
,('69e52834-f7b0-48ec-9293-080182f42e3b', 'curse') --Jaws of The Lion - First Of The Order - Standard - Level 5 | curse
,('69e52834-f7b0-48ec-9293-080182f42e3b', 'disarm') --Jaws of The Lion - First Of The Order - Standard - Level 5 | disarm
,('69e52834-f7b0-48ec-9293-080182f42e3b', 'immobilize') --Jaws of The Lion - First Of The Order - Standard - Level 5 | immobilize
,('69e52834-f7b0-48ec-9293-080182f42e3b', 'muddle') --Jaws of The Lion - First Of The Order - Standard - Level 5 | muddle
,('69e52834-f7b0-48ec-9293-080182f42e3b', 'stun') --Jaws of The Lion - First Of The Order - Standard - Level 5 | stun
,('cc2b9a36-6518-48f5-ad4f-f3e7ff6a5a89', 'curse') --Jaws of The Lion - First Of The Order - Standard - Level 6 | curse
,('cc2b9a36-6518-48f5-ad4f-f3e7ff6a5a89', 'disarm') --Jaws of The Lion - First Of The Order - Standard - Level 6 | disarm
,('cc2b9a36-6518-48f5-ad4f-f3e7ff6a5a89', 'immobilize') --Jaws of The Lion - First Of The Order - Standard - Level 6 | immobilize
,('cc2b9a36-6518-48f5-ad4f-f3e7ff6a5a89', 'muddle') --Jaws of The Lion - First Of The Order - Standard - Level 6 | muddle
,('cc2b9a36-6518-48f5-ad4f-f3e7ff6a5a89', 'stun') --Jaws of The Lion - First Of The Order - Standard - Level 6 | stun
,('dec4b990-fb0d-4e6b-a5df-9296d00325e5', 'curse') --Jaws of The Lion - First Of The Order - Standard - Level 7 | curse
,('dec4b990-fb0d-4e6b-a5df-9296d00325e5', 'disarm') --Jaws of The Lion - First Of The Order - Standard - Level 7 | disarm
,('dec4b990-fb0d-4e6b-a5df-9296d00325e5', 'immobilize') --Jaws of The Lion - First Of The Order - Standard - Level 7 | immobilize
,('dec4b990-fb0d-4e6b-a5df-9296d00325e5', 'muddle') --Jaws of The Lion - First Of The Order - Standard - Level 7 | muddle
,('dec4b990-fb0d-4e6b-a5df-9296d00325e5', 'stun') --Jaws of The Lion - First Of The Order - Standard - Level 7 | stun
;

INSERT INTO public."Scenario" ("Id", "ContentCode", "Name", "Description", "ScenarioNumber", "Goal", "CityMapLocation", "ScenarioBookPages", "SupplementalBookPages", "GameId") VALUES
 ('0c1e5d5b-e6f4-a62a-8d26-15b50ba346ce', 'roadside_ambush', 'Roadside Ambush', 'Scenario: Roadside Ambush', 1, 'Kill all enemies', 'B1', '{2,3}', '{}', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Roadside Ambush
,('5397e29d-9eb5-ed44-9579-d0846a51a40d', 'a_hole_in_the_wall', 'A Hole in the Wall', 'Scenario: A Hole in the Wall', 2, 'Kill all enemies', 'B1', '{4,5}', '{}', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - A Hole in the Wall
,('117bce00-3466-5802-de5e-95ea037a9ae1', 'the_black_ship', 'The Black Ship', 'Scenario: The Black Ship', 3, 'Kill all enemies', 'D5', '{6,7}', '{2}', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - The Black Ship
,('98cb3d1e-0dc8-d1c6-9d59-025e69b29f12', 'a_ritual_in_stone', 'A Ritual in Stone', 'Scenario: A Ritual in Stone', 4, 'Destroy all summoning stones, then kill all enemies', 'C2', '{8,9}', '{3}', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - A Ritual in Stone
,('eec56bc1-5fa8-c327-c074-388365277ece', 'a_deeper_understanding', 'A Deeper Understanding', 'Scenario: A Deeper Understanding', 5, 'Kill the Blood Tumor', 'C2', '{10,11}', '{4}', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - A Deeper Understanding
,('5d0c2238-19de-7aeb-f8db-971289354a38', 'corrupted_research', 'Corrupted Research', 'Scenario: Corrupted Research', 6, 'Destroy all growths', 'B4', '{12,13}', '{5}', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Corrupted Research
,('20a4b975-fc6c-3359-a2a3-bba47bf57169', 'sunken_tumor', 'Sunken Tumor', 'Scenario: Sunken Tumor', 7, 'Kill the Blood Tumor', 'D2', '{14,15}', '{2}', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Sunken Tumor
,('5aaf9ace-0dbe-3779-9628-2e3d2cba0841', 'hidden_tumor', 'Hidden Tumor', 'Scenario: Hidden Tumor', 8, 'Kill the Blood Tumor', 'C4', '{16,17}', '{6}', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Hidden Tumor
,('dcdf2c0e-3e09-8d43-8f47-109f4f9562cf', 'explosive_evolution', 'Explosive Evolution', 'Scenario: Explosive Evolution', 9, 'Kill the Blood Horror', 'D2 or C4', '{18,19}', '{7}', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Explosive Evolution
,('2ecdbe19-8752-34a2-7f99-614bf9e8c003', 'the_gauntlet', 'The Gauntlet', 'Scenario: The Gauntlet', 10, 'Kill all enemies', 'E2', '{20,21}', '{8}', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - The Gauntlet
,('5e93acb8-fdcf-6f16-006c-024a10d197e7', 'defiled_sewers', 'Defiled Sewers', 'Scenario: Defiled Sewers', 11, 'All characters must escape', 'D2', '{22,23}', '{9}', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Defiled Sewers
,('27ebd92d-c0b7-b85e-4ab4-88e457b4e3a0', 'beguiling_sewers', 'Beguiling Sewers', 'Scenario: Beguiling Sewers', 12, 'Destroy all pillars', 'B5', '{24,25}', '{}', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Beguiling Sewers
,('0fc79fe6-5f73-3cf2-a830-652e06c70e0e', 'vile_harvest', 'Vile Harvest', 'Scenario: Vile Harvest', 13, 'Kill all enemies', 'D3', '{26,27}', '{2}', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Vile Harvest
,('dd3fa264-9bc8-3e21-7ac0-1cf9305cd406', 'toxic_harvest', 'Toxic Harvest', 'Scenario: Toxic Harvest', 14, 'Kill all enemies', 'B4', '{28,29}', '{14}', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Toxic Harvest
,('5a8818a5-2e0d-3aad-96dc-ff0647d91d4c', 'tainted_blood', 'Tainted Blood', 'Scenario: Tainted Blood', 15, 'Destroy all tables and kill all enemies', 'C4', '{30,31}', '{12}', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Tainted Blood
,('ff03d53c-ee00-62ed-6c87-bcdaaaf2c18e', 'mixed_results', 'Mixed Results', 'Scenario: Mixed Results', 16, 'Kill all enemies', 'B4', '{32,33}', '{14}', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Mixed Results
,('1d10cab4-ca94-532e-b0bb-2a92b72b0cc0', 'red_twilight', 'Red Twilight', 'Scenario: Red Twilight', 17, 'Kill the First of the Order', 'C6', '{34,35}', '{15}', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Red Twilight
,('157f35fd-bc73-da0f-e6b5-20e0f2c1ee6b', 'the_heist', 'The Heist', 'Scenario: The Heist', 18, 'Loot C treasure tiles, then all characters must escape', 'A5', '{36,37}', '{13}', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - The Heist
,('c4e71612-47ce-5a9d-01d4-2563b7897de6', 'den_of_thieves', 'Den of Thieves', 'Scenario: Den of Theives', 19, 'Destroy all nests and loot all treasure tiles', 'A5', '{38,39}', '{11}', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Den of Thieves
,('7102f9d6-ba6a-3d46-6d9e-cd572a21e4de', 'misplaced_goods', 'Misplaced Goods', 'Scenario: Misplaced Goods', 20, 'Find all goods, then kill all revealed enemies', 'C5', '{40,41}', '{11}', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Misplaced Goods
,('589b57df-3f04-13ba-8e54-04f607cdbe48', 'agents_of_chaos', 'Agents of Chaos', 'Scenario: Agents of Chaos', 21, 'Kill four Chaos Demons', 'C9', '{42,43}', '{17}', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Agents of Chaos
,('1ee07f7c-c62f-362e-5bbc-7c3f85c13de5', 'unfreindly_message', 'Unfreindly Message', 'Scenario: Unfreindly Message', 22, 'Kill the Stone Golem', 'D1', '{44,45}', '{}', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Unfreindly Message
,('4c3f2e2f-1448-0c4d-cb42-72d2643e27a9', 'best_of_the_best', 'Best of the Best', 'Scenario: Best of the Best', 23, 'Kill all enemies', 'B5', '{46,47}', '{14}', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Best of the Best
,('082471d1-daa8-735a-5521-779553eca565', 'warding_the_void', 'Warding the Void', 'Scenario: Warding the Void', 24, 'Kill all enemies and protect all wards', 'A3', '{48,49}', '{11}', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Warding the Void
,('bed28a5c-2fdc-7dbe-702a-70263a71bccc', 'the_greatest_job_in_the_world', 'The Greatest Job in the World', 'Scenario: The Greatest Job in the World', 25, 'Destroy all pillars, then all characters must escape', 'C7', '{50,51}', '{16}', '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - The Greatest Job in the World
;

INSERT INTO public."Objective" ("Id", "ContentCode", "Name", "Description", "Health","RangeAttackable", "MeleeAttackable", "GameId") VALUES
 ('1e5e60f5-39bd-7fe1-f907-96b56557f54d', 'summoning_stone', 'Summoning Stone', 'Objective: Summoning Stone', 'C + 1', FALSE, TRUE, '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Summoning Stone
,('2b5e88bf-b6d7-7389-b08d-a43cb8ba1f2b', 'growth', 'Growth', 'Objective: Growth', '(1 + L) x C', TRUE, TRUE, '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Growth
,('80ae22f2-83b7-46a9-d6b0-712a34df42ff', 'sewer_pillar', 'Sewer Pillar', 'Objective: Sewer Pillar', '(1 + L) x C', TRUE, TRUE, '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Sewer Pillar
,('ab58ac60-8609-aeca-6c5e-d89391a7ba9d', 'alchemy_table', 'Alchemy Table', 'Objective: Alchemy Table', '(2 + L) x C / 2', TRUE, TRUE, '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Alchemy Table
,('f2e39476-f638-38f9-9953-c87baa4ae329', 'nest', 'Nest', 'Objective: Nest', '(1 + L) x C', TRUE, TRUE, '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Nest
,('8ae90114-e00e-cb49-3a86-453543699c84', 'pillar', 'Pillar', 'Objective: Pillar', 'L + C', TRUE, TRUE, '153dad18-1725-4a91-b337-521c52aaccd1') --Jaws of The Lion - Pillar
;

INSERT INTO public."ScenarioObjective" ("ScenarioId", "ObjectiveId") VALUES
 ('98cb3d1e-0dc8-d1c6-9d59-025e69b29f12', '1e5e60f5-39bd-7fe1-f907-96b56557f54d') --Jaws of The Lion - A Ritual in Stone - Jaws of The Lion - Summoning Stone
,('5d0c2238-19de-7aeb-f8db-971289354a38', '2b5e88bf-b6d7-7389-b08d-a43cb8ba1f2b') --Jaws of The Lion - Corrupted Research - Jaws of The Lion - Growth
,('27ebd92d-c0b7-b85e-4ab4-88e457b4e3a0', '80ae22f2-83b7-46a9-d6b0-712a34df42ff') --Jaws of The Lion - Beguiling Sewers - Jaws of The Lion - Sewer Pillar
,('5a8818a5-2e0d-3aad-96dc-ff0647d91d4c', 'ab58ac60-8609-aeca-6c5e-d89391a7ba9d') --Jaws of The Lion - Tainted Blood - Jaws of The Lion - Alchemy Table
,('c4e71612-47ce-5a9d-01d4-2563b7897de6', 'f2e39476-f638-38f9-9953-c87baa4ae329') --Jaws of The Lion - Den of Thieves - Jaws of The Lion - Nest
,('bed28a5c-2fdc-7dbe-702a-70263a71bccc', '8ae90114-e00e-cb49-3a86-453543699c84') --Jaws of The Lion - The Greatest Job in the World - Jaws of The Lion - Pillar
;

INSERT INTO public."ScenarioMonster" ("ScenarioId", "MonsterId") VALUES
 ('0c1e5d5b-e6f4-a62a-8d26-15b50ba346ce', 'abc857ec-2b99-4189-a2df-b5cfb98c3c31') --Jaws of The Lion - Roadside Ambush - Jaws of The Lion - Vermling Raider
,('5397e29d-9eb5-ed44-9579-d0846a51a40d', 'abc857ec-2b99-4189-a2df-b5cfb98c3c31') --Jaws of The Lion - A Hole in the Wall - Jaws of The Lion - Vermling Raider
,('117bce00-3466-5802-de5e-95ea037a9ae1', '75b4da15-43a4-46da-a410-6f90e8f9e9db') --Jaws of The Lion - The Black Ship - Jaws of The Lion - Giant Viper
,('117bce00-3466-5802-de5e-95ea037a9ae1', '01d2606d-8269-4e83-a6c4-4fb870e37ca0') --Jaws of The Lion - The Black Ship - Jaws of The Lion - Zealot
,('98cb3d1e-0dc8-d1c6-9d59-025e69b29f12', 'c22bac94-b488-924f-a8d6-8770fc530d62') --Jaws of The Lion - A Ritual in Stone - Jaws of The Lion - Stone Golem
,('98cb3d1e-0dc8-d1c6-9d59-025e69b29f12', '01d2606d-8269-4e83-a6c4-4fb870e37ca0') --Jaws of The Lion - A Ritual in Stone - Jaws of The Lion - Zealot
,('eec56bc1-5fa8-c327-c074-388365277ece', '3c5ee40c-1118-4f09-a733-c19bb73e9f69') --Jaws of The Lion - A Deeper Understanding - Jaws of The Lion - Blood Tumor
,('eec56bc1-5fa8-c327-c074-388365277ece', 'a3d6ad55-ba4c-ab9b-83bd-1dd2aa8c1b3f') --Jaws of The Lion - A Deeper Understanding - Jaws of The Lion - Chaos Demon
,('eec56bc1-5fa8-c327-c074-388365277ece', '01d2606d-8269-4e83-a6c4-4fb870e37ca0') --Jaws of The Lion - A Deeper Understanding - Jaws of The Lion - Zealot
,('5d0c2238-19de-7aeb-f8db-971289354a38', '89d6080f-3ff1-ca9d-900c-70651091a11a') --Jaws of The Lion - Corrupted Research - Jaws of The Lion - Rat Monstrosity
,('5d0c2238-19de-7aeb-f8db-971289354a38', '9462d916-3512-4692-a797-d2861d42c4b9') --Jaws of The Lion - Corrupted Research - Jaws of The Lion - Black Sludge
,('20a4b975-fc6c-3359-a2a3-bba47bf57169', '3c5ee40c-1118-4f09-a733-c19bb73e9f69') --Jaws of The Lion - Sunken Tumor - Jaws of The Lion - Blood Tumor
,('20a4b975-fc6c-3359-a2a3-bba47bf57169', '1b1c44d4-417d-4548-ab5c-87116956d51e') --Jaws of The Lion - Sunken Tumor - Jaws of The Lion - Vermling Scout
,('20a4b975-fc6c-3359-a2a3-bba47bf57169', '01d2606d-8269-4e83-a6c4-4fb870e37ca0') --Jaws of The Lion - Sunken Tumor - Jaws of The Lion - Zealot
,('5aaf9ace-0dbe-3779-9628-2e3d2cba0841', '3c5ee40c-1118-4f09-a733-c19bb73e9f69') --Jaws of The Lion - Hidden Tumor - Jaws of The Lion - Blood Tumor
,('5aaf9ace-0dbe-3779-9628-2e3d2cba0841', 'a3d6ad55-ba4c-ab9b-83bd-1dd2aa8c1b3f') --Jaws of The Lion - Hidden Tumor - Jaws of The Lion - Chaos Demon
,('5aaf9ace-0dbe-3779-9628-2e3d2cba0841', '01d2606d-8269-4e83-a6c4-4fb870e37ca0') --Jaws of The Lion - Hidden Tumor - Jaws of The Lion - Zealot
,('dcdf2c0e-3e09-8d43-8f47-109f4f9562cf', '15fb31da-55d3-48cc-8dcc-e98c329f8bee') --Jaws of The Lion - Explosive Evolution - Jaws of The Lion - Blood Horror
,('dcdf2c0e-3e09-8d43-8f47-109f4f9562cf', '1c153051-3d56-693a-d5a3-153bf86d1315') --Jaws of The Lion - Explosive Evolution - Jaws of The Lion - Living Corpse
,('dcdf2c0e-3e09-8d43-8f47-109f4f9562cf', '01d2606d-8269-4e83-a6c4-4fb870e37ca0') --Jaws of The Lion - Explosive Evolution - Jaws of The Lion - Zealot
,('2ecdbe19-8752-34a2-7f99-614bf9e8c003', '9462d916-3512-4692-a797-d2861d42c4b9') --Jaws of The Lion - The Gauntlet - Jaws of The Lion - Black Sludge
,('2ecdbe19-8752-34a2-7f99-614bf9e8c003', 'a3d6ad55-ba4c-ab9b-83bd-1dd2aa8c1b3f') --Jaws of The Lion - The Gauntlet - Jaws of The Lion - Chaos Demon
,('2ecdbe19-8752-34a2-7f99-614bf9e8c003', 'c22bac94-b488-924f-a8d6-8770fc530d62') --Jaws of The Lion - The Gauntlet - Jaws of The Lion - Stone Golem
,('5e93acb8-fdcf-6f16-006c-024a10d197e7', '9462d916-3512-4692-a797-d2861d42c4b9') --Jaws of The Lion - Defiled Sewers - Jaws of The Lion - Black Sludge
,('5e93acb8-fdcf-6f16-006c-024a10d197e7', '75b4da15-43a4-46da-a410-6f90e8f9e9db') --Jaws of The Lion - Defiled Sewers - Jaws of The Lion - Giant Viper
,('5e93acb8-fdcf-6f16-006c-024a10d197e7', '1b1c44d4-417d-4548-ab5c-87116956d51e') --Jaws of The Lion - Defiled Sewers - Jaws of The Lion - Vermling Scout
,('27ebd92d-c0b7-b85e-4ab4-88e457b4e3a0', '1c153051-3d56-693a-d5a3-153bf86d1315') --Jaws of The Lion - Beguiling Sewers - Jaws of The Lion - Living Corpse
,('27ebd92d-c0b7-b85e-4ab4-88e457b4e3a0', 'fa9f27a0-b6ca-60a2-327f-bf700a4e5d29') --Jaws of The Lion - Beguiling Sewers - Jaws of The Lion - Living Spirit
,('0fc79fe6-5f73-3cf2-a830-652e06c70e0e', '9462d916-3512-4692-a797-d2861d42c4b9') --Jaws of The Lion - Vile Harvest - Jaws of The Lion - Black Sludge
,('0fc79fe6-5f73-3cf2-a830-652e06c70e0e', 'c18c7770-0c09-9e46-3b56-8d7015eed6e4') --Jaws of The Lion - Vile Harvest - Jaws of The Lion - Blood Imp
,('0fc79fe6-5f73-3cf2-a830-652e06c70e0e', '01d2606d-8269-4e83-a6c4-4fb870e37ca0') --Jaws of The Lion - Vile Harvest - Jaws of The Lion - Zealot
,('dd3fa264-9bc8-3e21-7ac0-1cf9305cd406', 'c18c7770-0c09-9e46-3b56-8d7015eed6e4') --Jaws of The Lion - Toxic Harvest - Jaws of The Lion - Blood Imp
,('dd3fa264-9bc8-3e21-7ac0-1cf9305cd406', '75b4da15-43a4-46da-a410-6f90e8f9e9db') --Jaws of The Lion - Toxic Harvest - Jaws of The Lion - Giant Viper
,('dd3fa264-9bc8-3e21-7ac0-1cf9305cd406', '01d2606d-8269-4e83-a6c4-4fb870e37ca0') --Jaws of The Lion - Toxic Harvest - Jaws of The Lion - Zealot
,('5a8818a5-2e0d-3aad-96dc-ff0647d91d4c', '7e7f370b-eab5-ded7-11e8-227a04a4f283') --Jaws of The Lion - Tainted Blood - Jaws of The Lion - Black Imp
,('5a8818a5-2e0d-3aad-96dc-ff0647d91d4c', 'c18c7770-0c09-9e46-3b56-8d7015eed6e4') --Jaws of The Lion - Tainted Blood - Jaws of The Lion - Blood Imp
,('5a8818a5-2e0d-3aad-96dc-ff0647d91d4c', '01d2606d-8269-4e83-a6c4-4fb870e37ca0') --Jaws of The Lion - Tainted Blood - Jaws of The Lion - Zealot
,('ff03d53c-ee00-62ed-6c87-bcdaaaf2c18e', '7e7f370b-eab5-ded7-11e8-227a04a4f283') --Jaws of The Lion - Mixed Results - Jaws of The Lion - Black Imp
,('ff03d53c-ee00-62ed-6c87-bcdaaaf2c18e', 'c18c7770-0c09-9e46-3b56-8d7015eed6e4') --Jaws of The Lion - Mixed Results - Jaws of The Lion - Blood Imp
,('ff03d53c-ee00-62ed-6c87-bcdaaaf2c18e', '89d6080f-3ff1-ca9d-900c-70651091a11a') --Jaws of The Lion - Mixed Results - Jaws of The Lion - Rat Monstrosity
,('1d10cab4-ca94-532e-b0bb-2a92b72b0cc0', 'c18c7770-0c09-9e46-3b56-8d7015eed6e4') --Jaws of The Lion - Red Twilight - Jaws of The Lion - Blood Imp
,('1d10cab4-ca94-532e-b0bb-2a92b72b0cc0', '91610c35-e291-8e88-f7ba-f487649ca947') --Jaws of The Lion - Red Twilight - Jaws of The Lion - Blood Monstrosity
,('1d10cab4-ca94-532e-b0bb-2a92b72b0cc0', '4775e3b4-3ee3-4d66-8d9b-c250700962f3') --Jaws of The Lion - Red Twilight - Jaws of The Lion - First Of The Order
,('157f35fd-bc73-da0f-e6b5-20e0f2c1ee6b', 'c22bac94-b488-924f-a8d6-8770fc530d62') --Jaws of The Lion - The Heist - Jaws of The Lion - Stone Golem
,('c4e71612-47ce-5a9d-01d4-2563b7897de6', '89d6080f-3ff1-ca9d-900c-70651091a11a') --Jaws of The Lion - Den of Thieves - Jaws of The Lion - Rat Monstrosity
,('c4e71612-47ce-5a9d-01d4-2563b7897de6', 'abc857ec-2b99-4189-a2df-b5cfb98c3c31') --Jaws of The Lion - Den of Thieves - Jaws of The Lion - Vermling Raider
,('c4e71612-47ce-5a9d-01d4-2563b7897de6', '1b1c44d4-417d-4548-ab5c-87116956d51e') --Jaws of The Lion - Den of Thieves - Jaws of The Lion - Vermling Scout
,('7102f9d6-ba6a-3d46-6d9e-cd572a21e4de', '9462d916-3512-4692-a797-d2861d42c4b9') --Jaws of The Lion - Misplaced Goods - Jaws of The Lion - Black Sludge
,('7102f9d6-ba6a-3d46-6d9e-cd572a21e4de', 'a3d6ad55-ba4c-ab9b-83bd-1dd2aa8c1b3f') --Jaws of The Lion - Misplaced Goods - Jaws of The Lion - Chaos Demon
,('7102f9d6-ba6a-3d46-6d9e-cd572a21e4de', '75b4da15-43a4-46da-a410-6f90e8f9e9db') --Jaws of The Lion - Misplaced Goods - Jaws of The Lion - Giant Viper
,('7102f9d6-ba6a-3d46-6d9e-cd572a21e4de', '89d6080f-3ff1-ca9d-900c-70651091a11a') --Jaws of The Lion - Misplaced Goods - Jaws of The Lion - Rat Monstrosity
,('589b57df-3f04-13ba-8e54-04f607cdbe48', 'a3d6ad55-ba4c-ab9b-83bd-1dd2aa8c1b3f') --Jaws of The Lion - Agents of Chaos - Jaws of The Lion - Chaos Demon
,('589b57df-3f04-13ba-8e54-04f607cdbe48', '75b4da15-43a4-46da-a410-6f90e8f9e9db') --Jaws of The Lion - Agents of Chaos - Jaws of The Lion - Giant Viper
,('1ee07f7c-c62f-362e-5bbc-7c3f85c13de5', '75b4da15-43a4-46da-a410-6f90e8f9e9db') --Jaws of The Lion - Unfreindly Message - Jaws of The Lion - Giant Viper
,('1ee07f7c-c62f-362e-5bbc-7c3f85c13de5', 'c22bac94-b488-924f-a8d6-8770fc530d62') --Jaws of The Lion - Unfreindly Message - Jaws of The Lion - Stone Golem
,('1ee07f7c-c62f-362e-5bbc-7c3f85c13de5', 'abc857ec-2b99-4189-a2df-b5cfb98c3c31') --Jaws of The Lion - Unfreindly Message - Jaws of The Lion - Vermling Raider
,('4c3f2e2f-1448-0c4d-cb42-72d2643e27a9', '75b4da15-43a4-46da-a410-6f90e8f9e9db') --Jaws of The Lion - Best of the Best - Jaws of The Lion - Giant Viper
,('4c3f2e2f-1448-0c4d-cb42-72d2643e27a9', '89d6080f-3ff1-ca9d-900c-70651091a11a') --Jaws of The Lion - Best of the Best - Jaws of The Lion - Rat Monstrosity
,('4c3f2e2f-1448-0c4d-cb42-72d2643e27a9', 'c22bac94-b488-924f-a8d6-8770fc530d62') --Jaws of The Lion - Best of the Best - Jaws of The Lion - Stone Golem
,('4c3f2e2f-1448-0c4d-cb42-72d2643e27a9', 'abc857ec-2b99-4189-a2df-b5cfb98c3c31') --Jaws of The Lion - Best of the Best - Jaws of The Lion - Vermling Raider
,('082471d1-daa8-735a-5521-779553eca565', '7e7f370b-eab5-ded7-11e8-227a04a4f283') --Jaws of The Lion - Warding the Void - Jaws of The Lion - Black Imp
,('082471d1-daa8-735a-5521-779553eca565', 'a3d6ad55-ba4c-ab9b-83bd-1dd2aa8c1b3f') --Jaws of The Lion - Warding the Void - Jaws of The Lion - Chaos Demon
,('bed28a5c-2fdc-7dbe-702a-70263a71bccc', 'fa9f27a0-b6ca-60a2-327f-bf700a4e5d29') --Jaws of The Lion - The Greatest Job in the World - Jaws of The Lion - Living Spirit
,('bed28a5c-2fdc-7dbe-702a-70263a71bccc', 'abc857ec-2b99-4189-a2df-b5cfb98c3c31') --Jaws of The Lion - The Greatest Job in the World - Jaws of The Lion - Vermling Raider
,('bed28a5c-2fdc-7dbe-702a-70263a71bccc', '1b1c44d4-417d-4548-ab5c-87116956d51e') --Jaws of The Lion - The Greatest Job in the World - Jaws of The Lion - Vermling Scout
;