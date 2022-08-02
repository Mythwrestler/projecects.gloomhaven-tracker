import type { AttackModifier } from "../Content/AttackModifier";

export interface AttackModifierDeck {
    drawPileCount: number,
    discardPileCount: number,
    shownCards: AttackModifier[]
}