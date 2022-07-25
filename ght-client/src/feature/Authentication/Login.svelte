<script lang="ts">
  import { UserManager, type Profile } from "oidc-client";

  import { getContext } from "svelte";
  import Button from "../../common/Components/Button/Button.svelte";

  import {
    isAuthenticated,
    isLoading,
    authError,
    accessToken,
    idToken,
    userInfo,
    login,
    OIDC_CONTEXT_CLIENT_PROMISE,
  } from "../../common/Utils/OidcSvelteClient";

  const oidcPromise = getContext<Promise<UserManager>>(
    OIDC_CONTEXT_CLIENT_PROMISE
  );

  const redirect_uri: string | null = window.location.toString();

  const handleLoginClick = async () => {
    await login(oidcPromise, true, redirect_uri);
  };

  let userFullName: string | undefined = undefined;
  userInfo.subscribe((userFromStore) => {
    userFullName = userFromStore?.name;
  });
</script>

<div
  class="relative mt-2 px-3 py-1 items-center max-w-md mx-auto bg-gray-100 dark:bg-gray-700 text-gray-600 dark:text-gray-400 rounded-md backdrop-blur-sm"
>
  <div aria-label="Available Scenarios" class="text-center text-xl">
    {$isAuthenticated
      ? `Welcom ${userFullName ?? "Player"}!`
      : "Login to Play!"}
  </div>
  <div class="border-b-2 border-solid" />
  {#if !$isAuthenticated}
    <div aria-label="Available Scenarios" class="text-center my-5">
      <Button
        variant="outlined"
        color="gray"
        disabled={$isAuthenticated}
        onClick={handleLoginClick}
      >
        Login with Keycloack
      </Button>
    </div>
    <div class="border-b-2 border-solid" />
  {/if}
  <div>
    <pre>isLoading: {$isLoading}</pre>
    <pre>isAuthenticated: {$isAuthenticated}</pre>
    <pre>accessToken: {`${$accessToken?.substring(1, 30) ?? ""}...`}</pre>
    <pre>idToken: {`${$idToken?.substring(1, 30) ?? ""}...`}</pre>
    <pre>userInfo: {JSON.stringify($userInfo, null, 2)}</pre>
    <pre>authError: {$authError}</pre>
  </div>
</div>
