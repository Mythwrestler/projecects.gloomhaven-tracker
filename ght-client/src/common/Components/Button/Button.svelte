<script lang="ts">
  import clsx from "clsx";
  export let onClick: (() => void) | undefined = undefined;
  export let disabled = false;
  export let variant: "outlined" | "open" | "filled" = "open";
  export let color: "red" | "blue" | "gray" = "gray";
</script>

<button
  class={clsx(
    "rounded-md",
    "px-3 py-1",
    variant === "open" && color === "gray" && "text-gray-800 hover:bg-gray-500",
    variant === "open" && color === "blue" && "text-blue-800 hover:bg-gray-500",
    variant === "open" && color === "red" && "text-red-800 hover:bg-gray-500",
    variant === "outlined" &&
      color === "gray" &&
      "border-2 border-gray-800 text-gray-800 hover:bg-gray-800 hover:text-gray-100",
    variant === "outlined" &&
      color === "blue" &&
      "border-2 border-blue-800 text-blue-800 hover:bg-blue-800 hover:text-gray-100",
    variant === "outlined" &&
      color === "red" &&
      "border-2 border-red-800 text-red-800 hover:bg-red-800 hover:text-gray-100",
    variant === "filled" &&
      color === "gray" &&
      "bg-gray-800  hover:bg-gray-500  text-white",
    variant === "filled" &&
      color === "blue" &&
      "bg-blue-800  hover:bg-gray-500  text-white",
    variant === "filled" &&
      color === "red" &&
      "bg-red-800  hover:bg-gray-500  text-white"
  )}
  on:click={disabled ? undefined : onClick}
>
  <slot />
</button>
