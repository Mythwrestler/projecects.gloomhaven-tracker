<script lang="ts">
  import clsx from "clsx";

  import { v4 as uuid } from "uuid";
  import { RadioOption } from "./types";
  export let groupId: string = uuid();
  export let options: RadioOption[] = [];
  export let value: string | number | undefined;
  export let onClick: (() => void) | undefined = undefined;
  export let centered = false;

  const handleClick = () => {
    if (onClick) onClick();
  };
</script>

<div class="flex flex-row flex-wrap mx-auto h-12 max-w-md w-full">
  {#each options as option}
    <label class={clsx("flex radio p-2 cursor-pointer", centered && "mx-auto")}>
      <input
        class="my-auto transform scale-125"
        type="radio"
        bind:group={value}
        name={groupId}
        value={option.value}
        on:click={handleClick}
      />
      <div class="title px-2">{option.label}</div>
    </label>
  {/each}
</div>
