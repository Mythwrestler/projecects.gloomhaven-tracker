<script lang="ts">
  import { onMount } from "svelte";
  import type { CombatSpaceSummary } from "../../models/Combat";
  import { AddContainedIcon, Table } from "../../common/Components";
  import type {
    ColumnDefinition,
    RowData,
    TableConfiguration,
  } from "../../common/Components";
  import CombatLink from "./CombatLink.svelte";
  import { useCampaignService } from "../../Service/CampaignService";
  import clsx from "clsx";
  import { useCombatService } from "../../Service/CombatService";

  const { State, getCombatListing } = useCombatService();
  const { combatListing } = State;

  const columns: ColumnDefinition[] = [
    {
      position: 1,
      header: {
        header: "Description",
      },
      property: "description",
      valueDisplayComponent: CombatLink,
    },
    {
      position: 2,
      header: {
        header: "Scenario Level",
      },
      property: "scenarioLevel",
    },
  ];
  const config: TableConfiguration = {
    columns,
  };
  let combatsRowData: RowData[] = [];

  let combatListingLoaded = false;

  combatListing.subscribe((combats) => {
    combatsRowData = combats.map((combat) => {
      return {
        description: {
          label: combat.description,
          path: `/combats/${combat.id}`,
        },
        scenarioLevel: combat.scenarioLevel,
      };
    });
    if (combats.length > 0) combatListingLoaded = true;
  });

  onMount(() => {
    void getCombatListing();
  });
</script>

<section class="text-center lg:text-left lg:pl-3 ">
  <div
    class={clsx(
      "relative mt-2 px-3 py-1 items-center max-w-md mx-auto rounded-md backdrop-blur-sm",
      "bg-gray-50 dark:bg-gray-700"
    )}
  >
    <div aria-label="Available Combats" class="text-center text-xl">
      Campaigns
    </div>
    {#if combatListingLoaded}
      <Table {config} rowData={combatsRowData} />
    {/if}
  </div>
</section>
