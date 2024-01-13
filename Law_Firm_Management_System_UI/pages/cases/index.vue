<template>
  <v-row>
    <v-col>
      <UiParentCard title="Case Management"> 
        <v-row class="px-7">
          <!-- Filters -->
          <v-col class="pe-0" cols="2">
            <v-select
              :items="clientList"
              item-title="fullName"
              item-value="id"
              placeholder="Client"
              density="compact"
              bg-color="white"
              variant="outlined"
              v-model="filter.clientId"
              hide-details
              :single-line="true"
            >
              <template v-slot:prepend-item>
                <v-list-item title="All Clients" @click="filter.clientId = null"/>
              </template>
            </v-select>
          </v-col>

          <v-col cols="2">
            <v-select
              :items="['Active', 'Under Review', 'Negotiation', 'Court Proceeding', 'On Hold', 'Settled']"
              placeholder="Status"
              density="compact"
              bg-color="white"
              variant="outlined"
              v-model="filter.status"
              hide-details
            >
              <template v-slot:prepend-item>
                <v-list-item title="All Statuses" @click="filter.status = null"/>
              </template>
            </v-select>
          </v-col>
          <v-col>
            <!-- Add New Case -->
            <v-btn class="float-end" color="primary" flat @click="addCaseModal = true">
              <v-icon>mdi-plus</v-icon> &nbsp; New Case
            </v-btn>
          </v-col>
        </v-row>
        <!--Case List-->
        <div class="pa-7 pt-1 text-body-1">
          <v-data-table
            density="comfortable"
            v-model:page="currentPage"
            :headers="headers"
            :items="caseList"
            :items-per-page="itemsPerPage"
            hover
          >
            <template v-slot:item="{ item }">
              <tr @click="loadCaseDetails(item)">
                <td>{{ caseList.indexOf(item) +1}}</td>
                <td>{{ item.name }}</td>
                <td>{{ item.clientName }}</td>
                <td>{{ dayjs(item.createdTime).format("DD MMM YYYY, hh:mm A") }}</td>
                <td>{{ item.updatedTime ? dayjs(item.updatedTime).format("DD MMM YYYY, hh:mm A") : '-'}}</td>
                <td>{{ item.closedTime ? dayjs(item.closedTime).format("DD MMM YYYY, hh:mm A") : '-'}}</td>
                <td>
                  <el-tag type="success" v-if="item.status === 'Active'">Active</el-tag>
                  <el-tag  v-else-if="item.status == 'Under Review'">Under Review</el-tag>
                  <el-tag  v-else-if="item.status === 'Negotiation'">Negotiation</el-tag>
                  <el-tag  v-else-if="item.status === 'Court Proceedings'">Court Proceedings</el-tag>
                  <el-tag type="danger" v-else-if="item.status === 'On Hold'">On Hold</el-tag>
                  <el-tag type="success" v-else>Settled</el-tag>
                </td>
                <td class="list-inline hstack">
                  <li>
                    <v-tooltip text="Case Detail" activator="parent" location="top" offset="2"/>
                    <v-icon @click.stop="openDialog(item)">mdi-open-in-new</v-icon>
                  </li>
                </td>
              </tr>
            </template>
            <template v-slot:bottom>
              <div class="d-flex justify-content-end pt-2">
                <el-pagination
                  layout="total, prev, pager, next"
                  v-model:current-page="currentPage"
                  :page-size="caseList.length/pageCount()"
                  :total="caseList.length"
                />
              </div>
            </template>
          </v-data-table>
        </div>
      </UiParentCard>
    </v-col>
  </v-row>

  <!-- Add New Case -->
  <v-dialog v-model="addCaseModal" max-width="500">
    <v-card elevation="10" class="withbg rounded-3 overflow-visible">
      <v-card-title class="px-4 py-4 d-sm-flex align-center justify-space-between bg-background rounded-top-3">
        <h5 class="text-h5 mb-0 d-flex align-center">
          Add New Case
        </h5>
        <v-btn density="compact" variant="plain" icon="mdi-close" @click="addCaseModal = false"/>
      </v-card-title>
      <v-divider class="m-0"/>
      <form @submit.prevent="addCase">
        <v-card-item class="px-8 py-4 text-body-1">
          <v-row>
            <v-col>
              <v-label class="text-caption">Case Name</v-label>
              <v-text-field v-model="newCaseDetails.name" outlined dense/>
            </v-col>
          </v-row>
          <v-row>
            <v-col>
              <v-label class="text-caption">Client</v-label>
              <v-select
                :items="clientList"
                item-title="fullName"
                item-value="id"
                placeholder="Select client"
                variant="outlined"
                density="compact"
                v-model="newCaseDetails.clientId"
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
  </v-dialog>
  
  <!--Case Details Dialog-->
  <v-dialog v-model="isDialogOpen" max-width="600" class="mx-auto my-12">
    <v-card class="withbg rounded-3 overflow-visible">
      <v-card-title class="px-10 py-5 d-sm-flex align-center justify-space-between bg-background rounded-top-3">
          <h5 class="text-h5 mb-0 d-flex align-center">
            Case Name : {{ selectedCaseDetails.name }} 
            <v-icon class="mr-2">mdi-information</v-icon>
          </h5>
        <v-btn density="compact" variant="plain" icon="mdi-close" @click="isDialogOpen = false"/>
      </v-card-title>
    <v-card-text>
      <!-- Display case details here -->
        <div v-if="selectedCaseDetails">
          <!-- Display case details as needed -->
            <div class=" d-flex flex-column case-details-container pa-md-4">
              <div class="case-detail-item">
                <strong>Client Name:</strong>
                {{ selectedCaseDetails.clientName }}
              </div>
            </div>
          <!-- Status Bar -->
          <div class="status-container">
            <div class="pa-md-4"><strong>Current Case Status</strong></div>
            <div class="status-bar">
            <div v-for="(status, index) in statusList" :key="index" class="status-item" :class="{ 'current-status': index === selectedCaseDetails.statusId - 1 }">
              <div class="status-part">
                <div class="circle">{{ status.id }}</div>
                <span class="status-name" :class="{ 'bold': index === selectedCaseDetails.statusId - 1 }">{{ status.name }}</span>
              </div>
            </div>
          </div>
          <!--Current Status Description-->
          <div class="status-description">
              <strong>{{ selectedCaseDetails.statusDescription }}</strong>
          </div>
        </div>
        <!--Client Information-->
        <v-card elevation="10" class="withbg bg-primary">
        <v-card-item>
          <v-row>
            <v-col cols="9" class="hstack">
              <v-avatar class="me-2" color="rgb(243, 244, 248)" size="small">
                <UserIcon color="gray" size="18"/>
              </v-avatar>
              <v-card-title class="text-h6">Client Contact</v-card-title>
              <v-divider
                vertical
                class="mx-5 my-0"
                style="border-color: white !important; opacity: 0.5;"
              />
              <div class="d-flex flex-column text-subtitle-1">
                <span class="mb-1"><strong>Client Name:</strong> {{ selectedCaseDetails.clientName }}</span>
                <span><strong>Phone Number: </strong>{{ selectedCaseDetails.clientPhone ?? '-' }}</span>
                <span><strong>Email: </strong>{{ selectedCaseDetails.clientEmail }}</span>
              </div>
            </v-col>
          </v-row>
        </v-card-item>
        </v-card>
        </div>
      </v-card-text>
    </v-card>
  </v-dialog>
</template>

<style>
.status-bar {
    display: flex;
    justify-content: center;
    align-items: center;
    margin: 30px 20px 60px 20px;
}

.status-item {
  display: flex;
  align-items: center;
  position: relative;
}

.circle {
  display: flex;
  align-items: center;
  justify-content: center;
  width: 40px;
  height: 40px;
  border-radius: 50%;
  background-color: #ddd; /* default circle color */
  margin-right: 40px;
  font-size: 12px; /* Adjust font size as needed */
}

.circle::after {
    content: "";
    position: absolute;
    width: 40%;
    height: 2px;
    background-color: #ddd;
    transform: translateY(-50%);
    transform: translateX(40px);
}

.status-item:last-child .circle::after {
  display: none;
}

.line {
  flex: 1;
  height: 2px;
  background-color: #ddd; /* default line color */
}

.current-status .circle {
  background-color: #2b4c65; /* blue background for active circle */
  color: white;
}

.status-description {
  text-align: center;
  padding-bottom: 20px;
}

.bold {
  font-weight: bold;
}

.status-name {
  position: absolute;
  font-size: 16px;
  color: #333;
  margin-left: -10px;
}

.status-item:nth-child(odd) .status-name {
  top: -30px;
  left: 0%;
}

.status-item:nth-child(even) .status-name {
  left: 10%;
}

</style>

<script setup>
import { ref, shallowRef } from 'vue';
import dayjs from 'dayjs';
import { BriefcaseIcon } from "vue-tabler-icons"
import UiParentCard from "@/components/shared/UiParentCard.vue";

const filter = ref({
  clientId: null,
  status:null,
})

// Data
const currentPage = ref(1)
const itemsPerPage = ref(10)
const headers = ref([
  { key: "number", title: "No." },
  { key: "name", title: "Case Name" },
  { key: "clientName", title: "Client Name" },
  { key: "createdTime" , title: "Created Time" },
  { key: "updatedTime" , title: "Updated Time" },
  { key: "closedTime", title: "Closed Time"},
  { key: "status", title: "Case Status"},
  { key: "actions", sortable: false },
])

// Define reactive variables for dialog
const { data: caseList } = await fetchData.$get("/Case/PartnerPerspectiveCaseList", filter.value)
const { data: clientList } = await fetchData.$get("/Client")

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
    },
  ],
})

// Methods
const pageCount = () => {
  return Math.ceil(caseList.value.length / itemsPerPage.value)
}

//Open Dialogs
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
const addCase = async () => {
  try {
    const result = await fetchData.$post(`/Case/CaseCreate`, newCaseDetails);
    if (!result.error) {
      addCaseModal.value = false;
      newCaseDetails.name = ''; // Clear the case name after successful submission
      ElNotification.success({ message: result.message });
      refreshNuxtData(); // Refresh the case list or perform any necessary actions
    } else {
      ElNotification.error({ message: result.message });
    }
  } catch {
    ElNotification.error({ message: "There is a problem with the server. Please try again later." });
  }
};

// Assuming you have a statusList and currentStatusIndex in your component data
const statusList = ref([
  { id: 1, name: 'Active' },
  { id: 2, name: 'Under Review' },
  { id: 3, name: 'Negotiation' },
  { id: 4, name: 'Court Proceedings' },
  { id: 5, name: 'In Hold' },
  { id: 6, name: 'Settled' },
  // ... other statuses ...
]);
</script>