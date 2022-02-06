import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import ENV_VARS from "../../common/Environment";
import { get, writable } from "svelte/store";
import type { Writable } from "svelte/store";
import { authToken as authTokenStore } from "@dopry/svelte-auth0";
import {
  combatSpaceId,
  combatHubConnected,
  requestCombatSpaceDisconnect,
  requestCombatSpaceDisconnectSuccess,
  requestCombatHubConnectionFailure,
} from "./Store";
import {
  requestCombatHubConnection,
  requestCombatHubConnectionSuccess,
  requestCombatSpaceConnection,
  requestCombatSpaceConnectionFailure,
  requestCombatSpaceConnectionSuccess,
  requestCombatSpaceDisconnectFailure,
} from ".";

const combatHub: Writable<HubConnection | undefined> = writable<
  HubConnection | undefined
>(undefined);

interface HubRequestResult {
  errorMessage?: string;
  data?: unknown;
}

interface HubListener {
  method: string;
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  effect: (...args: any[]) => void;
}

const HUB_METHODS = {
  join: "joinCombatSpace",
  leave: "leaveCombatSpace",
};

export const joinCombatSpace = async (combatId: string): Promise<void> => {
  console.log("joinCombatSpace - Start");
  const hubConnected = get(combatHubConnected);
  if (!hubConnected) return;
  const hub = get(combatHub);
  if (hub === undefined) return;

  const currentId = get(combatSpaceId);
  if (currentId === combatId) return;

  try {
    requestCombatSpaceConnection();
    await hub.send(HUB_METHODS.join, combatId);
  } catch {
    requestCombatSpaceConnectionFailure();
  }
};

export const joinCombatSpaceResult = (result: HubRequestResult): void => {
  console.log("joinCombatSpace - Result");
  if (result.errorMessage || !result.data) {
    requestCombatSpaceConnectionFailure();
  }
  requestCombatSpaceConnectionSuccess(result.data as string);
};

export const leaveCombatSpace = async (): Promise<void> => {
  console.log("leaveCombatSpace - Start");
  const hubConnected = get(combatHubConnected);
  if (!hubConnected) return;
  const hub = get(combatHub);
  const combatId = get(combatSpaceId);
  if (hub === undefined) return;
  try {
    requestCombatSpaceDisconnect();
    await hub.send(HUB_METHODS.leave, combatId);
  } catch {
    requestCombatSpaceDisconnectFailure();
  }
};

export const leaveCombatSpaceResult = (result: HubRequestResult): void => {
  console.log("leaveCombatSpace - Result");
  if (result.errorMessage) {
    requestCombatSpaceDisconnectFailure();
  }
  requestCombatSpaceDisconnectSuccess();
};

const hubListeners: HubListener[] = [
  {
    method: `${HUB_METHODS.join}Result`,
    effect: joinCombatSpaceResult,
  },
  {
    method: `${HUB_METHODS.leave}Result`,
    effect: leaveCombatSpaceResult,
  },
];

export const buildCombatHub = async (): Promise<void> => {
  const url = `${ENV_VARS.API.BaseURL()}hub/combatspace`;
  const authToken: string = get(authTokenStore);
  const hub =
    ENV_VARS.AUTH.Enabled() && authToken !== ""
      ? new HubConnectionBuilder()
          .withUrl(url, { accessTokenFactory: () => authToken ?? "" })
          .build()
      : new HubConnectionBuilder().withUrl(url).build();

  hubListeners.forEach((listener) => hub.on(listener.method, listener.effect));

  try {
    requestCombatHubConnection();
    await hub.start();
    combatHub.set(hub);
    requestCombatHubConnectionSuccess();
  } catch {
    combatHub.set(undefined);
    requestCombatHubConnectionFailure();
  }
};
