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
  import { writable, type Unsubscriber } from "svelte/store";

  import useContentServiceThick from "../../../Service/ContentServiceThick";
  import useCampaignService from "../../../Service/CampaignService";

  const { actions: contentThickActions } = useContentServiceThick();
  const { getAvailableGames: getAvailableGamesThick } = contentThickActions;

  const { actions: campaignActions, state: campaignState } =
    useCampaignService();
  const { createCampaign, clearCampaign } = campaignActions;
  const { campaignDetail } = campaignState;

  const navigate = useNavigate();

  export let open = false;

  const availableGames = writable<ContentItemSummary[]>([]);
  const handleGetGames = async () => {
    try {
      const games = await getAvailableGamesThick();
      availableGames.set(games);
    } catch {
      availableGames.set([]);
    }
  };

  const defaultNewCampaign = (): Campaign => {
    return {
      id: uuid(),
      description: "",
      name: "",
      game: "",
      scenarios: [],
      party: [],
      editable: true,
    };
  };
  let newCampaign: Campaign = { ...defaultNewCampaign() };

  const handleNewCampaign = async () => {
    await createCampaign({ ...newCampaign });
    newCampaign = { ...defaultNewCampaign() };
  };

  let campaignDetailUnsubscribe: Unsubscriber;
  onMount(() => {
    clearCampaign();
    campaignDetailUnsubscribe = campaignDetail.subscribe((campaign) => {
      if (campaign) navigate(`/campaigns/${campaign.id}`);
    });
    void handleGetGames();
  });

  const gameSelectKey = (contentCode: string | undefined) =>
    `${contentCode ?? ""}`;

  onDestroy(() => {
    campaignDetailUnsubscribe();
  });
</script>

<Dialog
  bind:open
  fullscreen
  surface$class="mt-12"
  surface$style="max-height: calc(100vh - 40px);"
>
  <DialogHeader>
    <DialogTitle>New Campaign</DialogTitle>
  </DialogHeader>
  <DialogContent class="flex flex-col">
    {#if $availableGames.length > 0}
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
        newCampaign = { ...defaultNewCampaign() };
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
