import type { Effect } from "./Effect";

export interface Objective {
  combatantCode: string;
  health: number;
  activeEffects: Effect[];
  objectiveNumber: number;
}

export interface ObjectiveGroup {
  contentCode: string;
  monsters: Objective[];
}
