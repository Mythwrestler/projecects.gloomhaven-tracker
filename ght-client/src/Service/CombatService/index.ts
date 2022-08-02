import { derived, writable, type Readable, type Writable } from "svelte/store";
import { getAPI } from "../../common/Utils/API";
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
  private combatStores = writable<Combat>();

  //#endregion

  //#region Create New Combat

  //#endregion

  public State = {
    combatListing: this.combatListing,
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
