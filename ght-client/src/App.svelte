<script lang="ts">
  import Display from "./feature/Display/Display.svelte";

  import { OidcContext } from "./common/Utils/OidcSvelteClient";

  import ENV_VARS from "./common/Environment";
</script>

<OidcContext
  issuer={ENV_VARS.AUTH.Domain()}
  client_id={ENV_VARS.AUTH.ClientId()}
  redirect_uri={ENV_VARS.CLIENT.BaseURL()}
  post_logout_redirect_uri={ENV_VARS.CLIENT.BaseURL()}
  extraOptions={{
    mergeClaims: true,
  }}
  scope={`${ENV_VARS.AUTH.APIAudience()} gloomhaven-tracker-user openid`}
>
  <Display />
</OidcContext>

<!-- </Auth0Context> -->
<style global>
  @tailwind base;
  @tailwind components;
  @tailwind utilities;
</style>
