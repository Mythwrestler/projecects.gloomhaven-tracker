import { getContext, setContext } from "svelte";
import { derived, writable, type Readable, type Writable } from "svelte/store";
import type { User } from "../../models/Combat";

interface ActiveCombatStateWritable {
  combatId: Writable<string | undefined>;
  combatConnecting: Writable<boolean>;
  combatConnected: Writable<boolean>;
  combatDisconnecting: Writable<boolean>;
  userList: Writable<User[]>;
}

export interface ActiveCombatState {
  combatId: Readable<string | undefined>;
  combatConnecting: Readable<boolean>;
  combatConnected: Readable<boolean>;
  combatDisconnecting: Readable<boolean>;
  userList: Readable<User[]>;
}

export const getActiveCombatState = (
  stateKey: string
): ActiveCombatStateWritable => {
  let state = getContext<ActiveCombatStateWritable | undefined>(stateKey);
  if (state) {
    const stateProperties = Object.keys(state as object);
    if (
      stateProperties.includes("combatId") &&
      stateProperties.includes("combatConnecting") &&
      stateProperties.includes("combatConnected") &&
      stateProperties.includes("combatDisconnecting") &&
      stateProperties.includes("userList")
    ) {
      return state;
    }
  }

  state = {
    combatId: writable<string | undefined>(undefined),
    combatConnecting: writable<boolean>(false),
    combatConnected: writable<boolean>(false),
    combatDisconnecting: writable<boolean>(false),
    userList: writable<User[]>([]),
  };
  setContext(stateKey, state);
  return state;
};

export const useActiveCombatState = (stateKey: string): ActiveCombatState => {
  const writableState = getActiveCombatState(stateKey);
  return {
    combatId: derived(writableState.combatId, (store) => store),
    combatConnecting: derived(writableState.combatConnecting, (store) => store),
    combatConnected: derived(writableState.combatConnected, (store) => store),
    combatDisconnecting: derived(
      writableState.combatDisconnecting,
      (store) => store
    ),
    userList: derived(writableState.userList, (store) => store, []),
  };
};
