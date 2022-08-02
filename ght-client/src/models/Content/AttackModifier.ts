import type { Effect } from ".";

export interface AttackModifier {
  contentCode: string;
  name: string;
  description: string;
  isCurse: boolean;
  isBlessing: boolean;
  triggerShuffle: boolean;
  value: string;
  effects: Effect[];
}
