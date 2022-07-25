<script lang="ts">
  import Display from "./feature/Display/Display.svelte";

  import { OidcContext } from "./common/Utils/OidcSvelteClient";

  import ENV_VARS from "./common/Environment";

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
  <Display />
</OidcContext>

<!-- </Auth0Context> -->
<style global>
  @tailwind base;
  @tailwind components;
  @tailwind utilities;
</style>
