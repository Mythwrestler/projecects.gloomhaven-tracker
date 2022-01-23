<script lang="ts">
  import clsx from "clsx";
  import { useNavigate } from "svelte-navigator";
  const navigate = useNavigate();
  export let label = "";
  export let path = "";
  const handleClick = () => {
    navigate(path);
  };
</script>

<button
  on:click={handleClick}
  class={clsx(
    "relative flex flex-row items-center h-11 focus:outline-none border-l-4 border-transparent pr-6 w-full",
    "text-gray-600 hover:text-blue-800"
  )}
>
  <span class="ml-2 text-sm tracking-wide hover:font-bold text-center"> {label} </span>
</button>
