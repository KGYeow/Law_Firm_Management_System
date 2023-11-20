<template>
  <div>
    <h5 class="text-h5 mb-6 pl-7 d-flex align-center">
      <LayoutFullVerticalSidebarIcon class="me-3" :item="CalendarEventIcon"/>
      Events
    </h5>
    <div class="pa-7 pt-1 text-body-1">
    </div>
  </div>
</template>

<script setup>
import { CalendarEventIcon } from 'vue-tabler-icons';
</script>