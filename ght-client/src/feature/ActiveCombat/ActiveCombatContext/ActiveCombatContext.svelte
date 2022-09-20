<script lang="ts">
  import { accessToken } from "@ci-lab/svelte-oidc-context";
  import {
    SignalRContext,
    type SignalRHubRequest,
  } from "@ci-lab/svelte-signalr-context";
  import { writable } from "svelte/store";
  import ENV_VARS from "../../../common/Environment";
  import ActiveCombatDetails from "../ActiveCombatUI/ActiveCombatDetails.svelte";
  import {
    defineActiveCombatActions,
    defineActiveCombatListeners,
  } from "./ActiveCombatService";

  export let combatId: string;

  const hubConnected = writable<boolean>(false);

  const listenerService = defineActiveCombatListeners();
  const actionsService = defineActiveCombatActions();

  const handleHubConnected = (singalRHubRequest: SignalRHubRequest) => {
    hubConnected.set(true);
    if (singalRHubRequest) {
      actionsService.setSendMessage(singalRHubRequest);
    }
    console.log(JSON.stringify(singalRHubRequest));
  };

  const handleHubClose = () => {
    void actionsService.actions.leaveCombat();
  };
</script>

{#if $accessToken && listenerService}
  <SignalRContext
    url={`${ENV_VARS.API.BaseURL()}hub/combats`}
    bearerToken={$accessToken}
    actionKey={ENV_VARS.CONTEXT.ActiveCombatHubService.HubActions}
    stateKey={ENV_VARS.CONTEXT.ActiveCombatHubService.HubState}
    onConnected={handleHubConnected}
    beforeDestroy={handleHubClose}
    listeners={listenerService.listeners}
  >
    {#if $hubConnected && actionsService}
      <ActiveCombatDetails {combatId} />
    {/if}
  </SignalRContext>
{/if}
