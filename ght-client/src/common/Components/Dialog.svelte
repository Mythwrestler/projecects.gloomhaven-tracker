<script lang="ts">
  import clsx from "clsx";
  export let open = false;
  export let close: (() => void) | undefined = undefined;
  export let closeButtonLabel: unknown | undefined = undefined;

  let closeButtonType = "string";
  $: {
    if (closeButtonLabel) {
      closeButtonType = typeof closeButtonLabel;
    } else {
      closeButtonType = "string";
    }
  }
</script>

<div
  id="modal_overlay"
  class={clsx(
    "absolute inset-0 bg-black bg-opacity-30 h-screen w-full flex justify-center items-start md:items-center pt-10 md:pt-0",
    !open && "hidden -translate-y-full opacity-0 scale-150"
  )}
>
  <!-- Modal -->
  <div
    id="modal"
    class="pacity-0 transform scale-150 relative w-1/2 bg-white rounded shadow-lg transition-transform duration-300"
  >
    <!-- Modal Header -->
    <div>
      <div class="px-4 py-3 border-b border-gray-200">
        <h2 class="text-xl font-semibold text-gray-600">Title</h2>
      </div>

      <!-- button close -->
      {#if close !== undefined}
        <button
          on:click={close}
          class={clsx(
            "absolute -top-1 -right-1 ",
            closeButtonType === "string" &&
              "bg-gray-900 hover:bg-blue-900 rounded-full focus:outline-none text-gray-300 text-lg w-7 h-7",
            closeButtonType !== "string" &&
              "text-gray-900 flex w-8 h-8 items-center justify-center"
          )}
        >
          {#if closeButtonLabel !== undefined}
            {#if typeof closeButtonLabel !== "string"}
              <svelte:component this={closeButtonLabel} />
            {:else}
              <span>{closeButtonLabel}</span>
            {/if}
          {:else}
            &cross;
          {/if}
        </button>
      {/if}
    </div>
    <!-- Modal Content -->
    {#if $$slots.modalBody || $$slots.modalFooter}
      <!-- body -->
      <div class="flex flex-col">
        {#if $$slots.modalBody}
          <slot name="modalBody" />
        {/if}
        <!-- footer -->
        {#if $$slots.modalFooter}
          <slot name="modalFooter" />
        {/if}
      </div>
    {/if}
  </div>
</div>
