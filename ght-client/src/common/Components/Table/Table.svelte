<script lang="ts">
  import { onMount } from "svelte";

  import TableBody from "./TableBody.svelte";
  import TableHeader from "./TableHeader.svelte";
  import type {
    TableConfiguration,
    RowData,
    ColumnDefinition,
    HeaderDefinition,
  } from "./types";
  export let rowData: RowData[] | undefined = undefined;
  export let config: TableConfiguration = { columns: [] };

  let _headers: Map<number, HeaderDefinition> = new Map<
    number,
    HeaderDefinition
  >();
  let _propertyPos: Map<number, string> = new Map<number, string>();
  let _cellRenders: Map<string, unknown> = new Map<string, unknown>();

  const orderTableHeaders = (a: ColumnDefinition, b: ColumnDefinition) =>
    a.position > b.position ? 1 : -1;

  onMount(() => {
    const hd: Map<number, HeaderDefinition> = new Map<
      number,
      HeaderDefinition
    >();
    let pr: Map<number, string> = new Map<number, string>();
    let cl: Map<string, unknown> = new Map<string, unknown>();
    config.columns.sort(orderTableHeaders).forEach((c, index) => {
      hd.set(index, c.header);
      pr.set(index, c.property);
      cl.set(c.property, c.valueDisplayComponent);
    });
    _headers = hd;
    _propertyPos = pr;
    _cellRenders = cl;
  });
</script>

<div class="flex flex-col">
  <div class="-my-2 overflow-x-auto max-h-64">
    <div class="py-2 align-middle inline-block min-w-full sm:px-6 lg:px-8">
      <div
        class="shadow overflow-hidden border-b border-gray-200 dark:bg-gray-800 sm:rounded-lg"
      >
        <table class="min-w-full divide-y divide-gray-200 overflow-y-auto">
          <TableHeader headers={_headers} />
          <TableBody
            cellRenders={_cellRenders}
            bind:rowData
            propertyPostion={_propertyPos}
          />
        </table>
      </div>
    </div>
  </div>
</div>
