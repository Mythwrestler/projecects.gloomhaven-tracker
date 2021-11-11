<script lang="ts">
  import clsx from "clsx";
  import { useNavigate, useLocation } from "svelte-navigator";
  const navigate = useNavigate();
  const location = useLocation();

  export let path = "";
  export let label = "";
  export let onClick: (() => void) | undefined = undefined;
  export let icon: unknown = undefined;

  let currentPath = "";
  location.subscribe((loc) => {
    currentPath = loc.pathname;
  });

  const handleClick = () => {
    navigate(path);
    if (onClick) onClick();
  };
</script>

<li class="hover:bg-gray-100">
  <button
    on:click={handleClick}
    class={clsx(
      "relative flex flex-row items-center h-11 focus:outline-none border-l-4 border-transparent pr-6",
      "text-gray-600 hover:bg-gray-50 hover:text-gray-800 hover:border-indigo-500"
    )}
  >
    {#if icon}
      <span class="inline-flex justify-center items-center ml-4">
        <svelte:component this={icon} />
      </span>
    {/if}
    <span class="ml-2 text-sm tracking-wide truncate"> {label} </span>
  </button>
</li>
