import { getAPI } from "../../common/Utils/API";
import { Campaign, CampaignSummary } from "../../models";
import * as CampaignStore from "./Store";
import * as GlobalError from "../../Service/Error";

export const getCampaigns = async (): Promise<void> => {
  try {
    CampaignStore.requestCampaignListing();
    const result = await getAPI<CampaignSummary[]>(`campaigns`);
    CampaignStore.requestCampaignListingSuccess(result);
  } catch (err: unknown) {
    CampaignStore.requestCampaignListingFailure();
    GlobalError.showErrorMessage("Failed to get Campaign Listing");
  }
};

export const getCampaign = async (
  campaignId: string
): Promise<Campaign | undefined> => {
  try {
    console.log(`campaign/${campaignId}`);
    const result = await getAPI<Campaign>(`campaigns/${campaignId}`);
    return result;
  } catch (err: unknown) {
    GlobalError.showErrorMessage("Failed To Retrieve Campaign");
  }
  return undefined;
};
