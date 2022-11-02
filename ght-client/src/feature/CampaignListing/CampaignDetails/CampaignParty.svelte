<script lang="ts">
  import { writable, type Unsubscriber } from "svelte/store";

  import Card, {
    Content as CardContent,
    Actions as CardActions,
    ActionIcons as CardActionIcons,
  } from "@smui/card";
  import List, { Item, Text, PrimaryText, SecondaryText } from "@smui/list";
  import IconButton, { Icon } from "@smui/icon-button";

  import { AddContainedIcon } from "../../../common/Components";
  import type { DropDownOption } from "../../../common/Components";

  import type { Campaign, Character } from "../../../models/Campaign";
  import CampaignCharacterEditor from "./CampaignCharacterEditor.svelte";
  import useContentService from "../../../Service/ContentService";
  import useCampaignService from "../../../Service/CampaignService";
  import { deepClone } from "fast-json-patch";
  import { onDestroy, onMount } from "svelte";
  //import Item from "@smui/list/src/Item.svelte";
  export let campaign: Campaign;

  const { actions: contentActions, state: contentState } = useContentService();
  const { getCharacterSummaries } = contentActions;
  const { characterSummaries } = contentState;

  const { actions: campaignActions } = useCampaignService();
  const { addPartyMember, updatePartyMember, getPartyMemberDetails } =
    campaignActions;

  const getContentSummary = (contentCode: string) => {
    return $characterSummaries.find((character) => {
      return character.contentCode === contentCode;
    });
  };

  let showPlayerDialog = false;
  let selectedCharacter: Character;
  let isNewCharacter = false;
  const handleOpenDialog = async (characterContentCode: string | undefined) => {
    isNewCharacter = characterContentCode === undefined;
    if (!isNewCharacter) {
      await getPartyMemberDetails(campaign.id, characterContentCode ?? "");
      selectedCharacter = deepClone(
        campaign.party.find(
          (chr) => chr.characterContentCode === characterContentCode
        )
      ) as Character;
      console.log(JSON.stringify(selectedCharacter));
    } else {
      selectedCharacter = {
        name: "",
        characterContentCode: characterContentCode ?? "",
        experience: 0,
        gold: 0,
        items: [],
        perkPoints: 0,
        appliedPerks: [],
      };
    }
    showPlayerDialog = true;
  };
  const handleCloseDialog = () => {
    isNewCharacter = false;
    showPlayerDialog = false;
  };

  let saving = false;
  const handleSaveCharacter = async () => {
    // Do Stuff
    saving = true;
    if (isNewCharacter) {
      await addPartyMember(campaign.id, selectedCharacter);
    } else {
      await updatePartyMember(campaign.id, selectedCharacter);
    }
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

  const characterListingProcessed = writable<boolean>(false);

  const handleGetCharacters = (gameCode: string) => {
    if ($characterSummaries.length <= 0) getCharacterSummaries(gameCode);
  };

  // let availableCharacterOptions: DropDownOption[] = [];
  let fullListOfPossibleCharacterOptions: DropDownOption[] = [];
  let usedCharacters: string[] = [];
  const determineUsedCharacters = (campaignCharacters: Character[]) => {
    usedCharacters = campaignCharacters.map((c) => c.characterContentCode);
  };

  $: if (campaign?.game) void handleGetCharacters(campaign.game);
  $: if (campaign?.party) checkSaving();
  $: if (campaign?.party) determineUsedCharacters(campaign.party);

  let characterSummariesUnsubscribe: Unsubscriber;
  onMount(() => {
    characterSummariesUnsubscribe = characterSummaries.subscribe(
      (characterContentList) => {
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
      }
    );
  });

  onDestroy(() => {
    characterSummariesUnsubscribe();
  });
</script>

<Card variant="outlined">
  <CardContent class="max-h-56">
    <div class="mdc-typography--headline5 text-center">Party</div>
    <hr class="my-1" />
    <List twoLine singleSelection>
      {#each campaign.party as character}
        <Item>
          <Text>
            <PrimaryText>{character.name}</PrimaryText>
            <SecondaryText>
              {`${
                // eslint-disable-next-line @typescript-eslint/no-unsafe-argument
                getContentSummary(character.characterContentCode)?.name ?? ""
              }`}
            </SecondaryText>
          </Text>
        </Item>
      {/each}
    </List>
  </CardContent>
  <CardActions>
    {#if $characterListingProcessed}
      <CardActionIcons>
        <IconButton
          class="material-icons"
          aria-label="Add Party Member"
          title="Add Party Member"
        >
          add_circle
        </IconButton>
      </CardActionIcons>
    {/if}
  </CardActions>
</Card>

<!-- <div
  class="relative mt-2 px-3 py-1 items-center max-w-md mx-auto bg-gray-100 dark:bg-gray-700 text-gray-600 dark:text-gray-400 rounded-md backdrop-blur-sm"
>
  <div aria-label="Party Members" class="text-center text-xl">Party</div>
  <div class="border-b-2 border-solid" />
  {#if $characterListingProcessed}
    <div class="absolute top-1 right-1">
      <button
        aria-label="Add New Party Member"
        on:click={() => void handleOpenDialog(undefined)}
        ><AddContainedIcon /></button
      >
    </div>
    <ul aria-label="Scenario Listing">
      {#each campaign.party as character}
        <li>
          <div class="flex flex-col">
            <div class="mx-auto">
              <button
                on:click={() =>
                  void handleOpenDialog(character.characterContentCode)}
                class="flex flex-row"
              >
                <span>{character.name}</span>
                <span class="ml-2">
                  {`(
                    ${
                      // eslint-disable-next-line @typescript-eslint/no-unsafe-argument
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
</div> -->
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
