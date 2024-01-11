<template>
  <v-dialog v-model="model" :width="width">
    <v-card elevation="10" class="withbg rounded-3 overflow-visible">
      <v-card-title class="px-4 py-4 d-sm-flex align-center justify-space-between bg-background rounded-top-3">
        <h5 class="text-h5 mb-0 d-flex align-center">
          {{ title }}
        </h5>
        <v-btn density="compact" variant="plain" icon="mdi-close" @click="model = false"/>
      </v-card-title>
      <v-divider class="m-0"/>
      <slot/>
    </v-card>
  </v-dialog>
</template>

<script setup lang="ts">
// Properties
const props = defineProps({
  title: String,
  width: String || Number || undefined,
})

// Model
const model = defineModel<boolean>()
</script>