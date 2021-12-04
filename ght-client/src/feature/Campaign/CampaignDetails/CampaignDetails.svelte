<script lang="ts">
  import { TextField } from "../../../common/Components";
  import { Campaign } from "../../../models";
  import * as GlobalError from "../../../Service/Error";

  import { getCampaign } from "../../../Service/CampaignService";
  import CampaignScenarios from "./CampaignScenarios.svelte";

  // your script goes here
  export let campaignId = "";

  let campaignLoading = false;
  let campaignLoaded = false;
  let campaign: Campaign | undefined;
  const handleGetCampaign = async (campaignId: string) => {
    campaignLoaded = false;
    campaignLoading = true;
    try {
      campaign = await getCampaign(campaignId);
      campaignLoaded = true;
      campaignLoading = false;
    } catch {
      campaignLoaded = false;
      campaignLoading = false;
      GlobalError.showErrorMessage("Failed to get campaign");
    }
  };

  $: void handleGetCampaign(campaignId);
</script>

<section>
  {#if !campaign}
    <div>Getting Campaign Details</div>
  {:else}
    <div class="mt-2">
      <TextField
        bind:value={campaign.description}
        placeholderText="Campaign Description"
        displayLabel="Description"
      />
    </div>
    <CampaignScenarios {campaign} />
  {/if}
</section>
