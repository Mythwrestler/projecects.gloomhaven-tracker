<script lang="ts">
  import { onMount } from "svelte";
  import { Campaign } from "../../../models";
  import {
    AddContainedIcon,
    ColumnDefinition,
    RowData,
    Table,
    TableConfiguration,
  } from "../../../common/Components";
  import {
    campaignListing,
    campaignListingLoaded,
    campaignListingLoading,
    getCampaigns,
  } from "../../../Service/CampaignService";
  import CampaignLink from "./CampaignLink.svelte";
  import CampaignNewDialog from "./CampaignNewDialog.svelte";

  const handleGetCampaigns = async () => {
    if (
      !$campaignListingLoaded &&
      !$campaignListingLoading &&
      ($campaignListing as Campaign[]).length === 0
    ) {
      await getCampaigns();
    }
  };

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
  campaignListing.subscribe((campaigns) => {
    campaignsRowData = campaigns.map((campaign) => {
      const translateGame = (game: string) => {
        if (game === "jawsOfTheLion") return "Jaws of the Lion";
        return "Original";
      };
      return {
        description: {
          label: campaign.description,
          path: `/campaign/${campaign.id}`,
        },
        game: translateGame(campaign.game),
      };
    });
  });

  let newDialogOpen = false;
  const handleOpenNewDialog = () => {
    newDialogOpen = true;
  };
  const handleCloseNewDialog = () => {
    newDialogOpen = false;
  };

  onMount(() => {
    void handleGetCampaigns();
  });
</script>

<section class="text-center lg:text-left lg:pl-3 ">
  <div
    class="relative mt-2 px-3 py-1 items-center max-w-md mx-auto bg-gray-50 rounded-md backdrop-blur-sm"
  >
    <div aria-label="Available Scenarios" class="text-center text-xl">
      Campaigns
    </div>
    <div class="absolute top-1 right-1">
      <button aria-label="Add New Scenario" on:click={handleOpenNewDialog}>
        <AddContainedIcon />
      </button>
    </div>
    {#if $campaignListingLoaded}
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
