<script lang="ts">
  import {} from "os";
  import {
    Button,
    Dialog,
    DialogBody,
    DialogFooter,
    DialogHeader,
    DropDown,
    DropDownOption,
  } from "../../../common/Components";
  import TextField from "../../../common/Components/TextField/TextField.svelte";
  import { Character } from "../../../models";

  export let showCampaignCharacterDialog = false;
  export let isNewCharacter = false;
  export let selectedCharacter: Character;
  export let fullCharcterOptionList: DropDownOption[] = [];
  export let characterOptionsAlreadyUsed: string[] = [];
  export let handleCloseDialog: () => void;
  export let handleSave: () => void;

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
    <div class="flex flex-row pt-3">
      <TextField
        type="number"
        bind:value={selectedCharacter.experience}
        placeholderText=""
        displayLabel="Experience"
        border
      />
      <TextField
        type="number"
        bind:value={selectedCharacter.gold}
        placeholderText=""
        displayLabel="Gold"
        border
      />
    </div>
    <div class="pt-3">
      <div class="mx-full text-center text-l mb-3">Items</div>
      <div class="border-b-2 border-solid" />
    </div>

    <div class="flex flex-row pt-3">
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
  </DialogBody>
  <DialogFooter slot="DialogFooter">
    <div class="bg-white dark:bg-gray-700 w-full py-3 pl-3">
      <Button variant="filled" onClick={handleSave}
        >{isNewCharacter ? "Add" : "Update"}</Button
      >
    </div>
  </DialogFooter>
</Dialog>
