<script lang="ts">
    import Center from "./Center/Center.svelte";
    import { Router } from "svelte-navigator";
    import Navigation from "./Navigation/Navigation.svelte";
</script>

<div class="h-screen w-screen flex flex-row bg-gray-200">
    <Router primary={false}>
        <Navigation />
        <Center />
    </Router>
</div>
