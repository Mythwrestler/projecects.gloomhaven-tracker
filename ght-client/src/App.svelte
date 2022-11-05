<script lang="ts">
  import Display from "./feature/Display/Display.svelte";

  import { OidcContext } from "@ci-lab/svelte-oidc-context";
  import { Router } from "svelte-navigator";

  import ENV_VARS from "./common/Environment";
  import ServiceContext from "./Service/ServiceContext.svelte";

  const post_logout_redirect_uri = `${ENV_VARS.CLIENT.BaseURL()}logout`;
  const scope = "openid gloomhaven-tracker-user gloomhaven-tracker-api";
</script>

<OidcContext
  issuer={ENV_VARS.AUTH.Domain()}
  client_id={ENV_VARS.AUTH.ClientId()}
  redirect_uri={ENV_VARS.CLIENT.BaseURL()}
  extraOptions={{
    mergeClaims: true,
  }}
  {post_logout_redirect_uri}
  {scope}
>
  <ServiceContext>
    <Router primary={false}>
      <Display />
    </Router>
  </ServiceContext>
</OidcContext>

<!-- </Auth0Context> -->
<style global>
  @tailwind base;
  @tailwind components;
  @tailwind utilities;
</style>
