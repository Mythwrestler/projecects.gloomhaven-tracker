<script lang="ts">
    import clsx from "clsx";

    export let isCenter: boolean = false;
    export let isLeft: boolean = false;
    export let isRight: boolean = false;
</script>

<div
    class={clsx(
        "p-10 h-full lg:h-4/6 container sm:flex sm:flex-col md:rounded-xl md:border-4 border-gray-700 text-center bg-yellow-100",
        isCenter && "md:z-10 md:mx-auto",
        isLeft && "hidden md:absolute md:z-0 md:left-20 md:visible md:mt-10",
        isRight && "hidden md:absolute md:z-0 md:right-20 md:visible md:mt-10"
    )}
>
    <slot />
</div>

<style>
    div.container {
        max-width: 25rem;
    }
</style>
