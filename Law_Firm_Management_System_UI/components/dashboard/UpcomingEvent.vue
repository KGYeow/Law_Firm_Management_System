<template>
  <div>
    <h5 class="text-h5 mb-6 pl-7 d-flex align-center">
      <LayoutFullVerticalSidebarIcon class="me-3" :item="CalendarEventIcon"/>
      Upcoming Events
    </h5>
    <div class="pa-7 py-1">
      <v-timeline class="upcomingEvents" truncate-line="start" line-thickness="1" side="end" align="start" v-if="list.length > 0">
        <v-timeline-item
          v-for="upcomingEvent in list"
          size="13"
          line-inset="5"
          dot-color="primary"
          icon="mdi-circle-outline"
        >
          <template #opposite>
            <h6 class="m-0 fw-bold text-body-2 textSecondary">{{ dayjs(upcomingEvent.time).format("DD MMM YYYY") }}</h6>
          </template>
          <v-sheet class="text-body-2">
            <h6 class="m-0 text-body-2 fw-bold">{{ upcomingEvent.title }}</h6>
            <span class="description">
              <v-tooltip :text="upcomingEvent.description" activator="parent" location="left" offset="5" width="300px"/>
              {{ upcomingEvent.description }}
            </span>
          </v-sheet>
        </v-timeline-item>
      </v-timeline>
      <el-empty class="p-0 pb-2" :image-size="110" description="No upcoming event" v-else/>
    </div>
  </div>
</template>

<script setup lang="ts">
import { CalendarEventIcon } from 'vue-tabler-icons'
import dayjs from 'dayjs'

// Array Item Type
type UpcomingList = {
  title: string,
  description: string,
  time: Date,
}

// Properties
const props = defineProps({
  list: { type: Array<UpcomingList>, default: [] },
})
</script>