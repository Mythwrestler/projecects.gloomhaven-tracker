<script lang="ts">
  import { onMount } from "svelte";

  import { TextField } from "../../common/Components";
  import CheckMarkIcon from "../../common/Components/Icons/CheckMarkIcon.svelte";
  import CombatPanel from "./CombatPanel.svelte";

  export let isLeft = false;
  export let isRight = false;
  export let isCenter = false;
  export let onNext: () => void;
  export let onPrevious: () => void;
  export let disabled = true;

  let playerName = "";
  let monsterName = "";
  const onAddPlayerClick = () => {
    console.log("Sexy AF Player");
  };
  const onAddMonsterClick = () => {
    console.log("Sexy AF Monster");
  };
  let objectName = "";
  const onAddObjectiveClick = () => {
    console.log("Sexy AF Ojbective");
  };

  onMount(() => {
    console.log("things!");
  });
</script>

<CombatPanel {isLeft} {isRight} {isCenter}>
  <div>
    <div class="flex flex-col">
      <div>
        <div>
          <h6>Players</h6>
        </div>
        <TextField
          placeholderText="New Player"
          bind:value={playerName}
          buttonLabel={CheckMarkIcon}
          onButtonClick={onAddPlayerClick}
        />
      </div>
      <TextField
        placeholderText="New Monster"
        bind:value={monsterName}
        buttonLabel={CheckMarkIcon}
        onButtonClick={onAddMonsterClick}
      />
      <TextField
        placeholderText="New Objective"
        bind:value={objectName}
        buttonLabel={CheckMarkIcon}
        onButtonClick={onAddObjectiveClick}
      />
    </div>
    <div class="flex flex-row">
      <button
        {disabled}
        class="w-1/2 py-2 text-center font-sans border-2 border-black bg-red-900 text-white"
        on:click={onPrevious}>Previous</button
      >
      <button
        {disabled}
        class="w-1/2 py-2 text-center font-sans border-2 border-black bg-red-900 text-white"
        on:click={onNext}>Next</button
      >
    </div>
  </div>
</CombatPanel>
