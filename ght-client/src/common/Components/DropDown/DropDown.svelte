<script lang="ts">
  import clsx from "clsx";

  import DownTickIcon from "../Icons/DownTickIcon.svelte";
  import { Option } from "../types";
  export let label = "";
  export let options: Option[] = [];
  export let placeHolder = "";
  export let variant: "rounded" | "square" = "square";
  export let selected: string | number = "";
  export let onSelection: ((value: string | number) => void) | undefined =
    undefined;

  const handleSelection = (
    event: Event & {
      currentTarget: EventTarget & HTMLSelectElement;
    }
  ) => {
    if (onSelection) onSelection(event.currentTarget.value);
  };
</script>

<div class="relative flex mx-auto h-12 max-w-md w-full">
  <div
    class="w-5 h-5 absolute right-3 inset-y-1/2 transform -translate-y-1/4 my-auto pointer-events-none"
  >
    <DownTickIcon />
  </div>
  <select
    bind:value={selected}
    class={clsx(
      "w-full border border-gray-300 text-gray-600 h-12 bg-white hover:border-gray-400 focus:outline-none pl-3 pr-7 appearance-none",
      variant === "rounded" && "rounded-full"
    )}
    on:change={handleSelection}
  >
    {#if placeHolder !== ""}
      <option value="">{placeHolder}</option>
    {/if}
    {#each options as optionToAdd}
      <option value={optionToAdd.value}>{optionToAdd.label}</option>
    {/each}
  </select>
</div>
