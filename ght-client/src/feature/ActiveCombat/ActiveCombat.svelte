<script lang="ts">
  import { accessToken } from "../../common/Utils/OidcSvelteClient";
  import { useCombatHubService } from "../../Service/CombatHubService";
  import { useCombatService } from "../../Service/CombatService";

  const { State: hubState } = useCombatHubService(accessToken);
  const { State: combatState } = useCombatService(accessToken);
</script>

<h2>Active Combat</h2>
<div>Combat Id: {hubState.combat.combatId}</div>
