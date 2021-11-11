import { getAPI, postAPI } from "../../../common/Utils/API";
import { Actors, CombatSpaceSummary } from "../../../models";
import { Scenario } from "../../../models/Content";
import * as CombatStore from "./Store";

export const getCombatSpaces = async (): Promise<void> => {
  try {
    CombatStore.requestCombatSpaces();
    const result = await getAPI<CombatSpaceSummary[]>("combatspace");
    CombatStore.requestCombatSpacesSuccess(result);
  } catch (err: unknown) {
    CombatStore.requestCombatSpacesFailure();
    CombatStore.showErrorMessage("Failed to get list of combat spaces");
  }
};

export const getScenarios = async (gameCode: string): Promise<void> => {
  try {
    CombatStore.requestScenarioListing();
    const result = await getAPI<Scenario[]>(
      `content/games/${gameCode}/scenarios`
    );
    CombatStore.requestScenarioListingSuccess(result);
  } catch (err: unknown) {
    CombatStore.requestScenarioListingFailure();
    CombatStore.showErrorMessage("Failed to get Scenario Listing");
  }
};

// export const getScenario = async (gameCode: string, scenarioCode: string): Promise<void> => {
//   try {
//     console.log("request Starting");
//     CombatStore.requestScenarioListing();

//     console.log("making request");
//     const result = await getAPI<Scenario[]>(
//       `content/games/${gameCode}/scenarios`
//     );

//     console.log("request made");
//     try {
//       CombatStore.requestScenarioListingSuccess(result);
//     } catch {
//       console.log("Why is this breaking...?");
//     }
//   } catch (err: unknown) {
//     console.log("request failed");
//     CombatStore.requestScenarioListingFailure();
//     CombatStore.showErrorMessage("Failed to get Scenario Listing");
//   }
// };

export const addCombatSpace = async (
  description: string
): Promise<CombatSpaceSummary | undefined> => {
  try {
    const result = await postAPI<CombatSpaceSummary>("combatspace/new", {
      description,
    });
    CombatStore.requestNewCombatSpaceSuccess(result);
    return result;
  } catch (err: unknown) {
    CombatStore.showErrorMessage("Failed to get new combatId");
  }
};

export const addActor = async (
  combatId: string,
  actors: Actors
): Promise<void> => {
  try {
    return await postAPI(`combatspace/${combatId}/actors`, actors);
  } catch (err: unknown) {
    CombatStore.showErrorMessage("Failed to add actors");
  }
};
