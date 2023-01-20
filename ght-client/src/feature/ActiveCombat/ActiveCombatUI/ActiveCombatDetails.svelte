<script lang="ts">
  // import { useLocation } from "svelte-navigator";
  import { onDestroy, onMount } from "svelte";
  import GhtPanel from "../../../common/Components/GHTPanel/GHTPanel.svelte";
  import { useNavigate } from "svelte-navigator";

  import { useActiveCombatService } from "../../../Service/ActiveCombatService";
  import Card, { Content, Actions, ActionButtons } from "@smui/card";
  import Button from "@smui/button";

  const { actions, state } = useActiveCombatService();
  const { joinCombat, leaveCombat } = actions;
  const {
    combatConnected,
    combatConnecting,
    combatDisconnecting,
    combatId: currentCombatId,
    participants,
  } = state;

  export let combatId = "";

  const navigate = useNavigate();

  const shortString = (text: string | undefined): string => {
    if (text === undefined) return "";
    if (text.length < 15) return text;
    return `${text.substring(0, 15)}...`;
  };

  const handlbeBackClick = () => {
    navigate(-1);
  };

  onMount(() => {
    void joinCombat(combatId);
  });

  onDestroy(async () => {
    await leaveCombat();
  });
</script>

<GhtPanel color="ght-panel">
  <Card>
    <Content>
      <div class="mdc-typography--headline5 text-center">Hub Connection</div>
      <hr class="my-1" />
    </Content>
  </Card>
  <Card>
    <Content>
      <div class="mdc-typography--headline5 text-center">Users:</div>
      <hr class="my-1" />
      {#if $participants}
        {#each $participants.participants as participant}
          <div>{participant.username}</div>
        {/each}
      {/if}
    </Content>
    <Actions>
      <ActionButtons>
        <Button color="primary" on:click={handlbeBackClick}>Back</Button>
      </ActionButtons>
    </Actions>
  </Card>
  <Card>
    <Content>
      <div class="mdc-typography--headline5 text-center">
        Combat Connection Status
      </div>
      <hr class="my-1" />
      <div>
        <pre>Combat Connected: {$combatConnected}</pre>
        <pre>Combat Connecting: {$combatConnecting}</pre>
        <pre>Combat Disconnecting: {$combatDisconnecting}</pre>
      </div>
      <div>Combat Id: {shortString($currentCombatId)}</div>
    </Content>
    <Actions>
      <ActionButtons>
        <Button color="primary" on:click={handlbeBackClick}>Back</Button>
      </ActionButtons>
    </Actions>
  </Card>
</GhtPanel>
