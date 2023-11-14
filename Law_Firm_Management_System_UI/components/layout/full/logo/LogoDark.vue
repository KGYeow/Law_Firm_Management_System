<script setup lang="ts">
const primary = ref('rgb(var(--v-theme-primary))');
const secondary = ref('rgb(var(--v-theme-secondary))');
</script>
<template>
  <div class="logo mx-auto">
    <NuxtLink to="/">
      <img src="/images/logos/casecraftLogo.png" height="150">
    </NuxtLink>
  </div>
</template>