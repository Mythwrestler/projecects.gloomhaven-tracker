import { accessToken as accessTokenWritable } from "@dopry/svelte-oidc";
import { derived } from "svelte/store";
import type { Readable } from "svelte/store";

export const accessToken: Readable<string> = derived(
  accessTokenWritable,
  ($store) => $store
);
