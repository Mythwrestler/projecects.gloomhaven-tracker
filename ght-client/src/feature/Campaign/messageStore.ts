import { Writable, writable } from "svelte/store";

export const messages:Writable<string[]> = writable(["Store | Initial Test Message"])