export enum ATTACK_MODIFIER_TYPE {
  Add = "add",
  Multiply = "multiply",
  Cancel = "cancel",
}

export interface AttackModifier {
  type: ATTACK_MODIFIER_TYPE;
  isCurse: boolean;
  isBlessing: boolean;
  value?: number;
}

export interface AttackModifierCounts {
  drawPileCount: number;
  discardPileCount: number;
}
