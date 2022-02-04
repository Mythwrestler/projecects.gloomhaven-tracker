import { derived, get, writable } from "svelte/store";
import type { Readable } from "svelte/store";
import { getAPI, postAPI, putAPI } from "../../common/Utils/API";
import * as GlobalError from "../Error";
import type {
  CampaignSummary,
  Campaign,
  Character,
  Scenario,
} from "../../models/Campaign";
import { cloneDeep, isEqual } from "lodash";

class CampaignServiceImplementation {
  private campaignListingStore = writable<CampaignSummary[]>([]);
  private campaignListing = derived(
    this.campaignListingStore,
    ($store) => $store,
    []
  );

  public getCampaignListing = async () => {
    try {
      const result = await getAPI<CampaignSummary[]>(`campaigns`);
      if (result && result.length > 0) this.campaignListingStore.set(result);
    } catch (err: unknown) {
      GlobalError.showErrorMessage("Failed to get Campaign Listing");
    }
  };

  private campaignStore = writable<Campaign>();
  private savedCampaign = writable<Campaign>();
  private campaign: Readable<Campaign | undefined> = derived(
    this.campaignStore,
    ($store) => $store
  );
  private campaignNotSaved = derived(
    [this.campaignStore, this.savedCampaign],
    ($campaignStates) => {
      const [campaignStore, savedCampaign] = $campaignStates;
      return !isEqual(campaignStore, savedCampaign);
    },
    false
  );

  public getCampaign = async (campaignId: string) => {
    try {
      const result = await getAPI<Campaign>(`campaigns/${campaignId}`);
      if (result) {
        this.campaignStore.set(result);
        this.savedCampaign.set(cloneDeep(result));
      }
    } catch (err: unknown) {
      GlobalError.showErrorMessage("Failed To Retrieve Campaign");
    }
  };

  public updateCampaignDescription = (description: string) => {
    this.campaignStore.update((campaignBeingUpdated) => {
      return {
        ...campaignBeingUpdated,
        description,
      };
    });
  };

  public addUpdatePartyMember = (character: Character) => {
    this.campaignStore.update((campaignBeingUpdated) => {
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

  public saveCampaign = async () => {
    if (get(this.campaignNotSaved)) {
      const campaignToSave = get(this.campaignStore);
      try {
        await putAPI<void>(
          `campaigns/${get(this.campaignStore).id}`,
          campaignToSave
        );
        this.savedCampaign.set(cloneDeep(campaignToSave));
      } catch (ex) {
        GlobalError.showErrorMessage("Failed To Create a New Campaign");
      }
    }
  };

  public createNewCampaign = async (campaign: Campaign): Promise<void> => {
    try {
      const result = await postAPI<Campaign>("campaigns/new", {
        id: campaign.id,
        description: campaign.description,
        game: campaign.game,
      });
      if (result) {
        this.campaignStore.set(result);
        this.savedCampaign.set(result);
        await this.getCampaignListing();
      }
    } catch (ex) {
      GlobalError.showErrorMessage("Failed To Create a New Campaign");
    }
  };

  public State = {
    campaignListing: this.campaignListing,
    campaign: this.campaign,
    campaignNotSaved: this.campaignNotSaved,
  };
}

let campaignService: CampaignServiceImplementation | undefined = undefined;
const useCampaignService = (): CampaignServiceImplementation => {
  if (!campaignService) campaignService = new CampaignServiceImplementation();
  return campaignService;
};

export { useCampaignService };
