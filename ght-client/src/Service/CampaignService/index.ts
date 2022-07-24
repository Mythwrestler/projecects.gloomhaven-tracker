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
import { cloneDeep, compact, isEqual } from "lodash";
import { compare as patchCompare } from "fast-json-patch";
import type { Operation as PatchOperation } from "fast-json-patch";

class CampaignServiceImplementation {
  private authToken?: string;

  constructor(authTokenStore: Readable<string | undefined>) {
    authTokenStore.subscribe((token) => (this.authToken = token));
    this.campaign.subscribe((campaign) => {
      const savedCampaign = get(this.savedCampaign);
      if (campaign && savedCampaign) {
        const patches = patchCompare(savedCampaign, campaign);
        this.pendingPatches.set(patches);
      }
    });
    this.savedCampaign.subscribe((saved) => {
      const campaign = get(this.campaignStore);
      if (campaign && saved) {
        const patches = patchCompare(saved, campaign);
        this.pendingPatches.set(patches);
      }
    });
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
  private savedCampaign = writable<Campaign | undefined>();
  private pendingPatches = writable<PatchOperation[]>([]);
  private campaign: Readable<Campaign | undefined> = derived(
    this.campaignStore,
    ($store) => $store
  );
  private campaignNotSaved = derived(
    [this.pendingPatches],
    (pendingPatches) => {
      return pendingPatches.length > 0;
    },
    false
  );

  public getCampaign = async (campaignId: string) => {
    try {
      const result = await getAPI<Campaign>(
        `campaigns/${campaignId}`,
        this.authToken ?? ""
      );
      if (result) {
        this.campaignStore.set(result);
        this.savedCampaign.set(cloneDeep(result));
        this.pendingPatches.set([]);
      }
    } catch (err: unknown) {
      GlobalError.showErrorMessage("Failed To Retrieve Campaign");
    }
  };

  public updateCampaignDescription = (description: string) => {
    this.campaignStore.update((campaignBeingUpdated) => {
      if (campaignBeingUpdated == undefined) return undefined;
      return {
        ...campaignBeingUpdated,
        description,
      };
    });
  };

  public updateCampaignName = (name: string) => {
    this.campaignStore.update((campaignBeingUpdated) => {
      if (campaignBeingUpdated == undefined) return undefined;
      return {
        ...campaignBeingUpdated,
        name,
      };
    });
  };

  public addUpdatePartyMember = (character: Character) => {
    this.campaignStore.update((campaignBeingUpdated) => {
      if (campaignBeingUpdated == undefined) return undefined;
      const characters = cloneDeep(campaignBeingUpdated.party);
      const characterIndex = characters.findIndex(
        (c) => c.characterContentCode === character.characterContentCode
      );
      if (characterIndex === -1) {
        characters.push(character);
      } else {
        characters[characterIndex] = { ...character };
      }
      return {
        ...campaignBeingUpdated,
        party: characters,
      };
    });
  };

  public addUpdateScenario = (scenario: Scenario) => {
    this.campaignStore.update((campaignBeingUpdated) => {
      if (campaignBeingUpdated == undefined) return campaignBeingUpdated;
      const scenarios = cloneDeep(campaignBeingUpdated.scenarios);
      const scenarioIndex = scenarios.findIndex(
        (s) => s.contentCode === scenario.contentCode
      );
      if (scenarioIndex === -1) {
        scenarios.push(scenario);
      } else {
        scenarios[scenarioIndex] = { ...scenario };
      }

      return {
        ...campaignBeingUpdated,
        scenarios,
      };
    });
  };

  //#endregion

  //#region Create / Save Campaign

  public saveCampaign = async () => {
    if (get(this.campaignNotSaved)) {
      const campaignToSave = get(this.campaignStore);
      const patches = get(this.pendingPatches);
      if (campaignToSave == undefined) return;
      try {
        await patchAPI<void>(
          `campaigns/${campaignToSave.id}`,
          this.authToken ?? "",
          patches
        );
        this.savedCampaign.set(cloneDeep(campaignToSave));
        this.pendingPatches.set([]);
      } catch (ex) {
        GlobalError.showErrorMessage("Failed To Create a New Campaign");
      }
    }
  };

  public createNewCampaign = async (campaign: Campaign): Promise<void> => {
    try {
      const result = await postAPI<Campaign>(
        "campaigns",
        this.authToken ?? "",
        {
          id: campaign.id,
          name: campaign.name,
          game: campaign.game,
        }
      );
      if (result) {
        this.campaignStore.set(result);
        this.savedCampaign.set(result);
        await this.getCampaignListing();
      }
    } catch (ex) {
      GlobalError.showErrorMessage("Failed To Create a New Campaign");
    }
  };

  public clearCampaign = () => {
    this.campaignStore.set(undefined);
    this.savedCampaign.set(undefined);
    this.pendingPatches.set([]);
  };

  //#endregion

  public State = {
    campaignListing: this.campaignListing,
    campaign: this.campaign,
    campaignNotSaved: this.campaignNotSaved,
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
