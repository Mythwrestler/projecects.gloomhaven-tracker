import type { AttackModifierDeck } from "./AttackModifierDeck";

export interface CombatSummary {
  id: string;
  scenarioContentCode: string;
  scenarioLevel: number;
  campaignId: string;
  description: string;
}

export interface Combat extends CombatSummary {
  monsterModifierDeck: AttackModifierDeck;
  characters: Character[];
}

export interface Combatant {
  id: string;
  level: number;
  health: number;
  initiative: number | undefined;
}

export interface Character extends Combatant {
  characterContentCode: string;
}
