import { getContext } from "svelte";
import ENV_VARS from "../../common/Environment";

export interface ContentActions {
  getAvailableGames: () => void;
  getScenarioSummaries: (gameCode: string) => void;
  getScenarioDefault: (gameCode: string, scenarioCode: string) => void;
  getCharacterSummaries: (gameCode: string) => void;
  getCharacterDefault: (gameCode: string, characterCode: string) => void;
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
