<script lang="ts">
  // import { useLocation } from "svelte-navigator";
  import { onDestroy, onMount } from "svelte";
  import GhtPanel from "../../../common/Components/GHTPanel/GHTPanel.svelte";
  import { useNavigate } from "svelte-navigator";

  import { useActiveCombatService } from "../../../Service/ActiveCombatService";
  import useContentService from "../../../Service/ContentService";
  import useCampaignService from "../../../Service/CampaignService";

  import Card, { Content, Actions, ActionButtons } from "@smui/card";
  import Button from "@smui/button";
  import { writable, type Unsubscriber } from "svelte/store";
  import {
    type Scenario,
    type Character as ContentCharacter,
  } from "../../../models/Content";
  import type {
    Campaign,
    Character as CampaignCharacter,
  } from "../../../models/Campaign";
  import List, {
    Item,
    Label,
    Meta,
    PrimaryText,
    SecondaryText,
    Text,
  } from "@smui/list";
  import Checkbox from "@smui/checkbox";
  import type { Character } from "../../../models/Combat";

  const { actions, state } = useActiveCombatService();
  const { joinCombat, leaveCombat, registerCharacters, registerObserver } =
    actions;
  const {
    combatConnected,
    combatConnecting,
    combatDisconnecting,
    combatSummary,
    combatCharacters,
    participants,
  } = state;

  const { actions: contentActions } = useContentService();
  const { getScenarioDefault, getCharacterDefault } = contentActions;

  const { actions: campaignActions } = useCampaignService();
  const { getCampaignDetail } = campaignActions;

  export let combatId = "";

  const navigate = useNavigate();

  const shortString = (text: string | undefined): string => {
    if (text === undefined) return "";
    if (text.length < 15) return text;
    return `${text.substring(0, 15)}...`;
  };

  const handleBackClick = () => {
    navigate(-1);
  };

  const campaign = writable<Campaign | undefined>();
  const scenario = writable<Scenario | undefined>();
  const characters = writable<ContentCharacter[]>([]);
  const handleInitialGetDetails = async (
    scenarioCode: string,
    campaignId: string
  ) => {
    const campaignDetail: Campaign | undefined = await getCampaignDetail(
      campaignId
    );
    let scenarioDetail: Scenario | undefined = undefined;
    let characterSummaries: ContentCharacter[] = [];
    if (campaignDetail) {
      scenarioDetail = await getScenarioDefault(
        campaignDetail.game,
        scenarioCode
      );

      const characterResults = await Promise.all(
        campaignDetail.party.map(async (chr) => {
          return await getCharacterDefault(
            campaignDetail.game,
            chr.characterContentCode
          );
        })
      );
      characterSummaries = characterResults.filter(
        (chr) => chr
      ) as ContentCharacter[];
    }

    if (campaignDetail && scenarioDetail) {
      campaign.set(campaignDetail);
      scenario.set(scenarioDetail);
      characters.set(characterSummaries);
    }
  };

  let combatSummaryUnsubscribe: Unsubscriber;
  onMount(() => {
    combatSummaryUnsubscribe = combatSummary.subscribe((summary) => {
      if (summary)
        void handleInitialGetDetails(
          summary.scenarioContentCode,
          summary.campaignId
        );
    });

    void joinCombat(combatId);
  });

  onDestroy(async () => {
    combatSummaryUnsubscribe();
    await leaveCombat();
  });

  const characterDetail = (
    character: Character
  ): ContentCharacter | undefined => {
    return $characters.find(
      (cntChr) => cntChr.contentCode === character.characterContentCode
    );
  };
  let selectedCharacters: string[] = [];
</script>

<GhtPanel color="ght-panel">
  <Card>
    <Content>
      <div class="mdc-typography--headline5 text-center">Hub Connection</div>
      <hr class="my-1" />
    </Content>
  </Card>
  <div class="mt-2">
    <Card>
      <Content>
        <div class="mdc-typography--headline5 text-center">
          Available Characters
        </div>
        <hr class="my-1" />
        {#if $characters.length > 0 && $combatCharacters.length > 0}
          <List checkList>
            {#each $combatCharacters as character}
              <Item>
                <Label>{characterDetail(character)?.name}</Label>
                <Meta>
                  <Checkbox
                    bind:group={selectedCharacters}
                    value={character.characterContentCode}
                  />
                </Meta>
              </Item>
            {/each}
          </List>
        {/if}
      </Content>
      <Actions>
        <ActionButtons>
          <Button
            variant="outlined"
            color="primary"
            on:click={() => {
              selectedCharacters.length > 0
                ? void registerCharacters(selectedCharacters)
                : void registerObserver();
            }}
          >
            {selectedCharacters.length > 0
              ? "Select Characters"
              : "Observe Game"}
          </Button>
        </ActionButtons>
      </Actions>
    </Card>
  </div>
  <div class="mt-2">
    <Card>
      <Content>
        <div class="mdc-typography--headline5 text-center">
          Connected Users:
        </div>
        <hr class="my-1" />
        {#if $participants}
          <List nonInteractive twoLine>
            {#each $participants.participants as participant}
              <Item>
                <Text>
                  <PrimaryText>{participant.username}</PrimaryText>
                  {#if participant.isObserver}
                    <SecondaryText>Is Observer</SecondaryText>
                  {/if}
                  {#if participant.characterCodes}
                    {#each participant.characterCodes as contentCode}
                      <SecondaryText>{contentCode}</SecondaryText>
                    {/each}
                  {/if}
                </Text>
              </Item>
            {/each}
          </List>
        {/if}
      </Content>
    </Card>
  </div>
  <div class="mt-2">
    <Card>
      <Content>
        <div class="mdc-typography--headline5 text-center">
          Combat Connection Status
        </div>
        <hr class="my-1" />
        <div>
          <pre>Combat Connected: {$combatConnected}</pre>
          <pre>Combat Connecting: {$combatConnecting}</pre>
          <pre>Combat Disconnecting: {$combatDisconnecting}</pre>
        </div>
        <div>Combat Id: {shortString($combatSummary?.id ?? "")}</div>
      </Content>
      <Actions>
        <ActionButtons>
          <Button color="primary" on:click={handleBackClick}>Back</Button>
        </ActionButtons>
      </Actions>
    </Card>
  </div>
</GhtPanel>
