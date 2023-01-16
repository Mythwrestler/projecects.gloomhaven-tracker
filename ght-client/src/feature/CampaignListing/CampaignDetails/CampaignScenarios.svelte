<script lang="ts">
  import { writable } from "svelte/store";

  import Card, {
    Content as CardContent,
    Actions as CardActions,
    ActionIcons as CardActionIcons,
  } from "@smui/card";
  import List, { Item, Text, Graphic } from "@smui/list";
  import IconButton from "@smui/icon-button";

  import type { Scenario as CampaignScenario } from "../../../models/Campaign";
  import type { ScenarioSummary } from "../../../models/Content";

  import useContentService from "../../../Service/ContentService";
  import useCampaignService from "../../../Service/CampaignService";

  import CampaignScenarioEditor from "./CampaignScenarioEditor.svelte";
  import { deepClone } from "fast-json-patch";

  export let campaignId: string;
  export let gameCode: string;
  export let campaignScenarios: CampaignScenario[];

  const { actions: contentActions } = useContentService();
  const { getScenarioSummaries } = contentActions;

  const { actions: campaignActions } = useCampaignService();
  const { addScenario, updateScenario } = campaignActions;

  const scenarioSummaries = writable<ScenarioSummary[]>([]);
  const handleGetScenarioSummaries = async (gameCode: string) => {
    try {
      const summaries = await getScenarioSummaries(gameCode);
      scenarioSummaries.set(summaries);
    } catch {
      scenarioSummaries.set([]);
    }
  };

  const buildCampaignScenario = (
    fromCampaign: CampaignScenario,
    fromSummary: ScenarioSummary
  ) => {
    return {
      scenarioContentCode: fromSummary.contentCode,
      description: fromSummary.description,
      scenarioNumber: fromSummary.sortOrder,
      name: fromSummary.name,
      isClosed: fromCampaign.isClosed,
      isCompleted: fromCampaign.isCompleted,
    };
  };

  const scenarioListing = writable<CampaignScenario[]>([]);
  const scenarioSummaryProcessed = writable<boolean>(false);
  const handleProcessScenarioSummaries = (
    scenarioSummaries: ScenarioSummary[],
    campaignScenarios: CampaignScenario[]
  ) => {
    const listing = campaignScenarios
      .map((cs) => {
        let scenarioSummary = scenarioSummaries.find(
          (ss) => ss.contentCode === cs.scenarioContentCode
        ) as ScenarioSummary;
        return buildCampaignScenario(cs, scenarioSummary);
      })
      .filter((cs) => cs !== undefined)
      .sort((a: CampaignScenario, b: CampaignScenario) =>
        a.scenarioNumber > b.scenarioNumber ? 1 : -1
      );
    scenarioListing.set(listing);
    scenarioSummaryProcessed.set(true);
  };

  const handleAddScenario = async (scenarioToAdd: CampaignScenario) => {
    const scenario = await addScenario(campaignId, scenarioToAdd);
    if (scenario) updateScenarioListing(scenario);
  };

  const handleUpdateScenario = async (scenarioToUpdate: CampaignScenario) => {
    const scenario = await updateScenario(campaignId, scenarioToUpdate);
    updateScenarioListing(scenario);
  };

  const updateScenarioListing = (scenario: CampaignScenario) => {
    let scenarioSummary = $scenarioSummaries.find(
      (ss) => ss.contentCode === scenario.scenarioContentCode
    ) as ScenarioSummary;
    const scenarioForUpdate = buildCampaignScenario(scenario, scenarioSummary);
    scenarioListing.update((currentListing) => {
      const listing = deepClone(currentListing) as CampaignScenario[];
      const location = listing.findIndex(
        (scn) =>
          scn.scenarioContentCode === scenarioForUpdate.scenarioContentCode
      );
      if (location === -1) listing.push(scenarioForUpdate);
      else listing.splice(location, 1, scenarioForUpdate);
      return listing
        .filter((cs) => cs !== undefined)
        .sort((a: CampaignScenario, b: CampaignScenario) =>
          a.scenarioNumber > b.scenarioNumber ? 1 : -1
        );
    });
  };

  let scenarioToEdit: CampaignScenario | undefined;
  let displayEditScenario = false;

  const getIconForScenarioState = (
    scenario: CampaignScenario
  ): string | undefined => {
    if (scenario.isCompleted) return "done_outline";
    if (scenario.isClosed) return "close";
    return undefined;
  };

  $: void handleGetScenarioSummaries(gameCode);
  $: if ($scenarioSummaries.length !== 0)
    handleProcessScenarioSummaries($scenarioSummaries, campaignScenarios);
</script>

<Card>
  <CardContent>
    <div class="mdc-typography--headline5 text-center">Scenarios</div>
    <hr class="my-1" />
    {#if !$scenarioSummaryProcessed}
      <div>...Loading</div>
    {:else}
      <List singleSelection>
        {#each $scenarioListing as scenario}
          <Item
            on:SMUI:action={() => {
              scenarioToEdit = scenario;
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
    {/if}
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
  {gameCode}
  campaignScenarios={$scenarioListing}
  scenario={scenarioToEdit}
  addScenario={handleAddScenario}
  updateScenario={handleUpdateScenario}
/>
