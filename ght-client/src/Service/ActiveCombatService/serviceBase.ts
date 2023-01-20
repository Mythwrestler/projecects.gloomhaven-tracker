import type { Writable } from "svelte/store";
import type { Participants } from "../../models/Combat";
import { getActiveCombatState } from "./state";

export class ServiceBase {
  protected combatId: Writable<string | undefined>;
  protected combatConnecting: Writable<boolean>;
  protected combatConnected: Writable<boolean>;
  protected combatDisconnecting: Writable<boolean>;
  protected participants: Writable<Participants | undefined>;
  constructor(stateKey: string) {
    const state = getActiveCombatState(stateKey);
    this.combatId = state.combatId;
    this.combatConnecting = state.combatConnecting;
    this.combatConnected = state.combatConnected;
    this.combatDisconnecting = state.combatDisconnecting;
    this.participants = state.participants;
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
    this.combatDisconnecting.set(false);
    this.combatConnected.set(false);
    this.combatId.set(undefined);
    this.participants.set(undefined);
  };
  protected requestCombatDisconnectFailure = (): void => {
    this.combatDisconnecting.set(false);
  };
}
