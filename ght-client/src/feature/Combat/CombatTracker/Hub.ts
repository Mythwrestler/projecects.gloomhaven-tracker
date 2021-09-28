import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import ENV_VARS from "../../../common/Environment";

import { combatHubConnected, combatSpaceConnected } from "./Store"

export class CombatHub {

    url = `${ENV_VARS.API.BaseURL()}hub/combatspace`
    combatId = "";
    authToken:string | undefined;
    hub:HubConnection | undefined;

    constructor(combatId:string, authToken:string | undefined)
    {
        this.combatId = combatId;
        this.authToken = authToken;
    }

    public Start = async ():Promise<boolean> => {
        return await this.Build()
    }

    private Build = async ():Promise<boolean> => {

        this.hub = this.authToken !== undefined
                ? new HubConnectionBuilder()
                    .withUrl(this.url, { accessTokenFactory: () => this.authToken ?? "" })
                    .build()
                : new HubConnectionBuilder().withUrl(this.url).build();

        let success= false;
        try {
            success = await this.ConnectToHub();
            if(success) success = await this.JoinCombatSpace();
            return success
        } catch {
            return false;
        }
    }

    private ConnectToHub = async (): Promise<boolean> => {
        if(this.hub == undefined) return false;
        try {
            await this.hub.start();
            combatHubConnected.set(true);
            return true;
        } catch {
            combatHubConnected.set(false);
            return false;
        }
        return false;
    }

    private JoinCombatSpace = async (): Promise<boolean> => {
        if(this.hub == undefined) return false;
        try {
            await this.hub.send("joinCombatSpace", this.combatId);
            combatSpaceConnected.set(true);
            return true;
        } catch {
            combatSpaceConnected.set(false);
            return false
        }
    }

}