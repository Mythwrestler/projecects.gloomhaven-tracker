export interface ContentItemSummary {
  name: string;
  contentCode: string;
}

export interface Scenario extends ContentItemSummary {
  monsters?: ContentItemSummary[];
}
