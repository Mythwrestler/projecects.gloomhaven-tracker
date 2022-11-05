<script lang="ts">
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
    await logout(oidcPromise, logout_url);
  };

  const handleSigninClick = () => {
    navigate("/", { replace: true });
  };

  let topAppBar: TopAppBar;
  let open = false;
</script>

<Drawer variant="modal" fixed bind:open class="h-screen">
  <Content>
    <List>
      <Item
        on:SMUI:action={() => {
          console.log($isAuthenticated);
          if ($isAuthenticated) {
            void handleLogoutClick();
          } else {
            handleSigninClick();
          }
        }}
      >
        <Graphic class="material-icons" aria-hidden="true">person</Graphic>
        <Text>{$isAuthenticated ? `Logout` : `Sign in`}</Text>
      </Item>
      <Separator />
      <Item
        on:SMUI:action={() => {
          navigate("/campaigns", { replace: true });
        }}
      >
        <Graphic class="material-icons" aria-hidden="true">castle</Graphic>
        <Text>Campaigns</Text>
      </Item>
      <Item
        on:SMUI:action={() => {
          navigate("/combats", { replace: true });
        }}
      >
        <Graphic class="material-icons" aria-hidden="true">
          military_tech
        </Graphic>
        <Text>Combats</Text>
      </Item>
    </List>
  </Content>
</Drawer>
<Scrim fixed />
<AppContent class="app-content overflow-auto h-max">
  <TopAppBar bind:this={topAppBar} variant="fixed" dense color="secondary">
    <Row>
      <Section>
        <IconButton
          class="material-icons"
          on:click={() => {
            open = !open;
          }}
        >
          menu
        </IconButton>
        <AppBarTitle class="mx-auto">Gloomhaven Tracker</AppBarTitle>
      </Section>
    </Row>
  </TopAppBar>
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
