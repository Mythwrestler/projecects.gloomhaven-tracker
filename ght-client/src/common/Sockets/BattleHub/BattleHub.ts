import { Writable, writable } from "svelte/store";
import * as signalR from "@microsoft/signalr";
import { BattleAction, BattleActionResult } from "../../../models/battle";


// export interface BattleAction {
//     action: "attack" | "move"
// }

export interface BattleHub {
    subscribe: Writable<string[]>["subscribe"],
    sendActionAsync: (action: BattleAction) => void
    close: () => void
}

let battleHubStore: BattleHub | undefined = undefined

const getBattleHubStore = () => {
    if(battleHubStore === undefined) {
        const { subscribe, update } = writable<string[]>([])

        const connection = new signalR.HubConnectionBuilder()
            .withUrl("http://localhost:5020/battle")
            .configureLogging(signalR.LogLevel.Trace)
            .build();

        connection.on(
            "battleActionReceived",
            (actionResult: BattleActionResult) => {
                console.log("Message Sending")
                update((a) => [...a, `Affect: ${actionResult.affect} | Affected: ${actionResult.affected}`])
            }
        );

        connection.start().catch(reason => console.log(reason)); 

        battleHubStore = {
            subscribe,
            sendActionAsync: async (action:BattleAction) => { 
                await connection.send("TakeBattleAction", action);
             },
            close: async () => {
                await connection.stop()
            }
        }
    }
    return battleHubStore;
}



export const battleHub: BattleHub = getBattleHubStore()