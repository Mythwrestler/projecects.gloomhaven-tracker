<script lang="ts">
  import { onDestroy, onMount } from "svelte";
  import { useNavigate } from "svelte-navigator";

  import {
    Dialog,
    DialogHeader,
    DialogBody,
    DialogFooter,
    Button,
    DropDown,
    TextField,
  } from "../../../common/Components";

  import type { DropDownOption } from "../../../common/Components";
  import useContentService from "../../../Service/ContentService";
  import useCampaignService from "../../../Service/CampaignService";
  import type { Campaign } from "../../../models/Campaign";
  import type { ContentItemSummary } from "../../../models/Content";
  // import { useCampaignService } from "../../../Service/CampaignService/index-old";
  import { v4 as uuid } from "uuid";
  // import { accessToken } from "@ci-lab/svelte-oidc-context";
  import type { Unsubscriber } from "svelte/store";
  const navigate = useNavigate();

  const { actions: contentActions, state: contentState } = useContentService();
  const { getAvailableGames } = contentActions;
  const { availableGames } = contentState;

  // const { State: campaignState, createNewCampaign } =
  //   useCampaignService(accessToken);
  const { actions: campaignActions, state: campaignState } =
    useCampaignService();
  const { createCampaign, clearCampaign } = campaignActions;
  const { campaignDetail } = campaignState;

  export let newDialogOpen = false;
  export let handleCloseDialog: () => void;

  let games: ContentItemSummary[] = [];
  let gameOptions: DropDownOption[] = [];
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

  let availableGamesUnsubscribe: Unsubscriber;
  let campaignDetailUnsubscribe: Unsubscriber;
  onMount(() => {
    clearCampaign();
    availableGamesUnsubscribe = availableGames.subscribe(
      (gamesFromState: ContentItemSummary[]) => {
        if (gamesFromState) {
          games = gamesFromState;
          gameOptions = gamesFromState.map((game) => {
            return {
              label: game.name,
              value: game.contentCode,
            };
          });
        }
      }
    );

    campaignDetailUnsubscribe = campaignDetail.subscribe((campaign) => {
      if (campaign) navigate(`/campaigns/${campaign.id}`);
    });

    void handleLoadGames();
  });

  onDestroy(() => {
    availableGamesUnsubscribe();
    campaignDetailUnsubscribe();
  });
</script>

<Dialog offClick open={newDialogOpen} onClose={handleCloseDialog}>
  <DialogHeader slot="DialogHeader">
    <div class="mx-full text-center text-2xl mb-3">Select You Game...</div>
    <div class="border-b-2 border-solid" />
  </DialogHeader>
  <DialogBody slot="DialogBody">
    {#if games}
      <div class="mt-2">
        <TextField
          border
          variant="square"
          bind:value={newCampaign.name}
          placeholderText="Campaign Name"
          displayLabel="Name"
        />
      </div>
      <DropDown
        variant="square"
        label="Character Class"
        bind:selected={newCampaign.game}
        placeHolder={"Select a Game"}
        options={gameOptions}
      />
    {/if}
  </DialogBody>
  <DialogFooter slot="DialogFooter">
    <div class="bg-white dark:bg-gray-700 w-full py-3 pl-3">
      <Button
        disabled={newCampaign.game === "" && newCampaign.name !== ""}
        variant="filled"
        onClick={() => void handleNewCampaign()}
      >
        New Campaign
      </Button>
    </div>
  </DialogFooter>
</Dialog>
