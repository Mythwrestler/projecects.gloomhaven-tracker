import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { get, writable, type Readable, type Writable } from "svelte/store";
import ENV_VARS from "../../common/Environment";
import * as GlobalError from "../Error";

const HUB_METHODS = {
  join: "joinCombat",
  leave: "leaveCombat",
};

interface HubRequestResult {
  errorMessage?: string;
  data?: unknown;
}

interface HubListener {
  method: string;
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  effect: (...args: any[]) => void;
}

class CombatHubServiceImplementation {
  private authToken?: string;

  constructor(authTokenStore: Readable<string | undefined>) {
    authTokenStore.subscribe((token) => (this.authToken = token));
  }

  //#region Combat Connection
  combatConnecting: Writable<boolean> = writable<boolean>(false);
  combatDisconnecting: Writable<boolean> = writable<boolean>(false);
  combatConnected: Writable<boolean> = writable<boolean>(false);
  combatId: Writable<string | undefined> = writable<string | undefined>();
  requestCombatConnection = (): void => {
    this.combatConnecting.set(true);
  };
  requestCombatConnectionSuccess = (combatId: string): void => {
    this.combatConnected.set(true);
    this.combatConnecting.set(false);
    this.combatId.set(combatId);
  };
  requestCombatConnectionFailure = (): void => {
    this.combatConnecting.set(false);
  };
  requestCombatDisconnect = (): void => {
    this.combatDisconnecting.set(true);
  };
  requestCombatDisconnectSuccess = (): void => {
    this.combatConnected.set(false);
    this.combatDisconnecting.set(false);
    this.combatId.set(undefined);
  };
  requestCombatDisconnectFailure = (): void => {
    this.combatDisconnecting.set(false);
  };

  public joinCombat = async (combatId: string): Promise<void> => {
    const hubConnected = get(this.combatHubConnected);
    if (!hubConnected) return;

    const hub = get(this.combatHub);
    if (hub === undefined) return;

    const currentId = get(this.combatId);
    if (currentId === combatId) return;

    try {
      this.requestCombatConnection();
      await hub.send(HUB_METHODS.join, combatId);
    } catch {
      this.requestCombatConnectionFailure();
      GlobalError.showErrorMessage("Failed To Join Combat Space");
    }
  };

  private joinCombatResult = (result: HubRequestResult): void => {
    if (result.errorMessage || !result.data) {
      this.requestCombatConnectionFailure();
    }
    this.requestCombatConnectionSuccess(result.data as string);
  };

  public leaveCombat = async (): Promise<void> => {
    const hubConnected = get(this.combatHubConnected);
    if (!hubConnected) return;
    const hub = get(this.combatHub);
    const combatId = get(this.combatId);
    if (hub === undefined) return;
    try {
      this.requestCombatDisconnect();
      await hub.send(HUB_METHODS.leave, combatId);
    } catch {
      this.requestCombatDisconnectFailure();
    }
  };

  private leaveCombatResult = (result: HubRequestResult): void => {
    if (result.errorMessage) {
      this.requestCombatDisconnectFailure();
    }
    this.requestCombatDisconnectSuccess();
  };

  //#endregion

  //#region Hub Listeners

  private hubListeners: HubListener[] = [
    {
      method: `${HUB_METHODS.join}Result`,
      effect: this.joinCombatResult,
    },
    {
      method: `${HUB_METHODS.leave}Result`,
      effect: this.leaveCombatResult,
    },
  ];

  //#endregion

  //#region Combat Hub Connection
  combatHub: Writable<HubConnection | undefined> = writable<
    HubConnection | undefined
  >(undefined);

  combatHubConnecting: Writable<boolean> = writable<boolean>(false);
  combatHubConnected: Writable<boolean> = writable<boolean>(false);

  private requestCombatHubConnection = (): void => {
    this.combatHubConnected.set(false);
    this.combatHubConnecting.set(true);
  };
  private requestCombatHubConnectionSuccess = (): void => {
    this.combatHubConnected.set(true);
    this.combatHubConnecting.set(false);
  };
  private requestCombatHubConnectionFailure = (): void => {
    this.combatHubConnected.set(false);
    this.combatHubConnecting.set(false);
  };

  public joinCombatHub = async (): Promise<void> => {
    const url = `${ENV_VARS.API.BaseURL()}hub/combathub`;
    const hub =
      ENV_VARS.AUTH.Enabled() && this.authToken !== ""
        ? new HubConnectionBuilder()
            .withUrl(url, { accessTokenFactory: () => this.authToken ?? "" })
            .build()
        : new HubConnectionBuilder().withUrl(url).build();

    this.hubListeners.forEach((listener) =>
      hub.on(listener.method, listener.effect)
    );

    try {
      this.requestCombatHubConnection();
      await hub.start();
      this.combatHub.set(hub);
      this.requestCombatHubConnectionSuccess();
    } catch {
      this.combatHub.set(undefined);
      this.requestCombatHubConnectionFailure();
    }
  };
  //#endregion

  public State = {
    combatHub: {
      connecting: this.combatHubConnecting,
      connected: this.combatHubConnected,
    },
    combat: {
      connecting: this.combatConnecting,
      connected: this.combatConnected,
      combatId: this.combatId,
    },
  };
}

let combatHubService: CombatHubServiceImplementation | undefined = undefined;

export const useCombatHubService = (
  accessToken: Readable<string | undefined>
): CombatHubServiceImplementation => {
  if (!combatHubService)
    combatHubService = new CombatHubServiceImplementation(accessToken);
  return combatHubService;
};
