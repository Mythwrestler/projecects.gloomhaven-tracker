import { getContext } from "svelte";
import ENV_VARS from "../../common/Environment";
import type { Combat, CombatSummary } from "../../models/Combat";

export interface CombatActions {
  getCombatSummaries: () => Promise<CombatSummary[]>;
  getCombatDetail: (combatId: string) => Promise<Combat | undefined>;
  createCombat: (
    campaignId: string,
    scenarioContentCode: string
  ) => Promise<Combat | undefined>;
}

export const useCombatServiceActions = (
  actionKey: string = ENV_VARS.CONTEXT.CombatService.Actions
): CombatActions => {
  const actions = getContext<CombatActions | undefined>(actionKey);
  if (actions) {
    const properties = Object.keys(actions);
    if (
      properties.includes("getCombatSummaries") &&
      properties.includes("getCombatDetail") &&
      properties.includes("createCombat")
    )
      return actions;
  }

  const notImplemented = () => {
    throw Error("Combat Service Actions Not Implemented");
  };
  return {
    getCombatSummaries: notImplemented,
    getCombatDetail: notImplemented,
    createCombat: notImplemented,
  };
};
