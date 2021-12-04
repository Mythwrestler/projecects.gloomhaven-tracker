<script lang="ts">
  import { Route } from "svelte-navigator";
  import Party from "../../Party/Party.svelte";
  import Campaigns from "../../Campaign/CampaignListing/Campaigns.svelte";
  import SetupCombat from "../../Combat/SetupCombat.svelte";
  import Sampler from "../../Sampler/Sampler.svelte";
  import CampaignDetails from "../../Campaign/CampaignDetails/CampaignDetails.svelte";
</script>

<main class="w-full h-full">
  <Route path="/">Landing</Route>
  <Route path="/campaign"><Campaigns /></Route>
  <Route path="/campaign/:id" let:params>
    <CampaignDetails campaignId={params.id} />
  </Route>
  <Route path="/party"><Party /></Route>
  <Route path="/combat"><SetupCombat /></Route>
  <Route path="/sampler"><Sampler /></Route>
</main>
