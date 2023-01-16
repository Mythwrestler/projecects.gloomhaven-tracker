<script lang="ts">
  import { writable } from "svelte/store";

  import Card, {
    Content as CardContent,
    Actions as CardActions,
    ActionIcons as CardActionIcons,
  } from "@smui/card";
  import List, { Item, Text, PrimaryText, SecondaryText } from "@smui/list";
  import IconButton from "@smui/icon-button";

  import type { Character } from "../../../models/Campaign";
  import CampaignCharacterEditor from "./CampaignCharacterEditor.svelte";
  import useContentService from "../../../Service/ContentService";
  import useCampaignService from "../../../Service/CampaignService";
  import type { ContentItemSummary } from "../../../models/Content";
  import { onMount } from "svelte";
  import { deepClone } from "fast-json-patch";

  // #region Props
  export let gameCode: string;
  export let campaignId: string;
  export let party: Character[];
  // #endregion

  const { actions: contentActions } = useContentService();
  const { getCharacterSummaries } = contentActions;

  const { actions: campaignActions } = useCampaignService();
  const { getPartyMemberDetails, addPartyMember, updatePartyMember } =
    campaignActions;

  const characterSummaries = writable<ContentItemSummary[]>([]);
  const handleGetCharacterSummaries = async (gameCode: string) => {
    try {
      const summaries = await getCharacterSummaries(gameCode);
      characterSummaries.set(summaries);
    } catch {
      characterSummaries.set([]);
    }
  };

  const usedCharacters = writable<string[]>([]);
  const characterListing = writable<Character[]>([]);
  const characterListingProcessed = writable<boolean>(false);
  const processPartyCharacters = async (party: Character[]) => {
    const partyMembers = deepClone(party) as Character[];
    partyMembers.forEach((char) => {
      if (!$usedCharacters.includes(char.characterContentCode))
        usedCharacters.update((usedCharacters) => [
          ...usedCharacters,
          char.characterContentCode,
        ]);
    });
    const detailRequests = partyMembers.map((char) =>
      getPartyMemberDetails(campaignId, char.characterContentCode)
    );
    try {
      const results = await Promise.all(detailRequests);
      results
        .filter((result) => result !== undefined)
        .forEach((result) => {
          const characterToSet = partyMembers.find(
            (char) => char.characterContentCode === result?.characterContentCode
          );
          if (characterToSet) {
            characterToSet.experience = (result as Character).experience;
            characterToSet.gold = (result as Character).gold;
            characterToSet.items = (result as Character).items;
            characterToSet.perkPoints = (result as Character).perkPoints;
            characterToSet.appliedPerks = (result as Character).appliedPerks;
          }
        });
      characterListing.set(partyMembers);
      characterListingProcessed.set(true);
    } catch {
      usedCharacters.set([]);
    }
  };

  const getContentSummary = (contentCode: string) =>
    $characterSummaries.find(
      (character) => character.contentCode === contentCode
    );

  let showPlayerDialog = false;
  let selectedCharacter: Character | undefined;
  const handleOpenDialog = (contentCode: string | undefined): void => {
    if (contentCode !== undefined) {
      selectedCharacter = $characterListing.find(
        (chr) => chr.characterContentCode === contentCode
      );
    } else {
      selectedCharacter = undefined;
    }
    showPlayerDialog = true;
  };

  const handleAddPartyMember = async (characterToAdd: Character) => {
    const addedCharacter = await addPartyMember(campaignId, characterToAdd);
    if (addedCharacter) updateCharacterListing(addedCharacter);
  };

  const handleUpdatePartyMember = async (characterToUpdate: Character) => {
    const currentCharacter = $characterListing.find(
      (char) =>
        char.characterContentCode === characterToUpdate.characterContentCode
    );
    if (currentCharacter) {
      const updatedCharacter = await updatePartyMember(
        campaignId,
        currentCharacter,
        characterToUpdate
      );
      updateCharacterListing(updatedCharacter);
    }
  };

  const updateCharacterListing = (partyMember: Character) => {
    characterListing.update((currentListing) => {
      const partyToUpdate = [...currentListing];
      const location = partyToUpdate.findIndex(
        (char) => char.characterContentCode === partyMember.characterContentCode
      );
      if (location === -1) partyToUpdate.push(partyMember);
      else partyToUpdate.splice(location, 1, partyMember);
      return partyToUpdate;
    });
  };

  onMount(() => {
    void handleGetCharacterSummaries(gameCode);
    void processPartyCharacters(party);
  });
</script>

<Card variant="outlined">
  <CardContent class="max-h-56">
    <div class="mdc-typography--headline5 text-center">Party</div>
    <hr class="my-1" />
    {#if !$characterListingProcessed}
      <div>...Loading</div>
    {:else}
      <List twoLine singleSelection>
        {#each $characterListing as character}
          <Item
            on:SMUI:action={() => {
              // eslint-disable-next-line @typescript-eslint/no-unsafe-argument
              handleOpenDialog(character.characterContentCode);
            }}
          >
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
    {/if}
  </CardContent>
  <CardActions>
    {#if $characterListingProcessed}
      <CardActionIcons>
        <IconButton
          on:click={() => {
            handleOpenDialog(undefined);
          }}
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
<CampaignCharacterEditor
  bind:open={showPlayerDialog}
  {gameCode}
  campaignParty={$characterListing}
  campaignCharacter={selectedCharacter}
  addPartyMember={handleAddPartyMember}
  updatePartyMember={handleUpdatePartyMember}
/>
