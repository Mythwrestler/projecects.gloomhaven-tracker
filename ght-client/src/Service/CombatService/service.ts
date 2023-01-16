import { setContext } from "svelte";
import { get, type Readable } from "svelte/store";
import ENV_VARS from "../../common/Environment";
import { getAPI, postAPI } from "../../common/Utils/API";
import type { Combat, CombatSummary } from "../../models/Combat";
import { useCombatServiceActions, type CombatActions } from "./actions";
import * as GlobalError from "../Error";

class CombatService {
  private accessToken: Readable<string | undefined>;

  constructor(accessToken: Readable<string | undefined>) {
    this.accessToken = accessToken;
  }

  public getCombatSummaries = async (): Promise<CombatSummary[]> => {
    try {
      const token: string | undefined = get(this.accessToken);
      const result = await getAPI<CombatSummary[]>(`combats`, token);
      return result ?? [];
    } catch (err: unknown) {
      GlobalError.showErrorMessage(
        `Failed to get Combat Listing ${JSON.stringify(err)}`
      );
      return [];
    }
  };

  public getCombatDetails = async (
    combatId: string
  ): Promise<Combat | undefined> => {
    try {
      const token: string | undefined = get(this.accessToken);
      return await getAPI<Combat>(`combats/${combatId}`, token);
    } catch (err: unknown) {
      GlobalError.showErrorMessage(
        `Failed To Get Combat Details ${JSON.stringify(err)}`
      );
      return undefined;
    }
  };

  public createCombat = async (
    campaignId: string,
    scenarioContentCode: string
  ): Promise<Combat | undefined> => {
    try {
      const token: string | undefined = get(this.accessToken);
      return await postAPI<Combat>("combats", token, {
        campaignId,
        scenarioContentCode,
      });
    } catch (err: unknown) {
      GlobalError.showErrorMessage(
        `Failed To Create a New Combat  ${JSON.stringify(err)}`
      );
      return undefined;
    }
  };

  public actions: CombatActions = {
    getCombatSummaries: this.getCombatSummaries,
    getCombatDetail: this.getCombatDetails,
    createCombat: this.createCombat,
  };
}

export const defineCombatService = (
  accessToken: Readable<string | undefined>,
  actionKey: string = ENV_VARS.CONTEXT.CombatService.Actions
): CombatService => {
  const service = new CombatService(accessToken);
  setContext<CombatActions>(actionKey, service.actions);
  return service;
};

export const useCombatService = (
  actionKey: string = ENV_VARS.CONTEXT.CombatService.Actions
) => {
  return {
    actions: useCombatServiceActions(actionKey),
  };
};

export default useCombatService;
