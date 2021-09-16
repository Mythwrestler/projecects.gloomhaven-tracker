import { writable, Writable } from "svelte/store";
import { HubConnection } from "@microsoft/signalr"
import { BattleActionResult } from "../../../models/battle";

export const battleHubConnection:Writable<HubConnection> = writable();
export const battleHubConnected:Writable<boolean> = writable(false);
export const actionTracker:Writable<BattleActionResult[]> = writable([]);
export const serverMessages:Writable<string[]> = writable([]);