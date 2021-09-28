export enum ACTOR_EFFECT_TYPES {
    Strength = "strength",
    Poison = "poison",
    Stun = "stun",
    Shield = "shield"
}

export interface ActorEffect {
    type: ACTOR_EFFECT_TYPES,
    value: number,
    duration: number,
}

export enum ATTACK_MODIFIER_TYPE {
    Add = "add",
    Multiply = "multiply",
    Cancel = "cancel"
}

export interface AttackModifier {
    type: ATTACK_MODIFIER_TYPE,
    isCurse: boolean
    isBlessing: boolean
    value?: number
}

export interface AttackModifierCounts {
        drawPileCount: number,
        discardPileCount: number
}


export interface Actor {
    id: string,
    name: string,
    health: string,
    effects: ActorEffect[]
}


export interface Player extends Actor {
    baseModifierDeck: AttackModifier[],
    modifierDeck: AttackModifierCounts,
    baseHealthStats?: {[key: number]: number},
    baseHealth?: number,
    levels?: {[key: number]: number}
    experience: number
}


export interface MonsterStatSet {
    health: number
    movement: number
    attack: number
    effects: ActorEffect[]
}

export interface BaseMonsterStatSet {
    elite: MonsterStatSet,
    standard: MonsterStatSet,
}

export interface Monster extends Actor {
    type: string,
    baseStats?: BaseMonsterStatSet,
    baseHealth?: number,
    level: number,
    isElite: boolean,
    monsterId: number,
    attack?: number,
    movement?: number,
}

export interface Objective extends Actor {
    objectiveId: string
}

export interface Actors {
    players: Player[],
    monsters: Monster[],
    objectives: Objective[],
}