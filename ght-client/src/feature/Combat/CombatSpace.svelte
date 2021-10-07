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
    import CombatPanel from "./CombatPanel.svelte";
    import NewCombat from "./NewCombat.svelte";
    import AddMonster from "./AddMonster.svelte";
    import StartCombat from "./StartCombat.svelte";
    import type { SvelteComponent } from "svelte/internal";

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

    // interface PanelDefiniton {
    //     position: PanelPosition;
    //     component: typeof StartCombat | typeof NewCombat | typeof AddMonster;
    // }

    // const newCombatPanel: PanelDefiniton = {
    //     position: "center",
    //     component: NewCombat,
    // };

    // const addMonsterPanel: PanelDefiniton = {
    //     position: "right",
    //     component: AddMonster,
    // };

    // const startCombatPanel: PanelDefiniton = {
    //     position: null,
    //     component: StartCombat,
    // };

    // const movePanelLeft = (panelPosition: PanelPosition): PanelPosition => {
    //     switch (panelPosition) {
    //         case "right":
    //             return "center";
    //         case "center":
    //             return "left";
    //         case "left":
    //             return null;
    //         default:
    //             return "right";
    //     }
    // };
    // const movePanelRight = (panelPosition: PanelPosition): PanelPosition => {
    //     switch (panelPosition) {
    //         case "left":
    //             return "center";
    //         case "center":
    //             return "right";
    //         case "right":
    //             return null;
    //         default:
    //             return "left";
    //     }
    // };

    // interface CombatPanelUsage {
    //     purpose: string;
    // }

    // interface Panels {
    //     component: typeof StartCombat | typeof NewCombat | typeof AddMonster;
    // }

    // const panels: (
    //     | typeof StartCombat
    //     | typeof NewCombat
    //     | typeof AddMonster
    // )[] = [StartCombat, NewCombat, AddMonster];

    // let currentPanel = 0;

    // const leftComponent = () => {
    //     if (currentPanel > 0) return panels[currentPanel - 1];
    // };
    // const centerComponent = () => panels[currentPanel];

    // const rightComponent = () => {
    //     if (currentPanel < panels.length) return panels[2];
    // };

    // const handleNextClick = () => {
    //     if (currentPanel + 1 < panels.length) currentPanel++;
    //     console.log(`currentPanel: ${currentPanel}`);
    // };

    // const handlePreviousClick = () => {
    //     if (currentPanel > 0) currentPanel--;
    //     console.log(`currentPanel: ${currentPanel}`);
    // };

    // interface PanelsOrder {
    //     [key:number]: PanelLabel
    // }
    // const panelsOrder:PanelsOrder = {
    //     [0]: "NewCombat",
    //     [1]: "AddMonster",
    //     [2]: "StartCombat"
    // }

    type PanelPosition = "left" | "center" | "right" | null;

    type PanelLabel = "NewCombat" | "AddMonster" | "StartCombat";

    interface PanelPositions {
        [key: string]: PanelPosition;
    }
    const panelPositions: PanelPositions = {
        ["NewCombat"]: "center",
        ["AddMonster"]: "right",
        ["StartCombat"]: null,
    };
    const panelOrder: Map<number, PanelLabel> = new Map<number, PanelLabel>();

    const centerIndex: number = 0
    const handleNext = () => {
        
    }
    const handlePrevious = () => {
        
    }



    onMount(async () => {
        if (!ENV_VARS.AUTH.Enabled()) CombatSpace.BuildAPI();
        panelOrder.set(0, "NewCombat");
        panelOrder.set(1, "AddMonster");
        panelOrder.set(2, "StartCombat");
    });

</script>

<section class="bg-gray-100 flex flex-col h-full">
    <div>APIs Are Ready: {$apisReady}</div>
    <div>Created Combat Space: {_combatId}</div>
    <div>Hub Is Connected: {$combatHubConnected}</div>
    <div>Joined Combat Space: {$combatSpaceConnected}</div>
    <div class="relative grid gap-6 mt-10 lg:grid-col-3 md:grid-col-2 h-full">
        <!-- {#if newCombatPanel.position != null}
            <NewCombat
                isLeft={newCombatPanel.position == "left"}
                isCenter={newCombatPanel.position == "center"}
                isRight={newCombatPanel.position == "right"}
                onNext={() => {}}
                onPrevious={() => {}}
            />
        {/if}
        {#if addMonsterPanel.position != null}
            <AddMonster
                isLeft={addMonsterPanel.position == "left"}
                isCenter={addMonsterPanel.position == "center"}
                isRight={addMonsterPanel.position == "right"}
                onNext={() => {}}
                onPrevious={() => {}}
            />
        {/if}
        {#if startCombatPanel.position != null}
            <StartCombat
                isLeft={startCombatPanel.position == "left"}
                isCenter={startCombatPanel.position == "center"}
                isRight={startCombatPanel.position == "right"}
                onNext={() => {}}
                onPrevious={() => {}}
            />
        {/if} -->
        <!-- <svelte:component
            this={leftComponent()}
            isLeft={true}
            onNext={handleNextClick}
            onPrevious={handlePreviousClick}
        />
        <svelte:component
            this={centerComponent()}
            isCenter={true}
            onNext={handleNextClick}
            onPrevious={handlePreviousClick}
            disabled={false}
        />
        <svelte:component
            this={rightComponent()}
            isRight={true}
            onNext={handleNextClick}
            onPrevious={handlePreviousClick}
        /> -->

        <!-- <StartCombat isLeft={true} />
        <NewCombat isCenter={true} />
        <AddMonster isRight={true} /> -->
        <!-- <CombatPanel isCenter={true}>
            <div>
                <h3 class="font-serif font-black top">This is a content box</h3>
            </div>
            <div class="flex flex-row">
                <button
                    class="w-1/2 py-2 text-center font-sans border-2 border-black bg-red-900 text-white"
                    >Previous</button
                >
                <button
                    class="w-1/2 py-2 text-center font-sans border-2 border-black bg-red-900 text-white"
                    >Next</button
                >
            </div></CombatPanel
        > -->
    </div>
</section>
