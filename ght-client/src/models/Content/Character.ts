import type { ContentItemSummary } from "./ContentItem";

interface BaseCharacterHealth {
  level: number;
  health: number;
}

interface CharacterLevel {
  experience: number;
  level: number;
}

interface BaseCharacterStats {
  levels: CharacterLevel[];
  health: BaseCharacterHealth[];
}

export interface Character extends ContentItemSummary {
  baseStats: BaseCharacterStats;
}
