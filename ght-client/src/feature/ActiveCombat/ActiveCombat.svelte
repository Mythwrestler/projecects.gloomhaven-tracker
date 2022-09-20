<script lang="ts">
  import { onMount } from "svelte";
  import { useLocation } from "svelte-navigator";
  import ActiveCombatContext from "./ActiveCombatContext/ActiveCombatContext.svelte";

  const location = useLocation();
  const searchParams = new URLSearchParams($location.search);
  let combatIdToLoad: string | null = null;

  onMount(() => {
    try {
      combatIdToLoad = searchParams.get("activeCombat");
      console.log(combatIdToLoad);
    } catch (err: unknown) {
      console.log(JSON.stringify(err));
    }
  });
</script>

{#if combatIdToLoad !== null}
  <ActiveCombatContext combatId={combatIdToLoad} />
{:else}
  <span>Capturing Combat Id</span>
{/if}
