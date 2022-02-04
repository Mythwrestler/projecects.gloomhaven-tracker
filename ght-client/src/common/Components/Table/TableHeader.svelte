<script lang="ts">
  import clsx from "clsx";
  import type { HeaderDefinition } from "./types";
  export let headers: Map<number, HeaderDefinition> = new Map<
    number,
    HeaderDefinition
  >();
  let _headers: Map<number, HeaderDefinition> = new Map<
    number,
    HeaderDefinition
  >();
  $: {
    _headers = headers;
  }
</script>

<thead class="bg-gray-100 dark:bg-gray-800">
  <tr>
    {#each [..._headers] as [key, value]}
      {#if typeof value.header === "string" || typeof value.header === "number"}
        <th
          scope="col"
          class="px-1 py-2 text-left text-xs font-medium text-gray-500 dark:text-gray-200 uppercase tracking-wider"
        >
          {value.header}
        </th>
      {:else}
        <th
          scope="col"
          class="px-6 py-3 text-left text-xs font-medium text-gray-500 dark:text-gray-200 uppercase tracking-wider"
        >
          <svelte:component this={value.header} {...value.headerProps ?? {}} />
        </th>
      {/if}
    {/each}
  </tr>
</thead>
