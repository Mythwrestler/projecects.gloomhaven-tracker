import { setContext } from "svelte";
import { type Readable, get } from "svelte/store";
import { getAPI } from "../../common/Utils/API";
import * as GlobalError from "../Error";
import type {
  Character,
  ContentItemSummary,
  Scenario,
  ScenarioSummary,
} from "../../models/Content";
import { useContentServiceActions, type ContentActions } from "./actions";
import ENV_VARS from "../../common/Environment";

export class ContentService {
  private accessToken: Readable<string | undefined>;

  private scenarioSummaries: Map<string, ScenarioSummary[]> = new Map<
    string,
    ScenarioSummary[]
  >();
  private scenarioDefaults: Map<string, Map<string, Scenario>> = new Map<
    string,
    Map<string, Scenario>
  >();
  private characterSummaries: Map<string, ContentItemSummary[]> = new Map<
    string,
    ContentItemSummary[]
  >();
  private characterDefaults: Map<string, Map<string, Character>> = new Map<
    string,
    Map<string, Character>
  >();

  constructor(accessToken: Readable<string | undefined>) {
    this.accessToken = accessToken;
  }

  // #region Games
  // -------------------------
  private availableGames: ContentItemSummary[] = [];

  private getAvailableGames = async (): Promise<ContentItemSummary[]> => {
    if (this.availableGames.length === 0) {
      try {
        const token: string | undefined = get(this.accessToken);
        const result = await getAPI<ContentItemSummary[]>(
          `content/games/`,
          token
        );
        this.availableGames = result;
      } catch (err: unknown) {
        GlobalError.showErrorMessage(
          `Failed to get Game Listing. ${JSON.stringify(err)}`
        );
      }
    }
    return this.availableGames;
  };

  // -------------------------
  // #endregion Games

  // #region Scenario Summaries
  // -------------------------
  private getScenarioSummaries = async (
    gameCode: string
  ): Promise<ScenarioSummary[]> => {
    if (!this.scenarioSummaries.has(gameCode)) {
      try {
        const token: string | undefined = get(this.accessToken);
        const result = await getAPI<ScenarioSummary[]>(
          `content/games/${gameCode}/scenarios`,
          token
        );
        if (Array.isArray(result)) {
          this.scenarioSummaries.set(gameCode, result);
        }
      } catch (err: unknown) {
        GlobalError.showErrorMessage(
          `Failed to get Scenario Summary Listing. ${JSON.stringify(err)}`
        );
      }
    }
    return this.scenarioSummaries.get(gameCode) || [];
  };

  // -------------------------
  // #endregion

  // #region Scenario Default Values
  // -------------------------
  private getScenarioDefault = async (
    gameCode: string,
    scenarioCode: string
  ): Promise<Scenario | undefined> => {
    let scenarioMap: Map<string, Scenario>;
    if (!this.scenarioDefaults.has(gameCode)) {
      scenarioMap = new Map<string, Scenario>();
      this.scenarioDefaults.set(gameCode, new Map<string, Scenario>());
    } else {
      scenarioMap = this.scenarioDefaults.get(gameCode) as Map<
        string,
        Scenario
      >;
    }

    if (!scenarioMap.has(scenarioCode)) {
      try {
        const token: string | undefined = get(this.accessToken);
        const result = await getAPI<Scenario>(
          `content/games/${gameCode}/scenarios/${scenarioCode}`,
          token
        );
        if (result !== undefined) scenarioMap.set(scenarioCode, result);
      } catch (err: unknown) {
        GlobalError.showErrorMessage(
          `Failed to get Scenario Default Values. ${JSON.stringify(err)}`
        );
      }
    }

    return scenarioMap.get(scenarioCode);
  };

  // -------------------------
  // #endregion

  // #region Character Summaries
  // -------------------------
  private getCharacterSummaries = async (
    gameCode: string
  ): Promise<ContentItemSummary[]> => {
    if (!this.characterSummaries.has(gameCode)) {
      try {
        const token: string | undefined = get(this.accessToken);
        const result = await getAPI<ContentItemSummary[]>(
          `content/games/${gameCode}/characters`,
          token
        );
        if (Array.isArray(result)) {
          this.characterSummaries.set(gameCode, result);
        }
      } catch (err: unknown) {
        GlobalError.showErrorMessage(
          `Failed to get Scenario Summary Listing. ${JSON.stringify(err)}`
        );
      }
    }
    return this.characterSummaries.get(gameCode) || [];
  };

  // -------------------------
  // #endregion

  // #region Character Default Values
  // -------------------------
  private getCharacterDefault = async (
    gameCode: string,
    characterCode: string
  ): Promise<Character | undefined> => {
    let characterMap: Map<string, Character>;
    if (!this.characterDefaults.has(gameCode)) {
      characterMap = new Map<string, Character>();
      this.characterDefaults.set(gameCode, new Map<string, Character>());
    } else {
      characterMap = this.characterDefaults.get(gameCode) as Map<
        string,
        Character
      >;
    }

    if (!characterMap.has(characterCode)) {
      try {
        const token: string | undefined = get(this.accessToken);
        const result = await getAPI<Character>(
          `content/games/${gameCode}/characters/${characterCode}`,
          token
        );
        if (result !== undefined) characterMap.set(characterCode, result);
      } catch (err: unknown) {
        GlobalError.showErrorMessage(
          `Failed to get Character Default Values. ${JSON.stringify(err)}`
        );
      }
    }

    return characterMap.get(characterCode);
  };

  // -------------------------
  // #endregion

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
  actionKey: string = ENV_VARS.CONTEXT.ContentService.Actions
): ContentService => {
  const service = new ContentService(accessToken);
  setContext<ContentActions>(actionKey, service.actions);
  return service;
};

export const useContentService = (
  actionKey: string = ENV_VARS.CONTEXT.ContentService.Actions
) => {
  return {
    actions: useContentServiceActions(actionKey),
  };
};

export default useContentService;
