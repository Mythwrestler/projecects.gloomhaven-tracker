<script lang="ts">
    import CombatPanel from "./CombatPanel.svelte";
    import CombatSpace, {
        apisReady,
        combatHubConnected,
        combatSpaceConnected,
    } from "./CombatTracker";

    export let isLeft: boolean = false;
    export let isRight: boolean = false;
    export let isCenter: boolean = false;
    export let onNext: () => void;
    export let onPrevious: () => void;
    export let disabled: boolean = true;
</script>

<CombatPanel {isLeft} {isRight} {isCenter}>
    <span>Start Panel</span>
    <div class="flex flex-row">
        <button
            {disabled}
            class="w-1/2 py-2 text-center font-sans border-2 border-black bg-red-900 text-white"
            on:click={onPrevious}>Previous</button
        >
        <button
            {disabled}
            class="w-1/2 py-2 text-center font-sans border-2 border-black bg-red-900 text-white"
            on:click={onNext}>Next</button
        >
    </div>
</CombatPanel>
