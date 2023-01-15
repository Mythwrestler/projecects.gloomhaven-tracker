<script lang="ts">
  import { defineContentService } from "./ContentService";
  import { defineContentService as defineContentServiceThick } from "./ContentServiceThick";
  import { defineCampaignService } from "./CampaignService";
  import { accessToken } from "@ci-lab/svelte-oidc-context";
  import { defineCombatService } from "./CombatService";

  const contentService = defineContentService(accessToken);

  const contentServiceThick = defineContentServiceThick(accessToken);

  const campaignService = defineCampaignService(accessToken);

  const combatService = defineCombatService(accessToken);
</script>

{#if contentService && contentServiceThick && campaignService && combatService}
  <slot />
{/if}
