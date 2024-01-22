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

        <!-- Event List Table Partner -->
        <div class="pa-7 pt-1 text-body-1" v-if="userRole == 'Partner'">
          <v-data-table 
            density="comfortable"
            v-model:page="currentPage"
            :headers="headersPartner"
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
                <td>{{ dayjs(item.createdTime).format("DD MMM YYYY, hh:mm A") }}</td>
                <td>{{ dayjs(item.eventTime).format("DD MMM YYYY, hh:mm A") }}</td>
                <td>
                  <el-tag type="success" v-if="item.isCompleted">Completed</el-tag>
                  <el-tag type="danger" v-else>Incompleted</el-tag>
                </td>
                <td>
                  <ul class="m-0 list-inline hstack">
                    <!--li>
                      <v-tooltip text="Update Status" activator="parent" location="top" offset="2"/>
                      <el-popconfirm
                        title="Are you sure to update status of this event?"
                        icon-color="orange"
                        width="190"
                        @confirm="updateEvent(item.id)"
                      >
                        <template #reference>
                          <v-btn icon="mdi-update" size="small" variant="text"/>
                        </template>
                      </el-popconfirm>
                    </li>
                    <li>
                      <v-tooltip text="Rename" activator="parent" location="top" offset="2"/>
                      <v-btn icon="mdi-rename-outline" size="small" variant="text" @click="renameEventGet(item.id, item.name)"/>
                    </li-->
                    <li>
                      <v-tooltip text="Delete Permanently" activator="parent" location="top" offset="2"/>
                      <el-popconfirm
                        title="Are you sure to delete this document permanently?"
                        icon-color="red"
                        width="190"
                        @confirm="deleteEvent(item.id)"
                      >
                        <template #reference>
                          <v-btn icon="mdi-delete-forever-outline" size="small" variant="text"/>
                        </template>
                      </el-popconfirm>
                    </li>
                    <li>
                      <v-tooltip text="View Details" activator="parent" location="top" offset="2"/>
                      <v-btn icon="mdi-open-in-new" size="small" variant="text" :href="`/events/${item.id} `" target="_blank"/>
                    </li>
                  </ul>
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

        <!-- Event List Table Paralegal-->
        <div class="pa-7 pt-1 text-body-1" v-if="userRole == 'Paralegal'">
          <v-data-table 
            density="comfortable"
            v-model:page="currentPage"
            :headers="headersParalegal"
            :items="eventListParalegal"
            :items-per-page="itemsPerPage"
            hover
          >
            <template v-slot:item="{ item }">
              <tr>
                <td>{{ eventListParalegal.indexOf(item) + 1 }}</td>
                <td>{{ item.name }}</td>
                <td>{{ item.caseName }}</td>
                <td>{{ item.partnerName }}</td>
                <td>{{ dayjs(item.createdTime).tz('Asia/London').format("DD MMM YYYY, hh:mm A") }}</td>
                <td>{{ dayjs(item.eventTime).tz('Asia/London').format("DD MMM YYYY, hh:mm A") }}</td>
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
                  :page-size="eventListParalegal.length/pageCount()"
                  :total="eventListParalegal.length"
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
  <el-scrollbar max-height="600px">
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
                variant="outlined"
                density="compact"
                :error-messages="addEventDetails.name.errorMessage"
                v-model="addEventDetails.name.value"
                hide-details="auto"
                outlined dense
              />
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
                :disabled-date="(time) => { return time.getTime() < new Date() }"
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
          <v-row>
            <v-col>
              <v-label class="text-caption">Description</v-label>
              <v-textarea
                variant="outlined"
                density="compact"
                :error-messages="addEventDetails.description.errorMessage"
                v-model="addEventDetails.description.value"
                hide-details="auto"
              />
            </v-col>
          </v-row>
        </v-card-item>
        <v-card-actions class="p-3 justify-content-end">
          <v-btn color="primary" type="submit">Submit</v-btn>
        </v-card-actions>
      </form>
    </v-card> 
  </el-scrollbar>
  </v-dialog>


  <!-- Rename Event Modal -->
  <SharedUiModal v-model="renameEventModal" title="Rename Event" width="500">
    <EventRenameForm
      :eventId="renameEventDetails.eventId"
      :eventName="renameEventDetails.name"
      @close-modal="(e) => renameEventModal = e"
    />
  </SharedUiModal>
</template>

<script setup>
import { useField, useForm } from 'vee-validate'
import dayjs from 'dayjs';
import { CalendarIcon } from "vue-tabler-icons"
import UiParentCard from "@/components/shared/UiParentCard.vue";
import moment from 'moment-timezone'
moment.tz.setDefault('Asia/London')

const filter = ref({
  caseID: null,
  paralegalID: null,
  isCompleted: null,
})

// Data
const { data: user } = useAuth()
const currentPage = ref(1)
const itemsPerPage = ref(10)

const headersPartner = ref([
  { key: "number", title: "No." },
  { key: "name", title: "Event Name" },
  { key: "caseName", title: "Case Name" },
  { key: "paralegalName", title: "Paralegal Name" },
  { key: "createdTime" , title: "Created Time" },
  { key: "eventTime", title: "Event Time"},
  { key: "isCompleted", title: "Status"},
])

const headersParalegal = ref([
  { key: "number", title: "No." },
  { key: "name", title: "Event Name" },
  { key: "caseName", title: "Case Name" },
  { key: "partnerName", title: "Partner Name" },
  { key: "createdTime" , title: "Created Time" },
  { key: "eventTime", title: "Event Time"},
  { key: "isCompleted", title: "Status"},
])

const { handleSubmit } = useForm({
  validationSchema: {
    name(value) {
      return value ? true : 'Event name is required'
    },
    description(value) {
      return value ? true : 'Description is required'
    },
    caseID(value) {
      return value ? true : 'Case is required'
    },
    eventTime(value) {
      return value ? true : 'Event time is required'
    }
  }
})

const renameEventDetails = ref({
  eventId: null,
  name: null,
})
const renameEventModal = ref(false)

// Define reactive variables for dialog
const { data: eventList } = await fetchData.$get("/Event/PartnerPerspectiveEventList", filter.value)
const { data: eventListParalegal } = await fetchData.$get("/Event/ParalegalPerspectiveEventList", filter.value)
const { data: caseList } = await fetchData.$get("/Case")
const { data: userRole } = await fetchData.$get("/UserRole/RoleName")


// Define reactive variable for adding event modal
const addEventModal = ref(false);
const addEventDetails = ref({
  name: useField('name'),
  description: useField('description'),
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
    const result = await fetchData.$post(`/Event/EventCreate`, {
      name: values.name,
      caseID: values.caseID,
      eventTime: dayjs(values.eventTime).format("DD MMM YYYY, h:mm A"),
      description: values.description,
    })

    if (!result.error) {
      addEventModal.value = false
      addEventDetails.value.name.resetField()
      addEventDetails.value.description.resetField()
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

/*
//Rename Event Methods
const renameEventGet = (eventId, eventName) => {
  renameEventDetails.value.eventId = eventId
  renameEventDetails.value.name = eventName
  renameEventModal.value = true
}

//Update event status
const updateEvent = async(eventId) => {
  try {
    const result = await fetchData.$put(`/Event/Update/${eventId}`)
    if (!result.error) {
      ElNotification.success({ message: result.message })
      refreshNuxtData()
    }
    else {
      ElNotification.error({ message: result.message })
    }
  } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
}
*/

//Delete event
const deleteEvent = async(eventId) => {
  try {
    const result = await fetchData.$delete(`/Event/${eventId}`)
    if (!result.error) {
      ElNotification.success({ message: result.message })
      refreshNuxtData()
    }
    else {
      ElNotification.error({ message: result.message })
    }
  } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
}
</script>