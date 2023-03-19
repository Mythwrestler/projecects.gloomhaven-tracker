import { type SignalRHubRequest } from "@ci-lab/svelte-signalr-context";
import { useActiveCombatState } from "./state";
import { get } from "svelte/store";
import { useActiveCombatActions, type ActiveCombatActions } from "./actions";
import { ServiceBase } from "./serviceBase";
import { setContext } from "svelte";
import ENV_VARS from "../../common/Environment";
import type { CombatRequest } from "../../models/Combat";

export class ServiceActions extends ServiceBase {
  private sendMessage: SignalRHubRequest | undefined = undefined;

  constructor(stateKey: string) {
    super(stateKey);
  }

  public setSendMessage = (sendMessage: SignalRHubRequest) => {
    this.sendMessage = sendMessage;
  };

  private joinCombat = async (combatId: string) => {
    if (!this.sendMessage) return;
    this.requestCombatConnection();

    const currentCombat = get(this.combatSummary);
    if (currentCombat?.id === combatId) return;

    try {
      this.requestCombatConnection();
      await this.sendMessage("JoinCombat", combatId);
    } catch (error: unknown) {
      console.log(JSON.stringify(error));
      this.requestCombatConnectionFailure();
    }
  };

  private leaveCombat = async (): Promise<void> => {
    if (!this.sendMessage) return;
    const currentCombat = get(this.combatSummary);
    try {
      this.requestCombatDisconnect();
      await this.sendMessage("LeaveCombat", currentCombat?.id);
      this.requestCombatDisconnectSuccess();
    } catch (err: unknown) {
      console.log(JSON.stringify(err));
      this.requestCombatDisconnectFailure();
    }
  };

  private registerCharacters = async (
    characterCodes: string[]
  ): Promise<void> => {
    if (!this.sendMessage) return;
    try {
      await this.sendMessage("RegisterCharacters", characterCodes);
    } catch (err: unknown) {
      console.log(JSON.stringify(err));
    }
  };

  private registerObserver = async (): Promise<void> => {
    if (!this.sendMessage) return;
    try {
      await this.sendMessage("RegisterObserver");
    } catch (err: unknown) {
      console.log(JSON.stringify(err));
    }
  };

  public actions: ActiveCombatActions = {
    joinCombat: this.joinCombat,
    leaveCombat: this.leaveCombat,
    registerCharacters: this.registerCharacters,
    registerObserver: this.registerObserver,
  };
}

export const defineActiveCombatActions = (
  stateKey: string = ENV_VARS.CONTEXT.ActiveCombatHubService.State,
  actionKey: string = ENV_VARS.CONTEXT.ActiveCombatHubService.Actions
): ServiceActions => {
  const service = new ServiceActions(stateKey);
  setContext<ActiveCombatActions>(actionKey, service.actions);
  return service;
};

export const useActiveCombatService = (
  stateKey: string = ENV_VARS.CONTEXT.ActiveCombatHubService.State,
  actionKey: string = ENV_VARS.CONTEXT.ActiveCombatHubService.Actions
) => {
  return {
    actions: useActiveCombatActions(actionKey),
    state: useActiveCombatState(stateKey),
  };
};

export default useActiveCombatService;
