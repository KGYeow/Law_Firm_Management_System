<template>
  <v-card flat color="transparent" dark>
    <v-tabs v-model="tab" bg-color="transparent">
      <v-tab value="one">
        <h5 class="text-h5 mb-3 pl-7 pt-3 ps-0 d-flex align-center">
                Upcoming Events
        </h5>
      </v-tab>
      <v-tab value="two">
        <h5 class="text-h5 mb-3 pl-7 pt-3 ps-0 d-flex align-center">
                Past Events
        </h5>
      </v-tab>
      <v-tab value="three">
        <h5 class="text-h5 mb-3 pl-7 pt-3 ps-0 d-flex align-center">
                Calendar
        </h5>
      </v-tab>
    </v-tabs>

    <v-card-text>
      <v-window v-model="tab">
        <!--Upcoming Event-->
        <v-window-item value="one"> 
          <v-row>
            <v-col cols="12">
              <v-row>
                  <!-- Filters -->
                  <v-col class="pe-0" cols="2">
                    <v-select
                      :items="caseList"
                      item-title="name"
                      item-value="id"
                      placeholder="Cases"
                      density="compact"
                      variant="outlined"
                      v-model="filter.caseID"
                      hide-details
                  >
                    <template v-slot:prepend-item>
                      <v-list-item title="All Cases" @click="filter.caseID = null"/>
                    </template>
                  </v-select>
                </v-col>
              </v-row>
                <v-divider/>
          <!-- Event Item List -->
          <v-data-table
            class="clientEventList bg-transparent"
            v-model:page="currentPage"
            :items="upcomingEventList"
            :items-per-page="itemsPerPage"
          >
            <template #headers/>
            <template #body v-if="upcomingEventList.length == 0">
              <div class="p-3 w-100 fs-6 text-center">
                No Events
              </div>
            </template>
            <template v-slot:item="{ item }">
              <v-col class="pb-8" cols="3">
                <v-card elevation="10" class="withbg">
                  <el-tag
                          :type="item.isCompleted ? 'success' : 'warning'"
                          class="position-absolute"
                          effect="dark"
                          style="bottom: -10px; right: -15px; border-radius: 10px;"
                        >
                          <i class="mdi mdi-check" v-if="item.isCompleted"></i>
                          <i class="mdi mdi-timer-sand-complete" v-else></i>
                          {{ item.isCompleted ? 'Completed' : 'Ongoing' }}
                  </el-tag>
                  <v-card-actions
                    class="p-0 text-subtitle-2 fw-bold justify-content-center text-white"
                    style="background-color: rgb(43, 76, 101); min-height: 35px; border-top-right-radius: 10px; border-top-left-radius: 10px;"
                  >
                    {{ item.caseName }}
                  </v-card-actions>
                  <v-card-item class="pt-3">
                    <v-card-title class="text-subtitle-2 fw-bold">
                      Event Time
                    </v-card-title>
                    <v-card-subtitle class="text-subtitle-2">
                      <i class="mdi mdi-calendar-blank-outline me-1"></i>
                      {{ dayjs(item.eventTime).format("DD MMM YYYY, hh:mm A") }}
                    </v-card-subtitle>
                    <v-divider class="my-3"/>
                    <el-scrollbar class="text-body-1" >
                      <div class="d-flex pt-sm-2 align-center ">
                        <ChecklistIcon/>
                        <strong class="ms-5">{{ item.name }}</strong>
                      </div>
                    </el-scrollbar>
                    <v-card-text class="px-0 py-2 ext-body-1">
                      {{ item.description }}
                    </v-card-text>
                  </v-card-item>
                </v-card>
              </v-col>
            </template>
            <template v-slot:bottom>
              <div class="d-flex justify-content-end pt-2">
                <el-pagination
                  layout="total, prev, pager, next"
                  v-model:current-page="currentPage"
                  :page-size="upcomingEventList.length/pageCount()"
                  :total="upcomingEventList.length"
                  background
                />
              </div>
            </template>
            </v-data-table>
            </v-col>
            </v-row>
    </v-window-item>

    <!--Past Event-->
    <v-window-item value="two"> 
      <v-row>
            <v-col cols="12">
              <v-row>
                  <!-- Filters -->
                  <v-col class="pe-0" cols="2">
                    <v-select
                    :items="caseList"
                    item-title="name"
                    item-value="id"
                    placeholder="Cases"
                    density="compact"
                    variant="outlined"
                    v-model="filter.caseID"
                    hide-details
                  >
                    <template v-slot:prepend-item>
                      <v-list-item title="All Cases" @click="filter.caseID = null"/>
                    </template>
                  </v-select>
                </v-col>
                </v-row>
                <v-divider/>
          <!-- Event Item List -->
          <v-data-table
            class="clientEventList bg-transparent"
            v-model:page="currentPage"
            :items="pastEventList"
            :items-per-page="itemsPerPage"
          >
            <template #headers/>
            <template #body v-if="pastEventList.length == 0">
              <div class="p-3 w-100 fs-6 text-center">
                No Events
              </div>
            </template>
            <template v-slot:item="{ item }">
              <v-col class="pb-8" cols="3">
                <v-card elevation="10" class="withbg">
                  <el-tag
                          :type="item.isCompleted ? 'success' : 'danger'"
                          class="position-absolute"
                          effect="dark"
                          style="bottom: -10px; right: -15px; border-radius: 10px;"
                        >
                          <i class="mdi mdi-check" v-if="item.isCompleted"></i>
                          <i class="mdi mdi-close" v-else></i>
                          {{ item.isCompleted ? 'Completed' : 'Incompleted' }}
                  </el-tag>
                  <v-card-actions
                    class="p-0 text-subtitle-2 fw-bold justify-content-center text-white"
                    style="background-color: rgb(43, 76, 101); min-height: 35px; border-top-right-radius: 10px; border-top-left-radius: 10px;"
                  >
                    {{ item.caseName }}
                  </v-card-actions>
                  <v-card-item class="pt-3">
                    <v-card-title class="text-subtitle-2 fw-bold">
                      Event Time
                    </v-card-title>
                    <v-card-subtitle class="text-subtitle-2">
                      <i class="mdi mdi-calendar-blank-outline me-1"></i>
                      {{ dayjs(item.eventTime).format("DD MMM YYYY, hh:mm A") }}
                    </v-card-subtitle>
                    <v-divider class="my-3"/>
                    <el-scrollbar class="text-body-1" >
                      <div class="d-flex pt-sm-2 align-center ">
                        <ChecklistIcon/>
                        <strong class="ms-5">{{ item.name }}</strong>
                      </div>
                    </el-scrollbar>
                    <v-card-text class="px-0 py-2 ext-body-1">
                      {{ item.description }}
                    </v-card-text>
                  </v-card-item>
                </v-card>
              </v-col>
            </template>
            <template v-slot:bottom>
              <div class="d-flex justify-content-end pt-2">
                <el-pagination
                  layout="total, prev, pager, next"
                  v-model:current-page="currentPage"
                  :page-size="pastEventList.length/pageCount()"
                  :total="pastEventList.length"
                  background
                />
              </div>
            </template>
            </v-data-table>
            </v-col>
            </v-row> </v-window-item>

      <!--Calendar-->
      <v-window-item value="three">  

        <EventCalendar :list="upcomingEventCalendar"/>
        </v-window-item>

      </v-window>
</v-card-text>
</v-card>

</template>
  
  <script setup>
  import { CalendarIcon } from "vue-tabler-icons"
  import { ChecklistIcon } from "vue-tabler-icons"
  import dayjs from 'dayjs'
  import { ref } from 'vue';
  const tab = ref(null)
  
  const filter = ref({
    caseID: null,
    partnerId: null,
    isCompleted:null,
  })
  

// Data
const currentPage = ref(1)
const itemsPerPage = ref(10)
/*
const headers = ref([
    { key: "number", title: "No." },
    { key: "name", title: "Event Name" },
    { key: "caseID", title: "Case ID" },
    { key: "clientName", title: "Client Name" },
    { key: "createdTime" , title: "Created Time" },
    { key: "eventTime" , title: "Event Time" },
    { key: "isCompleted", title: "Event Status"},
    { key: "actions", sortable: false },
])
*/
// Define reactive variables 
const { data: upcomingEventList } = await fetchData.$get("/Event/ClientPerspectiveUpcomingEventList", filter.value)
const { data: pastEventList } = await fetchData.$get("/Event/ClientPerspectivePastEventList", filter.value)
const { data: caseList } = await fetchData.$get("/Case/ClientPerspectiveCaseList")
const { data: upcomingEventCalendar } = await fetchData.$get("/Event/UpcomingEvents/Client")
 
  // Head
  useHead({
    title: "Events | CaseCraft",
  })
  
  // Page Meta
  definePageMeta({
    breadcrumbsIcon: shallowRef(CalendarIcon),
    breadcrumbs: [
      {
        title: 'Events',
        disabled: false,
      },
    ],
  })
  
// Methods
const pageCount = () => {
const totalEvents = upcomingEventList.value.length + pastEventList.value.length;
  return Math.ceil(totalEvents / itemsPerPage.value);
};

  </script>