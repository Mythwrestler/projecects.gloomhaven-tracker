import { getContext } from "svelte";
import ENV_VARS from "../../common/Environment";
import type {
  Campaign,
  CampaignSummary,
  Character,
  Scenario,
} from "../../models/Campaign";

export interface CampaignActions {
  getCampaignSummaries: () => Promise<void>;
  getCampaignDetail: (campaignId: string) => Promise<void>;
  createCampaign: (campaign: Campaign) => Promise<void>;
  updateCampaign: (campaign: CampaignSummary) => Promise<void>;
  clearCampaign: () => void;
  addPartyMember: (campaignId: string, character: Character) => Promise<void>;
  updatePartyMember: (
    campaignId: string,
    character: Character
  ) => Promise<void>;
  getPartyMemberDetails: (
    campaignId: string,
    characterContentCode: string
  ) => Promise<void>;
  addScenario: (campaignId: string, scenario: Scenario) => Promise<void>;
  updateScenario: (campaignId: string, scenario: Scenario) => Promise<void>;
}

export const useCampaignServiceActions = (
  actionKey: string = ENV_VARS.CONTEXT.ContentService.Actions
): CampaignActions => {
  const actions = getContext<CampaignActions | undefined>(actionKey);
  if (actions) {
    const properties = Object.keys(actions);
    if (
      properties.includes("getCampaignSummaries") &&
      properties.includes("getCampaignDetail") &&
      properties.includes("createCampaign") &&
      properties.includes("updateCampaign") &&
      properties.includes("clearCampaign") &&
      properties.includes("addPartyMember") &&
      properties.includes("updatePartyMember") &&
      properties.includes("getPartyMemberDetails") &&
      properties.includes("addScenario") &&
      properties.includes("updateScenario")
    )
      return actions;
  }

  const notImplemented = () => {
    throw Error("Campaign Service Actions Not Implemented");
  };
  return {
    getCampaignSummaries: notImplemented,
    getCampaignDetail: notImplemented,
    createCampaign: notImplemented,
    updateCampaign: notImplemented,
    clearCampaign: notImplemented,
    addPartyMember: notImplemented,
    updatePartyMember: notImplemented,
    getPartyMemberDetails: notImplemented,
    addScenario: notImplemented,
    updateScenario: notImplemented,
  };
};
