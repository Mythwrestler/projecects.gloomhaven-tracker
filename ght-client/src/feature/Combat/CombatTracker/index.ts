import { CombatHub } from './Hub'
import { CombatAPI } from './API'
import { Actors } from '../../../models/battle';
import { apisReady } from './Store';

let hub:CombatHub;
let api:CombatAPI;

const StartHub = async (combatId:string, authToken:string | undefined): Promise<void> => {
    hub = new CombatHub(combatId, authToken);
    await hub.Start();
}

const BuildAPI = (authToken:string | undefined = undefined): void => {
    api = new CombatAPI(authToken);
    apisReady.set(true);
}

const CombatSpace = {
    BuildAPI,

    // Api Calls
    NewCombatSpace: async ():Promise<string | undefined> => await api.NewCombatSpace(),
    GetCombatSpaces: async ():Promise<void> => await api.GetCombatSpaces(),
    AddActors: async (combatId:string, actors:Actors): Promise<void> => await api.AddActors(combatId, actors),

    StartHub,
}


export * from './Store'
export default CombatSpace
