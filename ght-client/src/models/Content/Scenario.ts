import { ContentItemSummary } from "./ContentItem";

export interface Scenario extends ContentItemSummary {
  monsters?: ContentItemSummary[];
}
