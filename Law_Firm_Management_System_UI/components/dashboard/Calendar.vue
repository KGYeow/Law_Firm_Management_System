<template>
  <h5 class="text-h5 mb-6 pl-7 d-flex align-center">
    <LayoutFullVerticalSidebarIcon class="me-3" :item="CalendarIcon"/>
    Calendar
  </h5>
  <div class="pa-7 pt-1 text-body-2">
    <el-calendar v-model="value"/>
  </div>
</template>

<script setup>
import { CalendarIcon } from 'vue-tabler-icons';

// Data
const value = ref(new Date())
</script>