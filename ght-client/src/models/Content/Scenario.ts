import type { ContentItemSummary } from "./ContentItem";

export interface ScenarioSummary extends ContentItemSummary {
  scenarioNumber: number;
}

export interface Scenario extends ContentItemSummary {
  scenarioNumber: number;
  goal: string;
  cityMapLocation: string;
  scenarioBook: number[];
  supplementalBook: number[];
  monsters?: ContentItemSummary[];
  objectives?: ContentItemSummary[];
}
