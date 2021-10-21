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