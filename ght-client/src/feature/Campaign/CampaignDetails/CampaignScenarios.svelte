<script lang="ts">
  import { CheckMarkIcon, CloseIconOpen } from "../../../common/Components";
  import { Campaign } from "../../../models";
  import { ContentItemSummary, Scenario } from "../../../models/Content";
  import {
    getScenarios,
    scenarioListing,
    scenarioListingLoaded,
    scenarioListingLoading,
  } from "../../../Service/ContentService";

  export let campaign: Campaign | undefined;

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
  const handleProcessScenarios = () => {
    if (campaign)
      scenarios = ($scenarioListing as ContentItemSummary[]).filter(
        (scenario) =>
          campaign?.availableScenarios.includes(scenario.contentCode)
      );
  };

  $: if (campaign?.game) void handleGetScenarios(campaign.game);
  $: if ($scenarioListingLoaded || campaign) handleProcessScenarios();
</script>

{#if campaign}
  <div
    class=" mt-2 p-3 items-center max-w-md mx-auto bg-gray-50 rounded-md backdrop-blur-sm"
  >
    <div class="text-center text-lg">Available Scenarios</div>
    <div>
      <ul>
        {#each scenarios as scenario}
          <li>
            <div class="flex flex-row">
              <div class="mx-auto flex flex-row">
                <div>
                  {scenario.name}
                </div>
                {#if campaign.completedScenarios.includes(scenario.contentCode)}
                  <div class="pl-2">
                    <CheckMarkIcon iconClassOverride="h-4 w-4 mt-1" />
                  </div>
                {/if}
                {#if campaign.closedScenarios.includes(scenario.contentCode)}
                  <div class="pl-2">
                    <CloseIconOpen iconClassOverride="h-4 w-4 mt-1" />
                  </div>
                {/if}
              </div>
            </div>
          </li>
        {/each}
      </ul>
    </div>
  </div>
{/if}
