<script lang="ts">
  import clsx from "clsx";

  export let textBoxName = "textBox";
  export let displayLabel: string | undefined = undefined;
  export let buttonLabel: unknown | undefined = undefined;
  export let variant: "rounded" | "square" = "square";
  export let border = false;
  export let onButtonClick: (() => void) | undefined = undefined;
  export let onChange: (() => void) | undefined = undefined;
  export let onBlur: (() => void) | undefined = undefined;
  export let value: string | number;
  export let placeholderText: string;
  export let type: "text" | "number" = "text";
</script>

<div
  class={clsx(
    "flex items-center max-w-md h-12 mx-auto bg-white",
    variant === "rounded" && "rounded-full",
    variant === "square" && "rounded-md",
    border === true && "border-gray-400 border-opacity-50 border"
  )}
>
  <div class="w-full relative">
    {#if type === "text"}
      <input
        type="text"
        name={textBoxName}
        bind:value
        on:change={onChange}
        class={clsx(
          "w-full px-4 py-1 text-gray-900 focus:outline-none",
          variant === "rounded" && "rounded-full",
          variant === "square" && "rounded-sm"
        )}
        placeholder={displayLabel ? "" : placeholderText}
      />
    {:else if type === "number"}
      <input
        type="number"
        name={textBoxName}
        bind:value
        on:change={onChange}
        class={clsx(
          "w-full px-4 py-1 text-gray-900 focus:outline-none",
          variant === "rounded" && "rounded-full",
          variant === "square" && "rounded-sm"
        )}
        placeholder={displayLabel ? "" : placeholderText}
      />
    {/if}
    {#if displayLabel}
      <label
        for={textBoxName}
        class="absolute rounded-lg duration-300 ml-2 px-1 top-1 left-0 origin-0 text-gray-500 bg-white dark:bg-gray-700"
        >{displayLabel}</label
      >
    {/if}
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

<style>
  .-z-1 {
    z-index: -1;
  }

  .origin-0 {
    transform-origin: 0%;
  }

  input:focus ~ label,
  input:not(:placeholder-shown) ~ label,
  textarea:focus ~ label,
  textarea:not(:placeholder-shown) ~ label,
  select:focus ~ label,
  select:not([value=""]):valid ~ label {
    /* @apply transform; scale-75; -translate-y-6; */
    --tw-translate-x: 0;
    --tw-translate-y: 0;
    --tw-rotate: 0;
    --tw-skew-x: 0;
    --tw-skew-y: 0;
    transform: translateX(var(--tw-translate-x))
      translateY(var(--tw-translate-y)) rotate(var(--tw-rotate))
      skewX(var(--tw-skew-x)) skewY(var(--tw-skew-y)) scaleX(var(--tw-scale-x))
      scaleY(var(--tw-scale-y));
    --tw-scale-x: 0.75;
    --tw-scale-y: 0.75;
    --tw-translate-y: -1.5rem;
  }

  input:focus ~ label,
  select:focus ~ label {
    /* @apply text-black; left-0; */
    --tw-text-opacity: 1;
    color: rgba(0, 0, 0, var(--tw-text-opacity));
    left: 0px;
  }
</style>
