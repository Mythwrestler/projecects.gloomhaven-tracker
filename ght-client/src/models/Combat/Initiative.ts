interface CombatantInitiative {
  combatantCode: string;
  initiative: number;
  order: number;
}

export interface Initiative {
  turnOrder: CombatantInitiative[];
  currentCombatant: string;
}
