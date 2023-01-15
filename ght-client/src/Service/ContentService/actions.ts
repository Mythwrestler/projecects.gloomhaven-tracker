import { getContext } from "svelte";
import ENV_VARS from "../../common/Environment";
import type {
  ContentItemSummary,
  Scenario,
  Character,
  ScenarioSummary,
} from "../../models/Content";

export interface ContentActions {
  getAvailableGames: () => Promise<ContentItemSummary[]>;
  getScenarioSummaries: (gameCode: string) => Promise<ScenarioSummary[]>;
  getScenarioDefault: (
    gameCode: string,
    scenarioCode: string
  ) => Promise<Scenario | undefined>;
  getCharacterSummaries: (gameCode: string) => Promise<ContentItemSummary[]>;
  getCharacterDefault: (
    gameCode: string,
    characterCode: string
  ) => Promise<Character | undefined>;
}

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
