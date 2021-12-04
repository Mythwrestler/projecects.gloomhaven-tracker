import { Scenario } from "../../models/Content";
import {
  requestScenarioListing,
  requestScenarioListingFailure,
  requestScenarioListingSuccess,
} from "./Store";
import * as GlobalError from "../Error";
import { getAPI } from "../../common/Utils/API";

export const getScenarios = async (gameCode: string): Promise<void> => {
  try {
    requestScenarioListing();
    const result = await getAPI<Scenario[]>(
      `content/games/${gameCode}/scenarios`
    );
    requestScenarioListingSuccess(result);
  } catch (err: unknown) {
    requestScenarioListingFailure();
    GlobalError.showErrorMessage("Failed to get Scenario Listing");
  }
};
