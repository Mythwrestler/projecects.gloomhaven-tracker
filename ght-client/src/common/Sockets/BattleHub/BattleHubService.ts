
import { HubConnection, HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import { BattleAction, BattleActionResult } from "../../../models/battle";
import ENV_VARS from "../../Environment";
import { battleHubConnection, actionTracker, battleHubConnected } from "./BattleHubStore"

const createClient = (token: string):void => {

    const builder = new HubConnectionBuilder();
    const url = `${ENV_VARS.API.BaseURL()}battle`;

    ENV_VARS.AUTH.Enabled() ? builder.withUrl(url, {accessTokenFactory: () => token}) : builder.withUrl(url);


    const connection = builder
            .configureLogging(LogLevel.Trace)
            .build();

            connection.on(
            "battleActionReceived",
            (actionResult: BattleActionResult) => {
                const { update: setActionTracker } = actionTracker;
                console.log(`Message Received: ${JSON.stringify(actionResult)} `)
                setActionTracker((at) => [...at, actionResult]);
            }
        );

    battleHubConnection.update(() => connection);
}

const startConnection = (client: HubConnection): void => {
    const { update: setBattleHubConnected } = battleHubConnected
    client.start().then(() => {
        setBattleHubConnected(() => true);
    }).catch((reason) => {
        console.error(reason)
        setBattleHubConnected(() => false);
    })
}

const stopConnection = (client: HubConnection): void => {
    const { update: setBattleHubConnected } = battleHubConnected
    client.start().then(() => {
        setBattleHubConnected(() => false);
    }).catch((reason) => {
        console.error(reason)
        setBattleHubConnected(() => true);
    })
}

const takeBattleActionAsync = async (client: HubConnection, action: BattleAction): Promise<void> => {
    console.log(JSON.stringify(action))
    return await client.send("takeBattleAction", action)
}


export const battleHubConnectionManager = {
  createClient,
  startConnection,
  stopConnection,
};

export const battleHubSend = {
    takeBattleActionAsync
}