import { derived, writable, type Readable, type Writable } from "svelte/store";
import { getAPI } from "../../common/Utils/API";
import type { CombatSpace, CombatSpaceSummary } from "../../models/Combat";
import * as GlobalError from "../Error";

class CombatServiceImplementation {
  //#region Combat Listing Store

  private combatListingStore = writable<CombatSpaceSummary[]>([]);
  private combatListing = derived(
    this.combatListingStore,
    ($store) => $store,
    []
  );

  public getCombatListing = async () => {
    try {
      const result = await getAPI<CombatSpaceSummary[]>(`combats`);
      if (result && result.length > 0) this.combatListingStore.set(result);
    } catch (err: unknown) {
      GlobalError.showErrorMessage("Failed to get Combat Listing");
    }
  };

  //#endregion

  //#region Combat
  private combatStores = writable<CombatSpace>();

  //#endregion

  //#region Create New Combat

  //#endregion

  public State = {
    combatListing: this.combatListing,
  };
}

let combatService: CombatServiceImplementation | undefined = undefined;

export const useCombatService = (): CombatServiceImplementation => {
  if (!combatService) combatService = new CombatServiceImplementation();
  return combatService;
};
