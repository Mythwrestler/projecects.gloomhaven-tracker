import { getContext, setContext } from "svelte";
import { writable, derived, type Readable, type Writable } from "svelte/store";
import type {
  Character,
  ContentItemSummary,
  Scenario,
  ScenarioSummary,
} from "../../models/Content";
import ENV_VARS from "../../common/Environment";

export interface ContentStateWritable {
  availableGames: Writable<ContentItemSummary[]>;
  scenarioSummaries: Writable<ScenarioSummary[]>;
  scenarioDefault: Writable<Scenario | undefined>;
  characterSummaries: Writable<ContentItemSummary[]>;
  characterDefault: Writable<Character | undefined>;
}

export interface ContentState {
  availableGames: Readable<ContentItemSummary[]>;
  scenarioSummaries: Readable<ScenarioSummary[]>;
  scenarioDefault: Readable<Scenario | undefined>;
  characterSummaries: Readable<ContentItemSummary[]>;
  characterDefault: Readable<Character | undefined>;
}


export const getContentState = (contextKey: unknown): ContentStateWritable => {
  let state = getContext<ContentStateWritable | undefined>(contextKey);
  if (state) {
    const stateProperties = Object.keys(state as object);
    if (
      stateProperties.includes("availableGames") &&
      stateProperties.includes("scenarioSummaries") &&
      stateProperties.includes("scenarioDefault") &&
      stateProperties.includes("characterSummaries") &&
      stateProperties.includes("characterDefault")
    ) {
      return state;
    }
  }

  state = {
    availableGames: writable<ContentItemSummary[]>([]),
    scenarioSummaries: writable<ScenarioSummary[]>(),
    scenarioDefault: writable<Scenario | undefined>(),
    characterSummaries: writable<ContentItemSummary[]>([]),
    characterDefault: writable<Character | undefined>(),
  };
  setContext(contextKey, state);
  return state;
};

export const useContentServiceState = (
  stateKey: string = ENV_VARS.CONTEXT.ContentService.State
): ContentState => {
  const writableState: ContentState = getContentState(stateKey);
  return {
    availableGames: derived(writableState.availableGames, (store) => store),
    scenarioSummaries: derived(
      writableState.scenarioSummaries,
      (store) => store
    ),
    scenarioDefault: derived(writableState.scenarioDefault, (store) => store),
    characterSummaries: derived(
      writableState.characterSummaries,
      (store) => store
    ),
    characterDefault: derived(writableState.characterDefault, (store) => store),
  };
};
