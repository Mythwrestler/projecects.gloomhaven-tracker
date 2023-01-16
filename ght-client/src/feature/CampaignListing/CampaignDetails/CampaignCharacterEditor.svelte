<script lang="ts">
  import { writable, type Writable } from "svelte/store";

  import Dialog, {
    Header as DialogHeader,
    Title as DialogTitle,
    Content as DialogContent,
    Actions as DialogActions,
  } from "@smui/dialog";
  import GhtPanel from "../../../common/Components/GHTPanel/GHTPanel.svelte";
  import Card, { Content as CardContent } from "@smui/card";
  import Button from "@smui/button";
  import Select, { Option } from "@smui/select";
  import Textfield from "@smui/textfield";
  import List, { Item, Text } from "@smui/list";

  import type { Character as CampaignCharacter } from "../../../models/Campaign";
  import type {
    Character as ContentCharacter,
    ContentItemSummary,
  } from "../../../models/Content";

  import useContentService from "../../../Service/ContentService";

  const { actions: contentActions } = useContentService();
  const { getCharacterDefault, getCharacterSummaries } = contentActions;

  // #region Props

  export let open: boolean;
  export let gameCode: string;
  export let campaignParty: CampaignCharacter[];
  export let campaignCharacter: CampaignCharacter | undefined;
  export let addPartyMember: (
    characterToAdd: CampaignCharacter
  ) => Promise<void>;
  export let updatePartyMember: (
    characterToAdd: CampaignCharacter
  ) => Promise<void>;

  // #endregion

  let name: string;
  let characterContentCode: string;
  let experience: number;
  let gold: number;
  let items: string[];
  let appliedPerks: string[];
  let perkPoints: number;

  let isNewCharacter = false;
  let selectedContentCode: string;

  let characterLevel = 0;
  let characterHealth = 0;

  const characterSummaries = writable<ContentItemSummary[]>([]);
  const characterDefault = writable<ContentCharacter | undefined>(undefined);
  const handleGetContent = async (gameCode: string, characterCode: string) => {
    try {
      const character = await getCharacterDefault(gameCode, characterCode);
      const summaries = await getCharacterSummaries(gameCode);
      characterDefault.set(character);
      characterSummaries.set(summaries);
    } catch {
      characterDefault.set(undefined);
      characterSummaries.set([]);
    }
  };

  const newCharacter: CampaignCharacter = {
    name: "",
    experience: 0,
    gold: 0,
    items: [],
    appliedPerks: [],
    perkPoints: 0,
    characterContentCode: "",
  };

  const handleOpen = (
    campaignCharacter: CampaignCharacter | undefined
  ): void => {
    const characterToLoad = campaignCharacter ?? newCharacter;
    isNewCharacter = !campaignCharacter;

    name = characterToLoad.name ?? "";
    experience = characterToLoad.experience ?? 0;
    gold = characterToLoad.gold ?? 0;
    items = characterToLoad.items ?? [];
    appliedPerks = characterToLoad.appliedPerks ?? [];
    perkPoints = characterToLoad.perkPoints ?? 0;
    characterContentCode = characterToLoad.characterContentCode;
    selectedContentCode = characterToLoad.characterContentCode;
  };

  const handleSave = () => {
    const characterToSave: CampaignCharacter = {
      name,
      experience,
      gold,
      items,
      appliedPerks: undefined,
      perkPoints,
      characterContentCode,
    };
    if (isNewCharacter) {
      void addPartyMember(characterToSave);
    } else {
      void updatePartyMember(characterToSave);
    }
    open = false;
  };

  const selectableCharacters: Writable<ContentItemSummary[]> = writable<
    ContentItemSummary[]
  >([]);
  const handleProcessCharacters = (
    isNewCharacter: boolean,
    party: CampaignCharacter[],
    characterSummaries: ContentItemSummary[] | undefined
  ): void => {
    const usedContentCodes: string[] = party.map(
      (pc) => pc.characterContentCode
    );
    if (characterSummaries === undefined) return;
    selectableCharacters.set(
      isNewCharacter
        ? characterSummaries.filter(
            (cs) => !usedContentCodes.includes(cs.contentCode)
          )
        : characterSummaries.filter(
            (cs) => cs.contentCode === characterContentCode
          )
    );
  };
  const characterSelectKey = (contentCode: string | undefined) =>
    `${contentCode ?? ""}`;

  const calculateCharacterLevel = (
    contentCode: string,
    character: ContentCharacter | undefined,
    experience: number
  ) => {
    characterLevel =
      contentCode === character?.contentCode
        ? character?.baseStats.levels
            .sort((a, b) => (a.level < b.level ? 1 : -1))
            .find((lvl) => lvl.experience <= experience)?.level ?? 0
        : 0;
  };
  const calculateCharacterHealth = (
    contentCode: string,
    character: ContentCharacter | undefined,
    characterLevel: number
  ) => {
    characterHealth =
      contentCode === character?.contentCode
        ? character?.baseStats.health.find((h) => h.level === characterLevel)
            ?.health ?? 0
        : 0;
  };

  const handleClose = () => {
    selectableCharacters.set([]);
    handleOpen(undefined);
  };

  $: open && handleOpen(campaignCharacter);
  $: open && handleGetContent(gameCode, characterContentCode);
  $: open &&
    handleProcessCharacters(isNewCharacter, campaignParty, $characterSummaries);

  $: open &&
    calculateCharacterLevel(
      characterContentCode,
      $characterDefault,
      experience
    );
  $: open &&
    calculateCharacterHealth(
      characterContentCode,
      $characterDefault,
      characterLevel
    );

  $: !open && handleClose();
</script>

<Dialog
  bind:open
  fullscreen
  surface$class="mt-12"
  surface$style="max-height: calc(100vh - 40px);"
>
  <DialogHeader>
    <DialogTitle>
      {`${isNewCharacter ? "Add" : "Update"} Character`}
    </DialogTitle>
  </DialogHeader>
  <DialogContent>
    <GhtPanel color="ght-panel">
      {#if open && $selectableCharacters.length > 0}
        <Card class="mt-2">
          <CardContent>
            <Textfield style="width: 100%;" label="Name" bind:value={name} />
            <Select
              key={characterSelectKey}
              bind:value={characterContentCode}
              label="Character"
              disabled={!isNewCharacter}
              menu$fixed
              menu$class="sm:max-w-lg max-w-xs"
              style="width: 100%;"
            >
              {#if isNewCharacter}
                <Option value={""} />
              {/if}
              {#each $selectableCharacters as character}
                <Option value={character.contentCode}>{character.name}</Option>
              {/each}
            </Select>
          </CardContent>
        </Card>
        {#if characterContentCode !== "" && $characterDefault?.contentCode === characterContentCode}
          <Card class="mt-2">
            <CardContent>
              <div class="mdc-typography--heading5 text-center">Character</div>
              <hr class="my-1" />
              <Textfield
                type="number"
                style="width: 100%;"
                label="Experience"
                bind:value={experience}
              />
              <div class="mdc-typography--subtitle2 text-center">
                {`Level: ${characterLevel}`}
              </div>
              <div class="mdc-typography--subtitle2 text-center">
                {`Health: ${characterHealth}`}
              </div>
            </CardContent>
          </Card>
          <Card class="mt-2">
            <CardContent>
              <div class="mdc-typography--heading5 text-center">Inventory</div>
              <hr class="my-1" />
              <Textfield
                type="number"
                style="width: 100%;"
                label="Gold"
                bind:value={gold}
              />
              <div class="mdc-typography--subtitle2 text-center">
                {`Items: ${items.length}`}
              </div>
              <List nonInteractive>
                {#each items as item}
                  <Item>
                    <Text>
                      {item}
                    </Text>
                  </Item>
                {/each}
              </List>
            </CardContent>
          </Card>
          <Card class="mt-2">
            <CardContent>
              <div class="mdc-typography--heading5 text-center">Perks</div>
              <hr class="my-1" />
              <Textfield
                type="number"
                style="width: 100%;"
                label="Perk Points"
                bind:value={perkPoints}
              />
              <div class="mdc-typography--subtitle2 text-center">
                {`Applied Perks: ${appliedPerks.length}`}
              </div>
              <List nonInteractive>
                {#each appliedPerks as perk}
                  <Item>
                    <Text>
                      {perk}
                    </Text>
                  </Item>
                {/each}
              </List>
            </CardContent>
          </Card>
        {/if}
      {/if}
    </GhtPanel>
  </DialogContent>
  <DialogActions>
    <Button
      on:click={() => {
        open = false;
      }}
      color="secondary"
    >
      Cancel
    </Button>
    <Button on:click={handleSave} color="primary">
      {isNewCharacter ? "Add" : "Update"}
    </Button>
  </DialogActions>
</Dialog>
