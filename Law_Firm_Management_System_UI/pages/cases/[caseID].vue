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
              <!--Status Bar-->
              <v-row class="pb-2">
                <v-col>
                  <v-label class="text-h6 pb-3">Current Case Status</v-label>
                  <!-- Make Vuetify timeline horizontal -->
                  <v-timeline direction="horizontal">
                    <v-timeline-item
                      v-for="(status, index) in statusList"
                      :key="index"
                      dot-color="#ddd"
                      :class="{ 'clickable-circle': true, 'current-status': index === caseInfo.statusId - 1 }"
                      @click="showConfirmationDialog(caseInfo.id, status.name)"
                    >
                      <v-timeline-item-title>
                        <span :class="{ 'current-status-name': index === caseInfo.statusId - 1 }">{{ status.name }}</span>
                      </v-timeline-item-title>
                    </v-timeline-item>
                  </v-timeline>
                  <div class="status-description">
                    <strong>{{ caseInfo.statusDescription }}</strong>
                  </div>
                </v-col>
              </v-row>
              
              
              <!-- Status Bar
            <div class="status-container">
              <div class="pa-md-4"><strong>Current Case Status</strong></div>
              <div class="status-bar">
                <div
                  v-for="(status, index) in statusList"
                  :key="index"
                  class="status-item"
                  :class="{ 'current-status': index === selectedCaseDetails.statusId - 1 }"
                >
                  <div class="status-part">
                    <div
                      class="circle"
                      @click="changeCaseStatus(selectedCaseDetails.id, status.name)"
                    >
                      {{ status.id }}
                    </div>
                    <span class="status-name" :class="{ 'bold': index === selectedCaseDetails.statusId - 1 }">
                      {{ status.name }}
                    </span>
                  </div>
                </div>
              </div>
            <div class="status-description">
                <strong>{{ selectedCaseDetails.statusDescription }}</strong>
            </div>
          </div>-->

            <!--Related Document-->
            <v-row class="pb-2">
              <v-col>
                <div class="d-flex align-center">
                  <v-label class="text-h6 pb-3">Related Documents</v-label>
                </div>
                <div v-if="caseDocumentList && caseDocumentList.length > 0" class="d-flex flex-column case-details-container pa-md-4">
                  <v-divider vertical class="mx-5 my-0" style="border-color: white !important; opacity: 0.5;"></v-divider>
                  <div v-for="document in caseDocumentList" :key="document.id">
                    <div class="case-detail-item">
                      <a :href="`/api/Document/GetDocument/${document.id}`" target="_blank">
                        <v-card-subtitle>{{ document.documentName }}</v-card-subtitle>
                      </a>
                    </div>
                  </div>
                </div>
                <!-- Display a message if no related document -->
                <div v-else class="d-flex flex-column case-details-container pa-md-4">
                  <v-divider vertical class="mx-5 my-0" style="border-color: white !important; opacity: 0.5;"></v-divider>
                  <div>
                    <div class="case-detail-item"><v-card-subtitle>No Related Document</v-card-subtitle></div>
                  </div>
                </div>
              </v-col>
            </v-row>
              <!--Upload Documents-->
              <v-file-input
                v-model="selectedFile"
                accept=".pdf, .doc, .docx"
                show-size
                label="Upload Document"
                @change="handleFileUpload"
              ></v-file-input>
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

<style scoped>
.clickable-circle {
  cursor:pointer;
  background-color: #ddd !important;
}
.current-status-name {
  color:#2b4c65;
  font-weight: bold;
}

.current-status .v-timeline-item-dot {
  background-color: #2b4c65 !important;
}
</style>

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
const { data: caseDocumentList } = await fetchData.$get(`/Document/GetDocumentNamesByCase/${routeParameter.value.caseID}`);
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
  { id: 5, name: 'On Hold' },
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

// Data for handling file upload
const selectedFile = ref(null);

// Method for handling file upload
const handleFileUpload = () => {
  // Check if a file is selected
  if (selectedFile.value) {
    // You can access the selected file using selectedFile.value
    const file = selectedFile.value;

    // Perform any logic you need with the selected file
    console.log('Selected file:', file);
  }
};

const showConfirmationDialog = async (caseId, newStatus) => {
  try {
    const confirmDialogOptions = {
      title: 'Are you sure to change the status?',
      icon: 'el-icon-warning-outline',
      type: 'warning',
      customClass: 'status-change-confirm-dialog', // Add a custom class for styling if needed
      showCancelButton: true,
      confirmButtonText: 'Yes',
      cancelButtonText: 'No',
    };

    const confirmResult = await ElMessageBox.confirm('Are you sure to change the status?', 'Warning', {
      confirmButtonText: 'Yes',
      cancelButtonText: 'No',
      type: 'warning',
    });

    if (confirmResult === 'confirm') {
      // User clicked "Yes" in the confirmation dialog
      await changeCaseStatus(caseId, newStatus);
    }
  } catch (error) {
    console.error('Error showing confirmation dialog:', error);
  }
};

//Change Case Status
const changeCaseStatus = async (caseId, newStatus) => {
  
  try {
    const response = await fetchData.$put(`/Case/ChangeStatus/${caseId}`, {
      NewStatus: newStatus,
    });

    if (!response.error) {
      // Update the ClosedTime if the new status is "Settled"
      if (newStatus === 'Settled') {
        // Make a new request to update the ClosedTime
        const closedTimeResponse = await fetchData.$put(`/Case/UpdateClosedTime/${caseId}`);
        console.log('ClosedTime updated:', closedTimeResponse);
      }

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