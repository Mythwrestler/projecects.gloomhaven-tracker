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

// Scenario Content Listing
export const characterListingLoading: Writable<boolean> =
  writable<boolean>(false);
export const characterListingLoaded: Writable<boolean> =
  writable<boolean>(false);
export const characterListing: Writable<ContentItemSummary[]> = writable<
  ContentItemSummary[]
>([]);
export const requestCharacterListing = (): void => {
  characterListingLoading.set(true);
};
export const requestCharacterListingSuccess = (
  characters: ContentItemSummary[]
): void => {
  characterListingLoading.set(false);
  characterListingLoaded.set(true);
  characterListing.set(characters);
};
export const requestCharacterListingFailure = (): void => {
  characterListingLoading.set(false);
  characterListingLoaded.set(false);
};
