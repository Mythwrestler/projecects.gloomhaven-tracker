<script lang="ts">
    import { stringify } from "querystring";
    import { onMount } from "svelte";
    import { authToken, isAuthenticated } from "@dopry/svelte-auth0";
    import ENV_VARS from "../../common/Environment";

    // import { ACTOR_EFFECT_TYPES } from "../../models/battle";

    import CombatSpace, {
        apisReady,
        combatHubConnected,
        combatSpaceConnected,
    } from "./CombatTracker";

    let _combatId: string = "";

    isAuthenticated.subscribe((isAuthenticated) => {
        if (ENV_VARS.AUTH.Enabled() && isAuthenticated)
            CombatSpace.BuildAPI($authToken);
    });

    apisReady.subscribe(async (apisReady) => {
        if (apisReady) {
            let combatId = await CombatSpace.NewCombatSpace();
            _combatId = combatId ?? "";
        }
        if (_combatId != "")
            await CombatSpace.StartHub(
                _combatId,
                ENV_VARS.AUTH.Enabled() ? $authToken : undefined
            );
    });

    onMount(async () => {
        if (!ENV_VARS.AUTH.Enabled()) CombatSpace.BuildAPI();
    });
</script>

<section class="bg-gray-100 flex flex-col">
    <div>APIs Are Ready: {$apisReady}</div>
    <div>Created Combat Space: {_combatId}</div>
    <div>Hub Is Connected: {$combatHubConnected}</div>
    <div>Joined Combat Space: {$combatSpaceConnected}</div>
</section>
