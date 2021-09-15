<script lang="ts">
  import Display from "./feature/Display/Display.svelte";

  import {
    Auth0Context,
    //   Auth0LoginButton,
    //   Auth0LogoutButton,
    //   authError,
    //   authToken,
    //   idToken,
    //   isAuthenticated,
    //   isLoading,
    //   login,
    //   logout,
    //   userInfo,
  } from "@dopry/svelte-auth0";

  import ENV_VARS from "./common/Environment";

  console.log(`AUTH_DOMAIN: ${ENV_VARS.AUTH.Domain()}`);
  console.log(`AUTH_CLIENT_ID: ${ENV_VARS.AUTH.ClientId()}`);
  console.log(`AUTH_API_AUDIENCE: ${ENV_VARS.AUTH.APIAudience()}`);
  console.log(`AUTH_ENABLED: ${ENV_VARS.AUTH.Enabled()}`);
  console.log(`CLIENT_BASE_URL: ${ENV_VARS.CLIENT.BaseURL()}`);
  console.log(`API_BASE_URL: ${ENV_VARS.API.BaseURL()}`);
</script>

<!-- <Display /> -->
<Auth0Context
  domain={ENV_VARS.AUTH.Domain()}
  client_id={ENV_VARS.AUTH.ClientId()}
  callback_url={ENV_VARS.CLIENT.BaseURL()}
  audience={ENV_VARS.AUTH.APIAudience()}
>
  <Display />
  <!-- <div>
    <pre>isLoading: {$isLoading}</pre>
    <pre>isAuthenticated: {$isAuthenticated}</pre>
    <pre>authToken: {$authToken}</pre>
    <pre>idToken: {$idToken}</pre>
    <pre>userInfo: {JSON.stringify($userInfo, null, 2)}</pre>
    <pre>authError: {$authError}</pre>
  </div> -->
</Auth0Context>

<style global>
  @tailwind base;
  @tailwind components;
  @tailwind utilities;
</style>
