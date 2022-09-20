import { getContext } from "svelte";
import ENV_VARS from "../../common/Environment";

export interface CombatActions {
  getCombatSummaries: () => Promise<void>;
  getCombatDetail: (combatId: string) => Promise<void>;
  clearCombatDetail: () => void;
  createCombat: (
    campaignId: string,
    scenarioContentCode: string
  ) => Promise<void>;
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
      properties.includes("clearCombatDetail") &&
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
    clearCombatDetail: notImplemented,
    createCombat: notImplemented,
  };
};
