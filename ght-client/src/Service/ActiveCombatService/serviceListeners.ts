import { type SignalRHubListeners } from "@ci-lab/svelte-signalr-context";
import { getContext, setContext } from "svelte";
import ENV_VARS from "../../common/Environment";
import type { Combat, Participants } from "../../models/Combat";
import { ServiceBase } from "./serviceBase";

interface HubRequestResult<T> {
  errorMessage?: string;
  data?: T;
}

class ServiceListeners extends ServiceBase {
  constructor(stateKey: string) {
    super(stateKey);
  }

  private joinCombatResult = (result: HubRequestResult<Combat>): void => {
    if (!result || result.errorMessage || !result.data) {
      this.requestCombatConnectionFailure();
    } else {
      const combat = result.data;
      this.combatSummary.set({
        id: combat.id,
        campaignId: combat.campaignId,
        description: combat.description,
        scenarioContentCode: combat.scenarioContentCode,
        scenarioLevel: combat.scenarioLevel,
      });
      this.combatCharacters.set(combat.characters);
      this.requestCombatConnectionSuccess();
    }
  };

  private leaveCombatResult = (result: HubRequestResult<void>): void => {
    if (result.errorMessage) {
      this.requestCombatDisconnectFailure();
    }
    this.requestCombatDisconnectSuccess();
  };

  private handleActiveUsers = (
    result: HubRequestResult<Participants>
  ): void => {
    if (!result || !result.data) return;
    console.log(JSON.stringify(result.data));
    this.participants.set(result.data);
  };

  public listeners: SignalRHubListeners[] = [
    {
      method: "JoinCombatResult",
      effect: this.joinCombatResult,
    },
    {
      method: "LeaveCombatResult",
      effect: this.leaveCombatResult,
    },
    {
      method: "ActiveUsers",
      effect: this.handleActiveUsers,
    },
  ];
}

export const defineActiveCombatListeners = (
  stateKey: string = ENV_VARS.CONTEXT.ActiveCombatHubService.State,
  listenerKey: string = ENV_VARS.CONTEXT.ActiveCombatHubService.Listeners
): ServiceListeners => {
  const service = new ServiceListeners(stateKey);
  setContext<SignalRHubListeners[]>(listenerKey, service.listeners);
  return service;
};

export const useActiveCombatListeners = (
  listenerKey: string = ENV_VARS.CONTEXT.ActiveCombatHubService.Listeners
) => {
  return {
    listeners: getContext<SignalRHubListeners[]>(listenerKey) ?? [],
  };
};

export default useActiveCombatListeners;
