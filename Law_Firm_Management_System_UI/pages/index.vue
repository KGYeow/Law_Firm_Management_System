<template>
  <v-parallax src="/images/background/login-cover.jpg"/>
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