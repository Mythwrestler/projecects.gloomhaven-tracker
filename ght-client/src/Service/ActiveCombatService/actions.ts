import { getContext } from "svelte";

export interface ActiveCombatActions {
  joinCombat: (combatId: string) => Promise<void>;
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
