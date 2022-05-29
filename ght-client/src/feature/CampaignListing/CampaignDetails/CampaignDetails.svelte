<script lang="ts">
  import { Button, TextField } from "../../../common/Components";
  import type { Campaign } from "../../../models/Campaign";
  import * as GlobalError from "../../../Service/Error";

  import { useCampaignService } from "../../../Service/CampaignService";
  import CampaignParty from "./CampaignParty.svelte";
  import { useLocation } from "svelte-navigator";
  import CampaignScenarios from "./CampaignScenarios.svelte";
  import { onDestroy, onMount } from "svelte";
  import { accessToken } from "@dopry/svelte-oidc";
  import { writable } from "svelte/store";

  // your script goes here
  export let campaignId = "";
  const location = useLocation();
  const {
    State: campaignState,
    getCampaign,
    saveCampaign,
    addUpdateScenario,
    updateCampaignDescription,
    updateCampaignName,
    clearCampaign,
  } = useCampaignService(accessToken);
  const { campaignNotSaved } = campaignState;

  let newGameCode = "";
  const getNewGameCode = () => {
    newGameCode = $location.search.replace("?selectedGame=", "");
  };

  let campaign: Campaign | undefined;
  campaignState.campaign.subscribe((c) => {
    campaign = c;
  });

  const requestCampaign = writable<boolean>(false);
  const handleGetCampaign = async (campaignId: string) => {
    if ($accessToken == undefined || $accessToken.trim() == "") return;
    campaign = undefined;
    try {
      await getCampaign(campaignId);
    } catch {
      GlobalError.showErrorMessage("Failed to get campaign");
    }
  };

  requestCampaign.subscribe(() => {
    void handleGetCampaign(campaignId);
  });

  accessToken.subscribe(() => {
    void handleGetCampaign(campaignId);
  });

  const handleSaveCampaign = () => {
    void saveCampaign();
  };

  const handleUpdateCampaignDescription = () => {
    updateCampaignDescription(campaign?.description ?? "");
  };

  const handleUpdateCampaignName = () => {
    updateCampaignName(campaign?.name ?? "");
  };

  $: if ($location.search) getNewGameCode();
  $: if (campaignId !== campaign?.id) requestCampaign.set(true);

  onMount(() => {
    clearCampaign();
    requestCampaign.set(true);
  });

  onDestroy(() => {
    clearCampaign();
  });
</script>

<section>
  {#if !campaign}
    <div>Getting Campaign Details</div>
  {:else}
    <div class="mt-2">
      <TextField
        bind:value={campaign.name}
        onChange={handleUpdateCampaignName}
        placeholderText="Campaign Name"
        displayLabel="DescripNametion"
        `
      />
    </div>
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
