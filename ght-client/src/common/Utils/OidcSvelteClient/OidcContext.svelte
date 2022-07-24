<script lang="ts">
  import { onDestroy, onMount, setContext } from "svelte";
  import oidcClient from "oidc-client";
  import {
    OIDC_CONTEXT_CLIENT_PROMISE,
    OIDC_CONTEXT_POST_LOGOUT_REDIRECT_URI,
    OIDC_CONTEXT_REDIRECT_URI,
    refreshToken,
    accessToken,
    authError,
    idToken,
    isAuthenticated,
    isLoading,
    userInfo,
  } from "./oidc";

  // props.
  export let issuer: string;
  export let client_id: string;
  export let scope: string;

  // defaults to a build time speco
  export let redirect_uri = "";
  export let post_logout_redirect_uri = "";

  export let extraOptions: object = {};

  setContext(OIDC_CONTEXT_REDIRECT_URI, redirect_uri);
  setContext(OIDC_CONTEXT_POST_LOGOUT_REDIRECT_URI, post_logout_redirect_uri);

  const settings = {
    authority: issuer,
    client_id,
    redirect_uri,
    post_logout_redirect_uri,
    response_type: "code",
    scope,
    automaticSilentRenew: true,
    ...extraOptions,
  };

  const userManager = new oidcClient.UserManager(settings);
  userManager.events.addUserLoaded(function (user) {
    isAuthenticated.set(true);
    accessToken.set(user.access_token);
    idToken.set(user.id_token);
    userInfo.set(user.profile);
  });

  userManager.events.addUserUnloaded(function () {
    isAuthenticated.set(false);
    idToken.set(undefined);
    accessToken.set(undefined);
    userInfo.set(undefined);
  });

  userManager.events.addSilentRenewError(function (e) {
    authError.set(new Error(`SilentRenewError: ${e.message}`));
  });

  // does userManager needs to be wrapped in a promise? or is this a left over to maintain
  // symmetry with the @dopry/svelte-auth0 auth0 implementation
  let oidcPromise = Promise.resolve(userManager);
  setContext(OIDC_CONTEXT_CLIENT_PROMISE, oidcPromise);

  // Not all browsers support this, please program defensively!
  const params = new URLSearchParams(window.location.search);

  // Use 'error' and 'code' to test if the component is being executed as a part of a login callback. If we're not
  // running in a login callback, and the user isn't logged in, see if we can capture their existing session.
  if (!params.has("error") && !params.has("code") && !$isAuthenticated) {
    void refreshToken(oidcPromise);
  }

  async function handleOnMount() {
    // on run onMount after oidc
    const oidc = await oidcPromise;

    // Check if something went wrong during login redirect
    // and extract the error message
    if (params.has("error")) {
      authError.set(
        new Error(params.get("error_description") ?? "Unknown Error")
      );
    }

    // if code then login success
    if (params.has("code")) {
      // handle the callback
      const response = await oidc.signinCallback();
      let state: unknown = (response && response.state) || {};
      // Can be smart here and redirect to original path instead of root
      // eslint-disable-next-line @typescript-eslint/no-unsafe-assignment
      const url: string =
        // eslint-disable-next-line @typescript-eslint/no-unsafe-member-access
        state && (state as any).targetUrl
          ? // eslint-disable-next-line @typescript-eslint/no-unsafe-member-access
            (state as any).targetUrl
          : window.location.pathname;
      state = { ...(state as object), isRedirectCallback: true };

      // redirect to the last page we were on when login was configured if it was passed.
      history.replaceState(state, "", url);
      // location.href = url;
      // clear errors on login.
      authError.set(undefined);
    }
    // if code was not set and there was a state, then we're in an auth callback and there was an error. We still
    // need to wrap the sign-in silent. We need to sit down and chart out the various success and fail scenarios and
    // what the uris loook like. I fear this may be problematic in other auth flows in the future.
    else if (params.has("state")) {
      const response = await oidc.signinCallback();
      console.log("oidc.signinCallback::response", response);
    }
    isLoading.set(false);
  }
  // eslint-disable-next-line @typescript-eslint/no-empty-function
  async function handleOnDestroy() {}

  onMount(handleOnMount);
  onDestroy(handleOnDestroy);
</script>

<slot />
