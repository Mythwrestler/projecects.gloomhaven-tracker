<script lang="ts">
  import { RowData } from "./types";
  export let rowData: RowData;
  export let propertyPostion: Map<number, string> | undefined = undefined;
  export let cellRenders: Map<string, unknown> = new Map<string, unknown>();

  let _rowData: CellRenderData[] | undefined;

  interface CellRenderData {
    value: unknown;
    component?: unknown;
  }

  const handleRowDataCalcs = (data: RowData) => {
    const cells: CellRenderData[] = [];
    if (!propertyPostion) {
      _rowData = undefined;
      return;
    }
    propertyPostion?.forEach((value) => {
      cells?.push({
        value: data[value],
        component: cellRenders.get(value),
      });
    });
    _rowData = cells;
    console.log(JSON.stringify(_rowData));
  };

  $: {
    handleRowDataCalcs(rowData);
  }
</script>

{#if _rowData}
  <tr>
    {#each _rowData as cell}
      <td class="px-1 py-1 whitespace-nowrap text-left">
        {#if cell.component}
          <svelte:component this={cell.component} {...cell.value ?? {}} />
        {:else}
          <!-- Eventaully Handle Default String / Number / Date -->
          <div class="text-sm text-gray-900 p-2">{cell.value}</div>
        {/if}
      </td>
    {/each}
  </tr>
{/if}
