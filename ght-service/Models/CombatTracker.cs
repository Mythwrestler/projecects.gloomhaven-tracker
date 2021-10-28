using System;
using System.Collections.Generic;
using System.Linq;

namespace GloomhavenTracker.Service.Models
{

    public class CombatTrackerDO
    {
        public Guid CombatId { get; set; }
        public GAME_CODES GameCode {get; set;}
        public string Description { get; set; } = String.Empty;
        public ActorsDO Actors { get; set; } = new ActorsDO();
        public AttackModifierDeckDO MonsterDeck { get; set; } = new AttackModifierDeckDO();
        public Dictionary<ELEMENT_TYPE, ELEMENT_STRENGTH> Elements { get; set; } = new Dictionary<ELEMENT_TYPE, ELEMENT_STRENGTH>();
        public Dictionary<Guid, int> Initiative { get; set; } = new Dictionary<Guid, int>();
        public int CurrentActor { get; set; }
        public int RoundNumber { get; set; }
        public List<string> RegisteredHubClients { get; set; } = new List<string>();

        public CombatTrackerSummaryDTO SummaryDTO 
        {
            get
            {
                return new CombatTrackerSummaryDTO()
                {
                    CombatId = CombatId,
                    Description = Description,
                    GameCode = GameCode
                };
            }
        }
    }

    public class CombatTrackerDTO
    {
        public Guid CombatId { get; set; }
        public GAME_CODES GameCode {get; set;}
        public string Description { get; set; } = String.Empty;
        public ActorsDTO Actors { get; set; } = new ActorsDTO();
        public AttackModifierDeckDTO MonsterDeck { get; set; } = new AttackModifierDeckDTO();
        public Dictionary<ELEMENT_TYPE, ELEMENT_STRENGTH> Elements { get; set; } = new Dictionary<ELEMENT_TYPE, ELEMENT_STRENGTH>();
        public Dictionary<int, Guid> TurnOrder { get; set; } = new Dictionary<int, Guid>();
        public Guid? CurrentActor { get; set; }
        public int RoundNumber { get; set; }
    }

    public class CombatTrackerSummaryDTO
    {
        public Guid CombatId { get; set; }
        public GAME_CODES GameCode {get; set;}
        public string Description { get; set; } = String.Empty;
    }

    public class NewCombatTrackerDescription {
        public GAME_CODES GameCode {get; set;}
        public string Description { get; set; } = String.Empty;
    }


    public class CombatTracker
    {
        private readonly GAME_CODES _gameCode;
        private readonly Guid _combatId = Guid.NewGuid();

        private readonly string _description;
        private int _roundNumber = 0;

        private List<string> _registeredHubClients = new List<string>();
        public List<string> HubClients => _registeredHubClients.ToList();
        public void RegisterHubClient(string clientId)
        {
            if (!_registeredHubClients.Contains(clientId)) _registeredHubClients.Add(clientId);
        }
        public void RemoveHubClient(string clientId)
        {
            if (_registeredHubClients.Contains(clientId)) _registeredHubClients.Remove(clientId);
        }

        private Dictionary<Guid, Player> _players = new Dictionary<Guid, Player>();
        private Dictionary<Guid, Player> _exhaustedPlayers = new Dictionary<Guid, Player>();
        private Dictionary<Guid, Monster> _monsters = new Dictionary<Guid, Monster>();
        private Dictionary<Guid, Objective> _objectives = new Dictionary<Guid, Objective>();

        // Initiative of an Actor for the current turn. 
        private Dictionary<Guid, int> _initiative = new Dictionary<Guid, int>();

        // Turn Order, Actor Id
        private Dictionary<int, Guid> _turnOrder = new Dictionary<int, Guid>();

        private int _currentActor = -1;
        public Guid? CurrentActor => _turnOrder[_currentActor];

        private Elements _elements = new Elements();

        private AttackModifierDeck _monsterModDeck;

        public CombatTracker(String description, GAME_CODES gameCode, List<Player> players, List<Monster> monsters, List<AttackModifier> monsterModDeck)
        {
            _combatId = Guid.NewGuid();
            _gameCode = gameCode;
            _description = description;
            _monsterModDeck = new AttackModifierDeck(monsterModDeck);
            players.ForEach(p => AddPlayer(p));
            monsters.ForEach(m => AddMonster(m));
        }

        public CombatTracker(CombatTrackerDO combat)
        {
            _combatId = combat.CombatId;

            _description = combat.Description;

            _roundNumber = combat.RoundNumber;

            _monsterModDeck = new AttackModifierDeck(combat.MonsterDeck);

            _elements = new Elements(combat.Elements);

            combat.Actors.Players.ForEach(player =>
            {
                Player actor = new Player()
                {
                    Id = player.Id,
                    Name = player.Name,
                    Experience = player.Experience,
                    ModifierDeck = new AttackModifierDeck(player.modifierDeck),
                    BaseModifierDeck = player.BaseModifierDeck,
                    BaseHealthStats = player.BaseHealthStats,
                    Effects = new ActorEffects(player.Effects),
                    Health = player.Health,
                    Levels = player.Levels
                };

                if (actor.Health > 0)
                {
                    _players.Add(actor.Id, actor);
                }
                else
                {
                    _exhaustedPlayers.Add(actor.Id, actor);
                }
            });

            combat.Actors.Monsters.ForEach(monster =>
            {
                Monster actor = new Monster()
                {
                    Id = monster.Id,
                    BaseStats = monster.BaseStats,
                    Effects = new ActorEffects(monster.Effects),
                    Health = monster.Health,
                    IsElite = monster.IsElite,
                    Level = monster.Level,
                    MonsterId = monster.MonsterId,
                    Name = monster.Name,
                    Type = monster.Type
                };
                _monsters.Add(actor.Id, actor);
            });

            combat.Actors.Objectives.ForEach(objective =>
            {
                Objective actor = new Objective(objective.BaseHealth)
                {
                    Id = objective.Id,
                    ObjectiveId = objective.ObjectiveId,
                    Effects = new ActorEffects(objective.Effects),
                    Health = objective.Health,
                    Name = objective.Name
                };
                _objectives.Add(actor.Id, actor);
            });

            _initiative = combat.Initiative;

            CalculateTurnOrder();

            _currentActor = combat.CurrentActor;
        }

        public Guid CombatId => _combatId;

        public void AddPlayer(Player player)
        {
            _players.Add(player.Id, player);
        }
        public void AddMonster(Monster monster)
        {
            _monsters.Add(monster.Id, monster);
        }
        public void AddObjective(Objective objective)
        {
            _objectives.Add(objective.Id, objective);
        }

        public Player? GetPlayer(Guid id)
        {
            if (_players.ContainsKey(id))
            {
                return _players.GetValueOrDefault(id);
            }
            else
            {
                return _exhaustedPlayers.GetValueOrDefault(id);
            }
        }
        public Monster? GetMonster(Guid id) => _monsters.GetValueOrDefault(id);
        public Objective? GetObjective(Guid id) => _objectives.GetValueOrDefault(id);

        public Dictionary<int, Guid>? TurnOrder => _initiative.Count < _players.Count + _monsters.Count ? null : _turnOrder;


        public void SetCombatInitiative(CombatInitiative initiative)
        {
            if (String.IsNullOrWhiteSpace(initiative.MonsterType))
            {
                SetPlayerInitiative(
                    initiative.PlayerId,
                    initiative.IsLongRest ? 99 : initiative.Initiative
                );
            }
            else
            {
                SetMonsterInitiative(
                    initiative.MonsterType,
                    initiative.Initiative
                );
            }

            if (TurnOrder != null) _currentActor = 1;

        }

        public void SetPlayerInitiative(Guid playerId, int initiative)
        {
            Player? playerIdTest = _players.GetValueOrDefault(playerId);
            if (playerIdTest == null) throw new ArgumentException($"No Player found with id {playerId.ToString()}.");
            if (initiative < 1 || initiative > 99) throw new ArgumentException("Player initiative must be between 1 and 99");

            _initiative[playerId] = initiative;

            CalculateTurnOrder();

        }

        public void SetMonsterInitiative(string type, int initiative)
        {
            Dictionary<Guid, Monster> monstersForUpdate = _monsters.Where(kvp => kvp.Value.Type == type).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            if (monstersForUpdate.Count <= 0) throw new ArgumentException($"No Monsters of Type {type} in combat.");
            if (initiative < 1 || initiative > 99) throw new ArgumentException("Monster initiative must be between 1 and 99");

            monstersForUpdate
                .ToList()
                .ForEach(kvp => _initiative[kvp.Key] = initiative);

            CalculateTurnOrder();

        }

        private void CalculateTurnOrder()
        {
            _turnOrder.Clear();
            if (_initiative.Count == 0) return;

            var distinctInitiatives = _initiative.Select(kvp => kvp.Value).Distinct().ToList();

            int order = 0;
            distinctInitiatives.ForEach(i =>
            {
                List<Player> players = _initiative.Where(kvp => kvp.Value == i && _players.ContainsKey(kvp.Key)).Select(kvp => _players[kvp.Key]).ToList();

                players.ForEach(p =>
                {
                    order++;
                    _turnOrder.Add(order, p.Id);
                });

                List<Monster> monsters = _initiative.Where(kvp => kvp.Value == i && _monsters.ContainsKey(kvp.Key)).Select(kvp => _monsters[kvp.Key]).ToList();

                monsters
                    .Where(m => m != null).Select(m => m as Monster)
                    .OrderBy(m => m.Type).ThenBy(m => m.IsElite).ThenBy(m => m.MonsterId)
                    .ToList().ForEach(m =>
                    {
                        order++;
                        _turnOrder.Add(order, m.Id);
                    });

            });
        }

        public void NewRound()
        {
            _initiative.Clear();
            _turnOrder.Clear();
            _elements.ReduceAllElementCharges();
            _players.Values.ToList().ForEach(Actor => Actor.Effects.ReduceEffectDurations());
            _objectives.Values.ToList().ForEach(Actor => Actor.Effects.ReduceEffectDurations());
            _monsters.Values.ToList().ForEach(Actor => Actor.Effects.ReduceEffectDurations());
            _currentActor = -1;
            if (_roundNumber > -1) _roundNumber++;
        }

        public List<AttackModifier> DrawFromMonsterModDeck(int count) => _monsterModDeck.DrawNewCards(count);
        public List<AttackModifier> RedrawFromMonsterModDeck(int count) => _monsterModDeck.DrawNewCards(count, true);
        public List<AttackModifier> GetFlippedMonsterModCards() => _monsterModDeck.GetFlippedCards();
        public void ShuffleMonsterModDeck() => _monsterModDeck.ShuffleDeck();

        public Dictionary<ELEMENT_TYPE, ELEMENT_STRENGTH> ElementalStrength => _elements.ElementalStrength;

        public CombatActionResult ProcessBattleAction(CombatAction action)
        {
            if (!_players.ContainsKey(action.Source) && !_monsters.ContainsKey(action.Source) && !_objectives.ContainsKey(action.Source))
                throw new ArgumentException("Invalid Combat Action Source");


            if (!_players.ContainsKey(action.Target) && !_monsters.ContainsKey(action.Target) && !_objectives.ContainsKey(action.Target))
                throw new ArgumentException("Invalid Combat Action Target");

            action.ElementsCharged.ForEach(e => _elements.ChargeElement(e));
            action.ElementSpent.ForEach(e => _elements.SpendElementCharge(e));

            if (_players.ContainsKey(action.Target))
            {
                Actor actor = _players[action.Target];
                ApplyBattleActionToActor(action, ref actor);
                Player player = _players[action.Target];
                if (player.Health <= 0)
                {
                    _exhaustedPlayers.Add(player.Id, (player as Player) ?? new Player());
                    _players.Remove(player.Id);
                }
                return new CombatActionResult
                {
                    Actors = new ActorsDTO
                    {
                        Players = new List<PlayerDTO>() { player.DTO }
                    },
                    ElementalStrength = ElementalStrength
                };
            }
            else if (_monsters.ContainsKey(action.Target))
            {
                Monster monster = _monsters[action.Target];
                Actor actor = _monsters[action.Target];
                ApplyBattleActionToActor(action, ref actor);
                if (monster.Health <= 0) _monsters.Remove(monster.Id);

                return new CombatActionResult
                {
                    Actors = new ActorsDTO
                    {
                        Monsters = new List<MonsterDTO>() { monster.DTO }
                    },
                    ElementalStrength = ElementalStrength
                };
            }
            else if (_objectives.ContainsKey(action.Target))
            {
                Objective objective = _objectives[action.Target];
                Actor actor = _objectives[action.Target];
                ApplyBattleActionToActor(action, ref actor);
                if (objective.Health <= 0) _objectives.Remove(objective.Id);
                return new CombatActionResult
                {
                    Actors = new ActorsDTO
                    {
                        Objectives = new List<ObjectiveDTO>() { objective.DTO }
                    },
                    ElementalStrength = ElementalStrength
                };
            }

            return new CombatActionResult();
        }

        public void ApplyBattleActionToActor(CombatAction action, ref Actor actor)
        {
            var id = actor.Id;
            var baseHealth = actor.BaseHealth;

            // Heal Action
            if (actor.Effects.HasEffect(ACTOR_EFFECT_TYPE.poison) != null)
            {
                actor.Effects.RemoveEffect(ACTOR_EFFECT_TYPE.poison);
            }
            else if (actor.Effects.HasEffect(ACTOR_EFFECT_TYPE.wound) != null)
            {
                actor.Effects.RemoveEffect(ACTOR_EFFECT_TYPE.wound);
            }
            else
            {
                actor.Health += action.Healing;
                if (actor.Health > baseHealth) actor.Health = baseHealth;
            }

            // Damage Action
            actor.Health -= action.Damage;

            // If Player Passes Out
            if (actor.Health <= 0)
            {
                actor.Effects.ClearAll();
                actor.Health = 0;
                return;
            }

            // Apply Status Effects
            actor.Effects.ApplyEffects(action.EffectsApplied);

        }

        public CombatTrackerDO State
        {
            get
            {
                List<PlayerDO> players = _players.Values.Select(p => p.State).ToList();
                players.AddRange(_exhaustedPlayers.Values.Select(p => p.State));

                return new CombatTrackerDO
                {
                    CombatId = _combatId,
                    Description = _description,
                    Actors = new ActorsDO
                    {
                        Players = players,
                        Monsters = _monsters.Values.Select(m => m.State).ToList(),
                        Objectives = _objectives.Values.Select(o => o.State).ToList(),
                    },
                    Elements = _elements.ElementalStrength,
                    Initiative = _initiative,
                    CurrentActor = _currentActor,
                    MonsterDeck = _monsterModDeck.State,
                    RoundNumber = _roundNumber,
                    RegisteredHubClients = _registeredHubClients
                };
            }
        }

        public CombatTrackerDTO DTO
        {
            get
            {
                List<PlayerDTO> players = _players.Values.Select(p => p.DTO).ToList();
                players.AddRange(_exhaustedPlayers.Values.Select(p => p.DTO));

                return new CombatTrackerDTO
                {
                    CombatId = _combatId,
                    Description = _description,
                    Actors = new ActorsDTO
                    {
                        Players = players,
                        Monsters = _monsters.Values.Select(m => m.DTO).ToList(),
                        Objectives = _objectives.Values.Select(o => o.DTO).ToList(),
                    },
                    Elements = _elements.ElementalStrength,
                    TurnOrder = _turnOrder,
                    CurrentActor = (_currentActor == -1) ? null : _turnOrder[_currentActor],
                    MonsterDeck = _monsterModDeck.DTO,
                    RoundNumber = _roundNumber
                };

            }
        }

        public CombatTrackerSummaryDTO SummaryDTO
        {
            get
            {
                return new CombatTrackerSummaryDTO()
                {
                    CombatId = _combatId,
                    Description = _description
                };
            }
        }

    }

}