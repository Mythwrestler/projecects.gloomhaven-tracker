export interface Participants {
  combatId: string;
  participants: Participant[];
}

export interface Participant {
  username: string;
  isObserver: boolean;
  characterCodes: string[];
}
