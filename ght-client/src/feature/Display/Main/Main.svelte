<script lang="ts">
  import { Route } from "svelte-navigator";
  import Campaigns from "../../Campaigns/CampaignListing/Campaigns.svelte";
  import Sampler from "../../Sampler/Sampler.svelte";
  import CampaignDetails from "../../Campaigns/CampaignDetails/CampaignDetails.svelte";
  import Scenarios from "../../Scenarios/Scenarios.svelte";
</script>

<main class="w-full h-full">
  <Route path="/">Landing</Route>
  <Route path="/campaigns"><Campaigns /></Route>
  <Route path="/campaigns/:id" let:params>
    <CampaignDetails campaignId={params.id} />
  </Route>
  <Route path="/scenarios"><Scenarios /></Route>
  <Route path="/sampler"><Sampler /></Route>
</main>
