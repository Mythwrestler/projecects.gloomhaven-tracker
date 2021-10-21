import { getAPI, postAPI } from "../../../common/Utils/API";
import { Actors, CombatSpaceSummary } from "../../../models";
import * as CombatStore from './Store'

export const getCombatSpaces = async (): Promise<void> => {
    try {
        CombatStore.requestCombatSpaces();
        const result = await getAPI<CombatSpaceSummary[]>("");
        CombatStore.requestCombatSpacesSuccess(result);
        console.log(result)
    } catch (err: unknown) {
        CombatStore.requestCombatSpacesFailure();
        CombatStore.showErrorMessage('Failed to get list of combat spaces');
    }
}

export const addCombatSpace = async (description: string): Promise<CombatSpaceSummary | undefined> => {
    try {
        const result = await postAPI<CombatSpaceSummary>("new", { description })
        CombatStore.requestNewCombatSpaceSuccess(result);
        return result
    } catch (err: unknown) {
        CombatStore.showErrorMessage('Failed to get new combatId');
    }
}

export const addActor = async (combatId: string, actors: Actors): Promise<void> => {
    try {
        return await postAPI(`${combatId}/actors`, actors);
    } catch (err: unknown) {
        CombatStore.showErrorMessage('Failed to add actors');
    }
}
