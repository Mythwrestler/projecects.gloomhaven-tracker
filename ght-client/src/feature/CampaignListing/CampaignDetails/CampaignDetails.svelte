<script lang="ts">
  import TextField from "@smui/textfield";

  import type { Campaign, CampaignSummary } from "../../../models/Campaign";
  import useCampaignService from "../../../Service/CampaignService";
  import CampaignParty from "./CampaignParty.svelte";
  import CampaignScenarios from "./CampaignScenarios.svelte";
  import GhtPanel from "../../../common/Components/GHTPanel/GHTPanel.svelte";

  // #region Props

  export let campaignId = "";

  // #endregion

  const { actions: campaignActions } = useCampaignService();
  const { getCampaignDetail, updateCampaign } = campaignActions;

  let campaign: Campaign | undefined;
  let campaignName = "";
  let campaignDescription = "";
  const handleGetCampaign = async (campaignId: string) => {
    try {
      campaign = await getCampaignDetail(campaignId);
      campaignName = campaign?.name ?? "";
      campaignDescription = campaign?.description ?? "";
    } catch {
      campaign = undefined;
    }
  };

  const handleUpdateCampaignDescription = async () => {
    if (campaign) {
      const updatedCampaign = await updateCampaign(campaign, {
        ...campaign,
        description: campaignDescription,
        scenarios: undefined,
        party: undefined,
      } as CampaignSummary);
      campaign.description = updatedCampaign.description;
    }
  };

  const handleUpdateCampaignName = async () => {
    if (campaign) {
      const updatedCampaign = await updateCampaign(campaign, {
        ...campaign,
        name: campaignName,
        scenarios: undefined,
        party: undefined,
      } as CampaignSummary);
      campaign.name = updatedCampaign.name;
    }
  };

  $: void handleGetCampaign(campaignId);
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
      <CampaignParty
        campaignId={campaign.id}
        gameCode={campaign.game}
        party={campaign.party}
      />
    </div>

    <div class="mt-2">
      <CampaignScenarios
        campaignId={campaign.id}
        gameCode={campaign.game}
        campaignScenarios={campaign.scenarios}
      />
    </div>
  {/if}
</GhtPanel>
