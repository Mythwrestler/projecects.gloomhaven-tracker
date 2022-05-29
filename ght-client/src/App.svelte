<script lang="ts">
  import Display from "./feature/Display/Display.svelte";

  import {
    OidcContext,
    LoginButton,
    LogoutButton,
    RefreshTokenButton,
    authError,
    accessToken,
    idToken,
    isAuthenticated,
    isLoading,
    login,
    logout,
    userInfo,
  } from "@dopry/svelte-oidc";

  // import {
  //   Auth0Context,
  //   Auth0LoginButton,
  //   Auth0LogoutButton,
  //   authError,
  //   authToken,
  //   idToken,
  //   isAuthenticated,
  //   isLoading,
  //   //login,
  //   // logout,
  //   userInfo,
  // } from "@dopry/svelte-auth0";

  import ENV_VARS from "./common/Environment";

  // const metadata = {
  //   // added to overcome missing value in auth0 .well-known/openid-configuration
  //   // see: https://github.com/IdentityModel/oidc-client-js/issues/1067
  //   // see: https://github.com/IdentityModel/oidc-client-js/pull/1068
  //   end_session_endpoint: `process.env.OIDC_ISSUER/v2/logout?client_id=process.env.OIDC_CLIENT_ID`,
  // };

  console.log(`AUTH_DOMAIN: ${ENV_VARS.AUTH.Domain()}`);
  console.log(`AUTH_CLIENT_ID: ${ENV_VARS.AUTH.ClientId()}`);
  console.log(`AUTH_API_AUDIENCE: ${ENV_VARS.AUTH.APIAudience()}`);
  console.log(`AUTH_ENABLED: ${JSON.stringify(ENV_VARS.AUTH.Enabled())}`);
  console.log(`CLIENT_BASE_URL: ${ENV_VARS.CLIENT.BaseURL()}`);
  console.log(`API_BASE_URL: ${ENV_VARS.API.BaseURL()}`);
</script>

<!-- <Display /> -->
<!-- <Auth0Context
  domain={ENV_VARS.AUTH.Domain()}
  client_id={ENV_VARS.AUTH.ClientId()}
  callback_url={ENV_VARS.CLIENT.BaseURL()}
  audience={ENV_VARS.AUTH.APIAudience()}
> -->
<OidcContext
  issuer={ENV_VARS.AUTH.Domain()}
  client_id={ENV_VARS.AUTH.ClientId()}
  redirect_uri={ENV_VARS.CLIENT.BaseURL()}
  post_logout_redirect_uri={ENV_VARS.CLIENT.BaseURL()}
  extraOptions={{
    mergeClaims: true,
  }}
  scope={ENV_VARS.AUTH.APIAudience()}
>
  <Display />
  <div>
    <LoginButton>Login</LoginButton>
    <LogoutButton>Logout</LogoutButton>
    <RefreshTokenButton>RefreshToken</RefreshTokenButton><br />
    <pre>isLoading: {$isLoading}</pre>
    <pre>isAuthenticated: {$isAuthenticated}</pre>
    <pre>authToken: {$accessToken}</pre>
    <pre>idToken: {$idToken}</pre>
    <pre>userInfo: {JSON.stringify($userInfo, null, 2)}</pre>
    <pre>authError: {$authError}</pre>
    <!-- <Auth0LoginButton class="btn">Login</Auth0LoginButton>
    <Auth0LogoutButton class="btn">Logout</Auth0LogoutButton>
    <pre>isLoading: {$isLoading}</pre>
    <pre>isAuthenticated: {$isAuthenticated}</pre>
    <pre>authToken: {$authToken}</pre>
    <pre>idToken: {$idToken}</pre>
    <pre>userInfo: {JSON.stringify($userInfo, null, 2)}</pre>
    <pre>authError: {$authError}</pre> -->
  </div>
</OidcContext>

<!-- </Auth0Context> -->
<style global>
  @tailwind base;
  @tailwind components;
  @tailwind utilities;
</style>
