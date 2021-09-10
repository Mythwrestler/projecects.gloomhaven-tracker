<script lang="ts">
    import { onDestroy, onMount } from "svelte";
    import * as signalR from "@microsoft/signalr";

    let messages: string[] = [];

    let connection: signalR.HubConnection;

    onMount(async () => {
        connection = new signalR.HubConnectionBuilder()
            .withUrl("http://localhost:5020/chat")
            .build();

        connection.on(
            "messageRecieved",
            (username: string, message: string) => {
                messages.push(`${username} | ${message}`);
            }
        );

        await connection.start();
    });

    onDestroy(async () => {
        await connection.stop();
    });

</script>

<section class="bg-gray-100">
    <h1 class="font-serif font-thin">Campaign</h1>
</section>
