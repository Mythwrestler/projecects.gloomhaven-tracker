<script lang="ts">
  import { onMount } from "svelte";
  import { Campaign } from "../../../models";
  import {
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

  onMount(() => {
    void handleGetCampaigns();
  });
</script>

<section class="text-center lg:text-left lg:pl-3 ">
  <span class="text-2xl">Campaigns</span>
  {#if $campaignListingLoaded}
    <Table {config} rowData={campaignsRowData} />
  {/if}
</section>
