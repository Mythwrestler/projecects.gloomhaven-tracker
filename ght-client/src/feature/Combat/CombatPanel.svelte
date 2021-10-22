<script lang="ts">
    import clsx from "clsx";
    export let isCenter = false;
    export let isLeft = false;
    export let isRight = false;
</script>

<div
    class={clsx(
        "p-10 h-full w-full md:h-4/6 container md:flex md:flex-col md:rounded-xl md:border-4 border-gray-700 text-center bg-yellow-100",
        isCenter && "md:z-10 md:mx-auto",
        isLeft && "hidden md:absolute md:z-0 md:visible md:mt-10 md:right-2/3",
        isRight && "hidden md:absolute md:z-0 md:visible md:mt-10 md:left-2/3"
    )}
>
    <slot />
</div>

<style>
    div.container {
        max-width: 25rem;
    }
</style>
