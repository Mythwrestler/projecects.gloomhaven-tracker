import type { Actors } from "./Actors";

export interface CombatSpaceSummary {
  gameCode: string;
  scenarioCode: string;
  combatId: string;
  description: string;
}

export interface CombatSpace extends CombatSpaceSummary {
  actors: Actors;

  // public ActorsDTO Actors { get; set; } = new ActorsDTO();
  // public AttackModifierDeckDTO MonsterDeck { get; set; } = new AttackModifierDeckDTO();
  // public Dictionary<ELEMENT_TYPE, ELEMENT_STRENGTH> Elements { get; set; } = new Dictionary<ELEMENT_TYPE, ELEMENT_STRENGTH>();
  // public Dictionary<int, Guid> TurnOrder { get; set; } = new Dictionary<int, Guid>();
  // public Guid? CurrentActor { get; set; }
  // public int RoundNumber { get; set; }
}
