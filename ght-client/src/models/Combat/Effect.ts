export type EffectType =
  | "strength"
  | "poison"
  | "wound"
  | "stun"
  | "shield"
  | "disarm"
  | "muddle"
  | "immobilize"
  | "curse";

export interface Effect {
  type: EffectType;
  value: number;
  duration: number;
}
