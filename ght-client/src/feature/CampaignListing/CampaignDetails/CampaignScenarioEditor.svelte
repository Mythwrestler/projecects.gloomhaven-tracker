<script lang="ts">
  import Dialog, {
    Header as DialogHeader,
    Title as DialogTitle,
    Content as DialogContent,
    Actions as DialogActions,
  } from "@smui/dialog";
  import Select, { Option } from "@smui/select";
  import Radio from "@smui/radio";
  import FormField from "@smui/form-field";
  import Button from "@smui/button";
  import GhtPanel from "../../../common/Components/GHTPanel/GHTPanel.svelte";
  import Card, { Content as CardContent } from "@smui/card";
  import List, { Item, Text, PrimaryText, SecondaryText } from "@smui/list";

  import { writable, type Writable } from "svelte/store";

  import type { Scenario as CampaignScenario } from "../../../models/Campaign";
  import type {
    Scenario as ContentScenario,
    ScenarioSummary,
  } from "../../../models/Content";

  import useContentService from "../../../Service/ContentService";
  import useCampaignService from "../../../Service/CampaignService";
  import { lowerCase } from "lodash";
  const { actions: campaignActions } = useCampaignService();
  const { addScenario, updateScenario } = campaignActions;
  const { state: contentState, actions: contentActions } = useContentService();
  const { getScenarioDefault, getScenarioSummaries } = contentActions;
  const { scenarioDefault, scenarioSummaries } = contentState;

  export let open = false;
  export let gameCode: string;
  export let campaignId: string;
  export let campaignScenarios: CampaignScenario[];
  export let scenario: CampaignScenario | undefined;

  const handleGetGetGameScenarios = (
    gameCode: string,
    scenarioSummaries: ScenarioSummary[]
  ) => {
    if (scenarioSummaries.length === 0 && gameCode !== "")
      getScenarioSummaries(gameCode);
  };

  const scenarioSelectKey = (contentCode: string | undefined) =>
    `${contentCode ?? ""}`;

  let scenarioContentCode = "";
  let scenarioStatus: "closed" | "completed" | "available";
  let isNewScenario = false;

  const handleOpen = (scenario: CampaignScenario | undefined): void => {
    if (scenario === undefined) {
      scenarioContentCode = "";
      scenarioStatus = "available";
      isNewScenario = true;
    } else {
      scenarioContentCode = scenario.scenarioContentCode;
      scenarioStatus = calcScenarioStatus(scenario);
      isNewScenario = false;
    }
  };

  const calcScenarioStatus = (
    scenario: CampaignScenario
  ): "completed" | "closed" | "available" => {
    if (scenario.isClosed) return "closed";
    else if (scenario.isCompleted) return "completed";
    else return "available";
  };

  const handleGetScenarioDefault = (
    contentCode: string,
    scenarioDefault: ContentScenario | undefined
  ) => {
    if (contentCode !== "" && contentCode !== scenarioDefault?.contentCode) {
      getScenarioDefault(gameCode, contentCode);
    }
  };

  let disableSave = true;
  const calcSaveDisable = (
    contentCode: string,
    scenarioDefault: ContentScenario | undefined
  ): void => {
    if (scenarioDefault?.contentCode !== contentCode) {
      disableSave = true;
    } else {
      disableSave = false;
    }
  };

  const handleSaveScenario = (): void => {
    if (
      $scenarioDefault === undefined ||
      $scenarioDefault.contentCode !== scenarioContentCode
    )
      return;
    else {
      const editedScenario: CampaignScenario = {
        name: $scenarioDefault.name,
        scenarioContentCode,
        description: $scenarioDefault.description,
        isClosed: scenarioStatus === "closed",
        isCompleted: scenarioStatus === "completed",
        scenarioNumber: $scenarioDefault.sortOrder,
      };

      if (isNewScenario) void addScenario(campaignId, editedScenario);
      else void updateScenario(campaignId, editedScenario);
    }
  };

  const selectableScenarios: Writable<ScenarioSummary[]> = writable<
    ScenarioSummary[]
  >([]);
  const handleProcessScenarios = (
    isNewScenario: boolean,
    campaignScenarios: CampaignScenario[],
    scenarioSummaries: ScenarioSummary[]
  ) => {
    const usedScenarioContentCodes = campaignScenarios.map(
      (cs) => cs.scenarioContentCode
    );
    selectableScenarios.set(
      isNewScenario
        ? scenarioSummaries.filter(
            (ss) => !usedScenarioContentCodes.includes(ss.contentCode)
          )
        : scenarioSummaries.filter(
            (ss) => ss.contentCode == scenarioContentCode
          )
    );
  };

  $: open && handleOpen(scenario);
  $: open && handleGetScenarioDefault(scenarioContentCode, $scenarioDefault);
  $: open && handleGetGetGameScenarios(gameCode, $scenarioSummaries);
  $: open &&
    handleProcessScenarios(
      isNewScenario,
      campaignScenarios,
      $scenarioSummaries
    );
  $: open && calcSaveDisable(scenarioContentCode, $scenarioDefault);
</script>

<Dialog bind:open fullscreen surface$class="min-h-1/2">
  <DialogHeader>
    <DialogTitle>{`${!isNewScenario ? "Edit" : "Add"} Scenario`}</DialogTitle>
  </DialogHeader>
  <DialogContent>
    <GhtPanel color="ght-panel">
      {#if open && $selectableScenarios.length > 0}
        <div class="mt-2">
          <Card>
            <CardContent>
              <div class="flex flex-col">
                <Select
                  key={scenarioSelectKey}
                  bind:value={scenarioContentCode}
                  label="Scenario"
                  disabled={!isNewScenario}
                  menu$fixed
                  menu$class="sm:max-w-lg max-w-xs"
                >
                  {#if isNewScenario}
                    <Option value={""} />
                  {/if}
                  {#each $selectableScenarios as scenario}
                    <Option value={scenario.contentCode}>{scenario.name}</Option
                    >
                  {/each}
                </Select>
                <div class="flex sm:flex-row flex-col mt-5">
                  {#each ["Completed", "Closed", "Available"] as option}
                    <FormField class="sm:mx-auto ml-3">
                      <Radio
                        bind:group={scenarioStatus}
                        value={lowerCase(option)}
                      />
                      <span slot="label" class="mr-3">{option}</span>
                    </FormField>
                  {/each}
                </div>
              </div>
            </CardContent>
          </Card>
        </div>
      {/if}
      {#if $scenarioDefault?.contentCode === scenarioContentCode}
        <div class="mt-2">
          <Card>
            <CardContent>
              <div class="mdc-typography--headline5 text-center">Mosters</div>
              <hr class="my-1" />
              <List nonInteractive>
                {#each $scenarioDefault?.monsters ?? [] as monster}
                  <Item>
                    <Text>
                      {monster.name}
                    </Text>
                  </Item>
                {/each}
              </List>
            </CardContent>
          </Card>
        </div>
        {#if ($scenarioDefault?.objectives ?? []).length > 0}
          <div class="mt-2">
            <Card>
              <CardContent>
                <div class="mdc-typography--headline5 text-center">
                  Objectives
                </div>
                <hr class="my-1" />
                <List nonInteractive>
                  {#each $scenarioDefault?.objectives ?? [] as objective}
                    <Item>
                      <Text>
                        {objective.name}
                      </Text>
                    </Item>
                  {/each}
                </List>
              </CardContent>
            </Card>
          </div>
        {/if}
        <div class="mt-2">
          <Card>
            <CardContent>
              <div class="mdc-typography--headline5 text-center">
                Information
              </div>
              <hr class="my-1" />
              <List nonInteractive twoLine>
                <Item>
                  <Text>
                    <PrimaryText>{$scenarioDefault?.goal ?? ""}</PrimaryText>
                    <SecondaryText>Goal</SecondaryText>
                  </Text>
                </Item>
                <Item>
                  <Text>
                    <PrimaryText
                      >{$scenarioDefault?.cityMapLocation ?? ""}</PrimaryText
                    >
                    <SecondaryText>City Map Location</SecondaryText>
                  </Text>
                </Item>
                <Item>
                  <Text>
                    <PrimaryText
                      >{($scenarioDefault?.scenarioBook ?? []).join(
                        ", "
                      )}</PrimaryText
                    >
                    <SecondaryText>Scenario Book Pages</SecondaryText>
                  </Text>
                </Item>
                {#if ($scenarioDefault?.supplementalBook ?? []).length > 0}
                  <Item>
                    <Text>
                      <PrimaryText
                        >{($scenarioDefault?.supplementalBook ?? []).join(
                          ", "
                        )}</PrimaryText
                      >
                      <SecondaryText>Supplemental Book Pages</SecondaryText>
                    </Text>
                  </Item>
                {/if}
              </List>
            </CardContent>
          </Card>
        </div>
      {/if}
    </GhtPanel>
  </DialogContent>
  <DialogActions>
    <Button
      color="secondary"
      on:click={() => {
        open = false;
      }}>Cancel</Button
    >
    <Button
      color="primary"
      disabled={disableSave}
      on:click={() => {
        handleSaveScenario();
        open = false;
      }}
    >
      {isNewScenario ? "Add" : "Update"}
    </Button>
  </DialogActions>
</Dialog>
