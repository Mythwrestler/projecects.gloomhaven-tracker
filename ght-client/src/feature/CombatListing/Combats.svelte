<script lang="ts">
  import { onMount } from "svelte";
  import { writable } from "svelte/store";
  import type { CombatSummary } from "../../models/Combat";
  import { AddContainedIcon, Table } from "../../common/Components";
  import type {
    ColumnDefinition,
    RowData,
    TableConfiguration,
  } from "../../common/Components";
  import CombatLink from "./CombatLink.svelte";
  import clsx from "clsx";
  import { accessToken } from "../../common/Utils/OidcSvelteClient";
  import { useCombatService } from "../../Service/CombatService";
  import ENV_VARS from "../../common/Environment";

  const { State, getCombatListing } = useCombatService(accessToken);
  const { combatListing } = State;

  const refreshListing = writable<boolean>(false);
  const combatListingLoaded = writable<boolean>(false);

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
    if (combats.length > 0) combatListingLoaded.set(true);
  });

  const handleGetCampaigns = async (
    token: string | undefined
  ): Promise<void> => {
    if (token == undefined || token.trim() == "") return;
    if ($refreshListing) {
      await getCombatListing();
      refreshListing.set(false);
    }
  };

  refreshListing.subscribe(() => {
    let token = "";
    if (ENV_VARS.AUTH.Enabled() && $accessToken !== null)
      token = $accessToken ?? "";
    void handleGetCampaigns(token);
  });

  accessToken.subscribe((tokenFromStore) => {
    let token = "";
    if (ENV_VARS.AUTH.Enabled() && tokenFromStore !== null)
      token = tokenFromStore ?? "";
    void handleGetCampaigns(token);
  });

  onMount(() => {
    combatListingLoaded.set(false);
    refreshListing.set(true);
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
    {#if $combatListingLoaded}
      <Table {config} rowData={combatsRowData} />
    {/if}
  </div>
</section>
