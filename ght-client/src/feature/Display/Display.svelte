<script lang="ts">
  import Main from "./Main/Main.svelte";
  import { Router } from "svelte-navigator";
  import Navigation from "./Navigation/Navigation.svelte";
  import { CloseIconOpen, MenuIcon } from "../../common/Components";
  import clsx from "clsx";

  let showNavMenu = false;
  const onNavOpen = () => {
    showNavMenu = true;
  };
  const onNavClose = () => {
    showNavMenu = false;
  };
</script>

<div class="h-screen w-screen flex flex-col relative">
  <header
    class={clsx(
      "text-center top-0 left-0 sticky",
      "bg-gray-300 dark:bg-gray-300",
      "lg:hidden"
    )}
  >
    <h6 class="p-2 text-gray-700 dark:text-gray-300">Gloom Haven Tracker</h6>
    <button
      class={"absolute top-1 left-1 text-gray-900 flex w-8 h-8 items-center justify-center"}
      on:click={() => (showNavMenu ? onNavClose() : onNavOpen())}
    >
      {#if showNavMenu}
        <CloseIconOpen />
      {:else}
        <MenuIcon />
      {/if}
    </button>
  </header>
  <Router primary={false}>
    <content
      class={clsx(
        "overflow-y-auto w-full h-full relative bg-gray-200",
        "lg:pt-0 lg:flex lg:flex-row"
      )}
    >
      <Navigation bind:showNavMenu />
      <Main />
    </content>
  </Router>
</div>
