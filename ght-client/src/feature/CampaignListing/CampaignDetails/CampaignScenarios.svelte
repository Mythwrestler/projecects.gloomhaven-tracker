<script lang="ts">
  import { writable } from "svelte/store";

  /** eslint-disable @typescript-eslint/restrict-template-expressions */
  import {
    CheckMarkIcon,
    CloseIconOpen,
    AddContainedIcon,
    RadioGroup,
    DropDown,
    Dialog,
    DialogBody,
    DialogHeader,
    DialogFooter,
    Button,
  } from "../../../common/Components";
  import type { RadioOption, DropDownOption } from "../../../common/Components";
  import type { Campaign, Scenario } from "../../../models/Campaign";
  import type {
    ContentItemSummary,
    Scenario as ScenarioContent,
    ScenarioSummary,
  } from "../../../models/Content";
  import { useContentService } from "../../../Service/ContentService";
  import { accessToken } from "../../../common/Utils/OidcSvelteClient";

  export let campaign: Campaign | undefined;
  export let saveScenario: (scenario: Scenario) => void | undefined;

  const { GetScenariosForGame, GetScenarioDefault } =
    useContentService(accessToken);

  const scenarioStatusOptions: RadioOption[] = [
    { label: "Completed", value: "completed" },
    { label: "Closed", value: "closed" },
    { label: "Available", value: "available" },
  ];

  let campaignScenarios: Scenario[] = [];

  const scenarioListing = writable<ScenarioSummary[]>([]);
  const scenarioListingProcessed = writable<boolean>(false);
  const handleGetScenarios = async (gameCode: string) => {
    if ($scenarioListing.length === 0) {
      const listing = await GetScenariosForGame(gameCode);
      scenarioListing.set(listing);
    }
  };

  let fullScenarioListOptions: DropDownOption[] = [];
  let unusedScenarioOptions: DropDownOption[] = [];
  const handleProcessScenarios = () => {
    if (campaign) {
      campaignScenarios = (campaign.scenarios ?? [])
        .map((cs) => {
          let scenarioForLoad = $scenarioListing.find(
            (ss) => ss.contentCode === cs.contentCode
          ) as ScenarioSummary;
          return {
            contentCode: scenarioForLoad.contentCode,
            description: scenarioForLoad.description,
            scenarioNumber: scenarioForLoad.sortOrder,
            name: scenarioForLoad.name,
            isClosed: cs.isClosed,
            isCompleted: cs.isCompleted,
          };
        })
        .filter((cs) => cs !== undefined)
        .sort((a: Scenario, b: Scenario) =>
          a.scenarioNumber > b.scenarioNumber ? 1 : -1
        );
      fullScenarioListOptions = $scenarioListing.map((scenario) => {
        return { label: scenario.name, value: scenario.contentCode };
      });
      unusedScenarioOptions = fullScenarioListOptions.filter(
        (scenario) =>
          campaign?.scenarios.findIndex(
            (s) => s.contentCode === scenario.value
          ) === -1
      );
      scenarioListingProcessed.set(true);
    }
  };

  let displayNewScenarioSelection = false;
  let existingScenario = false;
  let selectedScenario = "";
  let selectedScenarioStatus = "";
  let selectedScenarioDetail: ScenarioContent | undefined = undefined;
  const handleAddNewScenarioClick = () => {
    existingScenario = false;
    selectedScenario = "";
    selectedScenarioStatus = "";
    displayNewScenarioSelection = true;
  };
  const handleScenarioClick = (contentCode: string) => {
    existingScenario = true;
    const scenario = campaign?.scenarios.find(
      (s) => s.contentCode === contentCode
    );
    selectedScenario = contentCode;
    if (scenario?.isCompleted) {
      selectedScenarioStatus = "completed";
    } else if (scenario?.isClosed) {
      selectedScenarioStatus = "closed";
    } else if (scenario) {
      selectedScenarioStatus = "available";
    } else {
      selectedScenarioStatus = "";
    }
    displayNewScenarioSelection = true;
  };
  const handleCloseScenarioEdit = () => {
    existingScenario = false;
    selectedScenario = "";
    selectedScenarioStatus = "";
    displayNewScenarioSelection = false;
  };
  const handleSaveScenario = () => {
    if (saveScenario) {
      saveScenario({
        contentCode: selectedScenario,
        description:
          ($scenarioListing as ContentItemSummary[]).find(
            (li) => li.contentCode === selectedScenario
          )?.description ?? "",
        name:
          ($scenarioListing as ContentItemSummary[]).find(
            (li) => li.contentCode === selectedScenario
          )?.name ?? "",
        isClosed: selectedScenarioStatus === "closed",
        isCompleted: selectedScenarioStatus === "completed",
        scenarioNumber:
          $scenarioListing.find((ss) => ss.contentCode === selectedScenario)
            ?.scenarioNumber ?? 0,
      });
      handleCloseScenarioEdit();
    }
  };

  const scenarioCompleted = (scenario: Scenario): string => {
    return `${scenarioName(scenario)} Completed`;
  };
  const scenarioClosed = (scenario: Scenario): string => {
    return `${scenarioName(scenario)} Closed`;
  };
  const scenarioName = (scenario: Scenario): string => {
    return (
      ($scenarioListing as ContentItemSummary[]).find(
        (item) => item.contentCode === scenario.contentCode
      )?.name ?? scenario.contentCode
    );
  };

  let disableSave = true;
  const shouldDisableSave = (
    selectedScenario: string,
    selectedScenarioStatus: string
  ): boolean => {
    if (selectedScenario !== "" && selectedScenarioStatus !== "") return false;
    return true;
  };

  const handleScenarioSelected = async (contentCode: string): Promise<void> => {
    if (contentCode != "") {
      selectedScenarioDetail = await GetScenarioDefault(
        campaign?.game ?? "",
        contentCode
      );
    } else {
      selectedScenarioDetail = undefined;
    }
  };

  $: void handleScenarioSelected(selectedScenario);

  $: disableSave = shouldDisableSave(selectedScenario, selectedScenarioStatus);

  $: if (campaign?.game) void handleGetScenarios(campaign.game);
  $: if ($scenarioListing.length !== 0 && campaign?.scenarios)
    handleProcessScenarios();
</script>

{#if campaign}
  <div
    class="relative mt-2 px-3 py-1 items-center max-w-md mx-auto bg-gray-100 dark:bg-gray-700 text-gray-600 dark:text-gray-400 rounded-md backdrop-blur-sm"
  >
    <div aria-label="Available Scenarios" class="text-center text-xl">
      Scenarios
    </div>
    <div class="border-b-2 border-solid" />
    {#if $scenarioListingProcessed}
      <div class="absolute top-1 right-1">
        <button
          aria-label="Add New Scenario"
          on:click={handleAddNewScenarioClick}><AddContainedIcon /></button
        >
      </div>
      <div>
        <ul aria-label="Scenario Listing">
          {#each campaignScenarios as scenario}
            <li aria-label={scenario.name}>
              <div class="flex flex-row">
                <div class="mx-auto flex flex-row">
                  <div>
                    <button
                      on:click={() => {
                        // eslint-disable-next-line @typescript-eslint/no-unsafe-argument
                        handleScenarioClick(scenario.contentCode);
                      }}
                    >
                      {scenarioName(scenario)}
                    </button>
                  </div>
                  {#if scenario.isCompleted}
                    <div class="pl-2" aria-label={scenarioCompleted(scenario)}>
                      <CheckMarkIcon iconClassOverride="h-4 w-4 mt-1" />
                    </div>
                  {/if}
                  {#if scenario.isClosed}
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
<Dialog
  offClick
  open={displayNewScenarioSelection}
  onClose={handleCloseScenarioEdit}
>
  <DialogHeader slot="DialogHeader">
    <div class="mx-full text-center text-2xl mb-3">
      {existingScenario ? "Edit" : "Add"} Scenario
    </div>
    <div class="border-b-2 border-solid" />
  </DialogHeader>
  <DialogBody slot="DialogBody">
    <DropDown
      label="Scenarios"
      bind:selected={selectedScenario}
      placeHolder={selectedScenario === "" ? "Select a Scenario" : ""}
      options={selectedScenario === ""
        ? unusedScenarioOptions
        : fullScenarioListOptions}
      variant="rounded"
      disabled={existingScenario}
    />
    <RadioGroup
      centered
      bind:value={selectedScenarioStatus}
      options={scenarioStatusOptions}
    />
    {#if selectedScenarioDetail}
      <div
        class="relative mt-2 px-3 py-1 items-center max-w-md mx-auto bg-gray-100 dark:bg-gray-700 text-gray-600 dark:text-gray-400 rounded-md backdrop-blur-sm"
      >
        <div aria-label="Scenario Monsters" class="text-center text-xl">
          Monsters
        </div>
        <div class="border-b-2 border-solid" />
        <ul>
          {#each selectedScenarioDetail?.monsters ?? [] as monster}
            <li>
              <div class="flex flex-row">
                <div class="mx-auto flex flex-row">{monster.name}</div>
              </div>
            </li>
          {/each}
        </ul>
      </div>
    {/if}
  </DialogBody>
  <DialogFooter slot="DialogFooter">
    <Button
      variant="filled"
      color={disableSave ? "gray" : "blue"}
      disabled={disableSave}
      onClick={handleSaveScenario}
    >
      {existingScenario ? "Update" : "Add"} Scenario
    </Button>
  </DialogFooter>
</Dialog>
