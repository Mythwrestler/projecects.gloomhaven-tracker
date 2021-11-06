export interface ContentItemSummary {
  name: string;
  code: string;
}

export interface Scenario extends ContentItemSummary {
  monsters?: ContentItemSummary[];
}
