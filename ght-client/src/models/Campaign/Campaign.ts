export interface CampaignSummary {
  id: string;
  name: string;
  description: string;
  game: string;
  editable: boolean;
}

export interface Campaign {
  id: string;
  description: string;
  name: string;
  game: string;
  party: Character[];
  scenarios: Scenario[];
  editable: boolean;
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
  scenarioContentCode: string;
  name: string;
  description: string;
  scenarioNumber: number;
  isCompleted: boolean;
  isClosed: boolean;
}
