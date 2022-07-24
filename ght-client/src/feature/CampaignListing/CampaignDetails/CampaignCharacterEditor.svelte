<script lang="ts">
  import {
    Button,
    Dialog,
    DialogBody,
    DialogFooter,
    DialogHeader,
    DropDown,
    TextField,
  } from "../../../common/Components";
  import type { DropDownOption } from "../../../common/Components";
  import type { Character } from "../../../models/Campaign";
  import * as ContentModel from "../../../models/Content";

  import { accessToken } from "../../../common/Utils/OidcSvelteClient";
  import { useContentService } from "../../../Service/ContentService";

  export let gameCode = "";
  export let showCampaignCharacterDialog = false;
  export let isNewCharacter = false;
  export let selectedCharacter: Character;
  export let fullCharcterOptionList: DropDownOption[] = [];
  export let characterOptionsAlreadyUsed: string[] = [];
  export let handleCloseDialog: () => void;
  export let handleSave: () => void;

  const contentService = useContentService(accessToken);

  let characterDetails: ContentModel.Character | undefined;

  const handleCharacterSelection = async (characterCode: string) => {
    characterDetails = await contentService.GetCharacterDefault(
      gameCode,
      characterCode
    );
  };
  $: if (selectedCharacter?.characterContentCode)
    void handleCharacterSelection(selectedCharacter.characterContentCode);

  let characterLevel = 0;
  let characterHealth = 0;
  const calculateChracterLevel = () => {
    characterLevel =
      characterDetails?.baseStats.levels
        .sort((a, b) => (a.level < b.level ? 1 : -1))
        .find((lvl) => lvl.experience <= selectedCharacter.experience)?.level ??
      0;
    characterHealth =
      characterDetails?.baseStats.health.find((h) => h.level === characterLevel)
        ?.health ?? 0;
  };
  $: if (characterDetails && selectedCharacter.experience)
    calculateChracterLevel();

  let availableCharacterOptions: DropDownOption[] = [];
  const determineAvailableCharacterOptions = (
    fullOptions: DropDownOption[],
    usedOptions: string[]
  ) => {
    availableCharacterOptions = fullOptions.filter(
      (charOpt) => !usedOptions.includes(charOpt.value as string)
    );
  };

  $: determineAvailableCharacterOptions(
    fullCharcterOptionList,
    characterOptionsAlreadyUsed
  );
</script>

<Dialog offClick open={showCampaignCharacterDialog} onClose={handleCloseDialog}>
  <DialogHeader slot="DialogHeader">
    <div class="mx-full text-center text-2xl mb-3">
      {`${isNewCharacter ? "Add" : "Edit"} Character`}
    </div>
    <div class="border-b-2 border-solid" />
  </DialogHeader>
  <DialogBody slot="DialogBody">
    <div class="pt-3">
      <TextField
        type="text"
        bind:value={selectedCharacter.name}
        displayLabel="Character Name"
        placeholderText=""
        border
      />
    </div>
    <div class="pt-3">
      <DropDown
        label="Character Class"
        bind:selected={selectedCharacter.characterContentCode}
        placeHolder={isNewCharacter ? "Select a Chararter Class" : ""}
        options={isNewCharacter
          ? availableCharacterOptions
          : fullCharcterOptionList}
        disabled={!isNewCharacter}
      />
    </div>
    {#if characterDetails}
      <div class="flex flex-col pt-3">
        <TextField
          type="number"
          bind:value={selectedCharacter.experience}
          placeholderText=""
          displayLabel="Experience"
          border
        />
        <div class="max-w-md mx-auto">
          Character Level: {characterLevel}
        </div>
        <div class="max-w-md mx-auto">
          Character Health: {characterHealth}
        </div>
      </div>
      <div class="pt-3">
        <div class="mx-full text-center text-l mb-3">Items</div>
        <div class="border-b-2 border-solid" />
      </div>
      <div class="flex flex-row pt-3">
        <TextField
          type="number"
          bind:value={selectedCharacter.gold}
          placeholderText=""
          displayLabel="Gold"
          border
        />
        <TextField
          type="number"
          bind:value={selectedCharacter.perkPoints}
          placeholderText=""
          displayLabel="Perk Points"
          border
        />
      </div>
      <div class="pt-3">
        <div class="mx-full text-center text-l mb-3">Applied Perks</div>
        <div class="border-b-2 border-solid" />
      </div>
    {/if}
  </DialogBody>
  <DialogFooter slot="DialogFooter">
    <div class="bg-white dark:bg-gray-700 w-full py-3 pl-3">
      <Button variant="filled" onClick={handleSave}
        >{isNewCharacter ? "Add" : "Update"}</Button
      >
    </div>
  </DialogFooter>
</Dialog>
