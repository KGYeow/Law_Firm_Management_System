<template>
  <v-parallax src="https://cdn.vuetifyjs.com/images/backgrounds/vbanner.jpg"/>
</template>

<script setup>
definePageMeta({
  layout: "guest",
  auth: { unauthenticatedOnly: true },
})

// Head
useHead({
  title: "CaseCraft",
})
</script>