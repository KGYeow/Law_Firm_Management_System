<template>
  <UiParentCard title="Calendar" :icon="CalendarIcon">
    <div class="pa-7 pt-1 text-body-1">
      <el-calendar v-model="value"/>
    </div>
  </UiParentCard>
</template>

<script setup>
import { CalendarIcon } from 'vue-tabler-icons';
import UiParentCard from '@/components/shared/UiParentCard.vue';

// Data
const value = ref(new Date())
</script>