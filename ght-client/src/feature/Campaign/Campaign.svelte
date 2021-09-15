<script lang="ts">
    import { getContext, onDestroy, onMount } from "svelte";
    import {
        battleHubConnectionManager,
        //     battleHubConnected,
        battleHubConnection as battleHubClient,
        //     battleHubSend,
    } from "../../common/Sockets/BattleHub";
    // =import { isAuthenticated, auth0Client } from "../../common/Auth";
    import {
        AUTH0_CONTEXT_CLIENT_PROMISE,
        authToken,
        isAuthenticated,
    } from "@dopry/svelte-auth0";
    import { HubConnection } from "@microsoft/signalr";
    import Textbox from "../../common/Textbox/Textbox.svelte";
    import ENV_VARS from "../../common/Environment";

    // const messages: string[] = ["sample starting message"];

    let authenticated: boolean = false;
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

    // auth0Client.subscribe(async (client) => {
    //     console.log("attemptToSubscripbe");
    //     authenticated = await client.isAuthenticated();
    //     console.log(`authenticated ${authenticated}`);
    //     if (await client.isAuthenticated()) {
    //         client
    //             .getTokenSilently({
    //                 scope: "superuser",
    //                 audience: "Gloomhaven-tracker-service-local",
    //             })
    //             .then((token: string) => {
    //                 console.log(JSON.stringify(token));
    //                 let tokenForRequest = token;
    //                 console.log(tokenForRequest);
    //                 battleHubConnectionManager.createClient(tokenForRequest);
    //             });
    //     }
    // });

    battleHubClient.subscribe((hub) => {
        console.log("attempting to subscribe");
        battleHub = hub;
        if (!connected && battleHub !== undefined) {
            battleHubConnectionManager.startConnection(battleHub);
            connected = true;
        }
    });

    // battleHubConnected.subscribe((isConnected) => {
    //     connected = isConnected;
    // });

    let sendingUser: string = "";
    let messageToSend: string = "";
</script>

<section class="bg-gray-100 flex flex-row">
    {#if connected}
        <div class="flex justify-center items-center max-h-screen">
            <div
                class="flex max-w-sm w-full h-64 justify-center bg-white shadow-md rounded-lg overflow-hidden mx-auto flex flex-col p-5"
            >
                <h3 class="text-2xl font-bold mb-4">Messages To Send</h3>
                <!-- This is the input component -->
                <Textbox
                    textBoxName="userName"
                    displayLabel="User Name"
                    ariaLabel="user name"
                    bind:value={sendingUser}
                />
                <Textbox
                    textBoxName="message"
                    displayLabel="Message"
                    ariaLabel="message"
                    bind:value={messageToSend}
                />
                <!-- <button
                    on:click={() =>
                        battleHubSend.takeBattleActionAsync(battleHub, {
                            target: "f3a58114-6061-41f3-8e2b-58386ddabc32",
                            damage: 2,
                            source: "e9add3da-ea0c-46e7-864b-7c130fbad5a9",
                            effects: [],
                        })}
                    class="py-3 px-6 text-white rounded-lg bg-red-500 shadow-lg block md:inline-block"
                    >Attack</button
                >
                <button
                    on:click={() =>
                        battleHubSend.takeBattleActionAsync(battleHub, {
                            target: "fbb56e73-f8af-4fda-a50f-b393c2737323",
                            damage: 2,
                            source: "453ce292-0127-49be-ad25-59b8e0e7f84b",
                            effects: [],
                        })}
                    class="py-3 px-6 text-white rounded-lg bg-red-500 shadow-lg block md:inline-block"
                    >Move</button
                > -->
            </div>
        </div>
        <!-- <div class="flex flex-col justify-center items-center max-h-screen">
        <h3 class="text-2xl font-bold mb-4">Actions Recieved</h3>
        {#each $battleHub as action}
            <div><span>{action}</span></div>
        {/each}
    </div> -->
    {/if}
</section>
