import ENV_VARS from "../../../common/Environment";
import { Actors } from "../../../models/battle";
import { availableCombatSpaces } from "./Store";


export class CombatAPI {
    baseUrl = `${ENV_VARS.API.BaseURL()}api/combatspace/`;
    authToken:string | undefined;

    constructor(authToken: string | undefined){
        this.authToken = authToken
    }

    private Headers = () => {
        if(this.authToken) return
    }


    private Request = async <T>(url:string, method:"POST" | "GET", body:unknown = undefined): Promise<T> => {
        const headers:string[][] = [["Content-Type", "application/json"]]
        if(this.authToken != undefined) headers.push(["Bearer", this.authToken])

        const requestInit:RequestInit = {
            method: method,
            headers
        }

        if(body !== null && body !== undefined) requestInit.body = JSON.stringify(body)

        let response:Response

        try{
            response = await fetch(url, requestInit)
        } catch (reason:unknown) {
            throw new Error(`Failed to make ${method} request, Reason: ${JSON.stringify(reason)}`)
        }

        if(response.status >= 400)
            throw new Error(`Failed to ${method} resources. Response Status: ${response.status}`)

        if(response.status == 204 || response.status < 200)
            return {} as T;

        try {
            const jsonString = await response.text()
            if(jsonString.trim() == "") return {} as T;
            return JSON.parse(jsonString) as T
        } catch (error:unknown) {
            throw new Error(`Failed to parse response body. Response Status: ${response.status}`)
        }

    }

    private Post = async <T>(path:string, body:unknown = undefined): Promise<T> =>
    {
        return await this.Request<T>(`${this.baseUrl}${path}`, "POST", body);
    }

    private Get = async <T>(path:string): Promise<T> =>
    {
        return await this.Request<T>(`${this.baseUrl}${path}`, "GET");
    }

    public NewCombatSpace = async (): Promise<string | undefined> => {
        const result = await this.Post<string>("new")
        return result
    }

    public GetCombatSpaces = async (): Promise<void> => {
        const result = await this.Get<string[]>("");
        availableCombatSpaces.set(result ?? []);
    }

    public AddActors = async (combatId:string, actors:Actors):Promise<void> => {
        return await this.Post(`${combatId}/actors`, actors);
    }

}