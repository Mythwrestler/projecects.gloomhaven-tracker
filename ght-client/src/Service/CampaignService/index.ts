import { derived, get, writable } from "svelte/store";
import type { Readable } from "svelte/store";
import { getAPI, patchAPI, postAPI, putAPI } from "../../common/Utils/API";
import * as GlobalError from "../Error";
import type {
  CampaignSummary,
  Campaign,
  Character,
  Scenario,
} from "../../models/Campaign";
import { cloneDeep, compact, isEqual, update } from "lodash";
import { compare as patchCompare, deepClone } from "fast-json-patch";
import type { Operation as PatchOperation } from "fast-json-patch";
import { AsyncQueue } from "../../common/Utils/AsycnQueue";

class CampaignServiceImplementation {
  private authToken?: string;

  private requestQueue = new AsyncQueue<unknown>();

  constructor(authTokenStore: Readable<string | undefined>) {
    authTokenStore.subscribe((token) => (this.authToken = token));
  }

  //#region Campaign Listing
  private campaignListingStore = writable<CampaignSummary[]>([]);
  private campaignListing = derived(
    this.campaignListingStore,
    ($store) => $store,
    []
  );

  public getCampaignListing = async () => {
    try {
      const result = await getAPI<CampaignSummary[]>(
        `campaigns`,
        this.authToken ?? ""
      );
      if (result && result.length > 0) this.campaignListingStore.set(result);
    } catch (err: unknown) {
      GlobalError.showErrorMessage("Failed to get Campaign Listing");
    }
  };
  //#endregion

  //#region Campaign
  private campaignStore = writable<Campaign | undefined>();
  private campaign: Readable<Campaign | undefined> = derived(
    this.campaignStore,
    ($store) => $store
  );

  public getCampaign = async (campaignId: string) => {
    try {
      const result = await getAPI<Campaign>(
        `campaigns/${campaignId}`,
        this.authToken ?? ""
      );
      if (result) {
        this.campaignStore.set(result);
      }
    } catch (err: unknown) {
      GlobalError.showErrorMessage("Failed To Retrieve Campaign");
    }
  };

  public createNewCampaign = async (campaign: Campaign): Promise<void> => {
    return this.requestQueue.enqueue(async () => {
      try {
        const result = await postAPI<Campaign>("campaigns", this.authToken, {
          id: campaign.id,
          name: campaign.name,
          game: campaign.game,
        });
        if (result) {
          this.campaignStore.set(result);
          // this.savedCampaign.set(result);
          await this.getCampaignListing();
        }
      } catch (ex) {
        GlobalError.showErrorMessage("Failed To Create a New Campaign");
      }
    }) as Promise<void>;
  };

  public updateCampaign = async (
    campaignSummaryToReview: CampaignSummary
  ): Promise<void> => {
    const currentCampaign = get(this.campaign);
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
    return this.requestQueue.enqueue(async () => {
      try {
        const result = await patchAPI<CampaignSummary>(
          `campaigns/${currentCampaign.id}`,
          this.authToken,
          patchUpdatesToApply
        );
        if (result) {
          this.campaignStore.update((campaign) => {
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
        this.campaignStore.set(currentCampaign);
        GlobalError.showErrorMessage("Failed to update Campaign Summary");
      }
    }) as Promise<void>;
  };

  public clearCampaign = () => {
    this.campaignStore.set(undefined);
  };

  //#endregion

  //#region Campaign Party
  public addPartyMember = async (campaignId: string, character: Character) => {
    return this.requestQueue.enqueue(async () => {
      try {
        const result = await postAPI<Character>(
          `campaigns/${campaignId}/characters`,
          this.authToken,
          character
        );
        if (result) {
          this.campaignStore.update((campaign) => {
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
    });
  };

  public updatePartyMember = async (
    campaignId: string,
    character: Character
  ) => {
    const campaign = get(this.campaign);
    if (!campaign) return;
    const { party } = campaign;
    const currentCharacter = party.find(
      (chr) => (chr.characterContentCode = character.characterContentCode)
    );
    if (!currentCharacter) return;

    const patchUpdatesToApply = patchCompare(currentCharacter, character);
    if (patchUpdatesToApply.length == 0) return;

    return this.requestQueue.enqueue(async () => {
      try {
        const result = await patchAPI<Character>(
          `campaigns/${campaignId}/characters/${character.characterContentCode}`,
          this.authToken,
          patchUpdatesToApply
        );
        if (result) {
          this.campaignStore.update((campaign) => {
            if (campaign === undefined || campaign.id != campaignId)
              return campaign;
            const updatedCampaign = deepClone(campaign) as Campaign;
            updatedCampaign.party.splice(
              campaign.party.findIndex(
                (chr) =>
                  chr.characterContentCode === result.characterContentCode
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
    });
  };

  public getPartyMemberDetails = async (
    campaignId: string,
    characterContentCode: string
  ) => {
    return this.requestQueue.enqueue(async () => {
      try {
        const result = await getAPI<Character>(
          `campaigns/${campaignId}/characters/${characterContentCode}`,
          this.authToken
        );
        if (result) {
          this.campaignStore.update((campaign) => {
            if (campaign === undefined || campaign.id != campaignId)
              return campaign;
            const updatedCampaign = deepClone(campaign) as Campaign;
            updatedCampaign.party.splice(
              campaign.party.findIndex(
                (chr) =>
                  chr.characterContentCode === result.characterContentCode
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
    });
  };

  //#endregion

  //#region Campaign Scenarios
  public addScenario = async (campaignId: string, scenario: Scenario) => {
    return this.requestQueue.enqueue(async () => {
      try {
        const result = await postAPI<Scenario>(
          `campaigns/${campaignId}/scenarios`,
          this.authToken,
          scenario
        );
        if (result) {
          this.campaignStore.update((campaign) => {
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
    });
  };

  public updateScenario = async (campaignId: string, scenario: Scenario) => {
    return this.requestQueue.enqueue(async () => {
      try {
        const result = await putAPI<Scenario>(
          `campaigns/${campaignId}/scenarios/${scenario.scenarioContentCode}`,
          this.authToken,
          scenario
        );
        if (result) {
          this.campaignStore.update((campaign) => {
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
    });
  };

  //#endregion

  public State = {
    campaignListing: this.campaignListing,
    campaign: this.campaign,
  };
}

let campaignService: CampaignServiceImplementation | undefined = undefined;
const useCampaignService = (
  accessToken: Readable<string | undefined>
): CampaignServiceImplementation => {
  if (!campaignService)
    campaignService = new CampaignServiceImplementation(accessToken);
  return campaignService;
};

export { useCampaignService };
