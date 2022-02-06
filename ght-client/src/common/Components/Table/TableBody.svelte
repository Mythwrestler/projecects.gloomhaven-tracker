<script lang="ts">
  import TableRow from "./TableRow.svelte";
  import type { RowData } from "./types";
  export let rowData: RowData[] | undefined = undefined;
  export let propertyPostion: Map<number, string> | undefined = undefined;
  export let cellRenders: Map<string, unknown> = new Map<string, unknown>();
</script>

<tbody class="bg-gray-100 dark:bg-gray-800 divide-y divide-gray-200">
  {#if rowData && propertyPostion}
    {#each rowData as row}
      <TableRow rowData={row} {cellRenders} {propertyPostion} />
    {/each}
  {/if}
</tbody>
