<template>
    <div class="pa-7 py-1 text-body-1">
      <Calendar
        ref="calendar"
        :attributes="attrs"
        expanded
        borderless
        @dayclick="(day) => upcomingEventDataGet(day.attributes)"
      >
        <template #footer>
          <div class="px-3 pb-1">
            <v-btn variant="outlined" size="small" @click="moveToToday">Today</v-btn>
          </div>
        </template>
      </Calendar>
    </div>
  
    <!-- Upcoming Event Modal -->
    <SharedUiModal v-model="upcomingEventModal" title="Upcoming Event" max-width="700">
      <el-scrollbar max-height="350px">
        <v-card-item class="px-8 py-4 text-body-1">
          <h5 class="text-h5 mb-5 pl-7 ps-4 d-flex align-center">
            <LayoutFullVerticalSidebarIcon class="me-3" :item="CalendarTimeIcon"/>
            {{ dayjs(selectedDate).format("DD MMM YYYY") }}
          </h5>
          <v-list class="p-0">
            <v-list-item class="pb-4" v-for="data in upcomingEventData" v-show="data.title">
              <v-row>
                <v-col cols="4">
                  <div class="text-h6 pt-1">{{ data.title }}</div>
                </v-col>
                <v-divider class="mx-4 border-opacity-50" :thickness="5" vertical/>
                <v-col>
                  <div class="pt-1">{{ data.description }}</div>
                </v-col>
              </v-row>
            </v-list-item>
          </v-list>
        </v-card-item>
      </el-scrollbar>
    </SharedUiModal>
  </template>
  
  <script setup>
  import { CalendarTimeIcon } from 'vue-tabler-icons'
  import { Calendar } from 'v-calendar'
  import dayjs from 'dayjs';
  
  // Properties
  const props = defineProps({
    list: { type: Array, default: [] },
  })
  
  // Data
  const upcomingEventModal = ref(false)
  const upcomingEventData = ref([])
  const selectedDate = ref(null)
  const calendar = ref(null)
  const attrs = computed(() => [
    {
      highlight: true,
      dates: new Date(),
    },
    ...props.list.map(item => ({
      ...item,
      dot: true,
      dates: item.time,
      content: {
        class: "fw-bold",
      }
    }))
  ])
  
  // Methods
  const moveToToday = () => {
    calendar.value.move(new Date());
  }
  const upcomingEventDataGet = (data) => {
    if (data.length != 0) {
      selectedDate.value = data[0].dates
      upcomingEventData.value = data
      upcomingEventModal.value = true
    }
  }
  </script>