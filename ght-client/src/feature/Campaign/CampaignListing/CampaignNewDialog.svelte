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
  import { Campaign, CampaignSummary } from "../../../models";
  import { ContentItemSummary } from "../../../models/Content";
  import useContentService from "../../../Service/ContentService";
  import { v4 as uuid } from "uuid";
  import { addCampaign } from "../../../Service/CampaignService";
  import { writable } from "svelte/store";
  const navigate = useNavigate();

  const { GetAvailableGames } = useContentService();

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
    availableScenarios: [],
    closedScenarios: [],
    completedScenarios: [],
    party: {
      characters: [],
    },
  };

  const savedNewCampaign = writable<CampaignSummary>();
  const handleNewCampaign = async () => {
    let savedCampaign = await addCampaign(newCampaign);
    if (savedCampaign) savedNewCampaign.set(savedCampaign);
  };
  const handleNavigate = () => {
    navigate(`/campaign/${($savedNewCampaign as CampaignSummary).id}`);
  };
  $: if ($savedNewCampaign) handleNavigate();
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
