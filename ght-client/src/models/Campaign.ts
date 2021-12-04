export interface CampaignSummary {
  id: string;
  description: string;
  game: string;
}

export interface Campaign {
  party: Party;
  id: string;
  description: string;
  game: string;
  completedScenarios: string[];
  closedScenarios: string[];
  availableScenarios: string[];
}

export interface Party {
  characters: Character[];
}

export interface Character {
  name: string;
  characterContentCode: string;
  experience: number;
  gold: number;
  items: string[];
  appliedPerks: string[];
  perkPoints: number;
}
