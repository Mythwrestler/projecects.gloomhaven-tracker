import { getContext } from "svelte";

export interface ActiveCombatActions {
  joinCombat: (combatId: string) => Promise<void>;
  leaveCombat: () => Promise<void>;
  registerCharacters: (characterCodes: string[]) => Promise<void>;
  registerObserver: () => Promise<void>;
}

export const useActiveCombatActions = (
  actionKey: string
): ActiveCombatActions => {
  const actions = getContext<ActiveCombatActions | undefined>(actionKey);
  if (actions) {
    const properties = Object.keys(actions);
    if (
      properties.includes("joinCombat") &&
      properties.includes("leaveCombat") &&
      properties.includes("registerCharacters") &&
      properties.includes("registerObserver")
    )
      return actions;
  }

  const notImplemented = () => {
    throw Error("Active Combat Service Actions Not Implemented");
  };
  return {
    joinCombat: notImplemented,
    leaveCombat: notImplemented,
    registerCharacters: notImplemented,
    registerObserver: notImplemented,
  };
};
