<script lang="ts">
  import { Route } from "svelte-navigator";
  import Campaigns from "../../CampaignListing/CampaignListing/Campaigns.svelte";
  import Sampler from "../../Sampler/Sampler.svelte";
  import CampaignDetails from "../../CampaignListing/CampaignDetails/CampaignDetails.svelte";
  import CombatListing from "../../CombatListing/CombatListing.svelte";
</script>

<main class="w-full h-full">
  <Route path="/">Landing</Route>
  <Route path="/campaigns"><Campaigns /></Route>
  <Route path="/campaigns/:id" let:params>
    <CampaignDetails campaignId={params.id} />
  </Route>
  <Route path="/combats"><CombatListing /></Route>
  <Route path="/sampler"><Sampler /></Route>
</main>
