declare module "@dopry/svelte-oidc" {
  import { SvelteComponentTyped } from "svelte";
  import { Readable } from "svelte/store";
  import oidcClient from "oidc-client";

  declare type OidcContextProps = {
    issuer: string;
    client_id: string;
    redirect_uri: string;
    post_logout_redirect_uri: string;
    extraOptions: unknown;
    scope?: string;
    /** The context key used to retrieve the Auth0 client in Svelte components. */
    OIDC_CONTEXT_CLIENT_PROMISE: object;
    /** The context key used to retrieve the Auth0 login callback URL in Svelte components. */
    OIDC_CONTEXT_CALLBACK_URL: object;
    /** The context key used to retrieve the Auth0 login callback URL in Svelte components. */
    OIDC_CONTEXT_LOGOUT_URL: object;
  };

  export class OidcContext extends SvelteComponentTyped<
    OidcContextProps,
    unknown,
    { default: unknown }
  > {}

  declare type LoginButtonProps = {
    callback_url?: string;
    preserveRoute?: string;
  };
  export class LoginButton extends SvelteComponentTyped<
    LoginButtonProps,
    unknown,
    { default: unknown }
  > {}

  declare type LogoutButtonProps = {
    logout_url?: string;
  };
  export class LogoutButton extends SvelteComponentTyped<
    LogoutButtonProps,
    unknown,
    { default: unknown }
  > {}

  export class RefreshTokenButton extends SvelteComponentTyped<> {}

  /** The Auth0 service loading status. True if Auth0 is still loading */
  export const isLoading: Readable<boolean>;
  /** The Auth0 user authentication status. True if the user is authenticated */
  export const isAuthenticated: Readable<boolean>;
  /** The user's Auth0 authentication token. */
  export const accessToken: Readable<string>;
  /** The user's Auth0 ID token claims, if available */
  export const idToken: Readable<string>;
  /** The authenticated user's info, decoded from the Auth0 ID token. */
  export const userInfo: Readable<any>;
  /** The last authentication error encountered. */
  export const authError: Readable<Error | null>;


  /**
   * Initiates the Auth0 login process.
   *
   * @param oidcPromise The Auth0 client used to initiate the login process.
   * @param preserveRoute Whether to return back to the URL of the page
   * where the login process was initiated from. Defaults to `true`.
   * @param callbackURL The URL that Auth0 will redirect back to after login.
   * Defaults to `window.location.href`.
   */
  export function login(
    oidcPromise: Promise<UserManager>,
    preserveRoute?: boolean,
    callbackURL?: string
  ): Promise<void>;

  /**
   * Logs out the given Auth0 client.
   *
   * @param oidcPromise The Auth0 client to logout.
   * @param logoutURL The URL that Auth0 will redirect back to after logout.
   * Defaults to `window.location.href`.
   */
  export function logout(
    oidcPromise: Promise<UserManager>,
    logoutURL?: string
  ): Promise<void>;

  /**
   * Refreshes the authentication token for the given client.
   *
   * @param oidcPromise The token's holder.
   * @return bool indicated whether the token was refreshed, if false error will be set
   * in the authError store.
   */
  export function refreshToken(
    oidcPromise: Promise<UserManager>
  ): Promise<bool>;
}
