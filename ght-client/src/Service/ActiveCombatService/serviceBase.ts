import type { Writable } from "svelte/store";
import type { User } from "../../models/Combat";
import { getActiveCombatState } from "./state";

export class ServiceBase {
  protected combatId: Writable<string | undefined>;
  protected combatConnecting: Writable<boolean>;
  protected combatConnected: Writable<boolean>;
  protected combatDisconnecting: Writable<boolean>;
  protected userList: Writable<User[]>;
  constructor(stateKey: string) {
    const state = getActiveCombatState(stateKey);
    this.combatId = state.combatId;
    this.combatConnecting = state.combatConnecting;
    this.combatConnected = state.combatConnected;
    this.combatDisconnecting = state.combatDisconnecting;
    this.userList = state.userList;
  }
  protected requestCombatConnection = (): void => {
    this.combatConnecting.set(true);
  };
  protected requestCombatConnectionSuccess = (combatId: string): void => {
    this.combatConnected.set(true);
    this.combatConnecting.set(false);
    this.combatId.set(combatId);
  };
  protected requestCombatConnectionFailure = (): void => {
    this.combatConnecting.set(false);
  };
  protected requestCombatDisconnect = (): void => {
    this.combatDisconnecting.set(true);
  };
  protected requestCombatDisconnectSuccess = (): void => {
    this.combatConnected.set(false);
    this.combatId.set(undefined);
    this.userList.set([]);
  };
  protected requestCombatDisconnectFailure = (): void => {
    this.combatDisconnecting.set(false);
  };
}
