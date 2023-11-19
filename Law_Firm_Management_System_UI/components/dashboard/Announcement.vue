<template>
  <UiParentCard title="Announcement" :icon="SpeakerphoneIcon">
    <div class="pa-7 pt-1 text-body-1">
      <v-card
        class="mb-3 p-3"
        v-for="announcement in item"
        color="rgb(149, 180, 204, 0.15)"
      >
        <v-card-item class="p-0">
          <v-card-title class="fs-6 fw-bold">
            {{ announcement.title }}
          </v-card-title>
          <v-card-subtitle style="font-size: smaller;">
            {{ announcement.createdTime }}
          </v-card-subtitle>
        </v-card-item>
        <v-card-text class="p-0 pt-2">
          {{ announcement.description }}
        </v-card-text>
      </v-card>
    </div>
  </UiParentCard>
</template>

<script setup>
import { SpeakerphoneIcon } from 'vue-tabler-icons';
import UiParentCard from '@/components/shared/UiParentCard.vue';

// Props
const props = defineProps({ item: Object });

// Data
const item = fetchData.$get("/Dashboard/Announcement").data
</script>