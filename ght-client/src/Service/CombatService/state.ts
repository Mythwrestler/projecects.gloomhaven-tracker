import { getContext, setContext } from "svelte";
import { derived, writable, type Readable, type Writable } from "svelte/store";
import ENV_VARS from "../../common/Environment";
import type { Combat, CombatSummary } from "../../models/Combat";

export interface CombatStateWritable {
  combatSummaries: Writable<CombatSummary[]>;
  combatDetail: Writable<Combat | undefined>;
}

export interface CombatState {
  combatSummaries: Readable<CombatSummary[]>;
  combatDetail: Readable<Combat | undefined>;
}

export const getCombatState = (contextKey: unknown): CombatStateWritable => {
  let state = getContext<CombatStateWritable | undefined>(contextKey);
  if (state) {
    const stateProperties = Object.keys(state as object);
    if (
      stateProperties.includes("combatSummaries") &&
      stateProperties.includes("combatDetail")
    ) {
      return state;
    }
  }

  state = {
    combatSummaries: writable<CombatSummary[]>([]),
    combatDetail: writable<Combat>(),
  };
  setContext(contextKey, state);
  return state;
};

export const useCombatServiceState = (
  stateKey: string = ENV_VARS.CONTEXT.CombatService.State
): CombatState => {
  const writableState: CombatStateWritable = getCombatState(stateKey);
  return {
    combatSummaries: derived(writableState.combatSummaries, (store) => store),
    combatDetail: derived(writableState.combatDetail, (store) => store),
  };
};
