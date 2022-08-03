import { derived, writable, type Readable, type Writable } from "svelte/store";
import { getAPI, postAPI } from "../../common/Utils/API";
import type { Combat, CombatSummary } from "../../models/Combat";
import * as GlobalError from "../Error";
import { AsyncQueue } from "../../common/Utils/AsycnQueue";

class CombatServiceImplementation {
  private authToken?: string;

  private requestQueue = new AsyncQueue<unknown>();

  constructor(authTokenStore: Readable<string | undefined>) {
    authTokenStore.subscribe((token) => (this.authToken = token));
  }
  //#region Combat Listing Store

  private combatListingStore = writable<CombatSummary[]>([]);
  private combatListing = derived(
    this.combatListingStore,
    ($store) => $store,
    []
  );

  public getCombatListing = async () => {
    try {
      const result = await getAPI<CombatSummary[]>(`combats`, this.authToken);
      if (result && result.length > 0) this.combatListingStore.set(result);
    } catch (err: unknown) {
      GlobalError.showErrorMessage("Failed to get Combat Listing");
    }
  };

  //#endregion

  //#region Combat
  private combatStore = writable<Combat | undefined>();
  private combat = derived(this.combatStore, ($store) => $store);

  public clearCombat = () => {
    this.combatStore.set(undefined);
  };

  //#endregion

  //#region Combat

  public createNewCombat = async (
    campaignId: string,
    scenarioContentCode: string
  ): Promise<void> => {
    return this.requestQueue.enqueue(async () => {
      try {
        const result = await postAPI<Combat>("combats", this.authToken, {
          campaignId,
          scenarioContentCode,
        });
        if (result) {
          this.combatStore.set(result);
          await this.getCombatListing();
        }
      } catch (ex) {
        GlobalError.showErrorMessage("Failed To Create a New Combat");
      }
    }) as Promise<void>;
  };

  public getCombat = async (combatId: string): Promise<void> => {
    return this.requestQueue.enqueue(async () => {
      try {
        const result = await getAPI<Combat>(
          `combats/${combatId}`,
          this.authToken
        );
        if (result) {
          this.combatStore.set(result);
          await this.getCombatListing();
        }
      } catch (ex) {
        GlobalError.showErrorMessage("Failed To Get Combat Details");
      }
    }) as Promise<void>;
  };

  //#endregion

  public State = {
    combatListing: this.combatListing,
    combat: this.combat,
  };
}

let combatService: CombatServiceImplementation | undefined = undefined;

export const useCombatService = (
  accessToken: Readable<string | undefined>
): CombatServiceImplementation => {
  if (!combatService)
    combatService = new CombatServiceImplementation(accessToken);
  return combatService;
};
