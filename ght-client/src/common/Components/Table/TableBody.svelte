<script lang="ts">
  import TableRow from "./TableRow.svelte";
  import { RowData } from "./types";
  export let rowData: RowData[] | undefined = undefined;
  export let propertyPostion: Map<number, string> | undefined = undefined;
  export let cellRenders: Map<string, unknown> = new Map<string, unknown>();
</script>

<tbody class="bg-white divide-y divide-gray-200">
  {#if rowData && propertyPostion}
    {#each rowData as row}
      <TableRow rowData={row} {cellRenders} {propertyPostion} />
    {/each}
  {/if}
</tbody>
