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
  import useContentService from "../../../Service/ContentService";

  export let gameCode = "";
  export let showCampaignCharacterDialog = false;
  export let isNewCharacter = false;
  export let selectedCharacter: Character;
  export let fullCharcterOptionList: DropDownOption[] = [];
  export let characterOptionsAlreadyUsed: string[] = [];
  export let handleCloseDialog: () => void;
  export let handleSave: () => void;

  const { actions: contentActions, state: contentState } = useContentService();
  const { getCharacterDefault } = contentActions;
  const { characterDefault } = contentState;

  const handleCharacterSelection = (characterCode: string) => {
    getCharacterDefault(gameCode, characterCode);
  };

  let characterContentCode: string;
  let experience: number;
  let gold: number;
  let name: string;
  let perkPoints: number;

  // Spread Selection
  const handleSpreadSelection = (character: Character) => {
    console.log(JSON.stringify(character));
    const {
      characterContentCode: ccc,
      experience: exp,
      gold: gld,
      name: nme,
      perkPoints: pkp,
    } = character;

    characterContentCode = ccc;
    experience = exp;
    gold = gld;
    name = nme;
    perkPoints = pkp;
  };

  $: handleSpreadSelection(selectedCharacter);
  $: if (characterContentCode) handleCharacterSelection(characterContentCode);

  let characterLevel = 0;
  let characterHealth = 0;
  const calculateChracterLevel = () => {
    characterLevel =
      $characterDefault?.baseStats.levels
        .sort((a, b) => (a.level < b.level ? 1 : -1))
        .find((lvl) => lvl.experience <= experience)?.level ?? 0;
    characterHealth =
      $characterDefault?.baseStats.health.find(
        (h) => h.level === characterLevel
      )?.health ?? 0;
  };
  $: if ($characterDefault && experience) calculateChracterLevel();

  let availableCharacterOptions: DropDownOption[] = [];
  const determineAvailableCharacterOptions = (
    fullOptions: DropDownOption[],
    usedOptions: string[]
  ) => {
    availableCharacterOptions = fullOptions.filter(
      (charOpt) => !usedOptions.includes(charOpt.value as string)
    );
  };

  const handleSaveClick = () => {
    selectedCharacter = {
      ...selectedCharacter,
      characterContentCode,
      gold,
      name,
      perkPoints,
      experience,
    };
    handleSave();
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
        bind:value={name}
        displayLabel="Character Name"
        placeholderText=""
        border
      />
    </div>
    <div class="pt-3">
      <DropDown
        label="Character Class"
        bind:selected={characterContentCode}
        placeHolder={isNewCharacter ? "Select a Chararter Class" : ""}
        options={isNewCharacter
          ? availableCharacterOptions
          : fullCharcterOptionList}
        disabled={!isNewCharacter}
      />
    </div>
    {#if $characterDefault}
      <div class="flex flex-col pt-3">
        <TextField
          type="number"
          bind:value={experience}
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
          bind:value={gold}
          placeholderText=""
          displayLabel="Gold"
          border
        />
        <TextField
          type="number"
          bind:value={perkPoints}
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
      <Button variant="filled" onClick={handleSaveClick}
        >{isNewCharacter ? "Add" : "Update"}</Button
      >
    </div>
  </DialogFooter>
</Dialog>
