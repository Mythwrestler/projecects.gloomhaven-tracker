import type { Writable } from "svelte/store";
import type {
  Character,
  Combat,
  CombatSummary,
  Participants,
} from "../../models/Combat";
import { getActiveCombatState } from "./state";

export class ServiceBase {
  protected combatSummary: Writable<CombatSummary | undefined>;
  protected combatConnecting: Writable<boolean>;
  protected combatConnected: Writable<boolean>;
  protected combatDisconnecting: Writable<boolean>;
  protected participants: Writable<Participants | undefined>;
  protected combatCharacters: Writable<Character[]>;
  constructor(stateKey: string) {
    const state = getActiveCombatState(stateKey);
    this.combatConnecting = state.combatConnecting;
    this.combatConnected = state.combatConnected;
    this.combatDisconnecting = state.combatDisconnecting;
    this.combatSummary = state.combatSummary;
    this.combatCharacters = state.combatCharacters;
    this.participants = state.participants;
  }
  protected requestCombatConnection = (): void => {
    this.combatConnecting.set(true);
  };
  protected requestCombatConnectionSuccess = (): void => {
    this.combatConnected.set(true);
    this.combatConnecting.set(false);
  };
  protected requestCombatConnectionFailure = (): void => {
    this.combatConnecting.set(false);
  };
  protected requestCombatDisconnect = (): void => {
    this.combatDisconnecting.set(true);
  };
  protected requestCombatDisconnectSuccess = (): void => {
    this.combatSummary.set(undefined);
    this.combatCharacters.set([]);
    this.participants.set(undefined);

    this.combatDisconnecting.set(false);
    this.combatConnected.set(false);
  };
  protected requestCombatDisconnectFailure = (): void => {
    this.combatDisconnecting.set(false);
  };
}
