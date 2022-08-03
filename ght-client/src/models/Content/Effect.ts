export enum EFFECT_TYPE {
  STRENGTH = "strength",
  POISON = "poison",
  WOUND = "wound",
  STUN = "stun",
  SHIELD = "shield",
  DISARM = "disarm",
  MUDDLE = "muddle",
  IMMOBILIZE = "immobilize",
  CURSE = "curse",
  DISADVANTAGE = "disadvantage",
  ADVANTAGE = "advantage",
  DAMAGE = "damage",
  HEAL_ALLY = "healAlly",
  CHARGE_ELEMENT = "chargeElement",
  SPEND_ELEMENT = "spendElement",
  PUSH = "push",
}

export enum ELEMENT {
  FIRE = "fire",
  ICE = "ice",
  AIR = "air",
  EARTH = "earth",
  LIGHT = "light",
  DARK = "dark",
}

export interface Effect {
  type: EFFECT_TYPE;
  value?: number;
  duration?: number;
  range?: number;
  element?: ELEMENT;
}
