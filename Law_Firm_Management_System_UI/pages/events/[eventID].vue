<template>
 <v-row>
    <v-col cols="12" md="12">
        <v-card elevation="10" class="withbg">
        <div class="hstack justify-content-center align-items-stretch">
          <div class="p-4 w-25 text-center">
          <!-- Client Profile Image -->
          <v-avatar
              class="border my-5"
              :image="clientInfo.profilePhoto ? getProfilePhoto(clientInfo.profilePhoto) : '/images/users/avatar.jpg'"
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
                        <v-btn class="mt-n3" icon="mdi-rename-outline" size="small" variant="text" @click="renameEventGet(eventInfo.id, eventInfo.name)"/>
                      </li>
                    </ul>
                  </div>
                  <v-card-subtitle>{{ eventInfo.name }}</v-card-subtitle>
                </v-col>

                <v-col cols="4">
                  <div class="d-flex align-center">
                    <v-label class="text-h6 pb-3">Related Case</v-label>
                    <ul class="m-0 list-inline hstack">
                      <li>
                        <v-tooltip text="Edit Case" activator="parent" location="top" offset="2"/>
                        <v-btn class="mt-n3" icon="mdi-rename-outline" size="small" variant="text" @click="editCaseGet(eventInfo.id, eventInfo.caseId)"/>
                      </li>
                    </ul>
                  </div>
                  <v-card-subtitle>
                  <a :href="`/cases/${eventInfo.caseID}`" target="_blank" class="row-link">{{ eventInfo.caseName }}</a>
                  </v-card-subtitle>
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
                          <v-btn class="mt-n3" icon="mdi-update" size="small" variant="text"/>
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
                  <v-col cols="4">
                  <div class="d-flex align-center">
                    <v-label class="text-h6 pb-3">Event Time</v-label>
                    <ul class="m-0 list-inline hstack">
                      <li>
                        <v-tooltip text="Reschedule" activator="parent" location="top" offset="2"/>
                        <v-btn class="mt-n3" icon="mdi-rename-outline" size="small" variant="text" @click="editTimeGet(eventInfo.id, eventInfo.time)"/>
                      </li>
                    </ul>
                  </div>
                  <v-card-subtitle>{{ dayjs(eventInfo.eventTime).format("DD MMM YYYY, hh:mm A") }}</v-card-subtitle>
                </v-col>
                <v-col cols="4">
                  <div class="d-flex align-center">
                    <v-label class="text-h6 pb-3">Description</v-label>
                    <ul class="m-0 list-inline hstack">
                      <li>
                        <v-tooltip text="Edit Description" activator="parent" location="top" offset="2"/>
                        <v-btn class="mt-n3" icon="mdi-rename-outline" size="small" variant="text" @click="editDescriptionGet(eventInfo.id, eventInfo.description)"/>
                      </li>
                    </ul>
                  </div>
                  <v-card-subtitle>{{ eventInfo.description }}</v-card-subtitle>
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

  <!-- Edit Case Modal -->
  <SharedUiModal v-model="editCaseModal" title="Edit Case" width="500">
  <EventEditCase
      :eventId="editCaseDetails.eventId"
      :caseId="editCaseDetails.caseId"
      @close-modal="(e) => editCaseModal = e"
    />
  </SharedUiModal>

  <!-- Edit Event Time Modal -->
  <SharedUiModal v-model="editTimeModal" title="Edit Event Time" width="500">
  <EventEditTime
      :eventId="editTimeDetails.eventId"
      :eventTime="editTimeDetails.eventTime"
      @close-modal="(e) => editTimeModal = e"
    />
  </SharedUiModal>

  <!-- Edit Event Description Modal -->
  <SharedUiModal v-model="editDescriptionModal" title="Edit Event Description" width="500">
  <EventEditDescription
      :eventId="editDescriptionDetails.eventId"
      :description="editDescriptionDetails.description"
      @close-modal="(e) => editDescriptionModal = e"
    />
  </SharedUiModal>
</template>

<script setup>
import { ref, shallowRef } from 'vue';
import { CalendarIcon } from "vue-tabler-icons"
import dayjs from 'dayjs';
import { Buffer } from 'buffer'

const filter = ref({
  clientId: null,
})

// Define reactive variables for dialog
const routeParameter = ref(useRoute().params)
const { data: eventInfo } = await fetchData.$get(`/Event/Info/${routeParameter.value.eventID}`)
const { data: eventList } = await fetchData.$get("/Event/PartnerPerspectiveEventList", filter.value)
const { data: clientInfo } = await fetchData.$get(`/Client/Info/${eventInfo.value.clientId}`)

//avatar profile 
const getProfilePhoto = (attachment) => {
    const arrayBuffer = Buffer.from(attachment, 'base64');
    const blob = new Blob([arrayBuffer], { type: 'image/jpeg' })
    return URL.createObjectURL(blob)
  }

const renameEventDetails = ref({
  eventId: null,
  name: null,
})
const renameEventModal = ref(false)

const editCaseDetails = ref({
  eventId: null,
  caseId: null,
});
const editCaseModal = ref(false);

const editTimeDetails = ref({
  eventId: null,
  eventTime: null,
});
const editTimeModal = ref(false);

const editDescriptionDetails = ref({
  eventId: null,
  description: null,
});
const editDescriptionModal = ref(false);

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

// Edit Event Case Methods
const editCaseGet = (eventId, caseId) => {
  editCaseDetails.value.eventId = eventId;
  editCaseDetails.value.caseId = caseId;
  editCaseModal.value = true;
};

// Edit Event Time Methods
const editTimeGet = (eventId, eventTime) => {
  editTimeDetails.value.eventId = eventId;
  editTimeDetails.value.time = eventTime;
  editTimeModal.value = true;
};

// Edit Event Description Methods
const editDescriptionGet = (eventId, description) => {
  editDescriptionDetails.value.eventId = eventId;
  editDescriptionDetails.value.description = description;
  editDescriptionModal.value = true;
};

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
</script>