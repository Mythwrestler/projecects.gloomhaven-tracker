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
}
