<script lang="ts">
  import { onMount } from "svelte";
  import { Campaign } from "../../../models/Campaign";
  import {
    AddContainedIcon,
    ColumnDefinition,
    RowData,
    Table,
    TableConfiguration,
  } from "../../../common/Components";
  import CampaignLink from "./CampaignLink.svelte";
  import CampaignNewDialog from "./CampaignNewDialog.svelte";
  import { useCampaignService } from "../../../Service/CampaignService";

  const { State, getCampaignListing } = useCampaignService();
  const { campaignListing } = State;

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

  campaignListing.subscribe((campaigns) => {
    campaignsRowData = campaigns.map((campaign) => {
      const translateGame = (game: string) => {
        if (game === "jawsOfTheLion") return "Jaws of the Lion";
        return "Original";
      };
      return {
        description: {
          label: campaign.description,
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

  onMount(() => {
    void getCampaignListing();
  });
</script>

<section class="text-center lg:text-left lg:pl-3 ">
  <div
    class="relative mt-2 px-3 py-1 items-center max-w-md mx-auto bg-gray-50 rounded-md backdrop-blur-sm"
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
