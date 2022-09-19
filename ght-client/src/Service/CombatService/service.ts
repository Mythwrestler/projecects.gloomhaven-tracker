import { setContext } from "svelte";
import { get, type Readable, type Writable } from "svelte/store";
import ENV_VARS from "../../common/Environment";
import { getAPI, postAPI } from "../../common/Utils/API";
import * as GlobalError from "../Error";
import type { Combat, CombatSummary } from "../../models/Combat";
import { useCombatServiceActions, type CombatActions } from "./actions";
import { getCombatState, useCombatServiceState } from "./state";
import { AsyncQueue } from "@ci-lab/async-queue";

class CombatService {
  private accessToken: Readable<string | undefined>;
  private combatSummaries: Writable<CombatSummary[]>;
  private combatDetail: Writable<Combat | undefined>;

  private requestQueue: AsyncQueue<void> = new AsyncQueue<void>();

  constructor(accessToken: Readable<string | undefined>, stateKey: string) {
    const { combatSummaries, combatDetail } = getCombatState(stateKey);
    this.accessToken = accessToken;
    this.combatSummaries = combatSummaries;
    this.combatDetail = combatDetail;
  }

  public getCombatSummaries = async () => {
    const token = get(this.accessToken);
    try {
      const result = await getAPI<CombatSummary[]>(`combats`, token);
      if (result && result.length > 0) this.combatSummaries.set(result);
    } catch (err: unknown) {
      GlobalError.showErrorMessage(
        `Failed to get Combat Listing ${JSON.stringify(err)}`
      );
    }
  };

  public getCombatDetails = async (combatId: string): Promise<void> => {
    const token = get(this.accessToken);
    try {
      const result = await getAPI<Combat>(`combats/${combatId}`, token);
      if (result) {
        this.combatDetail.set(result);
        await this.getCombatSummaries();
      }
    } catch (err: unknown) {
      console.log(`Get Combat Error ${JSON.stringify(err)}`);
      GlobalError.showErrorMessage(
        `Failed To Get Combat Details ${JSON.stringify(err)}`
      );
    }
  };

  public clearCombatDetail = () => {
    this.combatDetail.set(undefined);
  };

  public createCombat = async (
    campaignId: string,
    scenarioContentCode: string
  ): Promise<void> => {
    const token = get(this.accessToken);
    try {
      const result = await postAPI<Combat>("combats", token, {
        campaignId,
        scenarioContentCode,
      });
      if (result) {
        this.combatDetail.set(result);
        await this.getCombatSummaries();
      }
    } catch (err: unknown) {
      console.log(`Create Combat Error ${JSON.stringify(err)}`);
      GlobalError.showErrorMessage("Failed To Create a New Combat");
    }
  };

  public actions: CombatActions = {
    getCombatSummaries: this.getCombatSummaries,
    getCombatDetail: this.getCombatDetails,
    clearCombatDetail: this.clearCombatDetail,
    createCombat: this.createCombat,
  };
}

export const defineCombatService = (
  accessToken: Readable<string | undefined>,
  stateKey: string = ENV_VARS.CONTEXT.CombatService.State,
  actionKey: string = ENV_VARS.CONTEXT.CombatService.Actions
): CombatService => {
  const service = new CombatService(accessToken, stateKey);
  setContext<CombatActions>(actionKey, service.actions);
  return service;
};

export const useCombatService = (
  actionKey: string = ENV_VARS.CONTEXT.CombatService.Actions,
  stateKey: string = ENV_VARS.CONTEXT.CombatService.State
) => {
  return {
    actions: useCombatServiceActions(actionKey),
    state: useCombatServiceState(stateKey),
  };
};

export default useCombatService;
