<template>
 <v-row>
    <v-col cols="12" md="12">
        <v-card elevation="10" class="withbg">
        <div class="hstack justify-content-center align-items-stretch">
          <div class="p-4 w-25 text-center">
          <!-- Client Profile Image -->
          <v-avatar
              class="border my-5"
              image="/images/users/avatar.jpg"
              size="110"
              style="border-width: 3px !important; border-color: lightgrey !important;"
            />
            <div class="mb-2 text-h5 d-sm-flex align-center justify-content-center">
              {{ eventInfo.clientName }}
            </div>
            <div>
              <span><strong>Phone Number: </strong>{{ eventInfo.clientPhone ?? '-' }}</span><br>
              <span><strong>Email: </strong>{{ eventInfo.clientEmail }}</span>
            </div>
          </div>
          <v-divider vertical/>
          <div class="p-4 w-75">
            <v-card-title class="text-h6 text-body-tertiary">
              Event Information
            </v-card-title>
            <v-card-item>
              <v-row class="pb-2">
                <v-col cols="4">
                  <div class="d-flex align-center">
                    <v-label class="text-h6 pb-3">Event Name</v-label>
                    <ul class="m-0 list-inline hstack">
                      <li>
                        <v-tooltip text="Rename" activator="parent" location="top" offset="2"/>
                        <v-btn class="mt-n2" icon="mdi-rename-outline" size="small" variant="text" @click="renameEventGet(eventInfo.id, eventInfo.name)"/>
                      </li>
                    </ul>
                  </div>
                  <v-card-subtitle>{{ eventInfo.name }}</v-card-subtitle>
                </v-col>

                <v-col cols="4">
                  <div class="d-flex align-center">
                    <v-label class="text-h6 pb-3">Related Case</v-label>
                  </div>
                  <v-card-subtitle>{{ eventInfo.caseName }}</v-card-subtitle>
                </v-col>

                <v-col cols="4">
                  <div class="d-flex align-center">
                    <v-label class="text-h6 pb-3">Status</v-label>
                    <ul class="m-0 list-inline hstack" >
                    <li>
                      <v-tooltip text="Update Status" activator="parent" location="top" offset="2"/>
                      <el-popconfirm 
                        title="Are you sure to update status of this event?"
                        icon-color="orange"
                        width="190"
                        @confirm="updateEvent(eventInfo.id)"
                      >
                        <template #reference>
                          <v-btn class="mt-n2" icon="mdi-update" size="small" variant="text"/>
                        </template>
                      </el-popconfirm>
                    </li>
                    </ul>
                  </div>
                    <td>
                      <el-tag type="success" v-if="eventInfo.isCompleted">Completed</el-tag>
                      <el-tag type="danger" v-else>Incompleted</el-tag>
                    </td>
                  </v-col>
              </v-row>
              <v-row class="pb-2">
                <v-col>
                  <div class="d-flex align-center">
                    <v-label class="text-h6 pb-3">Description</v-label>
                  </div>
                  <v-card-subtitle>{{ eventInfo.description }}</v-card-subtitle>
                </v-col>
              </v-row>
                <v-row class="pb-2">
                <v-col>
                  <div class="d-flex align-center">
                    <v-label class="text-h6 pb-3">Related Documents</v-label>
                  </div>
                  <div v-if="eventInfo.documentName" class=" d-flex flex-column event-details-container pa-md-4">
                    <v-divider vertical class="mx-5 my-0" style="border-color: white !important; opacity: 0.5;"></v-divider>
                    <div>
                      <div class="event-detail-item"><v-card-subtitle>{{ eventInfo.documentName }}</v-card-subtitle></div>
                    </div>
                  </div>
                  <!-- Display a message if no related document -->
                  <div v-else class=" d-flex flex-column event-details-container pa-md-4">
                    <v-divider vertical class="mx-5 my-0" style="border-color: white !important; opacity: 0.5;"></v-divider>
                    <div>
                      <div class="event-detail-item"><v-card-subtitle>No Related Document</v-card-subtitle></div>
                    </div>
                  </div>
                </v-col>
              </v-row>
              <v-row class="pb-2">
                <v-col>
                  <v-file-input label="Upload Documents" variant="outlined"></v-file-input>
                </v-col>
              </v-row>
            </v-card-item>
          </div>
        </div>
      </v-card>
    </v-col>
</v-row>

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
import { ref, shallowRef } from 'vue';
import { CalendarIcon } from "vue-tabler-icons"

const filter = ref({
  clientId: null,
})

// Define reactive variables for dialog
const routeParameter = ref(useRoute().params)
const { data: eventInfo } = await fetchData.$get(`/Event/Info/${routeParameter.value.eventID}`)
const { data: eventList } = await fetchData.$get("/Event/PartnerPerspectiveEventList", filter.value)
const { data: clientList } = await fetchData.$get("/Client")

const renameEventDetails = ref({
  eventId: null,
  name: null,
})
const renameEventModal = ref(false)

/*
const editClientDetails = ref({
  caseId: null,
  clientId: null,
});
const editClientModal = ref(false);
*/

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
      href: '/events',
    },
    {
      title: 'Event Details',
      disabled: false,
    },
  ]
})

// Methods
const pageCount = () => {
  return Math.ceil(eventList.value.length / itemsPerPage.value)
}

//Rename Event Methods
const renameEventGet = (eventId, caseName) => {
  renameEventDetails.value.eventId = eventId
  renameEventDetails.value.name = caseName
  renameEventModal.value = true
}

const props = defineProps({
  eventId: Number,
});

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
};
/*
// Edit Client Event Methods
const editClientGet = (caseId, clientId) => {
  editClientDetails.value.caseId = caseId;
  editClientDetails.value.clientId = clientId;
  editClientModal.value = true;
};
*/


</script>