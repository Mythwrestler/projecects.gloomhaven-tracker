import { getContext, setContext } from "svelte";
import { derived, writable, type Readable, type Writable } from "svelte/store";
import type {
  Character,
  CombatSummary,
  Participants,
} from "../../models/Combat";

interface ActiveCombatStateWritable {
  combatSummary: Writable<CombatSummary | undefined>;
  combatConnecting: Writable<boolean>;
  combatConnected: Writable<boolean>;
  combatDisconnecting: Writable<boolean>;
  participants: Writable<Participants | undefined>;
  combatCharacters: Writable<Character[]>;
}

export interface ActiveCombatState {
  combatSummary: Readable<CombatSummary | undefined>;
  combatConnecting: Readable<boolean>;
  combatConnected: Readable<boolean>;
  combatDisconnecting: Readable<boolean>;
  participants: Readable<Participants | undefined>;
  combatCharacters: Readable<Character[]>;
}

export const getActiveCombatState = (
  stateKey: string
): ActiveCombatStateWritable => {
  let state = getContext<ActiveCombatStateWritable | undefined>(stateKey);
  if (state) {
    const stateProperties = Object.keys(state as object);
    if (
      stateProperties.includes("combatSummary") &&
      stateProperties.includes("combatConnecting") &&
      stateProperties.includes("combatConnected") &&
      stateProperties.includes("combatDisconnecting") &&
      stateProperties.includes("participants") &&
      stateProperties.includes("combatCharacters")
    ) {
      return state;
    }
  }

  state = {
    combatSummary: writable<CombatSummary | undefined>(undefined),
    combatConnecting: writable<boolean>(false),
    combatConnected: writable<boolean>(false),
    combatDisconnecting: writable<boolean>(false),
    participants: writable<Participants | undefined>(undefined),
    combatCharacters: writable<Character[]>([]),
  };
  setContext(stateKey, state);
  return state;
};

export const useActiveCombatState = (stateKey: string): ActiveCombatState => {
  const writableState = getActiveCombatState(stateKey);
  return {
    combatSummary: derived(writableState.combatSummary, (store) => store),
    combatConnecting: derived(writableState.combatConnecting, (store) => store),
    combatConnected: derived(writableState.combatConnected, (store) => store),
    combatDisconnecting: derived(
      writableState.combatDisconnecting,
      (store) => store
    ),
    participants: derived(writableState.participants, (store) => store),
    combatCharacters: derived(
      writableState.combatCharacters,
      (store) => store,
      []
    ),
  };
};
