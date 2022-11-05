<script lang="ts">
  import { onMount } from "svelte";
  import Card, { Content } from "@smui/card";
  import List, { Item, Text } from "@smui/list";

  import GhtPanel from "../../common/Components/GHTPanel/GHTPanel.svelte";

  import useCombatService from "../../Service/CombatService";

  import { useNavigate } from "svelte-navigator";

  const { actions: combatActions, state: combatState } = useCombatService();
  const { getCombatSummaries } = combatActions;
  const { combatSummaries } = combatState;

  const navigate = useNavigate();

  onMount(() => {
    void getCombatSummaries();
  });
</script>

<GhtPanel color="ght-panel">
  <Card>
    <Content>
      <List singleSelection>
        {#each $combatSummaries as combatSummary}
          <Item
            on:SMUI:action={() => {
              // eslint-disable-next-line @typescript-eslint/restrict-template-expressions
              navigate(`/combats/${combatSummary.id}`);
            }}
          >
            <Text>
              {combatSummary.description}
            </Text>
          </Item>
        {/each}
      </List>
    </Content>
  </Card>
</GhtPanel>
