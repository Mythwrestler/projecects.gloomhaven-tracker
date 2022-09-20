<script lang="ts">
  import { onDestroy, onMount } from "svelte";
  import type { CampaignSummary } from "../../../models/Campaign";
  import { AddContainedIcon, Table } from "../../../common/Components";
  import type {
    ColumnDefinition,
    RowData,
    TableConfiguration,
  } from "../../../common/Components";
  import CampaignLink from "./CampaignLink.svelte";
  import CampaignNewDialog from "./CampaignNewDialog.svelte";
  import useContentService from "../../../Service/ContentService";
  import useCampaignService from "../../../Service/CampaignService";

  import clsx from "clsx";
  import type { ContentItemSummary } from "../../../models/Content";
  import { writable, type Unsubscriber } from "svelte/store";
  const { actions: campaignActions, state: campaignState } =
    useCampaignService();
  const { getCampaignSummaries } = campaignActions;
  const { campaignSummaries } = campaignState;

  const { actions: contentActions, state: contentState } = useContentService();
  const { getAvailableGames } = contentActions;
  const { availableGames } = contentState;

  const refreshListing = writable<boolean>(false);

  const columns: ColumnDefinition[] = [
    {
      position: 1,
      header: {
        header: "Description",
      },
      property: "description",
      valueDisplayComponent: CampaignLink,
    },
    {
      position: 2,
      header: {
        header: "Game",
      },
      property: "game",
    },
  ];
  const config: TableConfiguration = {
    columns,
  };
  let campaignsRowData: RowData[] = [];

  let campaignListingLoaded = false;

  const calculateCampagignRows = (
    campaigns: CampaignSummary[],
    availableGames: ContentItemSummary[]
  ) => {
    campaignsRowData = campaigns.map((campaign) => {
      const translateGame = (game: string) => {
        return availableGames.find((g) => g.contentCode === game)?.name ?? "";
      };
      return {
        description: {
          label: campaign.name,
          path: `/campaigns/${campaign.id}`,
        },
        game: translateGame(campaign.game),
      };
    });
    if (campaigns.length > 0) campaignListingLoaded = true;
  };

  let newDialogOpen = false;

  const handleOpenNewDialog = () => {
    newDialogOpen = true;
  };
  const handleCloseNewDialog = () => {
    newDialogOpen = false;
  };

  const handleGetCampaigns = async () => {
    if ($refreshListing) {
      await getCampaignSummaries();
      getAvailableGames();
      refreshListing.set(false);
    }
  };

  refreshListing.subscribe(() => {
    void handleGetCampaigns();
  });

  let availableGamesUnsubscribe: Unsubscriber;
  let campaignListingUnsubscribe: Unsubscriber;
  onMount(() => {
    availableGamesUnsubscribe = availableGames.subscribe((games) => {
      if ($campaignSummaries.length > 0 && games.length > 0)
        calculateCampagignRows($campaignSummaries, games);
    });

    campaignListingUnsubscribe = campaignSummaries.subscribe((campaigns) => {
      if (campaigns.length > 0 && $availableGames.length > 0)
        calculateCampagignRows(campaigns, $availableGames);
    });

    refreshListing.set(true);
  });

  onDestroy(() => {
    availableGamesUnsubscribe();
    campaignListingUnsubscribe();
  });
</script>

<section class="text-center lg:text-left lg:pl-3 ">
  <div
    class={clsx(
      "relative mt-2 px-3 py-1 items-center max-w-md mx-auto rounded-md backdrop-blur-sm",
      "bg-gray-50 dark:bg-gray-700"
    )}
  >
    <div aria-label="Current Campaigns" class="text-center text-xl">
      Campaigns
    </div>
    <div class="absolute top-1 right-1">
      <button aria-label="Add New Campaign" on:click={handleOpenNewDialog}>
        <AddContainedIcon />
      </button>
    </div>
    {#if campaignListingLoaded}
      <Table {config} rowData={campaignsRowData} />
    {/if}
  </div>
  {#if newDialogOpen}
    <CampaignNewDialog
      {newDialogOpen}
      handleCloseDialog={handleCloseNewDialog}
    />
  {/if}
</section>
