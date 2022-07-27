<script lang="ts">
  import { Button, TextField } from "../../../common/Components";
  import type { Campaign, CampaignSummary } from "../../../models/Campaign";
  import * as GlobalError from "../../../Service/Error";

  import { useCampaignService } from "../../../Service/CampaignService";
  import CampaignParty from "./CampaignParty.svelte";
  import { useLocation } from "svelte-navigator";
  import CampaignScenarios from "./CampaignScenarios.svelte";
  import { onDestroy, onMount } from "svelte";
  import { accessToken } from "../../../common/Utils/OidcSvelteClient";
  import { writable } from "svelte/store";

  // your script goes here
  export let campaignId = "";
  const location = useLocation();
  const {
    State: campaignState,
    getCampaign,
    //saveCampaign,
    addUpdateScenario,
    updateCampaign,
    // updateCampaignDescription,
    // updateCampaignName,
    clearCampaign,
  } = useCampaignService(accessToken);
  //const { campaignNotSaved } = campaignState;

  let newGameCode = "";
  const getNewGameCode = () => {
    newGameCode = $location.search.replace("?selectedGame=", "");
  };

  let campaign: Campaign | undefined;
  let campaignName = "";
  let campaignDescription = "";
  campaignState.campaign.subscribe((campaignFromStore) => {
    campaign = campaignFromStore;
    campaignName = campaignFromStore?.name ?? "";
    campaignDescription = campaignFromStore?.description ?? "";
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

  // const handleSaveCampaign = () => {
  //   void saveCampaign();
  // };

  const handleUpdateCampaignDescription = async () => {
    await updateCampaign({
      ...campaign,
      description: campaignDescription,
      scenarios: undefined,
      party: undefined,
    } as CampaignSummary);
  };

  const handleUpdateCampaignName = async () => {
    await updateCampaign({
      ...campaign,
      name: campaignName,
      scenarios: undefined,
      party: undefined,
    } as CampaignSummary);
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
        bind:value={campaignName}
        onBlur={handleUpdateCampaignName}
        placeholderText="Campaign Name"
        displayLabel="Name"
      />
    </div>
    <div class="mt-2">
      <TextField
        bind:value={campaignDescription}
        onBlur={handleUpdateCampaignDescription}
        placeholderText="Campaign Description"
        displayLabel="Description"
      />
    </div>
    <CampaignParty bind:campaign />
    <CampaignScenarios bind:campaign saveScenario={addUpdateScenario} />
    <!-- <div class="flex max-w-md h-12 mx-auto mt-2">
      <Button
        variant="outlined"
        disabled={!$campaignNotSaved}
        onClick={handleSaveCampaign}
        color={$campaignNotSaved ? "blue" : "gray"}
      >
        Save Campaign
      </Button>
    </div> -->
  {/if}
</section>
