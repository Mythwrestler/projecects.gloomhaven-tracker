<script lang="ts">
  import { TextField } from "../../../common/Components";
  import { Campaign } from "../../../models";
  import * as GlobalError from "../../../Service/Error";

  import { useCampaignService } from "../../../Service/CampaignService";
  import CampaignParty from "./CampaignParty.svelte";
  import { NavigatorLocation, useLocation } from "svelte-navigator";
  import AnyObject from "svelte-navigator/types/AnyObject";
  import CampaignScenarios from "./CampaignScenarios.svelte";

  // your script goes here
  export let campaignId = "";
  const location = useLocation();
  const { State: campaignState, getCampaign } = useCampaignService();

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
    try {
      await getCampaign(campaignId);
    } catch {
      GlobalError.showErrorMessage("Failed to get campaign");
    }
  };

  $: if ($location.search) getNewGameCode();
  $: if (!campaign) void handleGetCampaign(campaignId);
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
