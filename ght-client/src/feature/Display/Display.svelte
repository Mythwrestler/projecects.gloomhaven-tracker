<script lang="ts">
  import MediaQuery from "svelte-media-queries";
  import Main from "./Main/Main.svelte";
  import TopAppBar, {
    Row,
    Section,
    Title as AppBarTitle,
    AutoAdjust,
  } from "@smui/top-app-bar";

  import Drawer, { AppContent, Content, Scrim } from "@smui/drawer";

  import List, { Item, Text, Graphic, Separator } from "@smui/list";

  import IconButton from "@smui/icon-button";

  import {
    isAuthenticated,
    logout,
    OIDC_CONTEXT_CLIENT_PROMISE,
    OIDC_CONTEXT_POST_LOGOUT_REDIRECT_URI,
  } from "@ci-lab/svelte-oidc-context";

  import { getContext } from "svelte";
  import { UserManager } from "oidc-client";

  import { useNavigate } from "svelte-navigator";
  const navigate = useNavigate();

  const oidcPromise = getContext<Promise<UserManager>>(
    OIDC_CONTEXT_CLIENT_PROMISE
  );

  let logout_url: string = getContext<string>(
    OIDC_CONTEXT_POST_LOGOUT_REDIRECT_URI
  );

  const handleLogoutClick = async () => {
    navigate("/", { replace: true });
    await logout(oidcPromise, logout_url);
  };

  const handleSigninClick = () => {
    navigate("/", { replace: true });
  };

  let isMobile: boolean;
  const changeToFromMobile = (isMobile: boolean) => {
    if (!isMobile) {
      open = true;
    } else {
      open = false;
    }
  };

  $: changeToFromMobile(isMobile);

  let topAppBar: TopAppBar;
  let open = false;
</script>

<MediaQuery query="(max-width: 480px)" bind:matches={isMobile} />

<TopAppBar
  bind:this={topAppBar}
  variant="fixed"
  dense
  color="secondary"
  class="z-10"
>
  <Row>
    <Section>
      {#if isMobile}
        <IconButton
          class="material-icons"
          on:click={() => {
            open = !open;
          }}
        >
          menu
        </IconButton>
      {/if}
      <AppBarTitle class="mx-auto">Gloomhaven Tracker</AppBarTitle>
    </Section>
  </Row>
</TopAppBar>
<Drawer variant="modal" fixed={false} bind:open class="h-screen pt-11">
  <Content>
    <List>
      <Item
        on:SMUI:action={() => {
          if ($isAuthenticated) {
            void handleLogoutClick();
          } else {
            handleSigninClick();
          }
          if (isMobile) open = false;
        }}
      >
        <Graphic class="material-icons" aria-hidden="true">person</Graphic>
        <Text>{$isAuthenticated ? `Logout` : `Sign in`}</Text>
      </Item>
      <Separator />
      <Item
        on:SMUI:action={() => {
          navigate("/campaigns", { replace: true });
          if (isMobile) open = false;
        }}
        disabled={!$isAuthenticated}
      >
        <Graphic class="material-icons" aria-hidden="true">castle</Graphic>
        <Text>Campaigns</Text>
      </Item>
      <Item
        on:SMUI:action={() => {
          navigate("/combats", { replace: true });
          if (isMobile) open = false;
        }}
        disabled={!$isAuthenticated}
      >
        <Graphic
          class="material-icons"
          aria-hidden="true"
          on:SMUI:action={() => {
            if (isMobile) open = false;
          }}
        >
          military_tech
        </Graphic>
        <Text>Combats</Text>
      </Item>
    </List>
  </Content>
</Drawer>
{#if isMobile}
  <Scrim fixed={false} />
{/if}
<AppContent class={"app-content overflow-auto h-max"}>
  <AutoAdjust {topAppBar}>
    <Main />
  </AutoAdjust>
</AppContent>

<style>
  * :global(.app-content) {
    flex: auto;
    overflow: auto;
    position: relative;
    flex-grow: 1;
  }
</style>
