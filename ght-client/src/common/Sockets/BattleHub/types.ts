export interface AttackModifier {
    type: "+" | "-" | "*"
    value: number
}

export interface Stats {
    health: number
}

export enum BattleHubActions {
    attack,
    move
}