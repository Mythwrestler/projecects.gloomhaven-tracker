import { AttackModifier, AttackModifierCounts } from "./AttackModifier";
import { ActorEffect } from "./ActorEffect";

export interface Actor {
  id: string;
  name: string;
  health: string;
  effects: ActorEffect[];
}

export interface Player extends Actor {
  baseModifierDeck: AttackModifier[];
  modifierDeck: AttackModifierCounts;
  baseHealthStats?: { [key: number]: number };
  baseHealth?: number;
  levels?: { [key: number]: number };
  experience: number;
}

export interface MonsterStatSet {
  health: number;
  movement: number;
  attack: number;
  effects: ActorEffect[];
}

export interface BaseMonsterStatSet {
  elite: MonsterStatSet;
  standard: MonsterStatSet;
}

export interface Monster extends Actor {
  type: string;
  baseStats?: BaseMonsterStatSet;
  baseHealth?: number;
  level: number;
  isElite: boolean;
  monsterId: number;
  attack?: number;
  movement?: number;
}

export interface Objective extends Actor {
  objectiveId: string;
}

export interface Actors {
  players: Player[];
  monsters: Monster[];
  objectives: Objective[];
}
