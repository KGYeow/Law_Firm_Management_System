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
          <v-col v-if="userRole == 'Partner'">
            <!-- Add New Event -->
            <v-btn class="float-end" color="primary" prepend-icon="mdi-plus" flat @click="addEventModal = true">New Event</v-btn>
          </v-col>
        </v-row>

        <!-- Event List Table -->
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
                <td>{{ item.name }}</td>
                <td>{{ item.caseName }}</td>
                <td>{{ item.paralegalName }}</td>
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

  <!-- Add New Event Modal -->
  <v-dialog v-model="addEventModal" width="auto">
    <v-card elevation="10" class="withbg rounded-3 overflow-visible" width="500px">
      <v-card-title class="px-4 py-4 d-sm-flex align-center justify-space-between bg-background rounded-top-3">
        <h5 class="text-h5 mb-0 d-flex align-center">
          Add New Event
        </h5>
        <v-btn density="compact" variant="plain" icon="mdi-close" @click="addEventModal = false"/>
      </v-card-title>
      <v-divider class="m-0"/>
      <form @submit.prevent="addEvent">
        <v-card-item class="px-8 py-4 text-body-1">
          <v-row>
            <v-col>
              <v-label class="text-caption">Event Name</v-label>
              <v-text-field   
                v-model="addEventDetails.name.value" 
                :error-messages="addEventDetails.name.errorMessage"
                outlined dense/>
            </v-col>
          </v-row>
          <v-row>
            <v-col class="pb-0">
              <v-label class="text-caption">Case</v-label>
              <v-select
                :items="caseList"
                item-title="name"
                item-value="id"
                placeholder="Select case"
                variant="outlined"
                density="compact"
                :error-messages="addEventDetails.caseID.errorMessage"
                v-model="addEventDetails.caseID.value"
                hide-details="auto"
              />
            </v-col>
            <v-col>
              <v-label class="text-caption">Time</v-label>
              <el-date-picker
                :class="{ 'error': addEventDetails.eventTime.errorMessage }"
                placeholder="Select date and time"
                type="datetime"
                format="DD MMM YYYY, hh:mm A"
                date-format="DD MMM YYYY"
                time-format="HH:mm"
                :teleported="false"
                v-model="addEventDetails.eventTime.value"
                style="height: 40px;"
              />
              <v-messages
                :messages="addEventDetails.eventTime.errorMessage"
                :active="addEventDetails.eventTime.errorMessage ? true : false"
                color="#fa896b"
                :transition="false"
                style="padding: 3px 16px 3px; opacity: unset;"
              />
            </v-col>
          </v-row>
        </v-card-item>
        <v-card-actions class="p-3 justify-content-end">
          <v-btn color="primary" type="submit">Submit</v-btn>
        </v-card-actions>
      </form>
    </v-card>
  </v-dialog>
</template>

<script setup>
import { useField, useForm } from 'vee-validate'
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
  { key: "name", title: "Event Name" },
  { key: "caseName", title: "Case Name" },
  { key: "paralegalName", title: "Paralegal Name" },
  { key: "createdTime" , title: "Created Time" },
  { key: "eventTime", title: "Event Time"},
  { key: "isCompleted", title: "Status"},
])
// Data
const { handleSubmit } = useForm({
  validationSchema: {
    name(value) {
      return value ? true : 'Event name is required'
    },
    caseID(value) {
      return value ? true : 'Case is required'
    },
    eventTime(value) {
      return value ? true : 'Event time is required'
    }
  }
})


// Define reactive variables for dialog
const { data: eventList } = await fetchData.$get("/Event/PartnerPerspectiveEventList", filter.value)
const { data: caseList } = await fetchData.$get("/Case")
const { data: clientList } = await fetchData.$get("/Client")
const { data: userRole } = await fetchData.$get("/UserRole/RoleName")


// Define reactive variable for adding event modal
const addEventModal = ref(false);
const addEventDetails = ref({
  name: useField('name'),
  caseID: useField('caseID'),
  eventTime: useField('eventTime'),
})

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

//Add Event Methods
const addEvent = handleSubmit(async(values) => {
  try {
    const result = await fetchData.$post(`/Event/EventCreate`, values)
    if (!result.error) {
      addEventModal.value = false
      addEventDetails.value.name.resetField()
      addEventDetails.value.caseID.resetField()
      addEventDetails.value.eventTime.resetField()
      ElNotification.success({ message: result.message })
      refreshNuxtData()
    }
    else {
      ElNotification.error({ message: result.message })
    }
  } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
})


</script>