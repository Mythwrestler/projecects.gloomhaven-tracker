<script lang="ts">
  import { onDestroy, onMount } from "svelte";
  import { useNavigate } from "svelte-navigator";

  import Dialog, {
    Header as DialogHeader,
    Title as DialogTitle,
    Content as DialogContent,
    Actions as DialogActions,
  } from "@smui/dialog";
  import Textfield from "@smui/textfield";
  import Select, { Option } from "@smui/select";
  import Button from "@smui/button";

  import type { Campaign } from "../../../models/Campaign";
  import type { ContentItemSummary } from "../../../models/Content";
  import { v4 as uuid } from "uuid";
  import type { Unsubscriber } from "svelte/store";

  import useContentService from "../../../Service/ContentService";
  import useCampaignService from "../../../Service/CampaignService";

  const { actions: contentActions, state: contentState } = useContentService();
  const { getAvailableGames } = contentActions;
  const { availableGames } = contentState;

  const { actions: campaignActions, state: campaignState } =
    useCampaignService();
  const { createCampaign, clearCampaign } = campaignActions;
  const { campaignDetail } = campaignState;

  const navigate = useNavigate();

  export let open = false;

  let games: ContentItemSummary[] = [];
  const handleLoadGames = () => {
    getAvailableGames();
  };

  const newCampaign: Campaign = {
    id: uuid(),
    description: "",
    name: "",
    game: "",
    scenarios: [],
    party: [],
    editable: true,
  };

  const handleNewCampaign = async () => {
    await createCampaign(newCampaign);
  };

  let campaignDetailUnsubscribe: Unsubscriber;
  onMount(() => {
    clearCampaign();
    campaignDetailUnsubscribe = campaignDetail.subscribe((campaign) => {
      if (campaign) navigate(`/campaigns/${campaign.id}`);
    });

    void handleLoadGames();
  });

  const gameSelectKey = (contentCode: string | undefined) =>
    `${contentCode ?? ""}`;

  onDestroy(() => {
    campaignDetailUnsubscribe();
  });
</script>

<Dialog
  fullscreen
  bind:open
  surface$style="width: calc(100vw - 50vw); min-width: 150px; height: calc(100vw - 32px); min-height: 150px"
>
  <DialogHeader>
    <DialogTitle>New Campaign</DialogTitle>
  </DialogHeader>
  <DialogContent class="flex flex-col">
    {#if games}
      <Textfield
        class="w-80"
        bind:value={newCampaign.name}
        label="Campaign Name"
      />
      <Select key={gameSelectKey} bind:value={newCampaign.game} label="Game">
        <Option value={null} />
        {#each $availableGames as game}
          <Option value={game.contentCode}>{game.name}</Option>
        {/each}
      </Select>
    {/if}
  </DialogContent>
  <DialogActions>
    <Button
      variant="raised"
      color="secondary"
      on:click={() => {
        open = closed;
      }}
    >
      Close
    </Button>
    <Button
      variant="raised"
      color="primary"
      disabled={newCampaign.game === "" || newCampaign.name === ""}
      on:click={handleNewCampaign}
    >
      New Campaign
    </Button>
  </DialogActions>
</Dialog>
