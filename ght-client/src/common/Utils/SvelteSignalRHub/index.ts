import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { watch } from "fs";
import { get, type Writable } from "svelte/store";
import { writable, derived } from "svelte/store";

export interface SignalRHubListeners {
  method: string;
  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  effect: (...args: any[]) => void;
}

class SvelteSignalRHub {
  // private combatHubStore: Writable<HubConnection | undefined> = writable<
  //   HubConnection | undefined
  // >(undefined);
  private hub: HubConnection | undefined;

  private combatHubConnectingStore: Writable<boolean> =
    writable<boolean>(false);
  private combatHubConnecting = derived(
    this.combatHubConnectingStore,
    ($store) => $store
  );

  private combatHubConnectedStore: Writable<boolean> = writable<boolean>(false);
  private combatHubConnected = derived(
    this.combatHubConnectedStore,
    ($store) => $store
  );

  private requestCombatHubConnection = (): void => {
    this.combatHubConnectedStore.set(false);
    this.combatHubConnectingStore.set(true);
  };
  private requestCombatHubConnectionSuccess = (): void => {
    this.combatHubConnectedStore.set(true);
    this.combatHubConnectingStore.set(false);
  };
  private requestCombatHubConnectionFailure = (): void => {
    this.combatHubConnectedStore.set(false);
    this.combatHubConnectingStore.set(false);
  };
  private requestCombatHubDisconnectSuccess = (): void => {
    this.combatHubConnectedStore.set(false);
  };

  private handleCombatHubConnectionClose = (error: Error | undefined) => {
    if (error) this.requestCombatHubConnectionFailure();
    else this.requestCombatHubDisconnectSuccess();
  };

  private handleCombatHubReconnecting = () => {
    this.requestCombatHubConnection();
  };

  private handleCombatHubReconnected = () => {
    this.requestCombatHubConnectionSuccess();
  };

  public buildHub = (
    url: string,
    bearerToken: string | undefined,
    hubListeners: SignalRHubListeners[]
  ) => {
    const hubForCreate =
      bearerToken !== undefined && bearerToken.trim() !== ""
        ? new HubConnectionBuilder()
            .withUrl(url, { accessTokenFactory: () => bearerToken })
            .withAutomaticReconnect()
            .build()
        : new HubConnectionBuilder()
            .withUrl(url)
            .withAutomaticReconnect()
            .build();

    hubForCreate.onclose(this.handleCombatHubConnectionClose);
    hubForCreate.onreconnecting(this.handleCombatHubReconnecting);
    hubForCreate.onreconnected(this.handleCombatHubReconnected);

    hubListeners.forEach((listener) => {
      hubForCreate.on(listener.method, listener.effect);
    });

    this.hub = hubForCreate;
  };

  public startHub = async () => {
    if (!this.hub)
      throw new Error(
        "Hub must be created before a connection can be started."
      );
    try {
      this.requestCombatHubConnection();
      // this.combatHubConnectedStore.set(false);
      // this.combatHubConnectingStore.set(true);
      await this.hub.start();
      // this.combatHubConnectedStore.set(true);
      // this.combatHubConnectingStore.set(false);
      this.requestCombatHubConnectionSuccess();
    } catch (error: unknown) {
      this.requestCombatHubConnectionFailure();
      // this.combatHubConnectedStore.set(false);
      // this.combatHubConnectingStore.set(false);
      throw error as Error;
    }
  };

  public stopHub = async () => {
    if (!this.hub)
      throw new Error(
        "Hub must be created before a connection can be stopped."
      );
    try {
      await this.hub.stop();
      this.hub = undefined;
      this.requestCombatHubDisconnectSuccess();
    } catch (error: unknown) {
      throw error as Error;
    }
  };

  // eslint-disable-next-line @typescript-eslint/no-explicit-any
  public sendHubRequest = async (methodName: string, ...args: any[]) => {
    const hubConnected = get(this.combatHubConnected);
    if (!this.hub)
      throw new Error("Hub must be created before a request can be sent.");
    if (!hubConnected)
      throw new Error("Hub must be connected before a request can be sent.");
    // eslint-disable-next-line @typescript-eslint/no-unsafe-argument
    await this.hub.send(methodName, ...args);
  };

  public State = {
    combatHubConnecting: this.combatHubConnecting,
    combatHubConnected: this.combatHubConnected,
  };
}

export default SvelteSignalRHub;
