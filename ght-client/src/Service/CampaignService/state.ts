import { getContext, setContext } from "svelte";
import { derived, writable, type Readable, type Writable } from "svelte/store";
import ENV_VARS from "../../common/Environment";
import type { Campaign, CampaignSummary } from "../../models/Campaign";

export interface CampaignStateWritable {
  campaignSummaries: Writable<CampaignSummary[]>;
  campaignDetail: Writable<Campaign | undefined>;
}

export interface CampaignState {
  campaignSummaries: Readable<CampaignSummary[]>;
  campaignDetail: Readable<Campaign | undefined>;
}

export const getCampaignState = (
  contextKey: unknown
): CampaignStateWritable => {
  let state = getContext<CampaignStateWritable | undefined>(contextKey);
  if (state) {
    const stateProperties = Object.keys(state as object);
    if (
      stateProperties.includes("campaignSummaries") &&
      stateProperties.includes("campaignDetail")
    ) {
      return state;
    }
  }

  state = {
    campaignSummaries: writable<CampaignSummary[]>([]),
    campaignDetail: writable<Campaign>(),
  };
  setContext(contextKey, state);
  return state;
};

export const useCampaignServiceState = (
  stateKey: string = ENV_VARS.CONTEXT.CampaignService.State
): CampaignState => {
  const writableState: CampaignStateWritable = getCampaignState(stateKey);
  return {
    campaignSummaries: derived(
      writableState.campaignSummaries,
      (store) => store
    ),
    campaignDetail: derived(writableState.campaignDetail, (store) => store),
  };
};
