<script lang="ts">
  import { Route, useLocation, type NavigatorLocation } from "svelte-navigator";
  import Campaigns from "../../CampaignListing/CampaignListing/Campaigns.svelte";
  import Sampler from "../../Sampler/Sampler.svelte";
  import CampaignDetails from "../../CampaignListing/CampaignDetails/CampaignDetails.svelte";
  import CombatListing from "../../CombatListing/Combats.svelte";
  import CombatDetails from "../../CombatListing/CombatDetails.svelte";

  import {
    accessToken,
    isAuthenticated,
    isLoading,
    login,
    OIDC_CONTEXT_CLIENT_PROMISE,
    OIDC_CONTEXT_POST_LOGOUT_REDIRECT_URI,
  } from "@ci-lab/svelte-oidc-context";
  import type { UserManager } from "oidc-client";

  import { getContext, onMount } from "svelte";
  import Login from "../../Authentication/Login.svelte";
  import Logout from "../../Authentication/Logout.svelte";
  import ActiveCombat from "../../ActiveCombat/ActiveCombat.svelte";

  const location = useLocation();

  const redirect_uri: string | null = window.location.toString();

  const oidcPromise = getContext<Promise<UserManager>>(
    OIDC_CONTEXT_CLIENT_PROMISE
  );

  const autoLogin = async (
    isAuthenticated: boolean,
    isLoading: boolean,
    location: NavigatorLocation
  ) => {
    if (
      location.pathname == "/" ||
      location.pathname == "/login" ||
      location.pathname == "/logout"
    )
      return;
    if (!isAuthenticated && !isLoading)
      await login(oidcPromise, true, redirect_uri);
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
  <Route path="/"><Login /></Route>
  <Route path="/login"><Login /></Route>
  <Route path="/sampler"><Sampler /></Route>
  <Route path="/logout"><Logout /></Route>
  {#if $accessToken}
    <Route path="/campaigns"><Campaigns /></Route>
    <Route path="/campaigns/:id" let:params>
      <CampaignDetails campaignId={params.id} />
    </Route>
    <Route path="/combats"><CombatListing /></Route>
    <Route path="/combats/:id" let:params>
      <CombatDetails combatId={params.id} />
    </Route>
    <Route path="/combats/fight" let:params>
      <ActiveCombat />
    </Route>
  {/if}
</main>
