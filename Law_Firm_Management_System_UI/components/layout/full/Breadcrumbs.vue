<template>
  <v-card elevation="10" class="mb-5 withbg">
    <v-breadcrumbs
      class="px-7 text-subtitle-1"
      density="compact"
      :items="useRoute().meta.breadcrumbs"
    >
      <template v-slot:prepend>
        <v-icon class="me-1" size="small" :icon="useRoute().meta.breadcrumbsIcon"/>
      </template>
      <template v-slot:divider>
        <v-icon icon="mdi-chevron-right"></v-icon>
      </template>
    </v-breadcrumbs>
  </v-card>
</template>

<script setup>
import { useRoute } from 'vue-router'
</script>