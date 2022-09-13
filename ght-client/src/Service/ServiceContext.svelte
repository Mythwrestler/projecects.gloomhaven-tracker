<script lang="ts">
  import { defineContentService } from "./ContentService";
  import { defineCampaignService } from "./CampaignService";
  import { accessToken } from "@ci-lab/svelte-oidc-context";

  // eslint-disable-next-line @typescript-eslint/no-unused-vars
  const contentService = defineContentService(accessToken);

  // eslint-disable-next-line @typescript-eslint/no-unused-vars
  const campaignService = defineCampaignService(accessToken);
</script>

{#if contentService && campaignService}
  <slot />
{/if}
