<script lang="ts">
  import { writable } from "svelte/store";

  import { AddContainedIcon } from "../../../common/Components";
  import type { DropDownOption } from "../../../common/Components";

  import type { Campaign, Character } from "../../../models/Campaign";
  import type { ContentItemSummary } from "../../../models/Content";
  import { useCampaignService } from "../../../Service/CampaignService";
  import CampaignCharacterEditor from "./CampaignCharacterEditor.svelte";
  import { useContentService } from "../../../Service/ContentService";
  export let campaign: Campaign;

  const { GetCharactersForGame } = useContentService();
  const { addUpdatePartyMember } = useCampaignService();

  const getContentSummary = (contentCode: string) => {
    return ($characterListing as ContentItemSummary[]).find((character) => {
      return character.contentCode === contentCode;
    });
  };

  let showPlayerDialog = false;
  let selectedCharacter: Character;
  let isNewCharacter = false;
  const handleOpenDialog = (character: Character | undefined = undefined) => {
    selectedCharacter = character ?? {
      name: "",
      characterContentCode: "",
      experience: 0,
      gold: 0,
      items: [],
      perkPoints: 0,
      appliedPerks: [],
    };
    if (!character) {
      isNewCharacter = true;
    } else {
      isNewCharacter = false;
    }
    showPlayerDialog = true;
  };
  const handleCloseDialog = () => {
    isNewCharacter = false;
    showPlayerDialog = false;
  };

  let saving = false;
  const handleSaveCharacter = (): void => {
    // Do Stuff
    saving = true;
    addUpdatePartyMember(selectedCharacter);
    saving = false;
    handleCloseDialog();
  };

  const checkSaving = (): void => {
    if (
      saving &&
      campaign.party.findIndex(
        (c) => c.characterContentCode === selectedCharacter.characterContentCode
      ) === -1
    ) {
      handleCloseDialog();
    }
  };

  const characterListing = writable<ContentItemSummary[]>([]);
  const characterListingProcessed = writable<boolean>(false);

  const handleGetCharacters = async (gameCode: string) => {
    if (($characterListing as ContentItemSummary[]).length === 0) {
      const listing = await GetCharactersForGame(gameCode);
      characterListing.set(listing);
    }
  };

  // let availableCharacterOptions: DropDownOption[] = [];
  let fullListOfPossibleCharacterOptions: DropDownOption[] = [];
  let usedCharacters: string[] = [];
  const determineUsedCharacters = (campaignCharacters: Character[]) => {
    usedCharacters = campaignCharacters.map((c) => c.characterContentCode);
  };

  characterListing.subscribe((characterContentList) => {
    if (characterContentList && characterContentList.length > 0) {
      fullListOfPossibleCharacterOptions = characterContentList.map(
        (charCont) => {
          return {
            label: charCont.name,
            value: charCont.contentCode,
          };
        }
      );
      characterListingProcessed.set(true);
    }
  });

  $: if (campaign?.game) void handleGetCharacters(campaign.game);
  $: if (campaign?.party) checkSaving();
  // $: determineUsedCharacters(campaign.party.characters);
</script>

<div
  class="relative mt-2 px-3 py-1 items-center max-w-md mx-auto bg-gray-50 rounded-md backdrop-blur-sm"
>
  <div aria-label="Party Members" class="text-center text-xl">Party</div>
  <div class="border-b-2 border-solid" />
  {#if $characterListingProcessed}
    <div class="absolute top-1 right-1">
      <button
        aria-label="Add New Party Member"
        on:click={() => handleOpenDialog()}><AddContainedIcon /></button
      >
    </div>
    <ul aria-label="Scenario Listing">
      {#each campaign.party as character}
        <li>
          <div class="flex flex-col">
            <div class="mx-auto">
              <button
                on:click={() => handleOpenDialog(character)}
                class="flex flex-row"
              >
                <span>{character.name}</span>
                <span class="ml-2">
                  {`(
                    ${
                      getContentSummary(character.characterContentCode)?.name ??
                      ""
                    }
                  )`}
                </span>
              </button>
            </div>
          </div>
        </li>
      {/each}
    </ul>
  {:else}
    <div class="mx-auto">Loading...</div>
  {/if}
</div>
{#if showPlayerDialog}
  <CampaignCharacterEditor
    gameCode={campaign.game ?? ""}
    bind:selectedCharacter
    {isNewCharacter}
    showCampaignCharacterDialog={showPlayerDialog}
    handleSave={handleSaveCharacter}
    {handleCloseDialog}
    characterOptionsAlreadyUsed={usedCharacters}
    fullCharcterOptionList={fullListOfPossibleCharacterOptions}
  />
{/if}
