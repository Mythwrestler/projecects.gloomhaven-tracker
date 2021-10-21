<script lang="ts">
    import { onMount } from "svelte";
    import AddContainedIcon from "../../common/Icons/AddContainedIcon.svelte";
    import CheckMarkIcon from "../../common/Icons/CheckMarkIcon.svelte";
    import CombatPanel from "./CombatPanel.svelte";
    import {
        getCombatSpaces,
        addCombatSpace,
        combatSpaces,
        combatHubConnected,
        combatSpaceConnected,
        joinCombatSpace,
        combatSpaceDisconnecting,
        leaveCombatSpace,
        combatSpaceId,
        combatHubConnecting,
    } from "./CombatTrackerService";

    export let isLeft: boolean = false;
    export let isRight: boolean = false;
    export let isCenter: boolean = false;
    export let onNext: () => void;
    export let onPrevious: () => void;
    export let disabled: boolean = true;

    let changeCombatSpace: string | undefined;

    const handleNewCombatSpaceClick = async () => {
        // await addCombatSpace();
    };

    const handleCombatSpaceSelect = async (combatId: string) => {
        if (!$combatSpaceConnected) {
            joinCombatSpace(combatId);
        } else {
            changeCombatSpace = combatId;
            leaveCombatSpace();
        }
    };

    combatSpaceDisconnecting.subscribe((disconnecting) => {
        // If changing combat spaces, dispatch join after disconnect completes.
        if (!disconnecting && !$combatSpaceConnected && changeCombatSpace) {
            joinCombatSpace(changeCombatSpace);
            changeCombatSpace = undefined;
        }
    });

    let showAddCombatSpaceTextField: boolean = false;
    let addCombatSpaceTextField: string = "";
    const handleAddClicked = () => {
        showAddCombatSpaceTextField = true;
    };
    const handleAddbuttonClicked = () => {
        showAddCombatSpaceTextField = false;
        addCombatSpace(addCombatSpaceTextField);
        addCombatSpaceTextField = "";
    };

    onMount(async () => {
        await getCombatSpaces();
    });
</script>

<CombatPanel {isLeft} {isRight} {isCenter}>
    <div>
        <button class="flex flex-row" on:click={handleAddClicked}>
            <div>Add New Combat Spaces</div>
            <AddContainedIcon />
        </button>
    </div>
    <div class="overflow-y-auto p-1">
        {#if showAddCombatSpaceTextField}
            <div
                class="flex items-center max-w-md mx-auto bg-white rounded-full"
            >
                <div class="w-full">
                    <input
                        type="text"
                        bind:value={addCombatSpaceTextField}
                        class="w-full px-4 py-1 text-gray-900 rounded-full focus:outline-none"
                        placeholder="New Combat Space Description"
                    />
                </div>
                <div>
                    <button
                        on:click={handleAddbuttonClicked}
                        class="flex items-center justify-center w-12 h-12 font-bold bg-gray-800 text-gray-300 rounded-r-full"
                    >
                        <CheckMarkIcon />
                    </button>
                </div>
            </div>
        {/if}
        {#each $combatSpaces as space}
            <div>
                <span class="font-serif font-light text-justify text-sm">
                    <button
                        disabled={$combatSpaceDisconnecting ||
                            $combatSpaceId === space.combatId}
                        class="px-2 py-2 text-justify rounded-md text-sm font-medium focus:outline-none focus:ring transition text-gray-600 hover:bg-gray-50 active:bg-gray-100 focus:ring-gray-300"
                        on:click={() => handleCombatSpaceSelect(space.combatId)}
                    >
                        {space.description}
                    </button>
                </span>
            </div>
        {/each}
    </div>
    <div class="flex flex-row">
        <button
            {disabled}
            class="w-1/2 py-2 text-center font-sans border-2 border-black bg-red-900 text-white"
            on:click={onPrevious}
        >
            Previous
        </button>
        <button
            disabled={disabled || !$combatHubConnected || $combatHubConnecting}
            class="w-1/2 py-2 text-center font-sans border-2 border-black bg-red-900 text-white"
            on:click={handleNewCombatSpaceClick}
        >
            Start New Combat
        </button>
        <button
            disabled={disabled ||
                !$combatSpaceConnected ||
                $combatSpaceDisconnecting}
            class="w-1/2 py-2 text-center font-sans border-2 border-black bg-red-900 text-white"
            on:click={() => {
                console.log("Next Clicked");
                onNext();
            }}
        >
            Next
        </button>
    </div>
</CombatPanel>
