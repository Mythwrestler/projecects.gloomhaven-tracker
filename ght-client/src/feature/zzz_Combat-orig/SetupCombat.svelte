<script lang="ts">
  import { onMount } from "svelte";

  import {
    TextField,
    CheckMarkIcon,
    DropDown,
    DropDownOption,
  } from "../../common/Components";
  import { ContentItemSummary } from "../../models/Content";

  import {
    scenarioListing,
    scenarioListingLoading,
    scenarioListingLoaded,
  } from "../../Service/CombatTrackerService";

  import { useContentService } from "../../Service/ContentService";

  const { GetScenariosForGame } = useContentService();

  const handleGetScenarios = async () => {
    if (
      !$scenarioListingLoaded &&
      !$scenarioListingLoading &&
      ($scenarioListing as ContentItemSummary[]).length === 0
    ) {
      await GetScenariosForGame("jawsOfTheLion");
    }
  };

  let scenarioSelection: string | undefined = undefined;
  let scenarioOptions: DropDownOption[] = [];
  scenarioListing.subscribe((list) => {
    scenarioOptions = list.map((l) => {
      return { label: l.name, value: l.contentCode };
    });
  });
  const onSecenariSelection = (scenarioCode: string | number) => {};

  onMount(() => {
    void handleGetScenarios();
  });
</script>

<!-- Select Scenario -->

<div class="bg-opacity-50 bg-gray-100">
  <div>
    {#if $scenarioListingLoaded}
      <DropDown
        options={scenarioOptions}
        placeHolder="Scenarios"
        bind:selected={scenarioSelection}
        variant="square"
        onSelection={onSecenariSelection}
      />
    {/if}
  </div>

  <!-- Display monsters -->
  <div>
    <ul>
      <li />
    </ul>
  </div>
</div>
