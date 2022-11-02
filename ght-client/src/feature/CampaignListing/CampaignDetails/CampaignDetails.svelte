<script lang="ts">
  // import { Button, TextField } from "../../../common/Components";
  import TextField from "@smui/textfield";

  import type { Campaign, CampaignSummary } from "../../../models/Campaign";
  import * as GlobalError from "../../../Service/Error";
  import useCampaignService from "../../../Service/CampaignService";
  import CampaignParty from "./CampaignParty.svelte";
  import { useLocation } from "svelte-navigator";
  import CampaignScenarios from "./CampaignScenarios.svelte";
  import { onDestroy, onMount } from "svelte";
  import { accessToken } from "@ci-lab/svelte-oidc-context";
  import { writable, type Unsubscriber } from "svelte/store";
  import { deepClone } from "fast-json-patch";
  import GhtPanel from "../../../common/Components/GHTPanel/GHTPanel.svelte";
  export let campaignId = "";
  const location = useLocation();

  const { actions: campaignActions, state: campaignState } =
    useCampaignService();
  const { getCampaignDetail, updateCampaign, clearCampaign } = campaignActions;
  const { campaignDetail } = campaignState;

  let newGameCode = "";
  const getNewGameCode = () => {
    newGameCode = $location.search.replace("?selectedGame=", "");
  };

  let campaign: Campaign | undefined;
  let campaignName = "";
  let campaignDescription = "";

  const requestCampaign = writable<boolean>(false);
  const handleGetCampaign = async (campaignId: string) => {
    if ($accessToken == undefined || $accessToken.trim() == "") return;
    campaign = undefined;
    try {
      await getCampaignDetail(campaignId);
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

  let campaignDetailUnsubscribe: Unsubscriber;
  onMount(() => {
    clearCampaign();
    campaignDetailUnsubscribe = campaignDetail.subscribe(
      (campaignFromStore) => {
        campaign = deepClone(campaignFromStore) as Campaign | undefined;
        campaignName = campaignFromStore?.name ?? "";
        campaignDescription = campaignFromStore?.description ?? "";
      }
    );
    requestCampaign.set(true);
  });

  onDestroy(() => {
    campaignDetailUnsubscribe();
    clearCampaign();
  });
</script>

<GhtPanel color="ght-panel">
  {#if !campaign}
    <div>Getting Campaign Details</div>
  {:else}
    <TextField
      bind:value={campaignName}
      style="width: 100%;"
      on:blur={handleUpdateCampaignName}
      label="Name"
    />
    <div class="mt-2">
      <TextField
        bind:value={campaignDescription}
        style="width: 100%;"
        on:blur={handleUpdateCampaignDescription}
        label="Description"
      />
    </div>
    <div class="mt-2">
      <CampaignParty bind:campaign />
    </div>

    <div class="mt-2">
      <CampaignScenarios bind:campaign />
    </div>
  {/if}
</GhtPanel>
