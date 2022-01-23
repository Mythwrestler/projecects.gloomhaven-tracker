import { derived, get, Readable, writable } from "svelte/store";
import { getAPI, postAPI, putAPI } from "../../common/Utils/API";
import * as GlobalError from "../Error";
import {
  CampaignSummary,
  Campaign,
  Party,
  Scenarios,
  Character,
  Scenario,
} from "../../models/Campaign";
import { cloneDeep, isEqual, result } from "lodash";

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
      const [campaignStore, initialCampaign] = $campaignStates;
      return isEqual(campaignStore, initialCampaign);
    },
    false
  );

  public getCampaign = async (campaignId: string) => {
    try {
      const result = await getAPI<Campaign>(`campaigns/${campaignId}`);
      if (result) {
        this.campaignStore.set(result);
        this.savedCampaign.set(result);
      }
    } catch (err: unknown) {
      GlobalError.showErrorMessage("Failed To Retrieve Campaign");
    }
  };

  public addUpdatePartyMember = (character: Character) => {
    this.campaignStore.update((campaignBeingUpdated) => {
      const characters = cloneDeep(campaignBeingUpdated.party.characters);
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
        party: {
          ...campaignBeingUpdated.party,
          characters,
        },
      };
    });
  };

  public addUpdateScenario = (scenario: Scenario) => {
    this.campaignStore.update((campaignBeingUpdated) => {
      const scenarios = cloneDeep(campaignBeingUpdated.scenarios.scenarios);
      const scenarioIndex = scenarios.findIndex(
        (s) => (s.contentCode = scenario.contentCode)
      );
      if (scenarioIndex === -1) {
        scenarios.push(scenario);
      } else {
        scenarios[scenarioIndex] = { ...scenario };
      }

      return {
        ...campaignBeingUpdated,
        scenarios: {
          ...campaignBeingUpdated.scenarios,
          scenarios,
        },
      };
    });
  };

  // public updateCampaignScenarios = (scenarios: Scenarios) => {
  //   this.campaignStore.update((campaignBeingUpdated) => {
  //     campaignBeingUpdated.scenarios = scenarios;
  //     return cloneDeep<Campaign>(campaignBeingUpdated);
  //   });
  // };

  public saveCampaign = async (campaign: Campaign) => {
    const isNewCampaign =
      get(this.campaignListing).findIndex((c) => c.id === campaign.id) === -1;

    let result: Campaign | undefined;
    if (isNewCampaign) {
      result = await this.createNewCampaign(campaign);
    } else {
      result = await this.updateExistingCampaign(campaign);
    }

    if (result) {
      this.campaignStore.set(result);
      this.savedCampaign.set(result);
      if (isNewCampaign) await this.getCampaignListing();
    }
  };

  private createNewCampaign = async (
    campaign: Campaign
  ): Promise<Campaign | undefined> => {
    try {
      const result = await putAPI<Campaign>("campaigns/new", {
        id: campaign.id,
        description: campaign.description,
        game: campaign.game,
      });
      if (result) return result;
    } catch (ex) {
      GlobalError.showErrorMessage("Failed To Create a New Campaign");
    }
  };

  private updateExistingCampaign = async (
    campaign: Campaign
  ): Promise<Campaign | undefined> => {
    if (get(this.campaignNotSaved)) {
      try {
        return await putAPI<Campaign>(`campaigns/${campaign.id}`);
      } catch (ex) {
        GlobalError.showErrorMessage("Failed To Update Campaign");
      }
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
