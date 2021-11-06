<script lang="ts">
  import clsx from "clsx";

  export let buttonLabel: unknown | undefined = undefined;
  export let variant: "rounded" | "square" = "square";
  export let border = false;
  export let onButtonClick: (() => void) | undefined = undefined;
  export let value: string;
  export let placeholderText: string;
</script>

<div
  class={clsx(
    "flex items-center max-w-md h-12 mx-auto bg-white",
    variant === "rounded" && "rounded-full",
    variant === "square" && "rounded-md",
    border === true && "border-gray-400 border-opacity-50 border"
  )}
>
  <div class="w-full">
    <input
      type="text"
      bind:value
      class={clsx(
        "w-full px-4 py-1 text-gray-900 focus:outline-none",
        variant === "rounded" && "rounded-full",
        variant === "square" && "rounded-sm"
      )}
      placeholder={placeholderText}
    />
  </div>
  {#if buttonLabel !== undefined}
    <div>
      <button
        on:click={() => {
          if (onButtonClick) onButtonClick();
        }}
        class={clsx(
          "flex items-center justify-center w-12 h-12 font-bold bg-gray-800 text-gray-300",
          variant === "rounded" && "rounded-r-full",
          variant === "square" && "rounded-r-sm"
        )}
      >
        {#if typeof buttonLabel !== "string"}
          <svelte:component this={buttonLabel} />
        {:else}
          <span>{buttonLabel}</span>
        {/if}
      </button>
    </div>
  {/if}
</div>
