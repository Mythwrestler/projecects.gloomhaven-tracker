<script lang="ts">
  import { onMount } from "svelte";
  import Card, { Content } from "@smui/card";
  import List, { Item, Text } from "@smui/list";
  import { Title, Content as PaperContent } from "@smui/paper";

  import GhtPanel from "../../common/Components/GHTPanel/GHTPanel.svelte";

  import useCombatService from "../../Service/CombatService";

  import { useNavigate } from "svelte-navigator";
  import { writable } from "svelte/store";
  import type { CombatSummary } from "../../models/Combat";

  const { actions: combatActionsThin } = useCombatService();
  const { getCombatSummaries } = combatActionsThin;

  const navigate = useNavigate();

  const combatListing = writable<CombatSummary[]>([]);
  const handleCombatSummaries = async () => {
    const listing = await getCombatSummaries();
    combatListing.set(listing);
  };

  onMount(() => {
    void handleCombatSummaries();
  });
</script>

<GhtPanel color="ght-panel">
  <Title aria-label="Current Campaigns" class="text-center text-xl">
    Combats
  </Title>
  <PaperContent>
    <Card>
      <Content>
        <List singleSelection>
          {#each $combatListing as combatSummary}
            <Item
              on:SMUI:action={() => {
                // eslint-disable-next-line @typescript-eslint/restrict-template-expressions
                navigate(`/combats/fight/?activeCombat=${combatSummary.id}`);
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
  </PaperContent>
</GhtPanel>
