<script lang="ts">
  import { Route } from "svelte-navigator";
  import Party from "../../Party/Party.svelte";
  import Campaign from "../../Campaign/Campaign.svelte";
  import SetupCombat from "../../Combat/SetupCombat.svelte";
  import Sampler from "../../Sampler/Sampler.svelte";
</script>

<main class="w-full h-full">
  <Route path="/">Landing</Route>
  <Route path="/campaign"><Campaign /></Route>
  <Route path="/party"><Party /></Route>
  <Route path="/combat"><SetupCombat /></Route>
  <Route path="/sampler"><Sampler /></Route>
</main>
