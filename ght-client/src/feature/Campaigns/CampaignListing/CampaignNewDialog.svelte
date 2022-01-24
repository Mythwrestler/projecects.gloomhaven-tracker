<script lang="ts">
  import { onMount } from "svelte";
  import { useNavigate } from "svelte-navigator";

  import {
    Dialog,
    DialogHeader,
    DialogBody,
    DialogFooter,
    Button,
    DropDownOption,
    DropDown,
    TextField,
  } from "../../../common/Components";
  import { Campaign } from "../../../models/Campaign";
  import { ContentItemSummary } from "../../../models/Content";
  import { useContentService } from "../../../Service/ContentService";
  import { useCampaignService } from "../../../Service/CampaignService";
  import { v4 as uuid } from "uuid";
  const navigate = useNavigate();

  const { GetAvailableGames } = useContentService();
  const { State: campaignState, createNewCampaign } = useCampaignService();

  export let newDialogOpen = false;
  export let handleCloseDialog: () => void;

  let games: ContentItemSummary[] = [];
  let gameOptions: DropDownOption[] = [];
  const handleLoadGames = async () => {
    let gamesToLoad = await GetAvailableGames();
    if (gamesToLoad) {
      games = gamesToLoad;
      gameOptions = gamesToLoad.map((game) => {
        return {
          label: game.name,
          value: game.contentCode,
        };
      });
    }
  };

  const newCampaign: Campaign = {
    id: uuid(),
    description: "",
    game: "",
    scenarios: [],
    party: [],
  };

  const handleNewCampaign = async () => {
    await createNewCampaign(newCampaign);
  };

  campaignState.campaign.subscribe((campaign) => {
    if (campaign && campaign.id === newCampaign.id)
      navigate(`/campaigns/${campaign.id}`);
  });

  onMount(() => {
    void handleLoadGames();
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
          bind:value={newCampaign.description}
          placeholderText="Campaign Description"
          displayLabel="Description"
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
        disabled={newCampaign.game === "" && newCampaign.description !== ""}
        variant="filled"
        onClick={() => void handleNewCampaign()}
      >
        New Campaign
      </Button>
    </div>
  </DialogFooter>
</Dialog>
