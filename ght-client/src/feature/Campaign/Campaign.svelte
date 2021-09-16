<script lang="ts">
    import { onMount } from "svelte";
    import {
        battleHubConnectionManager,
        battleHubConnection as battleHubClient,
        actionTracker,
        battleHubSend,
    } from "../../common/Sockets/BattleHub";

    import { authToken, isAuthenticated } from "@dopry/svelte-auth0";

    import { HubConnection } from "@microsoft/signalr";
    import ENV_VARS from "../../common/Environment";
    import { ACTOR_EFFECT_TYPES } from "../../models/battle";
    import { serverMessages } from "../../common/Sockets/BattleHub/BattleHubStore";

    let connected: boolean = false;
    let battleHub: HubConnection;

    isAuthenticated.subscribe((authenticated) => {
        if (authenticated) {
            console.log("Super Authenticated");
        }
    });

    authToken.subscribe((token) => {
        if (token !== "") {
            battleHubConnectionManager.createClient(token);
        }
    });

    onMount(() => {
        console.log(`This is the Env Vars: ${ENV_VARS.AUTH.Enabled()}`);
        if (!ENV_VARS.AUTH.Enabled()) {
            battleHubConnectionManager.createClient("");
        }
    });

    battleHubClient.subscribe((hub) => {
        console.log("attempting to subscribe");
        battleHub = hub;
        if (!connected && battleHub !== undefined) {
            battleHubConnectionManager.startConnection(battleHub);
            connected = true;
        }
    });
</script>

<section class="bg-gray-100 flex flex-row">
    {#if connected}
        <div class="flex justify-center items-center max-h-screen">
            <div
                class="flex max-w-sm w-full h-64 justify-center bg-white shadow-md rounded-lg overflow-hidden mx-auto flex flex-col p-5"
            >
                <button
                    on:click={() =>
                        battleHubSend.takeBattleActionAsync(battleHub, {
                            target: "f3a58114-6061-41f3-8e2b-58386ddabc32",
                            damage: 2,
                            source: "e9add3da-ea0c-46e7-864b-7c130fbad5a9",
                            effects: [
                                {
                                    type: ACTOR_EFFECT_TYPES.Stun,
                                    duration: 2,
                                    value: -1,
                                },
                            ],
                        })}
                    class="py-3 px-6 text-white rounded-lg bg-red-500 shadow-lg block md:inline-block"
                    >Stun Attack</button
                >
                <button
                    on:click={() =>
                        battleHubSend.takeBattleActionAsync(battleHub, {
                            target: "fbb56e73-f8af-4fda-a50f-b393c2737323",
                            damage: 4,
                            source: "453ce292-0127-49be-ad25-59b8e0e7f84b",
                            effects: [
                                {
                                    type: ACTOR_EFFECT_TYPES.Poison,
                                    duration: 2,
                                    value: 2,
                                },
                            ],
                        })}
                    class="py-3 px-6 text-white rounded-lg bg-red-500 shadow-lg block md:inline-block"
                    >Poision Attack</button
                >
            </div>
        </div>
        <div
            class="flex flex-auto flex-col justify-center items-center max-h-screen"
        >
            <h3 class="text-2xl font-bold mb-4">Actions Recieved</h3>
            {#each $actionTracker as action}
                <div><span>{JSON.stringify(action)}</span></div>
            {/each}
        </div>
        <div
            class="flex flex-1 flex-col justify-center items-center max-h-screen"
        >
            <h3 class="text-2xl font-bold mb-4">Server Messages Recieved</h3>
            {#each $serverMessages as message}
                <div><span>{message}</span></div>
            {/each}
        </div>
    {/if}
</section>
