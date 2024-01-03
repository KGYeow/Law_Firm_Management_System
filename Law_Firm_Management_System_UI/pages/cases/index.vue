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
          <v-dialog v-model="isDialogOpen" max-width="600">
            <v-card class="withbg rounded-3 overflow-visible">
              <v-card-title class="px-4 py-4 d-sm-flex align-center justify-space-between bg-background rounded-top-3">
                <h5 class="text-h5 mb-0 d-flex align-center">
                  Case Name : {{ selectedCaseDetails.name }} 
                </h5>
                <v-btn density="compact" variant="plain" icon="mdi-close" @click="isDialogOpen = false"/>
              </v-card-title>
              <v-card-text>
                <!-- Display case details here -->
                <div v-if="selectedCaseDetails">
                  <!-- Display case details as needed -->
                  <div class="d-flex flex-column text-subtitle-1">
                    <span class="mb-1"><strong>Client Name: </strong>{{ selectedCaseDetails.clientName }}</span>
                  </div>
                  <!-- ... other case details ... -->
                </div>
              </v-card-text>
            </v-card>
          </v-dialog>
          <!--Case Details Dialog-->
            <!--<v-dialog v-model="dialogVisible" max-width="600">
              <v-card>
                <v-card-title>{{ selectedCase ? selectedCase.name : '' }}</v-card-title>
                <v-card-text>
                  <div class="d-flex flex-column text-subtitle-1">
                    <span class="mb-1"><strong>Client Name: </strong>{{ selectedCase ? selectedCase.clientName : '' }}</span>
                  </div>-->
                  <!-- Add more details as needed -->
                <!-- </v-card-text>
                <v-card-actions>
                  <v-btn @click="dialogVisible = false">Close</v-btn>
                </v-card-actions>
              </v-card>
            </v-dialog>-->
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
</template>

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

/*const loadCaseDetails = async (caseItem) => {
  dialogVisible.value = true;

  
  try {
    const { data } = await fetchData.$get(`/Case/DisplayCaseDetails/${caseItem}`);
    selectedCase.value = data;
    dialogVisible.value = true;
  } catch (error) {
    console.error('Error fetching case details:', error);
  }
}*/

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

</script>