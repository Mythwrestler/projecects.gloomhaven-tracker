<script lang="ts">
  import clsx from "clsx";
  import {
    CastleIcon,
    CompassIcon,
    SemanticWebIcon,
  } from "../../../common/Components";
  import NavListItem from "./NavListItem.svelte";
  export let showNavMenu = false;
</script>

<nav
  class={clsx(
    "z-10 flex flex-col overflow-y-auto overflow-x-hidden h-full w-full absolute top-0 left-0",
    "bg-white dark:bg-gray-700 text-gray-700 dark:text-white",
    "lg:static lg:min-w lg:pt-0 lg:w-auto lg:z-0",
    !showNavMenu && "hidden lg:block lg:min-w-min"
  )}
>
  <ul class="flex flex-col">
    <NavListItem
      path="/campaigns"
      label="Campaigns"
      icon={CastleIcon}
      onClick={() => {
        showNavMenu = false;
      }}
    />
    <NavListItem
      path="/combats"
      label="Combat Scenarios"
      icon={CompassIcon}
      onClick={() => {
        showNavMenu = false;
      }}
    />
    <NavListItem
      path="/sampler"
      label="Shared Component Examples"
      icon={SemanticWebIcon}
      onClick={() => {
        showNavMenu = false;
      }}
    />
  </ul>
</nav>
