<script lang="ts">
  import clsx from "clsx";
  import {
    CastleIcon,
    CompassIcon,
    GroupIcon,
    SemanticWebIcon,
  } from "../../../common/Components";
  import NavListItem from "./NavListItem.svelte";
  export let showNavMenu = false;
</script>

<nav
  class={clsx(
    "flex flex-col overflow-y-auto overflow-x-hidden h-full w-full absolute top-0 left-0 ",
    "bg-white dark:bg-gray-700 text-gray-700 dark:text-white",
    "lg:static lg:w-44 lg:pt-0",
    !showNavMenu && "hidden lg:block"
  )}
>
  <ul class="flex flex-col">
    <NavListItem
      path="/campaign"
      label="Campaign"
      icon={CastleIcon}
      onClick={() => {
        showNavMenu = false;
      }}
    />
    <NavListItem
      path="/party"
      label="Party"
      icon={GroupIcon}
      onClick={() => {
        showNavMenu = false;
      }}
    />
    <NavListItem
      path="/combat"
      label="Scenario"
      icon={CompassIcon}
      onClick={() => {
        showNavMenu = false;
      }}
    />
    <NavListItem
      path="/sampler"
      label="Share Component Examples"
      icon={SemanticWebIcon}
      onClick={() => {
        showNavMenu = false;
      }}
    />
  </ul>
</nav>
