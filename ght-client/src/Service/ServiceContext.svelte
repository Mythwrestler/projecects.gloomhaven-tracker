<script lang="ts">
  import { defineContentService } from "./ContentService";
  import { accessToken } from "@ci-lab/svelte-oidc-context";

  // eslint-disable-next-line @typescript-eslint/no-unused-vars
  const contentService = defineContentService(accessToken);
</script>

<slot />
