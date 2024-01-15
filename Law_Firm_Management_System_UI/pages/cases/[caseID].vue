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
              {{ caseInfo.clientName }}
              <ul class="m-0 list-inline hstack">
                <li>
                  <v-tooltip text="Rename" activator="parent" location="top" offset="2"/>
                  <v-btn icon="mdi-rename-outline" size="small" variant="text" @click="editClientGet(caseInfo.id, caseInfo.clientId)"/>
                </li>
              </ul>
            </div>
            <div>
              <span><strong>Phone Number: </strong>{{ caseInfo.clientPhone ?? '-' }}</span><br>
              <span><strong>Email: </strong>{{ caseInfo.clientEmail }}</span>
            </div>
          </div>
          <v-divider vertical/>
          <div class="p-4 w-75">
            <v-card-title class="text-h6 text-body-tertiary">
              Case Information
            </v-card-title>
            <v-card-item>
              <v-row class="pb-2">
                <v-col>
                  <div class="d-flex align-center">
                    <v-label class="text-h6 pb-3">Case Name</v-label>
                    <ul class="m-0 list-inline hstack">
                      <li>
                        <v-tooltip text="Rename" activator="parent" location="top" offset="2"/>
                        <v-btn class="mt-n2" icon="mdi-rename-outline" size="small" variant="text" @click="renameCaseGet(caseInfo.id, caseInfo.name)"/>
                      </li>
                    </ul>
                  </div>
                  <v-card-subtitle>{{ caseInfo.name }}</v-card-subtitle>
                </v-col>
              </v-row>
                <v-row class="pb-2">
                  <v-col>
                    <v-label class="text-h6 pb-3">Current Case Status</v-label>
                    <div class="status-container">
                      <div class="status-bar">
                        <div
                          v-for="(status, index) in statusList"
                          :key="index"
                          class="status-item"
                          :class="{ 'current-status': index === caseInfo.statusId - 1 }"
                        >
                          <div class="status-part">
                            <el-popconfirm
                              title="Are you sure to change the status?"
                              icon-color="red"
                              width="190"
                              @confirm="changeCaseStatus(caseInfo.id, status.name)"
                            >
                              <template #reference>
                                <div class="circle">
                                  {{ status.id }}
                                </div>
                              </template>
                            </el-popconfirm>
                            <span class="status-name" :class="{ 'bold': index === caseInfo.statusId - 1 }">
                              {{ status.name }}
                            </span>
                          </div>
                        </div>
                      </div>
                      <!--Current Status Description-->
                      <div class="status-description">
                        <strong>{{ caseInfo.statusDescription }}</strong>
                      </div>
                    </div>
                  </v-col>
                </v-row>
                <v-row class="pb-2">
                <v-col>
                  <div class="d-flex align-center">
                    <v-label class="text-h6 pb-3">Related Documents</v-label>
                  </div>
                  <div v-if="caseInfo.documentName" class=" d-flex flex-column case-details-container pa-md-4">
                    <v-divider vertical class="mx-5 my-0" style="border-color: white !important; opacity: 0.5;"></v-divider>
                    <div>
                      <div class="case-detail-item"><v-card-subtitle>{{ caseInfo.documentName }}</v-card-subtitle></div>
                    </div>
                  </div>
                  <!-- Display a message if no related document -->
                  <div v-else class=" d-flex flex-column case-details-container pa-md-4">
                    <v-divider vertical class="mx-5 my-0" style="border-color: white !important; opacity: 0.5;"></v-divider>
                    <div>
                      <div class="case-detail-item"><v-card-subtitle>No Related Document</v-card-subtitle></div>
                    </div>
                  </div>
                </v-col>
              </v-row>
            </v-card-item>
          </div>
        </div>
      </v-card>
    </v-col>
</v-row>

<!--Rename Case-->
<SharedUiModal v-model="renameCaseModal" title="Rename Case" width="500">
    <CaseRenameForm
      :caseId="renameCaseDetails.caseId"
      :caseName="renameCaseDetails.name"
      @close-modal="(e) => renameCaseModal = e"
    />
  </SharedUiModal>

  <!--Edit Client-->
  <SharedUiModal v-model="editClientModal" title="Edit Client" width="500">
  <CaseEditClientForm
    :caseId="editClientDetails.caseId"
    :clientId="editClientDetails.clientId"
    @close-modal="(e) => editClientModal = e"
  />
</SharedUiModal>
</template>

<script setup>
import { ref, shallowRef } from 'vue';
import { BriefcaseIcon } from "vue-tabler-icons"

const filter = ref({
  clientId: null,
  status:null,
})

// Define reactive variables for dialog
const routeParameter = ref(useRoute().params)
const { data: caseInfo } = await fetchData.$get(`/Case/Info/${routeParameter.value.caseID}`)
const { data: caseList } = await fetchData.$get("/Case/PartnerPerspectiveCaseList", filter.value)
const { data: clientList } = await fetchData.$get("/Client")

const renameCaseDetails = ref({
  caseId: null,
  name: null,
})
const renameCaseModal = ref(false)

const editClientDetails = ref({
  caseId: null,
  clientId: null,
});
const editClientModal = ref(false);

// Head
useHead({
  title: "Cases | CaseCraft",
})

// Page Meta
definePageMeta({
  breadcrumbsIcon: shallowRef(BriefcaseIcon),
  breadcrumbs: [
    {
      title: 'Cases',
      disabled: false,
      href: '/cases',
    },
    {
      title: 'Case Details',
      disabled: false,
    },
  ]
})

// Methods
const pageCount = () => {
  return Math.ceil(caseList.value.length / itemsPerPage.value)
}

//Open Case Details Dialogs
const isDialogOpen = ref(false);
const selectedCaseDetails = ref(null);
const openDialog = (caseDetails) => {
  selectedCaseDetails.value = caseDetails;
  isDialogOpen.value = true;
};

// Define reactive variable for adding case modal
const addCaseModal = ref(false);

// Define reactive variable for new case details
const newCaseDetails = reactive({
  name: '',
  clientId: null,
  status: null,
});

// Methods
const statusList = ref([
  { id: 1, name: 'Active' },
  { id: 2, name: 'Under Review' },
  { id: 3, name: 'Negotiation' },
  { id: 4, name: 'Court Proceedings' },
  { id: 5, name: 'In Hold' },
  { id: 6, name: 'Settled' },
  // ... other statuses ...
]);

//Rename Event Methods
const renameCaseGet = (caseId, caseName) => {
  renameCaseDetails.value.caseId = caseId
  renameCaseDetails.value.name = caseName
  renameCaseModal.value = true
}

const props = defineProps({
  caseId: Number,
});

// Edit Client Event Methods
const editClientGet = (caseId, clientId) => {
  editClientDetails.value.caseId = caseId;
  editClientDetails.value.clientId = clientId;
  editClientModal.value = true;
};

//Change Case Status
const changeCaseStatus = async (caseId, newStatus) => {
  try {
    console.log('Changing status for caseId:', caseId, 'to', newStatus);

    const response = await fetchData.$put(`/Case/ChangeStatus/${caseId}`, {
      NewStatus: newStatus,
    });

    console.log('Response from server:', response);

    if (response && response.data) {
      // Update the ClosedTime if the new status is "Settled"
      if (newStatus === 'Settled') {
        // Make a new request to update the ClosedTime
        const closedTimeResponse = await fetchData.$put(`/Case/UpdateClosedTime/${caseId}`);
        console.log('ClosedTime updated:', closedTimeResponse);
      }

      // Optionally, you can refresh the case details or perform other actions
      // based on the successful status change.
      ElNotification.success({ message: response.data.message });
      refreshNuxtData(); // Refresh the case list or perform any necessary actions
    } else {
      ElNotification.error({ message: "Failed to change case status" });
    }
  } catch (error) {
    console.error('Error while changing case status:', error);
    ElNotification.error({ message: "There is a problem with the server. Please try again later." });
  }
};
</script>