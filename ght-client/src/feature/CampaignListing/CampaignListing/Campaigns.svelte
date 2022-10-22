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
  import GhtPanel from "../../../common/Components/GHTPanel/GHTPanel.svelte";
  import { Title, Content as PaperContent } from "@smui/paper";
  import List, { Item, Text, PrimaryText, SecondaryText } from "@smui/list";
  import Card, {
    Content as CardContent,
    Actions,
    ActionButtons,
  } from "@smui/card";
  import Button, { Label as ButtonLabel } from "@smui/button";
  import { useNavigate } from "svelte-navigator";
  const { actions: campaignActions, state: campaignState } =
    useCampaignService();
  const { getCampaignSummaries } = campaignActions;
  const { campaignSummaries } = campaignState;
  const navigate = useNavigate();

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
  let selectedCampaign = "";

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

<GhtPanel color="ght-panel">
  <Title aria-label="Current Campaigns" class="text-center text-xl">
    Campaigns
  </Title>
  <PaperContent>
    {#if campaignListingLoaded}
      <Card variant="outlined">
        <CardContent>
          <List twoLine singleSelection>
            {#each campaignsRowData as campaignData}
              <Item
                on:SMUI:action={() => {
                  console.log(campaignData.description.path);
                  navigate(campaignData.description.path);
                }}
              >
                <Text>
                  <PrimaryText>{campaignData?.description?.label}</PrimaryText>
                  <SecondaryText>{campaignData?.game}</SecondaryText>
                </Text>
              </Item>
            {/each}
          </List>
        </CardContent>
        <Actions>
          <ActionButtons>
            <Button
              variant="raised"
              color="secondary"
              on:click={handleOpenNewDialog}
            >
              New
            </Button>
          </ActionButtons>
        </Actions>
      </Card>
    {/if}
  </PaperContent>
</GhtPanel>
{#if newDialogOpen}
  <CampaignNewDialog {newDialogOpen} handleCloseDialog={handleCloseNewDialog} />
{/if}

<style lang="scss">
</style>
