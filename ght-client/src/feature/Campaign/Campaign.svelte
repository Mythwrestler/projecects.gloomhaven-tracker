<script lang="ts">
    import { onDestroy, onMount } from "svelte";
    import { messages } from "./messageStore";
    import { battleHub } from "../../common/Sockets/BattleHub/BattleHub";
    import * as signalR from "@microsoft/signalr";
    import Textbox from "../../common/Textbox/Textbox.svelte";

    // const messages: string[] = ["sample starting message"];

    let sendingUser: string = "";
    let messageToSend: string = "";

    let connection: signalR.HubConnection;

    async function sendMessage() {
        console.log("Send Clicked");
        console.log(JSON.stringify(connection));
        try {
            await connection.send("NewMessage", sendingUser, messageToSend);
            messageToSend = "";
        } catch (er) {
            console.error(er);
        }
    }

    onMount(async () => {
        connection = new signalR.HubConnectionBuilder()
            .withUrl("http://localhost:5020/chat")
            .configureLogging(signalR.LogLevel.Trace)
            .build();

        connection.on(
            "messageReceived",
            (username: string, message: string) => {
                let newMessage = `${username} | ${message}`;
                messages.update((m) => [...m, newMessage]);
            }
        );

        await connection.start();
    });

    onDestroy(async () => {
        await connection.stop();
    });
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
                on:click={sendMessage}
                class="py-3 px-6 text-white rounded-lg bg-red-500 shadow-lg block md:inline-block"
                >Send</button
            >
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
        <h3 class="text-2xl font-bold mb-4">Messages Recieved</h3>
        {#each $messages as message}
            <div><span>{message}</span></div>
        {/each}
        <h3 class="text-2xl font-bold mb-4">Actions Recieved</h3>
        {#each $battleHub as action}
            <div><span>{action}</span></div>
        {/each}
    </div>
</section>
