import { type SignalRHubRequest } from "@ci-lab/svelte-signalr-context";
import { useActiveCombatState } from "./state";
import { get } from "svelte/store";
import { useActiveCombatActions, type ActiveCombatActions } from "./actions";
import { ServiceBase } from "./serviceBase";
import { setContext } from "svelte";
import ENV_VARS from "../../../../common/Environment";

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

    const currentId = get(this.combatId);
    if (currentId === combatId) return;

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
    const combatId = get(this.combatId);
    try {
      this.requestCombatDisconnect();
      await this.sendMessage("LeaveCombat", combatId);
      this.requestCombatDisconnectSuccess();
    } catch (err: unknown) {
      console.log(JSON.stringify(err));
      this.requestCombatDisconnectFailure();
    }
  };

  public actions: ActiveCombatActions = {
    joinCombat: this.joinCombat,
    leaveCombat: this.leaveCombat,
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
