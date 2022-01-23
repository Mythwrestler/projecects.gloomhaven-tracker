import { Writable, writable } from "svelte/store";

// Global Error Messaging
export const displayErrorMessage: Writable<boolean> = writable<boolean>(false);
export const errorMessage: Writable<string> = writable<string>("");
export const showErrorMessage = (message: string): void => {
  displayErrorMessage.set(true);
  errorMessage.set(message);
};
export const clearErrorMessage = (): void => {
  displayErrorMessage.set(false);
  errorMessage.set("");
};
