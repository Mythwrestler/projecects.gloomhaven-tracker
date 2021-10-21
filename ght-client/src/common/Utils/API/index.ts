import ENV_VARS from "../../Environment";
import { get } from "svelte/store"
import { authToken as authTokenStore } from "@dopry/svelte-auth0";

const baseUrl = `${ENV_VARS.API.BaseURL()}api/combatspace/`;

const request = async <T>(url:string, method:"POST" | "GET", body:unknown = undefined): Promise<T> => {
    const headers:string[][] = [["Content-Type", "application/json"]]
    const authToken = get(authTokenStore)
    if(authToken != undefined) headers.push(["Bearer", authToken])

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

export const postAPI = async <T>(path:string, body:unknown = undefined): Promise<T> => {
    return await request<T>(`${baseUrl}${path}`, "POST", body);
}

export const getAPI = async <T>(path:string): Promise<T> => {
    return await request<T>(`${baseUrl}${path}`, "GET");
}
