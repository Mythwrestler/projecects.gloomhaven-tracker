export interface CampaignSummary {
  id: string;
  description: string;
  game: string;
}

export interface Campaign {
  id: string;
  description: string;
  game: string;
  party: Character[];
  scenarios: Scenario[];
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

export interface Scenario {
  contentCode: string;
  name: string;
  description: string;
  scenarioNumber: number;
  isCompleted: boolean;
  isClosed: boolean;
}
