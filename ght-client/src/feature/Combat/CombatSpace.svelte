<script lang="ts">
    import { onMount } from "svelte";
    import { isAuthenticated } from "@dopry/svelte-auth0";
    import ENV_VARS from "../../common/Environment";
    import {
        buildCombatHub,
        combatHubConnected,
        combatSpaceConnected,
        combatSpaceId,
    } from "./CombatTrackerService";
    import CombatSpaceListing from "./CombatSpaceListing.svelte";
    import AddMonster from "./AddMonster.svelte";
    import StartCombat from "./StartCombat.svelte";

    type PanelPosition = "left" | "center" | "right" | null;

    type PanelLabel = "CombatSpaceListing" | "AddMonster" | "StartCombat";

    interface PanelPositions {
        [key: string]: PanelPosition;
    }
    const panelPositions: PanelPositions = {
        ["CombatSpaceListing"]: "center",
        ["AddMonster"]: "right",
        ["StartCombat"]: null,
    };
    const panelOrder: Map<number, PanelLabel> = new Map<number, PanelLabel>();

    let leftIndex: number | null = null;
    let centerIndex = 0;
    let rightIndex: number | null = 1;

    const movePanelLeft = (panelPosition: PanelPosition): PanelPosition => {
        switch (panelPosition) {
            case "right":
                return "center";
            case "center":
                return "left";
            case "left":
                return null;
            default:
                return "right";
        }
    };
    const movePanelRight = (panelPosition: PanelPosition): PanelPosition => {
        switch (panelPosition) {
            case "left":
                return "center";
            case "center":
                return "right";
            case "right":
                return null;
            default:
                return "left";
        }
    };

    const handleNext = () => {
        if (centerIndex + 1 >= panelOrder.size) return;

        let currentLeft = "";
        let currentCenter = "";
        let currentRight = "";
        let nextRight = "";

        currentLeft = panelOrder.get(centerIndex - 1) ?? "";
        currentCenter = panelOrder.get(centerIndex) ?? "";
        currentRight = panelOrder.get(centerIndex + 1) ?? "";
        nextRight = panelOrder.get(centerIndex + 2) ?? "";
        console.log(`${currentLeft} | ${currentCenter} | ${currentRight}`);
        panelPositions[currentLeft] = movePanelLeft(
            panelPositions[currentLeft]
        );
        panelPositions[currentCenter] = movePanelLeft(
            panelPositions[currentCenter]
        );
        panelPositions[currentRight] = movePanelLeft(
            panelPositions[currentRight]
        );
        panelPositions[nextRight] = movePanelLeft(panelPositions[nextRight]);
        centerIndex++;
    };

    const handlePrevious = () => {
        if (centerIndex - 1 < 0) return;

        let currentLeft = "";
        let currentCenter = "";
        let currentRight = "";
        let nextLeft = "";

        nextLeft = panelOrder.get(centerIndex - 2) ?? "";
        currentLeft = panelOrder.get(centerIndex - 1) ?? "";
        currentCenter = panelOrder.get(centerIndex) ?? "";
        currentRight = panelOrder.get(centerIndex + 1) ?? "";
        console.log(`${currentLeft} | ${currentCenter} | ${currentRight}`);
        panelPositions[currentLeft] = movePanelRight(
            panelPositions[currentLeft]
        );
        panelPositions[currentCenter] = movePanelRight(
            panelPositions[currentCenter]
        );
        panelPositions[currentRight] = movePanelRight(
            panelPositions[currentRight]
        );
        panelPositions[nextLeft] = movePanelRight(panelPositions[nextLeft]);
        centerIndex--;
    };

    isAuthenticated.subscribe((isAuthenticated) => {
        if (ENV_VARS.AUTH.Enabled() && isAuthenticated) {
            void (async () => {
                await buildCombatHub();
            })();
        }
    });

    onMount(async () => {
        if (!ENV_VARS.AUTH.Enabled()) await buildCombatHub();
        panelOrder.set(0, "CombatSpaceListing");
        panelOrder.set(1, "AddMonster");
        panelOrder.set(2, "StartCombat");
    });
</script>

<section class="bg-gray-100 flex flex-col h-full">
    <div>Hub Is Connected: {$combatHubConnected}</div>
    <div>Joined Combat Space: {$combatSpaceConnected}</div>
    <div>Combat Space Id: {$combatSpaceId}</div>
    <div class="relative grid gap-6 mt-10 lg:grid-col-3 md:grid-col-2 h-full">
        {#if panelPositions["CombatSpaceListing"] !== null}
            <CombatSpaceListing
                isLeft={panelPositions["CombatSpaceListing"] === "left"}
                isCenter={panelPositions["CombatSpaceListing"] === "center"}
                isRight={panelPositions["CombatSpaceListing"] === "right"}
                onNext={handleNext}
                onPrevious={handlePrevious}
                disabled={panelPositions["CombatSpaceListing"] !== "center"}
            />
        {/if}
        {#if panelPositions["AddMonster"] !== null}
            <AddMonster
                isLeft={panelPositions["AddMonster"] === "left"}
                isCenter={panelPositions["AddMonster"] === "center"}
                isRight={panelPositions["AddMonster"] === "right"}
                onNext={handleNext}
                onPrevious={handlePrevious}
                disabled={panelPositions["AddMonster"] !== "center"}
            />
        {/if}
        {#if panelPositions["StartCombat"] !== null}
            <StartCombat
                isLeft={panelPositions["StartCombat"] === "left"}
                isCenter={panelPositions["StartCombat"] === "center"}
                isRight={panelPositions["StartCombat"] === "right"}
                onNext={handleNext}
                onPrevious={handlePrevious}
                disabled={panelPositions["StartCombat"] !== "center"}
            />
        {/if}
    </div>
</section>
