<script lang="ts">
  // import { useLocation } from "svelte-navigator";
  import { onDestroy, onMount } from "svelte";

  import { useActiveCombatService } from "../ActiveCombatContext/ActiveCombatService";

  const { actions, state } = useActiveCombatService();
  const { joinCombat, leaveCombat } = actions;
  const {
    combatConnected,
    combatConnecting,
    combatDisconnecting,
    combatId: currentCombatId,
    userList,
  } = state;

  // import { accessToken } from "@ci-lab/svelte-oidc-context";
  // import { useCombatHubService } from "../../../Service/CombatHubService";
  // import useCombatService from "../../../Service/CombatService";

  // const {
  //   State: combatHubState,
  //   connectToHub,
  //   disconnectFromHub,
  //   joinCombat,
  //   leaveCombat,
  // } = useCombatHubService(accessToken);
  // const { combat: combatConnectionState, combatHub: hubConnectionState } =
  //   combatHubState;
  // const { connecting: hubConnecting, connected: hubConnected } =
  //   hubConnectionState;
  // const {
  //   connected: combatConnected,
  //   connecting: combatConnecting,
  //   combatId: connectedCombatId,
  //   connectedUsers: connectedCombatUsers,
  // } = combatConnectionState;
  // const { State: combatState } = useCombatService(accessToken);

  // const location = useLocation();
  // const searchParams = new URLSearchParams($location.search);
  // const combatIdToLoad: string | null = searchParams.get("activeCombat");

  // const handleCombatConnect = async (
  //   hubConnected: boolean,
  //   hubConnecting: boolean,
  //   combatConnected: boolean,
  //   combatConnecting: boolean
  // ) => {
  //   console.log(
  //     JSON.stringify({
  //       hubConnected: hubConnected,
  //       hubConnecting: hubConnecting,
  //       combatConnected: combatConnected,
  //       combatConnecting: combatConnecting,
  //     })
  //   );
  //   if (!hubConnected && !hubConnecting) await connectToHub();
  //   if (
  //     hubConnected &&
  //     !combatConnected &&
  //     !combatConnecting &&
  //     combatIdToLoad !== null
  //   )
  //     await joinCombat(combatIdToLoad);
  // };

  // connectedCombatUsers.subscribe((users) => {
  //   console.log(`User Store was updated: ${JSON.stringify(users)}`);
  // });

  // $: void handleCombatConnect(
  //   $hubConnected,
  //   $hubConnecting,
  //   $combatConnected,
  //   $combatConnecting
  // );

  // onDestroy(async () => {
  //   await leaveCombat();
  //   await disconnectFromHub();
  // });

  export let combatId = "";

  onMount(async () => {
    await actions.joinCombat(combatId);
  });
</script>

<h2>Active Combat</h2>
<div>
  <pre>Combat Connected: {$combatConnected}</pre>
  <pre>Combat Connecting: {$combatConnecting}</pre>
</div>
<div>Hub Connection Status:</div>
<div>Combat Id: {$currentCombatId}</div>
<div>Connected Users</div>
{#each $userList as user}
  <div>username: {user.userName} userId: {user.userId}</div>
{/each}
