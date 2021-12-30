<script lang="ts">
  import { TextField } from "../../../common/Components";
  import { Campaign } from "../../../models";
  import * as GlobalError from "../../../Service/Error";

  import { getCampaign } from "../../../Service/CampaignService";
  import CampaignScenarios from "./CampaignScenarios.svelte";
  import CampaignParty from "./CampaignParty.svelte";
  import { NavigatorLocation, useLocation } from "svelte-navigator";
  import AnyObject from "svelte-navigator/types/AnyObject";

  // your script goes here
  export let campaignId = "";
  const location = useLocation();

  let newGameCode = "";
  const getNewGameCode = () => {
    newGameCode = ($location as NavigatorLocation<AnyObject>).search.replace(
      "?selectedGame=",
      ""
    );
  };

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

  $: if ($location.search) getNewGameCode();
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
    <CampaignParty bind:campaign />
    <CampaignScenarios bind:campaign />
  {/if}
</section>
