<script lang="ts">
  import { Route, useLocation, type NavigatorLocation } from "svelte-navigator";
  import Campaigns from "../../CampaignListing/CampaignListing/Campaigns.svelte";
  import Sampler from "../../Sampler/Sampler.svelte";
  import CampaignDetails from "../../CampaignListing/CampaignDetails/CampaignDetails.svelte";
  import CombatListing from "../../CombatListing/Combats.svelte";
  import CombatDetails from "../../CombatListing/CombatDetails.svelte";

  import {
    isAuthenticated,
    isLoading,
    authError,
    accessToken,
    idToken,
    userInfo,
    login,
    logout,
    OIDC_CONTEXT_CLIENT_PROMISE,
    OIDC_CONTEXT_POST_LOGOUT_REDIRECT_URI,
  } from "../../../common/Utils/OidcSvelteClient";
  import type { UserManager } from "oidc-client";

  import { getContext, onMount } from "svelte";

  const location = useLocation();

  const redirect_uri: string | null = window.location.toString();

  const oidcPromise = getContext<Promise<UserManager>>(
    OIDC_CONTEXT_CLIENT_PROMISE
  );

  let logout_url: string = getContext<string>(
    OIDC_CONTEXT_POST_LOGOUT_REDIRECT_URI
  );

  const autoLogin = async (
    isAuthenticated: boolean,
    isLoading: boolean,
    location: NavigatorLocation
  ) => {
    if (location.pathname == "/") return;
    if (!isAuthenticated && !isLoading)
      await login(oidcPromise, true, redirect_uri);
    logout_url = getContext<string>(OIDC_CONTEXT_POST_LOGOUT_REDIRECT_URI);
  };

  onMount(() => {
    isAuthenticated.subscribe((isAuthenticated) => {
      void autoLogin(isAuthenticated, $isLoading, $location);
    });
    isLoading.subscribe((isLoading) => {
      void autoLogin($isAuthenticated, isLoading, $location);
    });
  });
</script>

<main class="w-full h-full">
  <Route path="/">
    <div>
      <div>
        <button
          class="btn"
          on:click|preventDefault={() => {
            void login(oidcPromise, true, redirect_uri);
          }}
        >
          Login
        </button>
        <button
          class="btn"
          on:click|preventDefault={() => {
            void logout(oidcPromise, logout_url);
          }}
        >
          Logout
        </button>
      </div>
      <pre>isLoading: {$isLoading}</pre>
      <pre>isAuthenticated: {$isAuthenticated}</pre>
      <pre>accessToken: {$accessToken}</pre>
      <pre>idToken: {$idToken}</pre>
      <pre>userInfo: {JSON.stringify($userInfo, null, 2)}</pre>
      <pre>authError: {$authError}</pre>
    </div>
  </Route>
  <Route path="/campaigns"><Campaigns /></Route>
  <Route path="/campaigns/:id" let:params>
    <CampaignDetails campaignId={params.id} />
  </Route>
  <Route path="/combats"><CombatListing /></Route>
  <Route path="/combats/:id" let:params>
    <CombatDetails combatId={params.id} />
  </Route>
  <Route path="/sampler"><Sampler /></Route>
</main>
