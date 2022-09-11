import { setContext } from "svelte";
import { type Readable, type Writable, get } from "svelte/store";
import { getAPI } from "../../common/Utils/API";
import * as GlobalError from "../Error";
import type {
  Character,
  ContentItemSummary,
  Scenario,
  ScenarioSummary,
} from "../../models/Content";
import { getContentState, useContentServiceState } from "./state";
import { useContentServiceActions, type ContentActions } from "./actions";
import ENV_VARS from "../../common/Environment";

export class ContentService {
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

export const useContentService = (
  actionKey: string = ENV_VARS.CONTEXT.ContentService.Actions,
  stateKey: string = ENV_VARS.CONTEXT.ContentService.State
) => {
  return {
    actions: useContentServiceActions(actionKey),
    state: useContentServiceState(stateKey),
  };
};

export default useContentService;