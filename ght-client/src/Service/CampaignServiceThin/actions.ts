import { getContext } from "svelte";
import ENV_VARS from "../../common/Environment";
import type {
  Campaign,
  CampaignSummary,
  Character,
  Scenario,
} from "../../models/Campaign";

export interface CampaignActions {
  getCampaignSummaries: () => Promise<CampaignSummary[]>;
  getCampaignDetail: (campaignId: string) => Promise<Campaign | undefined>;
  createCampaign: (campaign: Campaign) => Promise<Campaign | undefined>;
  updateCampaign: (
    originalCampaign: Campaign,
    updatedCampaignSummary: CampaignSummary
  ) => Promise<Campaign>;
  getPartyMemberDetails: (
    campaignId: string,
    characterContentCode: string
  ) => Promise<Character | undefined>;
  addPartyMember: (
    campaignId: string,
    character: Character
  ) => Promise<Character | undefined>;
  updatePartyMember: (
    campaignId: string,
    originalCharacter: Character,
    updatedCharacter: Character
  ) => Promise<Character>;
  addScenario: (
    campaignId: string,
    scenario: Scenario
  ) => Promise<Scenario | undefined>;
  updateScenario: (campaignId: string, scenario: Scenario) => Promise<Scenario>;
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
      properties.includes("getPartyMemberDetails") &&
      properties.includes("addPartyMember") &&
      properties.includes("updatePartyMember") &&
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
    addPartyMember: notImplemented,
    updatePartyMember: notImplemented,
    getPartyMemberDetails: notImplemented,
    addScenario: notImplemented,
    updateScenario: notImplemented,
  };
};
