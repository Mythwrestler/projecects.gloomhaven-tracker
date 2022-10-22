<script lang="ts">
  import { Title, Content } from "@smui/paper";
  import { UserManager } from "oidc-client";

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
  } from "@ci-lab/svelte-oidc-context";
  import GhtPanel from "../../common/Components/GHTPanel/GHTPanel.svelte";
  import { stubString, uniqueId } from "lodash";

  const oidcPromise = getContext<Promise<UserManager>>(
    OIDC_CONTEXT_CLIENT_PROMISE
  );

  const redirect_uri: string | null = window.location.toString();

  const handleLoginClick = async () => {
    await login(oidcPromise, true, redirect_uri);
  };

  let userFullName: string | undefined = undefined;
  let email: string | undefined = undefined;
  userInfo.subscribe((userFromStore) => {
    email = userFromStore?.email;
  });

  const shortString = (text: string | undefined): string => {
    if (text === undefined) return "";
    if (text.length < 15) return text;
    return `${text.substring(0, 15)}...`;
  };
</script>

<GhtPanel color="ght-panel">
  <Title class="w-auto text-center">
    {$isAuthenticated ? `Welcome "Player"!` : "Login to Play!"}
  </Title>
  <Content
    >{#if !$isAuthenticated}
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
      <pre>accessToken: {shortString($accessToken)}</pre>
      <pre>idToken: {shortString($idToken)}</pre>
      <pre>userInfo.email: {shortString($userInfo?.email)}</pre>
      <pre>authError: {$authError}</pre>
    </div>
  </Content>
</GhtPanel>
