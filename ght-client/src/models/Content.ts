export interface ContentItemSummary {
  name: string;
  contentCode: string;
}

export interface ScenarioDefault extends ContentItemSummary {
  monsters?: ContentItemSummary[];
}
