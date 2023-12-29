<template>
  <v-card elevation="10" class="mb-5 withbg">
    <v-breadcrumbs
      class="px-7 py-2 text-subtitle-1"
      :items="[]"
    >
      <template v-slot:prepend>
        <v-icon size="small" :icon="CalendarIcon"></v-icon>
      </template>
      <template v-slot:divider>
        <v-icon icon="mdi-chevron-right"></v-icon>
      </template>
    </v-breadcrumbs>
  </v-card>
</template>

<script setup>
import { CalendarIcon } from 'vue-tabler-icons';

const route = useRoute()
const currentBreadcrumbs = computed(() => {
  return route.fullPath
})
console.log(currentBreadcrumbs.value)
// Properties
// defineProps({
//   items: {
//     type: Array,
//     required: true,
//   },
// })
</script>