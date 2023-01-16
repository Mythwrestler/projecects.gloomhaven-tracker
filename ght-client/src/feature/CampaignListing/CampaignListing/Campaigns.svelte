<script lang="ts">
  import { onDestroy, onMount } from "svelte";
  import type { CampaignSummary } from "../../../models/Campaign";
  import CampaignNewDialog from "./CampaignNewDialog.svelte";
  import useContentService from "../../../Service/ContentService";
  import useCampaignService from "../../../Service/CampaignService";

  import type { ContentItemSummary } from "../../../models/Content";
  import { writable, type Unsubscriber } from "svelte/store";
  import GhtPanel from "../../../common/Components/GHTPanel/GHTPanel.svelte";
  import { Title, Content as PaperContent } from "@smui/paper";
  import List, { Item, Text, PrimaryText, SecondaryText } from "@smui/list";
  import Card, { Content as CardContent } from "@smui/card";
  import Button from "@smui/button";
  import { useNavigate } from "svelte-navigator";
  const navigate = useNavigate();

  const { actions: contentActions } = useContentService();
  const { getAvailableGames } = contentActions;

  const { actions: campaignActions } = useCampaignService();
  const { getCampaignSummaries } = campaignActions;

  const refreshListing = writable<boolean>(false);

  let campaignListingLoaded = false;
  let selectedCampaign = "";

  interface CampaignListItem {
    description: {
      label: string;
      path: string;
    };
    game: string;
  }
  let campaignListing: CampaignListItem[] = [];

  const calculateCampagignRows = (
    campaigns: CampaignSummary[],
    availableGames: ContentItemSummary[]
  ) => {
    campaignListing = campaigns.map((campaign) => {
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
      await handleGetCampaignSummaries();
      await handleGetGames();
      refreshListing.set(false);
    }
  };

  const availableGames = writable<ContentItemSummary[]>([]);
  const handleGetGames = async () => {
    try {
      const games = await getAvailableGames();
      availableGames.set(games);
    } catch {
      availableGames.set([]);
    }
  };

  const campaignSummaries = writable<CampaignSummary[]>([]);
  const handleGetCampaignSummaries = async () => {
    try {
      const summaries = await getCampaignSummaries();
      campaignSummaries.set(summaries);
    } catch {
      campaignSummaries.set([]);
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

  const getDescription = (campaign: CampaignListItem) => campaign.description;

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
            {#each campaignListing as campaign}
              {@const description = getDescription(campaign)}
              <Item
                on:SMUI:action={() => {
                  // eslint-disable-next-line @typescript-eslint/no-unsafe-argument
                  navigate(description.path);
                }}
              >
                <Text>
                  <PrimaryText>{description.label}</PrimaryText>
                  <SecondaryText>{campaign.game}</SecondaryText>
                </Text>
              </Item>
            {/each}
          </List>
        </CardContent>
      </Card>
      <div class="mt-3">
        <Button variant="raised" color="primary" on:click={handleOpenNewDialog}>
          New
        </Button>
      </div>
    {/if}
  </PaperContent>
</GhtPanel>
<CampaignNewDialog bind:open={newDialogOpen} />
