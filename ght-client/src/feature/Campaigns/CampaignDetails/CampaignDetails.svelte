<script lang="ts">
  import { Button, TextField } from "../../../common/Components";
  import { Campaign } from "../../../models/Campaign";
  import * as GlobalError from "../../../Service/Error";

  import { useCampaignService } from "../../../Service/CampaignService";
  import CampaignParty from "./CampaignParty.svelte";
  import { NavigatorLocation, useLocation } from "svelte-navigator";
  import AnyObject from "svelte-navigator/types/AnyObject";
  import CampaignScenarios from "./CampaignScenarios.svelte";
  import { onMount } from "svelte";

  // your script goes here
  export let campaignId = "";
  const location = useLocation();
  const {
    State: campaignState,
    getCampaign,
    saveCampaign,
    addUpdateScenario,
    updateCampaignDescription,
  } = useCampaignService();
  const { campaignNotSaved } = campaignState;

  let newGameCode = "";
  const getNewGameCode = () => {
    newGameCode = ($location as NavigatorLocation<AnyObject>).search.replace(
      "?selectedGame=",
      ""
    );
  };

  let campaign: Campaign | undefined;
  campaignState.campaign.subscribe((c) => {
    campaign = c;
  });

  const handleGetCampaign = async (campaignId: string) => {
    campaign = undefined;
    try {
      await getCampaign(campaignId);
    } catch {
      GlobalError.showErrorMessage("Failed to get campaign");
    }
  };

  const handleSaveCampaign = () => {
    void saveCampaign();
  };

  const handleUpdateCampaignDescription = () => {
    updateCampaignDescription(campaign?.description ?? "");
  };

  $: if ($location.search) getNewGameCode();
  $: if (campaignId !== campaign?.id) void handleGetCampaign(campaignId);

  onMount(() => {
    void handleGetCampaign(campaignId);
  });
</script>

<section>
  {#if !campaign}
    <div>Getting Campaign Details</div>
  {:else}
    <div class="mt-2">
      <TextField
        bind:value={campaign.description}
        onChange={handleUpdateCampaignDescription}
        placeholderText="Campaign Description"
        displayLabel="Description"
      />
    </div>
    <CampaignParty bind:campaign />
    <CampaignScenarios bind:campaign saveScenario={addUpdateScenario} />
    <div class="flex max-w-md h-12 mx-auto mt-2">
      <Button
        variant="outlined"
        disabled={!$campaignNotSaved}
        onClick={handleSaveCampaign}
        color={$campaignNotSaved ? "blue" : "gray"}
      >
        Save Campaign
      </Button>
    </div>
  {/if}
</section>
