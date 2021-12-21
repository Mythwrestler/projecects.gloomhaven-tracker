import { getAPI, postAPI } from "../../common/Utils/API";
import { Actors, CombatSpace, CombatSpaceSummary } from "../../models";
import { ScenarioDefault } from "../../models/Content";
import * as CombatStore from "./Store";
import * as GlobalError from "../../Service/Error";

export const getCombatSpaces = async (): Promise<void> => {
  try {
    CombatStore.requestCombatSpaces();
    const result = await getAPI<CombatSpaceSummary[]>("combatspace");
    CombatStore.requestCombatSpacesSuccess(result);
  } catch (err: unknown) {
    CombatStore.requestCombatSpacesFailure();
    GlobalError.showErrorMessage("Failed to get list of combat spaces");
  }
};

export const getCombatSpace = async (
  combatId: string
): Promise<CombatSpace | undefined> => {
  try {
    console.log(`combatspace/${combatId}`);
    const result = await getAPI<CombatSpace>(`combatspace/${combatId}`);
    return result;
  } catch (err: unknown) {
    GlobalError.showErrorMessage("Failed to get list of combat spaces");
  }
  return undefined;
};

export const getScenarios = async (gameCode: string): Promise<void> => {
  try {
    CombatStore.requestScenarioListing();
    const result = await getAPI<ScenarioDefault[]>(
      `content/games/${gameCode}/scenarios`
    );
    CombatStore.requestScenarioListingSuccess(result);
  } catch (err: unknown) {
    CombatStore.requestScenarioListingFailure();
    GlobalError.showErrorMessage("Failed to get Scenario Listing");
  }
};

export const addCombatSpace = async (
  gameCode: string,
  scenarioCode: string,
  description: string
): Promise<CombatSpaceSummary | undefined> => {
  try {
    const result = await postAPI<CombatSpaceSummary>("combatspace/new", {
      gameCode,
      scenarioCode,
      description,
    });
    CombatStore.requestNewCombatSpaceSuccess(result);
    return result;
  } catch (err: unknown) {
    GlobalError.showErrorMessage("Failed to get new combatId");
  }
};

export const addActor = async (
  combatId: string,
  actors: Actors
): Promise<void> => {
  try {
    return await postAPI(`combatspace/${combatId}/actors`, actors);
  } catch (err: unknown) {
    GlobalError.showErrorMessage("Failed to add actors");
  }
};
