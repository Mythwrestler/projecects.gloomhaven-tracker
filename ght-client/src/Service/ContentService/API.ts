import { ContentItemSummary, ScenarioDefault } from "../../models/Content";
import {
  requestCharacterListingFailure,
  requestCharacterListingSuccess,
  requestScenarioListing,
  requestScenarioListingFailure,
  requestScenarioListingSuccess,
} from "./Store";
import * as GlobalError from "../Error";
import { getAPI } from "../../common/Utils/API";
import { requestCharacterListing } from ".";

export const getScenarios = async (gameCode: string): Promise<void> => {
  try {
    requestScenarioListing();
    const result = await getAPI<ContentItemSummary[]>(
      `content/games/${gameCode}/scenarios`
    );
    requestScenarioListingSuccess(result);
  } catch (err: unknown) {
    requestScenarioListingFailure();
    GlobalError.showErrorMessage("Failed to get Scenario Listing");
  }
};

export const getCharacters = async (gameCode: string): Promise<void> => {
  try {
    requestCharacterListing();
    const result = await getAPI<ContentItemSummary[]>(
      `content/games/${gameCode}/characters`
    );
    requestCharacterListingSuccess(result);
  } catch (err: unknown) {
    requestCharacterListingFailure();
    GlobalError.showErrorMessage("Failed to get Scenario Listing");
  }
};
