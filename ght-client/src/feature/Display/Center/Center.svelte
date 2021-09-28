<script lang="ts">
    import { Route } from "svelte-navigator";
    import CombatSpace from "../../Combat/CombatSpace.svelte";
    import Party from "../../Party/Party.svelte";
    import Campaign from "../../Campaign/Campaign.svelte";
</script>

<main class="w-full">
    <Route path="/">Landing</Route>
    <Route path="/campaign"><Campaign /></Route>
    <Route path="/party"><Party /></Route>
    <Route path="/combat"><CombatSpace /></Route>
</main>
