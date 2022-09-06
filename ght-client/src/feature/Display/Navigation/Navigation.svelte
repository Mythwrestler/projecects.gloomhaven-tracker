<script lang="ts">
  import clsx from "clsx";
  import {
    CastleIcon,
    CompassIcon,
    GroupIcon,
    SemanticWebIcon,
  } from "../../../common/Components";
  import Button from "../../../common/Components/Button/Button.svelte";
  import NavListItem from "./NavListItem.svelte";
  import {
    isAuthenticated,
    logout,
    OIDC_CONTEXT_CLIENT_PROMISE,
    OIDC_CONTEXT_POST_LOGOUT_REDIRECT_URI,
  } from "@ci-lab/svelte-oidc-context";
  import { getContext } from "svelte";
  import { UserManager } from "oidc-client";

  export let showNavMenu = false;

  const oidcPromise = getContext<Promise<UserManager>>(
    OIDC_CONTEXT_CLIENT_PROMISE
  );

  let logout_url: string = getContext<string>(
    OIDC_CONTEXT_POST_LOGOUT_REDIRECT_URI
  );

  const handleLogoutClick = async () => {
    await logout(oidcPromise, logout_url);
  };
</script>

<nav
  class={clsx(
    "z-10 flex flex-col overflow-y-auto overflow-x-hidden h-full w-full absolute top-0 left-0",
    "bg-white dark:bg-gray-700 text-gray-700 dark:text-white",
    "lg:static lg:min-w lg:pt-0 lg:w-auto lg:z-0",
    !showNavMenu && "hidden lg:block lg:min-w-min"
  )}
>
  <div class={"relative h-full w-full"}>
    <ul class="flex flex-col">
      {#if !$isAuthenticated}
        <NavListItem
          path="/login"
          label="Login"
          icon={GroupIcon}
          onClick={() => {
            showNavMenu = false;
          }}
        />
      {/if}
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
    <div class="absolute bottom-0 right-0 w-full pb-3 pr-3 text-right">
      <Button
        variant="filled"
        color="gray"
        disabled={!$isAuthenticated}
        onClick={handleLogoutClick}
      >
        Logout
      </Button>
    </div>
  </div>
</nav>
