import { setContext } from "svelte";
import { get, type Readable, type Writable } from "svelte/store";
import { compare as patchCompare, deepClone } from "fast-json-patch";
import ENV_VARS from "../../common/Environment";
import * as GlobalError from "../Error";
import { getAPI, patchAPI, postAPI, putAPI } from "../../common/Utils/API";
import type {
  Campaign,
  CampaignSummary,
  Character,
  Scenario,
} from "../../models/Campaign";
import { useCampaignServiceActions, type CampaignActions } from "./actions";
import { getCampaignState, useCampaignServiceState } from "./state";

class CampaignService {
  private authToken: Readable<string | undefined>;
  private campaignSummaries: Writable<CampaignSummary[]>;
  private campaignDetail: Writable<Campaign | undefined>;

  constructor(authToken: Readable<string | undefined>, stateKey: string) {
    const { campaignDetail, campaignSummaries } = getCampaignState(stateKey);
    this.authToken = authToken;
    this.campaignSummaries = campaignSummaries;
    this.campaignDetail = campaignDetail;
  }

  private getCampaignListing = async (): Promise<void> => {
    const token = get(this.authToken);
    try {
      const result = await getAPI<CampaignSummary[]>(`campaigns`, token);
      if (result && Array.isArray(result)) this.campaignSummaries.set(result);
      else throw new Error("Invalid response.");
    } catch (err: unknown) {
      GlobalError.showErrorMessage(
        `Failed to get Campaign Listing ${JSON.stringify(err)}`
      );
    }
  };

  private getCampaignDetail = async (campaignId: string): Promise<void> => {
    const token = get(this.authToken);
    try {
      const result = await getAPI<Campaign>(`campaigns/${campaignId}`, token);
      if (result) this.campaignDetail.set(result);
      else throw new Error("Invalid response.");
    } catch (err: unknown) {
      GlobalError.showErrorMessage(
        `Failed To Retrieve Campaign ${JSON.stringify(err)}`
      );
    }
  };

  private createCampaign = async (campaign: Campaign): Promise<void> => {
    const token = get(this.authToken);
    let result: Campaign;
    try {
      result = await postAPI<Campaign>("campaigns", token, {
        id: campaign.id,
        name: campaign.name,
        game: campaign.game,
      });
      if (result) this.campaignDetail.set(result);
    } catch (err: unknown) {
      GlobalError.showErrorMessage(
        `Failed To Create a New Campaign ${JSON.stringify(err)}`
      );
    }
  };

  public updateCampaign = async (
    campaignSummaryToReview: CampaignSummary
  ): Promise<void> => {
    const token = get(this.authToken);
    const currentCampaign = get(this.campaignDetail);
    if (currentCampaign == undefined) return;
    if (campaignSummaryToReview.id !== currentCampaign.id) return;
    const currentCampaignSummary: CampaignSummary = {
      id: currentCampaign.id,
      game: currentCampaign.game,
      editable: currentCampaign.editable,
      name: currentCampaign.name,
      description: currentCampaign.description,
    };
    const patchUpdatesToApply = patchCompare(
      currentCampaignSummary,
      campaignSummaryToReview
    );
    if (patchUpdatesToApply.length == 0) return;
    try {
      const result = await patchAPI<CampaignSummary>(
        `campaigns/${currentCampaign.id}`,
        token,
        patchUpdatesToApply
      );
      if (result) {
        this.campaignDetail.update((campaign) => {
          if (
            campaign == undefined ||
            campaign.id !== campaignSummaryToReview.id
          )
            return campaign;
          const updatedCampaign = { ...campaign };
          updatedCampaign.name = result.name;
          updatedCampaign.description = result.description;
          return updatedCampaign;
        });
      }
    } catch (err: unknown) {
      this.campaignDetail.set(currentCampaign);
      GlobalError.showErrorMessage(
        `Failed to update Campaign Summary ${JSON.stringify(err)}`
      );
    }
  };

  public clearCampaign = () => {
    this.campaignDetail.set(undefined);
  };

  //#region Campaign Party
  public addPartyMember = async (
    campaignId: string,
    character: Character
  ): Promise<void> => {
    const token = get(this.authToken);
    try {
      const result = await postAPI<Character>(
        `campaigns/${campaignId}/characters`,
        token,
        character
      );
      if (result) {
        this.campaignDetail.update((campaign) => {
          if (campaign === undefined || campaign.id != campaignId)
            return campaign;
          const updatedCampaign = deepClone(campaign) as Campaign;
          updatedCampaign.party.push(result);
          return updatedCampaign;
        });
      }
    } catch (err: unknown) {
      GlobalError.showErrorMessage("Failed to update Campaign Summary");
    }
  };

  public updatePartyMember = async (
    campaignId: string,
    character: Character
  ): Promise<void> => {
    const token = get(this.authToken);
    const campaign = get(this.campaignDetail);
    if (!campaign) return;
    const { party } = campaign;
    const currentCharacter = party.find(
      (chr) => (chr.characterContentCode = character.characterContentCode)
    );
    if (!currentCharacter) return;

    const patchUpdatesToApply = patchCompare(currentCharacter, character);
    if (patchUpdatesToApply.length == 0) return;

    try {
      const result = await patchAPI<Character>(
        `campaigns/${campaignId}/characters/${character.characterContentCode}`,
        token,
        patchUpdatesToApply
      );
      if (result) {
        this.campaignDetail.update((campaign) => {
          if (campaign === undefined || campaign.id != campaignId)
            return campaign;
          const updatedCampaign = deepClone(campaign) as Campaign;
          updatedCampaign.party.splice(
            campaign.party.findIndex(
              (chr) => chr.characterContentCode === result.characterContentCode
            ),
            1,
            result
          );
          return updatedCampaign;
        });
      }
    } catch (err: unknown) {
      GlobalError.showErrorMessage("Failed to update Campaign Summary");
    }
  };

  public getPartyMemberDetails = async (
    campaignId: string,
    characterContentCode: string
  ): Promise<void> => {
    const token = get(this.authToken);
    try {
      const result = await getAPI<Character>(
        `campaigns/${campaignId}/characters/${characterContentCode}`,
        token
      );
      if (result) {
        this.campaignDetail.update((campaign) => {
          if (campaign === undefined || campaign.id != campaignId)
            return campaign;
          const updatedCampaign = deepClone(campaign) as Campaign;
          updatedCampaign.party.splice(
            campaign.party.findIndex(
              (chr) => chr.characterContentCode === result.characterContentCode
            ),
            1,
            result
          );
          return updatedCampaign;
        });
      }
    } catch (err: unknown) {
      GlobalError.showErrorMessage("Failed to retrieve character details");
    }
  };

  //#endregion

  //#region Campaign Scenarios
  public addScenario = async (
    campaignId: string,
    scenario: Scenario
  ): Promise<void> => {
    const token = get(this.authToken);
    try {
      const result = await postAPI<Scenario>(
        `campaigns/${campaignId}/scenarios`,
        token,
        scenario
      );
      if (result) {
        this.campaignDetail.update((campaign) => {
          if (campaign === undefined || campaign.id !== campaignId)
            return campaign;
          const updatedCampaign = deepClone(campaign) as Campaign;
          updatedCampaign.scenarios.push(result);
          return updatedCampaign;
        });
      }
    } catch (err: unknown) {
      GlobalError.showErrorMessage("Failed to add scenario to campaign");
    }
  };

  public updateScenario = async (
    campaignId: string,
    scenario: Scenario
  ): Promise<void> => {
    const token = get(this.authToken);
    try {
      const result = await putAPI<Scenario>(
        `campaigns/${campaignId}/scenarios/${scenario.scenarioContentCode}`,
        token,
        scenario
      );
      if (result) {
        this.campaignDetail.update((campaign) => {
          if (campaign === undefined || campaign.id !== campaignId)
            return campaign;
          const updatedCampaign = deepClone(campaign) as Campaign;
          updatedCampaign.scenarios.splice(
            campaign.scenarios.findIndex(
              (scn) => scn.scenarioContentCode === result.scenarioContentCode
            ),
            1,
            result
          );
          return updatedCampaign;
        });
      }
    } catch (err: unknown) {
      GlobalError.showErrorMessage("Failed to update scenario for campaign");
    }
  };

  //#endregion

  public actions: CampaignActions = {
    getCampaignSummaries: this.getCampaignListing,
    getCampaignDetail: this.getCampaignDetail,
    createCampaign: this.createCampaign,
    updateCampaign: this.updateCampaign,
    clearCampaign: this.clearCampaign,
    addPartyMember: this.addPartyMember,
    updatePartyMember: this.updatePartyMember,
    getPartyMemberDetails: this.getPartyMemberDetails,
    addScenario: this.addScenario,
    updateScenario: this.updateScenario,
  };
}

export const defineCampaignService = (
  accessToken: Readable<string | undefined>,
  stateKey: string = ENV_VARS.CONTEXT.CampaignService.State,
  actionKey: string = ENV_VARS.CONTEXT.CampaignService.Actions
): CampaignService => {
  const service = new CampaignService(accessToken, stateKey);
  setContext<CampaignActions>(actionKey, service.actions);
  return service;
};

export const useCampaignService = (
  actionKey: string = ENV_VARS.CONTEXT.CampaignService.Actions,
  stateKey: string = ENV_VARS.CONTEXT.CampaignService.State
) => {
  return {
    actions: useCampaignServiceActions(actionKey),
    state: useCampaignServiceState(stateKey),
  };
};

export default useCampaignService;
