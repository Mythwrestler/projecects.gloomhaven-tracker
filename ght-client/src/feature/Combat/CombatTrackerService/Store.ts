import { Writable, writable } from "svelte/store";
import { CombatSpaceSummary } from "../../../models";
import { ContentItemSummary, Scenario } from "../../../models/Content";

// Global Error Messaging
export const displayErrorMessage: Writable<boolean> = writable<boolean>(false);
export const errorMessage: Writable<string> = writable<string>("");
export const showErrorMessage = (message: string): void => {
  displayErrorMessage.set(true);
  errorMessage.set(message);
};
export const clearErrorMessage = (): void => {
  displayErrorMessage.set(false);
  errorMessage.set("");
};

// Hub Connection
export const combatHubConnecting: Writable<boolean> = writable<boolean>(false);
export const combatHubConnected: Writable<boolean> = writable<boolean>(false);
export const requestCombatHubConnection = (): void => {
  combatHubConnected.set(false);
  combatHubConnecting.set(true);
};
export const requestCombatHubConnectionSuccess = (): void => {
  combatHubConnected.set(true);
  combatHubConnecting.set(false);
};
export const requestCombatHubConnectionFailure = (): void => {
  combatHubConnected.set(false);
  combatHubConnecting.set(false);
};

// Combat Space Connection
export const combatSpaceConnecting: Writable<boolean> =
  writable<boolean>(false);
export const combatSpaceDisconnecting: Writable<boolean> =
  writable<boolean>(false);
export const combatSpaceConnected: Writable<boolean> = writable<boolean>(false);
export const combatSpaceId: Writable<string> = writable<string>("");
export const requestCombatSpaceConnection = (): void => {
  combatSpaceConnecting.set(true);
};
export const requestCombatSpaceConnectionSuccess = (combatId: string): void => {
  combatSpaceConnected.set(true);
  combatSpaceConnecting.set(false);
  combatSpaceId.set(combatId);
};
export const requestCombatSpaceConnectionFailure = (): void => {
  combatSpaceConnecting.set(false);
};
export const requestCombatSpaceDisconnect = (): void => {
  combatSpaceDisconnecting.set(true);
};
export const requestCombatSpaceDisconnectSuccess = (): void => {
  combatSpaceConnected.set(false);
  combatSpaceDisconnecting.set(false);
  combatSpaceId.set("");
};
export const requestCombatSpaceDisconnectFailure = (): void => {
  combatSpaceDisconnecting.set(false);
};

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
  console.log(JSON.stringify(scenarios));
  scenarioListing.set(scenarios);
};
export const requestScenarioListingFailure = (): void => {
  scenarioListingLoading.set(false);
  scenarioListingLoaded.set(false);
};

// Combat Space Listing
export const combatSpacesLoading: Writable<boolean> = writable<boolean>(false);
export const combatSpaces: Writable<CombatSpaceSummary[]> = writable<
  CombatSpaceSummary[]
>([]);
export const requestCombatSpaces = (): void => combatSpacesLoading.set(true);
export const requestCombatSpacesSuccess = (
  spaces: CombatSpaceSummary[]
): void => {
  combatSpacesLoading.set(false);
  combatSpaces.set(spaces);
};
export const requestCombatSpacesFailure = (): void =>
  combatSpacesLoading.set(false);
export const requestNewCombatSpaceSuccess = (
  combatSpace: CombatSpaceSummary
): void =>
  combatSpaces.update((spaces: CombatSpaceSummary[]) => [
    ...spaces,
    combatSpace,
  ]);
