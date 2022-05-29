<script lang="ts">
  import { onMount } from "svelte";
  import type { Campaign } from "../../../models/Campaign";
  import { AddContainedIcon, Table } from "../../../common/Components";
  import { accessToken } from "@dopry/svelte-oidc";
  import type {
    ColumnDefinition,
    RowData,
    TableConfiguration,
  } from "../../../common/Components";
  import CampaignLink from "./CampaignLink.svelte";
  import CampaignNewDialog from "./CampaignNewDialog.svelte";
  import { useCampaignService } from "../../../Service/CampaignService";
  import { useContentService } from "../../../Service/ContentService";
  import clsx from "clsx";
  import type { ContentItemSummary } from "../../../models/Content";
  import { writable } from "svelte/store";

  const { State: campaignState, getCampaignListing } =
    useCampaignService(accessToken);
  const { campaignListing } = campaignState;
  const { GetAvailableGames } = useContentService(accessToken);

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

  let availableGames: ContentItemSummary[] = [];
  campaignListing.subscribe((campaigns) => {
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
  });

  let newDialogOpen = false;

  const handleOpenNewDialog = () => {
    newDialogOpen = true;
  };
  const handleCloseNewDialog = () => {
    newDialogOpen = false;
  };

  const handleGetCampaigns = async (
    token: string | undefined
  ): Promise<void> => {
    if (token == undefined || token.trim() == "") return;
    if ($refreshListing) {
      await getCampaignListing();
      availableGames = await GetAvailableGames();
      refreshListing.set(false);
    }
  };

  refreshListing.subscribe(() => {
    void handleGetCampaigns($accessToken);
  });

  accessToken.subscribe((token) => {
    void handleGetCampaigns(token);
  });

  onMount(() => {
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
