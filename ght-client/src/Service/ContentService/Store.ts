import { writable, Writable } from "svelte/store";
import { ContentItemSummary } from "../../models/Content";

// Scenario Content Listing
export const scenarioListingLoading: Writable<boolean> =
  writable<boolean>(false);
export const scenarioListingLoaded: Writable<boolean> =
  writable<boolean>(false);
export const scenarioListing: Writable<ContentItemSummary[]> = writable<
  ContentItemSummary[]
>([]);
export const requestScenarioListing = (): void => {
  scenarioListingLoading.set(true);
};
export const requestScenarioListingSuccess = (
  scenarios: ContentItemSummary[]
): void => {
  scenarioListingLoading.set(false);
  scenarioListingLoaded.set(true);
  scenarioListing.set(scenarios);
};
export const requestScenarioListingFailure = (): void => {
  scenarioListingLoading.set(false);
  scenarioListingLoaded.set(false);
};
