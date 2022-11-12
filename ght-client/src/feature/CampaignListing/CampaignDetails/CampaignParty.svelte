<script lang="ts">
  import { writable } from "svelte/store";

  import Card, {
    Content as CardContent,
    Actions as CardActions,
    ActionIcons as CardActionIcons,
  } from "@smui/card";
  import List, { Item, Text, PrimaryText, SecondaryText } from "@smui/list";
  import IconButton from "@smui/icon-button";

  import type { Campaign, Character } from "../../../models/Campaign";
  import CampaignCharacterEditor from "./CampaignCharacterEditor.svelte";
  import useContentService from "../../../Service/ContentService";
  import useCampaignService from "../../../Service/CampaignService";
  import { onMount } from "svelte";
  export let campaign: Campaign;

  const { actions: contentActions, state: contentState } = useContentService();
  const { getCharacterSummaries } = contentActions;
  const { characterSummaries } = contentState;

  const { actions: campaignActions } = useCampaignService();
  const { getPartyMemberDetails } = campaignActions;

  const getContentSummary = (contentCode: string) => {
    return $characterSummaries.find((character) => {
      return character.contentCode === contentCode;
    });
  };

  let showPlayerDialog = false;
  let selectedCharacter: Character | undefined;
  const handleOpenDialog = (contentCode: string | undefined): void => {
    if (contentCode !== undefined) {
      void getPartyMemberDetails(campaign.id, contentCode);
      selectedCharacter = campaign.party.find(
        (chr) => chr.characterContentCode === contentCode
      );
    } else {
      selectedCharacter = undefined;
    }
    showPlayerDialog = true;
  };

  const characterListingProcessed = writable<boolean>(false);

  let retrievingCharacters = false;
  const handleGetCharacters = (gameCode: string) => {
    if ($characterSummaries.length === 0 && !retrievingCharacters) {
      retrievingCharacters = true;
      getCharacterSummaries(gameCode);
    }
  };

  let usedCharacters: string[] = [];
  const determineUsedCharacters = (campaignCharacters: Character[]) => {
    usedCharacters = campaignCharacters.map((c) => c.characterContentCode);
  };

  $: if (campaign?.game) void handleGetCharacters(campaign.game);
  $: if (campaign?.party) determineUsedCharacters(campaign.party);

  onMount(() => {
    if (campaign.party.length > 0) {
      const charDetails = campaign.party.map(async (chr) => {
        await getPartyMemberDetails(campaign.id, chr.characterContentCode);
      });
      Promise.all(charDetails)
        .then(() => {
          characterListingProcessed.set(true);
        })
        .catch((err) => console.log(JSON.stringify(err)));
    } else {
      characterListingProcessed.set(true);
    }
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
        {#each campaign.party as character}
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
  gameCode={campaign.game}
  campaignId={campaign.id}
  campaignParty={campaign.party}
  campaignCharacter={selectedCharacter}
/>
