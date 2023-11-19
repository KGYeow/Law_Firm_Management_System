<template>
  <UiParentCard title="Announcement" :icon="SpeakerphoneIcon">
    <div class="pa-7 pt-1 text-body-1">
      <UiChildCard :title="announcement.title" v-for="(announcement, index) in item">
        {{ announcement.description }}
      </UiChildCard>
    </div>
  </UiParentCard>
</template>

<script setup>
import { SpeakerphoneIcon } from 'vue-tabler-icons';
import UiParentCard from '@/components/shared/UiParentCard.vue';
import UiChildCard from '@/components/shared/UiChildCard.vue';

// Props
const props = defineProps({ item: Object });

// Data
const item = fetchData.$get("/Dashboard/Announcement").data
</script>