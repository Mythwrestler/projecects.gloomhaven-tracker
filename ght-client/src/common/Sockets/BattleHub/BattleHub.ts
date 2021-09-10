import { Writable, writable } from "svelte/store";
import * as signalR from "@microsoft/signalr";


export interface BattleAction {
    action: "attack" | "move"
}

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
            (battleAction: string) => {
                update(a => [ ...a, battleAction ])
            }
        );

        connection.start().catch(reason => console.log(reason)); 

        battleHubStore = {
            subscribe,
            sendActionAsync: async (action:BattleAction) => { 
                await connection.send("TakeBattleAction", action.action);
             },
            close: async () => {
                await connection.stop()
            }
        }
    }
    return battleHubStore;
}



export const battleHub: BattleHub = getBattleHubStore()