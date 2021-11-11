<script lang="ts">
  import { onMount } from "svelte";

  import {
    TextField,
    CheckMarkIcon,
    DropDown,
    Option,
  } from "../../common/Components";
  import { ContentItemSummary } from "../../models/Content";

  import {
    getScenarios,
    scenarioListing,
    scenarioListingLoading,
    scenarioListingLoaded,
  } from "./CombatTrackerService";

  const handleGetScenarios = async () => {
    console.log("handelGetCalled");
    console.log($scenarioListingLoaded);
    console.log($scenarioListingLoading);
    console.log(JSON.stringify($scenarioListing));
    if (
      !$scenarioListingLoaded &&
      !$scenarioListingLoading &&
      ($scenarioListing as ContentItemSummary[]).length === 0
    ) {
      await getScenarios("jawsOfTheLion");
    }
  };

  let scenarioSelection: string | undefined = undefined;
  let scenarioOptions: Option[] = [];
  scenarioListing.subscribe((list) => {
    scenarioOptions = list.map((l) => {
      return { label: l.name, value: l.code };
    });
  });
  const onSecenariSelection = (scenarioCode: string | number) => {
    console.log(scenarioCode);
  };

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
