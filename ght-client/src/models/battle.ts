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

export interface Actor {
    id: string,
    name: string,
}

export interface Player extends Actor {
    currentHealth: number,
    level: Map<number, number>,
    baseHealth: Map<number, number>,
    effects: ActorEffect[],
    baseModifierDeck: AttackModifier[],
    ModifierDeck: AttackModifier[],
}

export interface MonsterStatSet {
    health: number
    movement: number
    attack: number
    effects: ActorEffect[]
}

export interface ActiveMonsterStatSet extends MonsterStatSet {
    isElite: boolean
}

export interface BaseMonsterStatSet {
    elite: MonsterStatSet,
    standard: MonsterStatSet
}

export interface Monster extends Actor {
    baseStats: Map<number, BaseMonsterStatSet>
    activeMonsters: Map<number, ActiveMonsterStatSet>
}

export interface Battle {
    actors: Actor[],
    monsterDeck: AttackModifier[],
    initiative: Map<number, string>
}

export interface BattleAction {
    source: string
    target: string
    damage: number
    effects: ActorEffect[]
}

export interface BattleActionResult {
    affect: string,
    affected: string,
}