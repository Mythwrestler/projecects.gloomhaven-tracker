<script lang="ts">
  export let buttonLabel: unknown | undefined = undefined;
  export let onButtonClick: (() => void) | undefined = undefined;
  export let value: string;
  export let placeholderText: string;
</script>

<div class="flex items-center max-w-md mx-auto bg-white rounded-full">
  <div class="w-full">
    <input
      type="text"
      bind:value
      class="w-full px-4 py-1 text-gray-900 rounded-full focus:outline-none"
      placeholder={placeholderText}
    />
  </div>
  {#if buttonLabel !== undefined}
    <div>
      <button
        on:click={() => {
          if (onButtonClick) onButtonClick();
        }}
        class="flex items-center justify-center w-12 h-12 font-bold bg-gray-800 text-gray-300 rounded-r-full"
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
