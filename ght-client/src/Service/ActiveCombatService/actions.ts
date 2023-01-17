import { getContext } from "svelte";
import type { CombatRequest } from "../../models/Combat";

export interface ActiveCombatActions {
  joinCombat: (combatId: CombatRequest) => Promise<void>;
  leaveCombat: () => Promise<void>;
}

export const useActiveCombatActions = (
  actionKey: string
): ActiveCombatActions => {
  const actions = getContext<ActiveCombatActions | undefined>(actionKey);
  if (actions) {
    const properties = Object.keys(actions);
    if (properties.includes("joinCombat") && properties.includes("leaveCombat"))
      return actions;
  }

  const notImplemented = () => {
    throw Error("Active Combat Service Actions Not Implemented");
  };
  return {
    joinCombat: notImplemented,
    leaveCombat: notImplemented,
  };
};
