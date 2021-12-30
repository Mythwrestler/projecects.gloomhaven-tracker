import { getAPI, postAPI, putAPI } from "../../common/Utils/API";
import {
  Campaign,
  CampaignSummary,
  Character,
  ContentItemSummary,
} from "../../models";
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
    const result = await getAPI<Campaign>(`campaigns/${campaignId}`);
    return result;
  } catch (err: unknown) {
    GlobalError.showErrorMessage("Failed To Retrieve Campaign");
  }
  return undefined;
};

export const addCampaign = async (
  campaign: Campaign
): Promise<CampaignSummary | undefined> => {
  try {
    const result = await postAPI<CampaignSummary>(`campaigns/new`, campaign);
    await getCampaigns();
    return result;
  } catch (err: unknown) {
    GlobalError.showErrorMessage("Failed To Create Campaign");
  }
};

export const getCharacterForCampaign = async (
  campaignId: string,
  characterCode: string
): Promise<Character | undefined> => {
  try {
    const result = await getAPI<Character>(
      `campaigns/${campaignId}/characters/${characterCode}`
    );
    return result;
  } catch (err: unknown) {
    GlobalError.showErrorMessage("Failed To Retrieve Character");
  }
  return undefined;
};

export const addCharacterForCampaign = async (
  campaignId: string,
  character: Character
): Promise<Character | undefined> => {
  try {
    const result = await postAPI<Character>(
      `campaigns/${campaignId}/characters`,
      character
    );
    return result;
  } catch (err: unknown) {
    GlobalError.showErrorMessage("Failed To Retrieve Character");
  }
  return undefined;
};

export const updateCharacterForCampaign = async (
  campaignId: string,
  character: Character
): Promise<Character | undefined> => {
  try {
    const result = await putAPI<Character>(
      `campaigns/${campaignId}/characters`,
      character
    );
    return result;
  } catch (err: unknown) {
    GlobalError.showErrorMessage("Failed To Update Character");
  }
};
