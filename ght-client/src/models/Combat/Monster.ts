import type { Effect } from "./Effect";

export interface Monster {
  combatantCode: string;
  health: number;
  activeEffects: Effect[];
  instanceId: number;
  isElite: boolean;
}

export interface MonsterGroup {
  contentCode: string;
  monsters: Monster[];
}
