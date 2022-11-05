<script lang="ts">
  import { onDestroy, onMount } from "svelte";
  import { writable, type Unsubscriber } from "svelte/store";
  import { accessToken } from "@ci-lab/svelte-oidc-context";
  import * as GlobalError from "../../Service/Error";
  import type { Combat } from "../../models/Combat";
  // import { Button } from "../../common/Components";
  import { useNavigate } from "svelte-navigator";
  import useCombatService from "../../Service/CombatService";
  import GhtPanel from "../../common/Components/GHTPanel/GHTPanel.svelte";
  import Card, { Content, Actions, ActionButtons } from "@smui/card";
  import Button from "@smui/button";

  const { actions: combatActions, state: combatState } = useCombatService();
  const { getCombatDetail, clearCombatDetail } = combatActions;
  const { combatDetail } = combatState;

  const navigate = useNavigate();

  export let combatId = "";

  let combat: Combat | undefined;
  combatDetail.subscribe((combatFromState) => {
    combat = combatFromState;
  });

  const requestCombat = writable<boolean>(false);
  const handleGetCombat = async (combatId: string) => {
    if ($accessToken == undefined) return;
    try {
      await getCombatDetail(combatId);
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

  let combatDetailUnsubscribe: Unsubscriber;
  onMount(() => {
    combatDetailUnsubscribe = combatDetail.subscribe((combatFromState) => {
      combat = combatFromState;
    });

    clearCombatDetail();
    requestCombat.set(true);
  });

  onDestroy(() => {
    combatDetailUnsubscribe();
    clearCombatDetail();
  });
</script>

<GhtPanel color="ght-panel">
  {#if !combat}
    <div>Loading Combat</div>
  {:else}
    <Card>
      <Content>
        <div>Combat Description: {combat?.description}</div>
      </Content>
      <Actions>
        <ActionButtons>
          <Button
            color="primary"
            variant="raised"
            on:click={() => {
              handleStartCombat(combat?.id ?? "");
            }}
          >
            Fight!
          </Button>
        </ActionButtons>
      </Actions>
    </Card>
  {/if}
</GhtPanel>
