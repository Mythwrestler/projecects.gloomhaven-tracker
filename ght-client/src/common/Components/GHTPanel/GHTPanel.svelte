<script lang="ts">
  import Paper from "@smui/paper";
  export let color: "primary" | "secondary" | "ght-panel" | undefined =
    undefined;
  let className = "max-w-md mx-auto";
  export { className as class };
</script>

<Paper {color} class={className}>
  <slot />
</Paper>
