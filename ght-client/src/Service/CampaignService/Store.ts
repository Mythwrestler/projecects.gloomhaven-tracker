import { writable, Writable } from "svelte/store";
import { CampaignSummary } from "../../models";

export const campaignListingLoading: Writable<boolean> =
  writable<boolean>(false);
export const campaignListingLoaded: Writable<boolean> =
  writable<boolean>(false);
export const campaignListing: Writable<CampaignSummary[]> = writable<
  CampaignSummary[]
>([]);
export const requestCampaignListing = (): void => {
  campaignListingLoading.set(true);
};
export const requestCampaignListingSuccess = (
  campaigns: CampaignSummary[]
): void => {
  campaignListingLoading.set(false);
  campaignListingLoaded.set(true);
  campaignListing.set(campaigns);
};
export const requestCampaignListingFailure = (): void => {
  campaignListingLoading.set(false);
  campaignListingLoaded.set(false);
};
