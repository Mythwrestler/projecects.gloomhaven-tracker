<script lang="ts">
  import { defineContentService } from "./ContentService";
  import { defineCampaignService } from "./CampaignService";
  import { accessToken } from "@ci-lab/svelte-oidc-context";
  import { defineCombatService } from "./CombatService";

  const contentService = defineContentService(accessToken);

  const campaignService = defineCampaignService(accessToken);

  const combatService = defineCombatService(accessToken);
</script>

{#if contentService && campaignService && combatService}
  <slot />
{/if}
