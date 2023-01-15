<script lang="ts">
  import { writable } from "svelte/store";

  import Card, {
    Content as CardContent,
    Actions as CardActions,
    ActionIcons as CardActionIcons,
  } from "@smui/card";
  import List, { Item, Text, Graphic } from "@smui/list";
  import IconButton from "@smui/icon-button";

  import type {
    Campaign,
    Scenario as CampaignScenario,
  } from "../../../models/Campaign";
  import type { ScenarioSummary } from "../../../models/Content";

  import useContentService from "../../../Service/ContentService";

  import CampaignScenarioEditor from "./CampaignScenarioEditor.svelte";

  export let campaign: Campaign | undefined;

  const { actions: contentActions } = useContentService();
  const { getScenarioSummaries } = contentActions;

  const scenarioSummaries = writable<ScenarioSummary[]>([]);
  const handleGetScenarioSummaries = async (gameCode: string) => {
    try {
      const summaries = await getScenarioSummaries(gameCode);
      scenarioSummaries.set(summaries);
    } catch {
      scenarioSummaries.set([]);
    }
  };

  let campaignScenarios: CampaignScenario[] = [];

  const scenarioSummaryProcessed = writable<boolean>(false);
  const handleProcessScenarioSummaries = () => {
    if (campaign) {
      campaignScenarios = (campaign.scenarios ?? [])
        .map((cs) => {
          let scenarioForLoad = $scenarioSummaries.find(
            (ss) => ss.contentCode === cs.scenarioContentCode
          ) as ScenarioSummary;
          return {
            scenarioContentCode: scenarioForLoad.contentCode,
            description: scenarioForLoad.description,
            scenarioNumber: scenarioForLoad.sortOrder,
            name: scenarioForLoad.name,
            isClosed: cs.isClosed,
            isCompleted: cs.isCompleted,
          };
        })
        .filter((cs) => cs !== undefined)
        .sort((a: CampaignScenario, b: CampaignScenario) =>
          a.scenarioNumber > b.scenarioNumber ? 1 : -1
        );
      scenarioSummaryProcessed.set(true);
    }
  };

  let scenarioToEdit: CampaignScenario | undefined;
  let isNewScenario = false;
  let displayEditScenario = false;

  const getIconForScenarioState = (
    scenario: CampaignScenario
  ): string | undefined => {
    if (scenario.isCompleted) return "done_outline";
    if (scenario.isClosed) return "close";
    return undefined;
  };

  $: if (campaign?.game) void handleGetScenarioSummaries(campaign.game);
  $: if ($scenarioSummaries.length !== 0 && campaign?.scenarios)
    handleProcessScenarioSummaries();
</script>

{#if campaign && $scenarioSummaries.length > 0}
  <Card>
    <CardContent>
      <div class="mdc-typography--headline5 text-center">Scenarios</div>
      <hr class="my-1" />
      <List singleSelection>
        {#each campaignScenarios as scenario}
          <Item
            on:SMUI:action={() => {
              scenarioToEdit = scenario;
              isNewScenario = false;
              displayEditScenario = true;
            }}
          >
            <Graphic class="material-icons">
              {getIconForScenarioState(scenario)}
            </Graphic>
            <Text>
              {scenario.name}
            </Text>
          </Item>
        {/each}
      </List>
    </CardContent>
    <CardActions>
      {#if $scenarioSummaryProcessed}
        <CardActionIcons>
          <IconButton
            class="material-icons"
            aria-label="Add Scenario"
            title="Add Scenario"
            on:click={() => {
              scenarioToEdit = undefined;
              isNewScenario = true;
              displayEditScenario = true;
            }}
          >
            add_circle
          </IconButton>
        </CardActionIcons>
      {/if}
    </CardActions>
  </Card>

  <CampaignScenarioEditor
    bind:open={displayEditScenario}
    gameCode={campaign.game}
    campaignId={campaign.id}
    campaignScenarios={campaign.scenarios}
    scenario={scenarioToEdit}
  />
{/if}
