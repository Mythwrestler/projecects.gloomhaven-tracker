import { writable } from "svelte/store";
import { User, UserManager, type Profile } from "oidc-client";

/**
 * Stores
 */
export const manualLogout = writable<boolean>(false);
export const isLoading = writable<boolean>(true);
export const isAuthenticated = writable<boolean>(false);
export const accessToken = writable<string | undefined>();
export const authToken = writable<string | undefined>();
export const idToken = writable<string | undefined>();
export const userInfo = writable<Profile | undefined>();
export const authError = writable<Error | undefined>();

/**
 * Context Keys
 *
 * using an object literal means the keys are guaranteed not to conflict in any circumstance (since an object only has
 * referential equality to itself, i.e. {} !== {} whereas "x" === "x"), even when you have multiple different contexts
 * operating across many component layers.
 */
export const OIDC_CONTEXT_CLIENT_PROMISE = {};
export const OIDC_CONTEXT_REDIRECT_URI = {};
export const OIDC_CONTEXT_POST_LOGOUT_REDIRECT_URI = {};

/**
 * Refresh the accessToken using the silentRenew method (hidden iframe)
 *
 * @param {Promise<UserManager>} oidcPromise
 * @return bool indicated whether the token was refreshed, if false error will be set
 * in the authError store.
 */

export async function refreshToken(oidcPromise: Promise<UserManager>) {
  try {
    const oidc = await oidcPromise;
    await oidc.signinSilent();
    return true;
  } catch (e) {
    // set error state for reactive handling
    authError.set(e as Error);
    return false;
  }
}

/**
 * Initiate Register/Login flow.
 *
 * @param {Promise<UserManager>} oidcPromise
 * @param {boolean} preserveRoute - store current location so callback handler will navigate back to it.
 * @param {string} callback_url - explicit path to use for the callback.
 */
export async function login(
  oidcPromise: Promise<UserManager>,
  preserveRoute = true,
  callback_url: string | null = null
) {
  const oidc = await oidcPromise;
  const redirect_uri = callback_url ?? window.location.href;

  // try to keep the user on the same page from which they triggered login. If set to false should typically
  // cause redirect to /.
  const appState = preserveRoute
    ? {
        pathname: window.location.pathname,
        search: window.location.search,
      }
    : {};
  await oidc.signinRedirect({ redirect_uri, appState });
}

/**
 * Log out the current user.
 *
 * @param {Promise<UserManager>} oidcPromise
 * @param {string} logout_url - specify the url to return to after login.
 */
export async function logout(
  oidcPromise: Promise<UserManager>,
  logout_url: string | null = null
) {
  const oidc = await oidcPromise;
  const user = await oidc.getUser();
  const returnTo = logout_url ?? window.location.href;
  await oidc.signoutRedirect({ id_token_hint: user?.id_token, returnTo });
}
