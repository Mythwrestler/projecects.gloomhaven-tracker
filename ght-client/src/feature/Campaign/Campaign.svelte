<script lang="ts">
    import { onDestroy, onMount } from "svelte";
    import { battleHub } from "../../common/Sockets/BattleHub/BattleHub";
    import * as signalR from "@microsoft/signalr";
    import Textbox from "../../common/Textbox/Textbox.svelte";

    // const messages: string[] = ["sample starting message"];

    let sendingUser: string = "";
    let messageToSend: string = "";
</script>

<section class="bg-gray-100 flex flex-row">
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
            <button
                on:click={() => battleHub.sendActionAsync({ action: "attack" })}
                class="py-3 px-6 text-white rounded-lg bg-red-500 shadow-lg block md:inline-block"
                >Attack</button
            >
            <button
                on:click={() => battleHub.sendActionAsync({ action: "move" })}
                class="py-3 px-6 text-white rounded-lg bg-red-500 shadow-lg block md:inline-block"
                >Move</button
            >
        </div>
    </div>
    <div class="flex flex-col justify-center items-center max-h-screen">
        <h3 class="text-2xl font-bold mb-4">Actions Recieved</h3>
        {#each $battleHub as action}
            <div><span>{action}</span></div>
        {/each}
    </div>
</section>
