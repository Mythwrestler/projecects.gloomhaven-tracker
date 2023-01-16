<script lang="ts">
  // import { useLocation } from "svelte-navigator";
  import { onDestroy, onMount } from "svelte";

  import { type CombatRequest } from "../../../models/Combat";

  import { useActiveCombatService } from "../../../Service/ActiveCombatService";

  const { actions, state } = useActiveCombatService();
  const { joinCombat, leaveCombat } = actions;
  const {
    combatConnected,
    combatConnecting,
    combatDisconnecting,
    combatId: currentCombatId,
    userList,
  } = state;

  export let combatId = "";

  onMount(async () => {
    await joinCombat({
      combatId,
      combatantId: "",
      isObserver: false,
    });
  });

  onDestroy(async () => {
    await leaveCombat();
  });
</script>

<h2>Active Combat</h2>
<div>
  <pre>Combat Connected: {$combatConnected}</pre>
  <pre>Combat Connecting: {$combatConnecting}</pre>
  <pre>Combat Disconnecting: {$combatDisconnecting}</pre>
</div>
<div>Hub Connection Status:</div>
<div>Combat Id: {$currentCombatId}</div>
<div>Connected Users</div>
{#each $userList as user}
  <div>username: {user.userName} userId: {user.userId}</div>
{/each}
