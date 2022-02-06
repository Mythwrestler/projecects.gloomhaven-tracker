<script lang="ts">
  import clsx from "clsx";
  import { CloseIconOpen } from "../Icons";
  export let open = false;
  export let offClick = true;
  export let onClose: (() => void) | undefined = undefined;
  export let closeButtonIcon: unknown = undefined;
</script>

{#if open}
  <div
    class={clsx(
      "hidden fixed inset-0 bg-black bg-opacity-40 dark:bg-opacity-70 z-10",
      "lg:block"
    )}
    on:click={() => {
      if (offClick && onClose) onClose();
    }}
  />
  <div
    class={clsx(
      "fixed top-0 right-0 h-full w-full z-20 flex flex-col overflow-hidden",
      "bg-gray-100 dark:bg-gray-800",
      "lg:rounded-md lg:h-3/4 lg:w-1/2 lg:inset-1/2 lg:transform lg:-translate-y-1/2 lg:-translate-x-1/2"
    )}
  >
    <!-- button close -->
    {#if onClose !== undefined}
      <button
        on:click={() => {
          if (onClose) onClose();
        }}
        class={clsx(
          "absolute top-0 right-0 flex w-8 h-8 items-center justify-center",
          "text-gray-800 dark:text-gray-100"
        )}
      >
        {#if closeButtonIcon !== undefined}
          <svelte:component this={closeButtonIcon} />
        {:else}
          <CloseIconOpen />
        {/if}
      </button>
    {/if}
    <!-- Dialog Content -->
    {#if $$slots.DialogHeader || $$slots.DialogBody || $$slots.DialogFooter}
      <div class="flex flex-col h-full w-full">
        <!-- Dialog Header -->
        {#if $$slots.DialogHeader}
          <slot name="DialogHeader" />
        {/if}
        <!-- Dialog Body -->
        {#if $$slots.DialogBody}
          <slot name="DialogBody" />
        {/if}
        <!-- footer -->
        {#if $$slots.DialogFooter}
          <slot name="DialogFooter" />
        {/if}
      </div>
    {/if}
  </div>
{/if}
