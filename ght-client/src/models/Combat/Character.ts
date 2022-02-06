import type { Effect } from "./Effect";

export interface Character {
  contentCode: string;
  combatantCode: string;
  health: number;
  activeEffects: Effect[];
}
