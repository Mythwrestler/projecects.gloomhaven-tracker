<script lang="ts">
  import { Campaign, Character } from "../../../models";
  import { ContentItemSummary } from "../../../models/Content";
  import {
    characterListing,
    characterListingLoaded,
    characterListingLoading,
    getCharacters,
  } from "../../../Service/ContentService";
  export let campaign: Campaign;

  const getContentSummary = (contentCode: string) => {
    console.log(contentCode);
    return ($characterListing as ContentItemSummary[]).find((character) => {
      console.log(character.contentCode);
      return character.contentCode === contentCode;
    });
  };

  const handleGetCharacters = async (gameCode: string) => {
    if (
      !$characterListingLoading &&
      !$characterListingLoaded &&
      ($characterListing as ContentItemSummary[]).length === 0
    ) {
      await getCharacters(gameCode);
    }
  };

  $: if (campaign?.game) void handleGetCharacters(campaign.game);
</script>

<div
  class="mt-2 px-3 py-1 items-center max-w-md mx-auto bg-gray-50 rounded-md backdrop-blur-sm"
>
  <div aria-label="Available Scenarios" class="text-center text-xl">
    Party Members
  </div>
  <div class="border-b-2 border-solid" />
  {#if $characterListingLoaded}
    <ul aria-label="Scenario Listing">
      {#each campaign.party.characters as character}
        <li>
          <div class="flex flex-col">
            <div class="mx-auto flex flex-col">
              <span>{character.name}</span>
              <span>
                ({getContentSummary(character.characterContentCode)?.name ??
                  ""})
              </span>
            </div>
          </div>
        </li>
      {/each}
    </ul>
  {:else}
    <div class="mx-auto">Loading...</div>
  {/if}
</div>
