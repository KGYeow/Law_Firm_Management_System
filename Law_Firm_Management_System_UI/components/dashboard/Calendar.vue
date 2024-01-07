<template>
  <div class="mt-3 pa-7 py-1 text-body-1">
    <Calendar
      ref="calendar"
      :attributes="attrs"
      expanded
      borderless
    >
      <template #footer>
        <div class="px-3 pb-1">
          <v-btn variant="outlined" size="small" @click="moveToToday">Today</v-btn>
        </div>
      </template>
    </Calendar>
  </div>
</template>

<script setup>
import { setupCalendar, Calendar, DatePicker } from 'v-calendar';

// Data
const calendar = ref(null)
const attrs = ref([
  {
    highlight: true,
    dates: new Date(),
  },
])

// Methods
const moveToToday = () => {
  calendar.value.move(new Date());
}
</script>