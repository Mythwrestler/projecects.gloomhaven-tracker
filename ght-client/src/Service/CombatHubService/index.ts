import {
  derived,
  get,
  writable,
  type Readable,
  type Writable,
} from "svelte/store";
import ENV_VARS from "../../common/Environment";
import * as GlobalError from "../Error";
import SvelteSignalRHub from "../../common/Utils/SvelteSignalRHub";
import type { User } from "../../models/Combat";

const HUB_METHODS = {
  join: "JoinCombat",
  leave: "LeaveCombat",
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
  private combatHubStore = new SvelteSignalRHub();

  constructor(authTokenStore: Readable<string | undefined>) {
    authTokenStore.subscribe((token) => (this.authToken = token));
  }

  //#region Combat Connection
  private combatConnecting: Writable<boolean> = writable<boolean>(false);
  private combatDisconnecting: Writable<boolean> = writable<boolean>(false);
  private combatConnected: Writable<boolean> = writable<boolean>(false);
  private combatId: Writable<string | undefined> = writable<
    string | undefined
  >();
  private requestCombatConnection = (): void => {
    this.combatConnecting.set(true);
  };
  private requestCombatConnectionSuccess = (combatId: string): void => {
    this.combatConnected.set(true);
    this.combatConnecting.set(false);
    this.combatId.set(combatId);
  };
  private requestCombatConnectionFailure = (): void => {
    this.combatConnecting.set(false);
  };
  private requestCombatDisconnect = (): void => {
    this.combatDisconnecting.set(true);
  };
  private requestCombatDisconnectSuccess = (): void => {
    this.combatConnected.set(false);
    this.combatDisconnecting.set(false);
    this.combatId.set(undefined);
  };
  private requestCombatDisconnectFailure = (): void => {
    this.combatDisconnecting.set(false);
  };

  public joinCombat = async (combatId: string): Promise<void> => {
    const hubConnected = get(this.combatHubStore.State.combatHubConnected);
    if (!hubConnected) return;

    const currentId = get(this.combatId);
    if (currentId === combatId) return;

    try {
      this.requestCombatConnection();
      await this.combatHubStore.sendHubRequest(HUB_METHODS.join, combatId);
    } catch (error: unknown) {
      console.log(JSON.stringify(error));
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
    const hubConnected = get(this.combatHubStore.State.combatHubConnected);
    if (!hubConnected) return;
    const combatId = get(this.combatId);
    try {
      this.requestCombatDisconnect();
      await this.combatHubStore.sendHubRequest(HUB_METHODS.leave, combatId);
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

  private combatUsersStore: Writable<User[]> = writable<User[]>([]);
  private combatUsers = derived(this.combatUsersStore, ($store) => $store, []);

  private handleUserJoinedLeftCombat = (result: HubRequestResult): void => {
    console.log(`Got a result: ${JSON.stringify(result)}`);
    if (!result || !result.data) return;
    this.combatUsersStore.update((currentUsers) => {
      console.log(`Updating user store to: ${JSON.stringify(result.data)}`);
      return result.data as User[];
    });
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
    {
      method: "UserJoinedCombat",
      effect: this.handleUserJoinedLeftCombat,
    },
    {
      method: "UserLeftCombat",
      effect: this.handleUserJoinedLeftCombat,
    },
  ];

  //#endregion

  //#region Combat Hub Connection
  public connectToHub = async () => {
    this.combatHubStore.buildHub(
      `${ENV_VARS.API.BaseURL()}hub/combats`,
      this.authToken,
      this.hubListeners
    );
    await this.combatHubStore.startHub();
  };
  public disconnectFromHub = async () => {
    await this.combatHubStore.stopHub();
  };

  //#endregion

  public State = {
    combatHub: {
      connecting: this.combatHubStore.State.combatHubConnecting,
      connected: this.combatHubStore.State.combatHubConnected,
    },
    combat: {
      connecting: this.combatConnecting,
      connected: this.combatConnected,
      combatId: this.combatId,
      connectedUsers: this.combatUsers,
    },
  };
}

export type CombatHubService = CombatHubServiceImplementation;

let combatHubService: CombatHubServiceImplementation | undefined = undefined;

export const useCombatHubService = (
  accessToken: Readable<string | undefined>
): CombatHubServiceImplementation => {
  if (!combatHubService)
    combatHubService = new CombatHubServiceImplementation(accessToken);
  return combatHubService;
};
