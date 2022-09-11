import { getContext, setContext } from "svelte";
import {
  writable,
  derived,
  type Readable,
  type Writable,
  get,
} from "svelte/store";
import { getAPI } from "../../common/Utils/API";
import * as GlobalError from "../Error";
import type {
  Character,
  ContentItemSummary,
  Scenario,
  ScenarioSummary,
} from "../../models/Content";
import ENV_VARS from "../../common/Environment";

interface ContentStateWritable {
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

interface ContentActions {
  getAvailableGames: () => void;
  getScenarioSummaries: (gameCode: string) => void;
  getScenarioDefault: (gameCode: string, scenarioCode: string) => void;
  getCharacterSummaries: (gameCode: string) => void;
  getCharacterDefault: (gameCode: string, characterCode: string) => void;
}

const getContentState = (contextKey: unknown): ContentStateWritable => {
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

export const useContentServiceActions = (
  actionKey: string = ENV_VARS.CONTEXT.ContentService.Actions
): ContentActions => {
  const actions = getContext<ContentActions | undefined>(actionKey);
  if (actions) {
    const properties = Object.keys(actions);
    if (
      properties.includes("getAvailableGames") &&
      properties.includes("getScenarioSummaries") &&
      properties.includes("getScenarioDefault") &&
      properties.includes("getCharacterSummaries") &&
      properties.includes("getCharacterDefault")
    )
      return actions;
  }

  const notImplemented = () => {
    throw Error("Content Service Not Implements");
  };
  return {
    getAvailableGames: notImplemented,
    getScenarioSummaries: notImplemented,
    getScenarioDefault: notImplemented,
    getCharacterSummaries: notImplemented,
    getCharacterDefault: notImplemented,
  };
};

class ContentService {
  private accessToken: Readable<string | undefined>;
  private availableGames: Writable<ContentItemSummary[]>;
  private scenarioSummaries: Writable<ScenarioSummary[]>;
  private scenarioDefault: Writable<Scenario | undefined>;
  private characterSummaries: Writable<ContentItemSummary[]>;
  private characterDefault: Writable<Character | undefined>;

  constructor(contextKey: string, accessToken: Readable<string | undefined>) {
    const {
      availableGames,
      scenarioSummaries,
      scenarioDefault,
      characterSummaries,
      characterDefault,
    } = getContentState(contextKey);
    this.accessToken = accessToken;
    this.availableGames = availableGames;
    this.scenarioSummaries = scenarioSummaries;
    this.scenarioDefault = scenarioDefault;
    this.characterSummaries = characterSummaries;
    this.characterDefault = characterDefault;
  }

  private getAvailableGames = () => {
    if (get(this.availableGames).length > 0) return;
    const token: string | undefined = get(this.accessToken);
    this.availableGames.set([]);
    getAPI<ContentItemSummary[]>(`content/games/`, token)
      .then((games) => {
        this.availableGames.set(games);
      })
      .catch((err: unknown) => {
        GlobalError.showErrorMessage(
          `Failed to get Game Listing. ${JSON.stringify(err)}`
        );
      });
  };

  private scenarioSummariesRequest: string | undefined;
  private getScenarioSummaries = (gameCode: string) => {
    if (gameCode === this.scenarioSummariesRequest) return;
    const token: string | undefined = get(this.accessToken);
    this.scenarioSummaries.set([]);
    getAPI<ScenarioSummary[]>(`content/games/${gameCode}/scenarios`, token)
      .then((scenarios: ScenarioSummary[]) => {
        this.scenarioSummaries.set(scenarios);
      })
      .catch((err: unknown) => {
        GlobalError.showErrorMessage(
          `Failed to get Scenario Summary Listing. ${JSON.stringify(err)}`
        );
      });
  };

  private scenarioDefaultRequest: {
    gameCode: string | undefined;
    scenarioCode: string | undefined;
  } = { gameCode: undefined, scenarioCode: undefined };
  private getScenarioDefault = (gameCode: string, scenarioCode: string) => {
    if (
      this.scenarioDefaultRequest.gameCode === gameCode &&
      this.scenarioDefaultRequest.scenarioCode === scenarioCode
    )
      return;
    const token: string | undefined = get(this.accessToken);
    this.scenarioDefault.set(undefined);
    getAPI<Scenario>(
      `content/games/${gameCode}/scenarios/${scenarioCode}`,
      token
    )
      .then((scenario: Scenario) => {
        this.scenarioDefault.set(scenario);
      })
      .catch((err: unknown) => {
        GlobalError.showErrorMessage(
          `Failed to get Scenario Default Values. ${JSON.stringify(err)}`
        );
      });
  };

  private characterSummariesRequest: string | undefined;
  private getCharacterSummaries = (gameCode: string) => {
    if (this.characterSummariesRequest === gameCode) return;
    const token: string | undefined = get(this.accessToken);
    this.characterSummaries.set([]);
    getAPI<ContentItemSummary[]>(`content/games/${gameCode}/characters`, token)
      .then((characters: ContentItemSummary[]) => {
        this.characterSummaries.set(characters);
      })
      .catch((err: unknown) => {
        GlobalError.showErrorMessage(
          `Failed to get Character Summary Listing. ${JSON.stringify(err)}`
        );
      });
  };

  private characterDefaultRequest: {
    gameCode: string | undefined;
    characterCode: string | undefined;
  } = { gameCode: undefined, characterCode: undefined };
  private getCharacterDefault = (gameCode: string, characterCode: string) => {
    if (
      this.characterDefaultRequest.gameCode === gameCode &&
      this.characterDefaultRequest.characterCode === characterCode
    )
      return;
    const token: string | undefined = get(this.accessToken);
    this.characterDefault.set(undefined);
    getAPI<Character>(
      `content/games/${gameCode}/characters/${characterCode}`,
      token
    )
      .then((character: Character) => {
        this.characterDefault.set(character);
      })
      .catch((err: unknown) => {
        GlobalError.showErrorMessage(
          `Failed to get Character Default Values. ${JSON.stringify(err)}`
        );
      });
  };

  public actions: ContentActions = {
    getAvailableGames: this.getAvailableGames,
    getScenarioSummaries: this.getScenarioSummaries,
    getScenarioDefault: this.getScenarioDefault,
    getCharacterSummaries: this.getCharacterSummaries,
    getCharacterDefault: this.getCharacterDefault,
  };
}

export const defineContentService = (
  accessToken: Readable<string | undefined>,
  stateKey: string = ENV_VARS.CONTEXT.ContentService.State,
  actionKey: string = ENV_VARS.CONTEXT.ContentService.Actions
): ContentService => {
  const service = new ContentService(stateKey, accessToken);
  setContext<ContentActions>(actionKey, service.actions);
  return service;
};

const useContentService = (
  actionKey: string = ENV_VARS.CONTEXT.ContentService.Actions,
  stateKey: string = ENV_VARS.CONTEXT.ContentService.State
) => {
  return {
    actions: useContentServiceActions(actionKey),
    state: useContentServiceState(stateKey),
  };
};

export default useContentService;
