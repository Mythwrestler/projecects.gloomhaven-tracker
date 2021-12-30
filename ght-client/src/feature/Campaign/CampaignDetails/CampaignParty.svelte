<script lang="ts">
  import { writable } from "svelte/store";

  import { AddContainedIcon, DropDownOption } from "../../../common/Components";

  import { Campaign, Character } from "../../../models";
  import { ContentItemSummary } from "../../../models/Content";
  import {
    addCharacterForCampaign,
    updateCharacterForCampaign,
  } from "../../../Service/CampaignService";

  import useContentService from "../../../Service/ContentService";
  import CampaignCharacterEditor from "./CampaignCharacterEditor.svelte";
  export let campaign: Campaign;

  const contentService = useContentService();

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
  const handleSaveCharacter = async (): Promise<void> => {
    // Do Stuff
    let writtenCharacter: Character | undefined;
    if (isNewCharacter) {
      writtenCharacter = await addCharacterForCampaign(
        campaign.id,
        selectedCharacter
      );
    } else {
      writtenCharacter = await updateCharacterForCampaign(
        campaign.id,
        selectedCharacter
      );
    }
    if (!writtenCharacter) return;
    const posIndex = campaign.party.characters.findIndex(
      (c) => c.characterContentCode === selectedCharacter.characterContentCode
    );
    if (posIndex !== -1) {
      const arrayToUpdate = [...campaign.party.characters];
      arrayToUpdate.splice(posIndex, 1, writtenCharacter);
      campaign.party.characters = arrayToUpdate;
    } else {
      campaign.party.characters = [
        ...campaign.party.characters,
        writtenCharacter,
      ];
    }

    handleCloseDialog();
  };

  const characterListing = writable<ContentItemSummary[]>([]);
  const characterListingProcessed = writable<boolean>(false);

  const handleGetCharacters = async (gameCode: string) => {
    if (($characterListing as ContentItemSummary[]).length === 0) {
      const listing = await contentService.GetCharactersForGame(gameCode);
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
  $: determineUsedCharacters(campaign.party.characters);
</script>

<div
  class="relative mt-2 px-3 py-1 items-center max-w-md mx-auto bg-gray-50 rounded-md backdrop-blur-sm"
>
  <div aria-label="Party Members" class="text-center text-xl">
    Party Members
  </div>
  <div class="border-b-2 border-solid" />
  {#if $characterListingProcessed}
    <div class="absolute top-1 right-1">
      <button
        aria-label="Add New Party Member"
        on:click={() => handleOpenDialog()}><AddContainedIcon /></button
      >
    </div>
    <ul aria-label="Scenario Listing">
      {#each campaign.party.characters as character}
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
