<script lang="ts">
    import { Route } from "svelte-navigator";
    import Campaign from "../../Campaign/Campaign.svelte";
</script>

<main class="w-full">
    <Route path="/"><Campaign /></Route>
    <Route path="/campaign">Campaign</Route>
    <Route path="/party">Party</Route>
    <Route path="/mission">Mission</Route>
</main>
