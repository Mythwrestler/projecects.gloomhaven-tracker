import { setContext } from "svelte";
import { get, type Readable } from "svelte/store";
import ENV_VARS from "../../common/Environment";
import { getAPI, patchAPI, postAPI, putAPI } from "../../common/Utils/API";
import type {
  Campaign,
  CampaignSummary,
  Character,
  Scenario,
} from "../../models/Campaign";
import { useCampaignServiceActions, type CampaignActions } from "./actions";
import * as GlobalError from "../Error";
import { compare as patchCompare, deepClone } from "fast-json-patch";

class CampaignService {
  private accessToken: Readable<string | undefined>;

  constructor(accessToken: Readable<string | undefined>) {
    this.accessToken = accessToken;
  }

  private getCampaignSummaries = async (): Promise<CampaignSummary[]> => {
    try {
      const token: string | undefined = get(this.accessToken);
      return await getAPI<CampaignSummary[]>(`campaigns/`, token);
    } catch (err: unknown) {
      GlobalError.showErrorMessage(
        `Failed To Retrieve Campaign ${JSON.stringify(err)}`
      );
      return [];
    }
  };

  private getCampaignDetail = async (
    campaignId: string
  ): Promise<Campaign | undefined> => {
    try {
      const token: string | undefined = get(this.accessToken);
      return await getAPI<Campaign>(`campaigns/${campaignId}`, token);
    } catch (err: unknown) {
      GlobalError.showErrorMessage(
        `Failed To Retrieve Campaign ${JSON.stringify(err)}`
      );
      return undefined;
    }
  };

  private createCampaign = async (
    campaign: Campaign
  ): Promise<Campaign | undefined> => {
    try {
      const token: string | undefined = get(this.accessToken);
      return await postAPI<Campaign>("campaigns", token, {
        id: campaign.id,
        name: campaign.name,
        game: campaign.game,
      });
    } catch (err: unknown) {
      GlobalError.showErrorMessage(
        `Failed to create Campaign ${JSON.stringify(err)}`
      );
      return undefined;
    }
  };

  private updateCampaign = async (
    originalCampaign: Campaign,
    updatedCampaignSummary: CampaignSummary
  ): Promise<Campaign> => {
    try {
      const token: string | undefined = get(this.accessToken);
      const currentCampaign = deepClone(originalCampaign) as Campaign;
      const currentCampaignSummary: CampaignSummary = {
        id: currentCampaign.id,
        game: currentCampaign.game,
        editable: currentCampaign.editable,
        name: currentCampaign.name,
        description: currentCampaign.description,
      };
      const patchUpdatesToApply = patchCompare(
        currentCampaignSummary,
        updatedCampaignSummary
      );
      if (patchUpdatesToApply.length == 0) {
        return currentCampaign;
      } else {
        const result = await patchAPI<CampaignSummary>(
          `campaigns/${currentCampaign.id}`,
          token,
          patchUpdatesToApply
        );
        if (result) {
          currentCampaign.name = result.name;
          currentCampaign.description = result.description;
        }
      }
      return currentCampaign;
    } catch (err: unknown) {
      GlobalError.showErrorMessage(
        `Failed to update Campaign ${JSON.stringify(err)}`
      );
      return originalCampaign;
    }
  };

  private getPartyMemberDetails = async (
    campaignId: string,
    characterContentCode: string
  ): Promise<Character | undefined> => {
    try {
      const token: string | undefined = get(this.accessToken);
      return await getAPI<Character>(
        `campaigns/${campaignId}/characters/${characterContentCode}`,
        token
      );
    } catch (err: unknown) {
      GlobalError.showErrorMessage(
        `Failed to retrieve campaign character ${JSON.stringify(err)}`
      );
      return undefined;
    }
  };

  private addPartyMember = async (
    campaignId: string,
    character: Character
  ): Promise<Character | undefined> => {
    try {
      const token: string | undefined = get(this.accessToken);
      return await postAPI<Character>(
        `campaigns/${campaignId}/characters`,
        token,
        character
      );
    } catch (err: unknown) {
      GlobalError.showErrorMessage(
        `Failed to add campaign character ${JSON.stringify(err)}`
      );
      return undefined;
    }
  };

  private updatePartyMember = async (
    campaignId: string,
    originalCharacter: Character,
    updatedCharacter: Character
  ): Promise<Character> => {
    const currentCharacter = deepClone(originalCharacter) as Character;

    try {
      const token: string | undefined = get(this.accessToken);
      const patchUpdatesToApply = patchCompare(
        currentCharacter,
        updatedCharacter
      );

      if (patchUpdatesToApply.length == 0) {
        return currentCharacter;
      } else {
        const result = await patchAPI<Character>(
          `campaigns/${campaignId}/characters/${currentCharacter.characterContentCode}`,
          token,
          patchUpdatesToApply
        );
        return result;
      }
    } catch (err: unknown) {
      GlobalError.showErrorMessage(
        `Failed to update campaign character ${JSON.stringify(err)}`
      );
      return currentCharacter;
    }
  };

  private addScenario = async (
    campaignId: string,
    scenario: Scenario
  ): Promise<Scenario | undefined> => {
    try {
      const token: string | undefined = get(this.accessToken);
      return await postAPI<Scenario>(
        `campaigns/${campaignId}/scenarios`,
        token,
        scenario
      );
    } catch (err: unknown) {
      GlobalError.showErrorMessage(
        `Failed to add campaign scenario ${JSON.stringify(err)}`
      );
      return undefined;
    }
  };

  private updateScenario = async (
    campaignId: string,
    scenario: Scenario
  ): Promise<Scenario> => {
    try {
      const token: string | undefined = get(this.accessToken);
      return await putAPI<Scenario>(
        `campaigns/${campaignId}/scenarios/${scenario.scenarioContentCode}`,
        token,
        scenario
      );
    } catch (err: unknown) {
      throw err as Error;
    }
  };

  public actions: CampaignActions = {
    getCampaignSummaries: this.getCampaignSummaries,
    getCampaignDetail: this.getCampaignDetail,
    createCampaign: this.createCampaign,
    updateCampaign: this.updateCampaign,
    getPartyMemberDetails: this.getPartyMemberDetails,
    addPartyMember: this.addPartyMember,
    updatePartyMember: this.updatePartyMember,
    addScenario: this.addScenario,
    updateScenario: this.updateScenario,
  };
}

export const defineCampaignService = (
  accessToken: Readable<string | undefined>,
  actionKey: string = ENV_VARS.CONTEXT.CampaignService.Actions
): CampaignService => {
  const service = new CampaignService(accessToken);
  setContext<CampaignActions>(actionKey, service.actions);
  return service;
};

export const useCampaignService = (
  actionKey: string = ENV_VARS.CONTEXT.CampaignService.Actions
) => {
  return {
    actions: useCampaignServiceActions(actionKey),
  };
};

export default useCampaignService;
