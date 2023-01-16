<script lang="ts">
  import { writable } from "svelte/store";
  import type { Combat } from "../../models/Combat";
  import { useNavigate } from "svelte-navigator";
  import useCombatService from "../../Service/CombatService";
  import GhtPanel from "../../common/Components/GHTPanel/GHTPanel.svelte";
  import Card, { Content, Actions, ActionButtons } from "@smui/card";
  import Button from "@smui/button";

  const { actions: combatActionsThin } = useCombatService();
  const { getCombatDetail } = combatActionsThin;

  const navigate = useNavigate();

  export let combatId = "";

  const combat = writable<Combat | undefined>(undefined);
  const handleGetCombat = async (combatId: string) => {
    const combatForLoad = await getCombatDetail(combatId);
    combat.set(combatForLoad);
  };

  const handleStartCombat = (combatId: string) => {
    navigate(`/combats/fight/?activeCombat=${combatId}`);
  };

  $: void handleGetCombat(combatId);
</script>

<GhtPanel color="ght-panel">
  {#if !$combat}
    <div>Loading Combat</div>
  {:else}
    <Card>
      <Content>
        <div>Combat Description: {$combat?.description}</div>
      </Content>
      <Actions>
        <ActionButtons>
          <Button
            color="primary"
            variant="raised"
            on:click={() => {
              handleStartCombat($combat?.id ?? "");
            }}
          >
            Fight!
          </Button>
        </ActionButtons>
      </Actions>
    </Card>
  {/if}
</GhtPanel>
