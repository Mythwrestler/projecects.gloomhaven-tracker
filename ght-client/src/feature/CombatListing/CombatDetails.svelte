<script lang="ts">
  import { onDestroy, onMount } from "svelte";
  import { writable } from "svelte/store";
  import { accessToken } from "../../common/Utils/OidcSvelteClient";
  import { useCombatService } from "../../Service/CombatService";
  import { useCombatHubService } from "../../Service/CombatHubService";
  import * as GlobalError from "../../Service/Error";
  import type { Combat } from "../../models/Combat";
  import { Button } from "../../common/Components";
  import { useNavigate } from "svelte-navigator";

  const {
    getCombat,
    clearCombat,
    State: combatState,
  } = useCombatService(accessToken);
  const { State: hubState } = useCombatHubService(accessToken);

  const navigate = useNavigate();

  export let combatId = "";

  let combat: Combat | undefined;
  combatState.combat.subscribe((combatFromState) => {
    combat = combatFromState;
  });

  const requestCombat = writable<boolean>(false);
  const handleGetCombat = async (combatId: string) => {
    if ($accessToken == undefined) return;
    try {
      await getCombat(combatId);
    } catch {
      GlobalError.showErrorMessage("Failed to get campaign");
    }
  };
  requestCombat.subscribe((requesting) => {
    if (requesting && $accessToken) {
      void handleGetCombat(combatId);
      requestCombat.set(false);
    }
  });
  accessToken.subscribe((accessToken) => {
    if ($requestCombat && accessToken) {
      void handleGetCombat(combatId);
      requestCombat.set(false);
    }
  });

  const handleStartCombat = (combatId: string) => {
    navigate(`/combats/fight/?activeCombat=${combatId}`);
  };

  $: if (combatId !== combat?.id) requestCombat.set(true);

  onMount(() => {
    clearCombat();
    requestCombat.set(true);
  });

  onDestroy(() => {
    clearCombat();
  });
</script>

{#if !combat}
  <div>Loading Combat</div>
{:else}
  <div>Combat Description: {combat?.description}</div>
  <Button
    variant="filled"
    color="red"
    onClick={() => {
      handleStartCombat(combat?.id ?? "");
    }}>Fight!</Button
  >
{/if}
