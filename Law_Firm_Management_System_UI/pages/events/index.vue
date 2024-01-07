<template>
  <v-row>
    <v-col cols="12" md="12">
      <UiParentCard title="Events"> 
        <v-row class="px-7">
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
              :items="['Completed', 'Incompleted']"
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

        <div class="pa-7 pt-1 text-body-1">
          <v-data-table
            density="comfortable"
            v-model:page="currentPage"
            :headers="headers"
            :items="eventList"
            :items-per-page="itemsPerPage"
            hover
          >
            <template v-slot:item="{ item }">
              <tr>
                <td>{{ eventList.indexOf(item) + 1 }}</td>
                <td>{{ item.caseName }}</td>
                <td>{{ item.name }}</td>
                <td>{{ dayjs(item.CreatedTime).format("DD MMM YYYY, hh:mm A") }}</td>
                <td>{{ dayjs(item.EventTime).format("DD MMM YYYY, hh:mm A") }}</td>
                <td>
                  <el-tag type="success" v-if="item.isCompleted">Completed</el-tag>
                  <el-tag type="danger" v-else>Incompleted</el-tag>
                </td>
              </tr>
            </template>
            <template v-slot:bottom>
              <div class="d-flex justify-content-end pt-2">
                <el-pagination
                  layout="total, prev, pager, next"
                  v-model:current-page="currentPage"
                  :page-size="eventList.length/pageCount()"
                  :total="eventList.length"
                />
              </div>
            </template>
          </v-data-table>
        </div>
      </UiParentCard>
    </v-col>
  </v-row>
</template>

<script setup>
import dayjs from 'dayjs';
import { CalendarIcon } from "vue-tabler-icons"
import UiParentCard from "@/components/shared/UiParentCard.vue";

const filter = ref({
  caseID: null,
  paralegalID: null,
  isCompleted: null,
})

// Data
const { data: user } = useAuth()
const currentPage = ref(1)
const itemsPerPage = ref(10)
const headers = ref([
  { key: "number", title: "No." },
  { key: "caseName", title: "Case Name" },
  { key: "name", title: "Event Name" },
  { key: "createdTime" , title: "Created Time" },
  { key: "eventTime", title: "Event Time"},
  { key: "isCompleted", title: "Status"},
])


// Define reactive variables for dialog
const { data: eventList } = await fetchData.$get("/Event/PartnerPerspectiveEventList", filter.value)
const { data: caseList } = await fetchData.$get("/Case")
const { data: clientList } = await fetchData.$get("/Client")

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