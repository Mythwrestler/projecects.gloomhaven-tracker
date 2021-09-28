import { HubConnection } from "@microsoft/signalr";
import { Writable, writable } from "svelte/store";

// API Store
export const apisReady:Writable<boolean> = writable<boolean>(false);
export const combatSpaceId:Writable<string> = writable<string>("");
export const availableCombatSpaces:Writable<string[]> = writable<string[]>([])

// Hub Store
export const combatHubConnected:Writable<boolean> = writable<boolean>(false);
export const combatSpaceConnected:Writable<boolean> = writable<boolean>(false);
