<template>
  <v-row>
    <v-col cols="12" md="12">
      <UiParentCard title="Legal Information"> 
        <div class="pa-7 pt-1"><p class="text-body-1">This is a legal information page</p></div>
      </UiParentCard>
    </v-col>
  </v-row>
</template>

<script setup>
import { HelpIcon } from "vue-tabler-icons"
import UiParentCard from '@/components/shared/UiParentCard.vue';

// Head
useHead({
  title: "Legal Information | CaseCraft",
})

// Page Meta
definePageMeta({
  breadcrumbsIcon: shallowRef(HelpIcon),
  breadcrumbs: [
    {
      title: 'Legal Information',
      disabled: false,
    },
  ],
})
</script>