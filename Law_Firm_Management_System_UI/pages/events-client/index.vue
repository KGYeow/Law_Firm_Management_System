<template>
  <v-row>
      <v-col cols="12">
        <h5 class="text-h5 mb-6 pl-7 pt-7 ps-0 d-flex align-center">
          Events Related
        </h5>
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
          <v-col cols="2">
            <v-select
              :items="[
                {
                  name:'Completed',
                  value:true,
                },
                {
                  name:'Incompleted',
                  value:false,
                },
                ]"
              item-title="name"
              item-value="value"
              placeholder="Status"
              density="compact"
              bg-color="white"
              variant="outlined"
              v-model="filter.isCompleted"
              hide-details
            >
              <template v-slot:prepend-item>
                <v-list-item title="All Statuses" @click="filter.isCompleted = null"/>
              </template>
            </v-select>
          </v-col>
          </v-row>
          <v-divider/>

          <!--Event List-->
          <v-data-table
            class="eventList bg-transparent"
            v-model:page="currentPage"
            :items="eventList"
            :items-per-page="itemsPerPage"
          >
            <template #headers/>
            <template #body v-if="eventList.length == 0">
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
                    {{ item.name }}
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
                    <el-scrollbar class="text-body-1" height="60px">
                      <div class="d-flex pt-sm-2 align-center">
                        <v-avatar
                          class="mb-3"
                          image="/images/users/avatar.jpg"
                          size="40"
                        />
                        <strong class="ms-5">{{ item.partnerName }}</strong>
                      </div>
                    </el-scrollbar>
                  </v-card-item>
                </v-card>
              </v-col>
            </template>
            <template v-slot:bottom>
              <div class="d-flex justify-content-end pt-2">
                <el-pagination
                  layout="total, prev, pager, next"
                  v-model:current-page="currentPage"
                  :page-size="eventList.length/pageCount()"
                  :total="eventList.length"
                  background
                />
              </div>
            </template>
        </v-data-table>
      </v-col>
    </v-row>
   
</template>

<script setup>
import { CalendarIcon } from "vue-tabler-icons"
import dayjs from 'dayjs';

const filter = ref({
    caseID: null,
    partnerId: null,
    isCompleted:null,
  })

// Data
const currentPage = ref(1)
const itemsPerPage = ref(10)
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

// Define reactive variables 
const { data: eventList } = await fetchData.$get("/Event/ClientPerspectiveEventList", filter.value)
const { data: partnerList } = await fetchData.$get("/Partner")
const { data: caseList } = await fetchData.$get("/Case")
  
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
    return Math.ceil(eventList.value.length / itemsPerPage.value)
}
</script>

