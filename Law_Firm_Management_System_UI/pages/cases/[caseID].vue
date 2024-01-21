//cases
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
            <div class="mb-2 text-h5 d-sm-flex align-center justify-content-center" >
              <div style="flex-direction: column;">
                <div style="display: flex; justify-content: center;">
                  <span>{{ caseInfo.clientName }}</span>
                  <ul class="m-0 list-inline hstack">
                    <li>
                      <v-tooltip text="Rename" activator="parent" location="top" offset="2"/>
                      <v-btn class="mt-n1" icon="mdi-rename-outline" size="small" variant="text" @click="editClientGet(caseInfo.id, caseInfo.clientId)"/>
                    </li>
                  </ul>
                </div>
                <div style="display: flex; flex-direction: column;">
                  <span><strong>Phone Number: </strong>{{ caseInfo.clientPhone ?? '-' }}</span>
                  <span><strong>Email: </strong>{{ caseInfo.clientEmail }}</span>
                </div>
              </div>
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
                  <v-timeline direction="horizontal" class="timeline-status">
                    <v-timeline-item
                      v-for="(status, index) in statusList"
                      :key="index"
                      :dot-color="index === caseInfo.statusId - 1 ? '#2b4c65' : '#ddd'"
                      :class="{ 'clickable-circle': true, 'current-status': index === caseInfo.statusId - 1 }"
                      @click="userRole == 'Partner' ? showConfirmationDialog(caseInfo.id, status.name) : null"
                    >
                      <v-timeline-item-title>
                        <span :class="{ 'current-status-name': index === caseInfo.statusId - 1 }">
                          {{ status.name }}
                        </span>
                      </v-timeline-item-title>
                    </v-timeline-item>
                  </v-timeline>
                  <div class="status-description">
                    <strong>{{ caseInfo.statusDescription }}</strong>
                  </div>
                </v-col>
              </v-row>

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
                      <v-list-item
                        class="client-case-document-list mb-2 rounded-3 bg-background list-link"
                        density="compact"
                        prepend-icon="mdi-file-document-outline"
                        link
                        @click="downloadDocument(document.id)"
                        style="padding: 10px;"
                      >
                        {{ document.documentName }}
                        <ul class="m-0 list-inline hstack">
                          <li>
                            <v-tooltip text="Update Document" activator="parent" location="top" offset="2"/>
                            <v-btn class = "mt-n1" icon="mdi-upload-outline" size="small" variant="text" @click.stop="getEditAttachmentInfo(document.id)"/>
                          </li>
                        </ul>
                      </v-list-item>
                    </div>
                    <div class="mb-5" v-if="document.isSignedDocument == true">
                      <v-card-subtitle>This document require client's signed.</v-card-subtitle>
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
            
              <!-- Add New Document Button -->
                <el-affix
                  class="position-absolute"
                  position="bottom"
                  :offset="30"
                  style="right: 20px; bottom: 20px;"
                >
                  <v-tooltip text="Upload Document" activator="parent" location="left" offset="2"/>
                  <v-btn icon="mdi-file-document-plus-outline" color="primary" size="large" @click="addDocumentModal = true"/>
                </el-affix>
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

<!-- Add New Document Modal -->
<SharedUiModal v-model="addDocumentModal" title="Add New Document" width="500">
  <CaseUploadDocument 
  :caseId="caseInfo.id"
  @close-modal="(e) => addDocumentModal = e"/>
</SharedUiModal>

   <!-- Edit Document Attachment Modal -->
   <SharedUiModal v-model="editAttachmentModal" title="Update Document File" width="500">
    <DocumentEditAttachmentForm
      :document-id="editAttachmentInfoId"
      @close-modal="(e) => editAttachmentModal = e"
    />
  </SharedUiModal>

</template>

<style scoped>
.timeline-status {
  padding:20px 0;
}

.clickable-circle {
  cursor:pointer;
  background-color: #ddd !important;
}
.current-status-name {
  color:#2b4c65;
  font-weight: bold;
}
</style>

<script setup>
import { ref, shallowRef } from 'vue';
import { BriefcaseIcon } from "vue-tabler-icons"
import { Buffer } from 'buffer'

const filter = ref({
  clientId: null,
  status:null,
})

// Define reactive variables for dialog
const routeParameter = ref(useRoute().params)
const { data: caseInfo } = await fetchData.$get(`/Case/Info/${routeParameter.value.caseID}`)
const { data: caseList } = await fetchData.$get("/Case/PartnerPerspectiveCaseList", filter.value)
const { data: caseDocumentList } = await fetchData.$get(`/Document/GetDocumentNamesByCase/${routeParameter.value.caseID}`);
const { data: userRole } = await fetchData.$get("/UserRole/RoleName")
const { data: clientInfo } = await fetchData.$get(`/Client/Info/${caseInfo.clientID}`)

const addDocumentModal = ref(false)
const editAttachmentInfoId = ref(null)
const editAttachmentModal = ref(false)
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

// Methods
const statusList = ref([
  { id: 1, name: 'Active' },
  { id: 2, name: 'Under Review' },
  { id: 3, name: 'Negotiation' },
  { id: 4, name: 'Court Proceedings' },
  { id: 5, name: 'On Hold' },
  { id: 6, name: 'Settled' },
]);

const getProfilePhoto = (attachment) => {
  const arrayBuffer = Buffer.from(attachment, 'base64');
  const blob = new Blob([arrayBuffer], { type: 'image/jpeg' })
  return URL.createObjectURL(blob)
}

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

const getEditAttachmentInfo = (docId) => {
  editAttachmentInfoId.value = docId
  editAttachmentModal.value = true
}

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
    const result = await fetchData.$put(`/Case/ChangeStatus/${caseId}`, {
      NewStatus: newStatus,
    });

    if (!result.error) {
      // Update the ClosedTime if the new status is "Settled"
      if (newStatus === 'Settled') {
        await fetchData.$put(`/Case/UpdateClosedTime/${caseId}`);
      }
      ElNotification.success({ message: result.message });
      refreshNuxtData(); // Refresh the case list or perform any necessary actions
    } else {
      ElNotification.error({ message: "Failed to change case status" });
    }
  } catch (error) {
    console.error('Error while changing case status:', error);
    ElNotification.error({ message: "There is a problem with the server. Please try again later." });
  }
};

const downloadDocument = async(docId) => {
  const { data: docInfo } = await fetchData.$get(`/Document/Info/${docId}`)
  const { data: attachment } = await fetchData.$get(`/Document/GetAttachment/${docId}`)
  const mimeType = {
    "PDF": "application/pdf",
    "Word": "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
    "Excel": "application/vnd.ms-excel",
  }
  const arrayBuffer = Buffer.from(attachment.value, 'base64');
  const blob = new Blob([arrayBuffer], { type: mimeType[docInfo.value.type] })
  const url = URL.createObjectURL(blob)

  if (docInfo.value.type == 'PDF')
    window.open(url, '_blank')
  else {
    const link = document.createElement('a')
    link.href = url
    link.download = docInfo.value.name
    link.click()
    document.body.removeChild(link)
  }
}
</script>