<template>
  <v-row>
    <v-col cols="12" md="12">
      <UiParentCard title="Tasks"> 
        <div class="pa-7 pt-1"><p class="text-body-1">This is a task management page</p></div>
      </UiParentCard>
    </v-col>
  </v-row>
</template>

<script setup>
import { ClipboardTextIcon } from "vue-tabler-icons"
import UiParentCard from '@/components/shared/UiParentCard.vue';

// Head
useHead({
  title: "Tasks | CaseCraft",
})

// Page Meta
definePageMeta({
  breadcrumbsIcon: shallowRef(ClipboardTextIcon),
  breadcrumbs: [
    {
      title: 'Tasks',
      disabled: false,
    },
  ],
})
</script>