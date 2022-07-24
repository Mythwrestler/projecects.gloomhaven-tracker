<script lang="ts">
  import { onMount } from "svelte";
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

  import type { Campaign } from "../../../models/Campaign";
  import type { ContentItemSummary } from "../../../models/Content";
  import { useContentService } from "../../../Service/ContentService";
  import { useCampaignService } from "../../../Service/CampaignService";
  import { v4 as uuid } from "uuid";
  import { accessToken } from "../../../common/Utils/OidcSvelteClient";
  const navigate = useNavigate();

  const { GetAvailableGames } = useContentService(accessToken);
  const { State: campaignState, createNewCampaign } =
    useCampaignService(accessToken);

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
    name: "",
    game: "",
    scenarios: [],
    party: [],
    editable: true,
  };

  const handleNewCampaign = async () => {
    await createNewCampaign(newCampaign);
  };

  campaignState.campaign.subscribe((campaign) => {
    if (campaign) navigate(`/campaigns/${campaign.id}`);
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
