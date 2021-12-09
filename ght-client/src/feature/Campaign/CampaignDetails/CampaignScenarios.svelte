<script lang="ts">
  /** eslint-disable @typescript-eslint/restrict-template-expressions */
  import {
    CheckMarkIcon,
    CloseIconOpen,
    AddContainedIcon,
    RadioGroup,
    RadioOption,
    DropDown,
    DropDownOption,
    Dialog,
    DialogBody,
    DialogHeader,
    DialogFooter,
    Button,
  } from "../../../common/Components";
  import { Campaign } from "../../../models";
  import { ContentItemSummary, Scenario } from "../../../models/Content";
  import {
    getScenarios,
    scenarioListing,
    scenarioListingLoaded,
    scenarioListingLoading,
  } from "../../../Service/ContentService";

  export let campaign: Campaign | undefined;

  const scenarioStatusOptions: RadioOption[] = [
    { label: "Completed", value: "completed" },
    { label: "Closed", value: "closed" },
    { label: "Available", value: "available" },
  ];

  const handleGetScenarios = async (gameCode: string) => {
    if (
      !$scenarioListingLoaded &&
      !$scenarioListingLoading &&
      ($scenarioListing as Scenario[]).length === 0
    ) {
      await getScenarios(gameCode);
    }
  };

  let scenarios: Scenario[] = [];
  let unusedScenarioOptions: DropDownOption[] = [];
  let fullScenarioListOptions: DropDownOption[] = [];
  const handleProcessScenarios = () => {
    if (campaign) {
      scenarios = ($scenarioListing as ContentItemSummary[]).filter(
        (scenario) =>
          campaign?.availableScenarios.includes(scenario.contentCode)
      );
      fullScenarioListOptions = scenarios.map((scenario) => {
        return { label: scenario.name, value: scenario.contentCode };
      });
      unusedScenarioOptions = fullScenarioListOptions.filter(
        (scenario) =>
          !campaign?.availableScenarios.includes(scenario.value as string)
      );
    }
  };

  let displayNewScenarioSelection = false;
  let selectedScenario = "";
  let selectedScenarioStatus = "";
  const handleAddNewScenarioClick = () => {
    selectedScenario = "";
    selectedScenarioStatus = "";
    displayNewScenarioSelection = true;
  };
  const handleScenarioClick = (contentCode: string) => {
    selectedScenario = contentCode;
    if (campaign?.completedScenarios.includes(contentCode)) {
      selectedScenarioStatus = "completed";
    } else if (campaign?.closedScenarios.includes(contentCode)) {
      selectedScenarioStatus = "closed";
    } else if (campaign?.availableScenarios.includes(contentCode)) {
      selectedScenarioStatus = "available";
    } else {
      selectedScenarioStatus = "";
    }
    displayNewScenarioSelection = true;
  };
  const handleCloseNewScenarioSelection = () => {
    selectedScenario = "";
    selectedScenarioStatus = "";
    displayNewScenarioSelection = false;
  };

  const scenarioCompleted = (scenario: Scenario): string => {
    return `${scenario.name} Completed`;
  };
  const scenarioClosed = (scenario: Scenario): string => {
    return `${scenario.name} Closed`;
  };

  $: if (campaign?.game) void handleGetScenarios(campaign.game);
  $: if ($scenarioListingLoaded || campaign) handleProcessScenarios();
</script>

{#if campaign}
  <div
    class="relative mt-2 px-3 py-1 items-center max-w-md mx-auto bg-gray-50 rounded-md backdrop-blur-sm"
  >
    <div aria-label="Available Scenarios" class="text-center text-xl">
      Available Scenarios
    </div>
    <div class="border-b-2 border-solid" />
    {#if $scenarioListingLoaded}
      <div class="absolute top-1 right-1">
        <button
          aria-label="Add New Scenario"
          on:click={handleAddNewScenarioClick}><AddContainedIcon /></button
        >
      </div>
      <div>
        <ul aria-label="Scenario Listing">
          {#each scenarios as scenario}
            <li aria-label={scenario.name}>
              <div class="flex flex-row">
                <div class="mx-auto flex flex-row">
                  <div>
                    <button
                      on:click={() => handleScenarioClick(scenario.contentCode)}
                    >
                      {scenario.name}
                    </button>
                  </div>
                  {#if campaign.completedScenarios.includes(scenario.contentCode)}
                    <div class="pl-2" aria-label={scenarioCompleted(scenario)}>
                      <CheckMarkIcon iconClassOverride="h-4 w-4 mt-1" />
                    </div>
                  {/if}
                  {#if campaign.closedScenarios.includes(scenario.contentCode)}
                    <div class="pl-2" aria-label={scenarioClosed(scenario)}>
                      <CloseIconOpen iconClassOverride="h-4 w-4 mt-1" />
                    </div>
                  {/if}
                </div>
              </div>
            </li>
          {/each}
        </ul>
      </div>
    {:else}
      <div class="mx-auto">Loading...</div>
    {/if}
  </div>
{/if}
{#if displayNewScenarioSelection}
  <Dialog
    offClick
    open={displayNewScenarioSelection}
    onClose={handleCloseNewScenarioSelection}
  >
    <DialogHeader slot="DialogHeader">Select a scenario to add</DialogHeader>
    <DialogBody slot="DialogBody">
      <DropDown
        label="Scenarios"
        selected={selectedScenario}
        placeHolder={selectedScenario === "" ? "Select a Scenario" : ""}
        options={selectedScenario === ""
          ? unusedScenarioOptions
          : fullScenarioListOptions}
        variant="rounded"
        disabled={selectedScenario !== ""}
      />
      <RadioGroup
        centered
        value={selectedScenarioStatus}
        options={scenarioStatusOptions}
      />
    </DialogBody>
    <DialogFooter slot="DialogFooter">
      <Button
        variant="filled"
        onClick={selectedScenarioStatus !== ""
          ? handleCloseNewScenarioSelection
          : undefined}
      >
        Add Scenario
      </Button>
    </DialogFooter>
  </Dialog>
{/if}
