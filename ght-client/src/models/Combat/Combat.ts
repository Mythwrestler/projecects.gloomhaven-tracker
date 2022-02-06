import type { Character } from "./Character";
import type { Element } from "./Element";
import type { Initiative } from "./Initiative";
import type { MonsterGroup } from "./Monster";
import type { ObjectiveGroup } from "./Objective";

export interface CombatSpaceSummary {
  gameCode: string;
  scenarioCode: string;
  combatId: string;
  description: string;
}

export interface CombatSpace {
  id: string;
  gameCode: string;
  campaign: string;
  scenarioContentCode: string;
  scenarioLevel: number;
  description: string;
  elements: Element[];
  characters: Character[];
  monsters: MonsterGroup[];
  objectives: ObjectiveGroup;
  initiative: Initiative;
}
