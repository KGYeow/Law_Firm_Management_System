<template>
    <v-row>
      <v-col cols="12">
        <h5 class="text-h5 mb-6 pl-7 pt-7 ps-0 d-flex align-center">
          Cases Related
        </h5>
          <v-row>
            <!-- Filters -->
            <v-col class="pe-0" cols="2">
              <v-select
                :items="partnerList"
                item-title="fullName"
                item-value="userId"
                placeholder="Partner"
                density="compact"
                bg-color="white"
                variant="outlined"
                v-model="filter.partnerUserId"
                hide-details
                :single-line="true"
              >
                <template v-slot:prepend-item>
                  <v-list-item title="All Partners" @click="filter.partnerUserId = null"/>
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
          </v-row>
          <v-divider/>

          <!--Case List-->
          <v-data-table
            class="caseList bg-transparent"
            v-model:page="currentPage"
            :items="caseList"
            :items-per-page="itemsPerPage"
          >
            <template #headers/>
            <template #body v-if="caseList.length == 0">
              <div class="p-3 w-100 fs-6 text-center">
                No Appointments
              </div>
            </template>
            <template v-slot:item="{ item }">
              <v-col class="pb-8" cols="3">
                <v-card elevation="10" class="withbg">
                  <el-tag
                    :type="item.status == 'Active' ? 'success' : item.status == 'Under Review' ? '' :item.status == 'Negotiation' ? '' : item.status == 'Court Proceeding' ? '' : item.status == 'On Hold' ? 'danger' : item.status == 'Settled' ? 'success' : 'info'"
                    class="position-absolute"
                    effect="dark"
                    style="bottom: -10px; right: -15px; border-radius: 10px;"
                  >
                    <i class="mdi mdi-check" v-if="item.status == 'Active'"></i>
                    <i class="mdi mdi-file-find" v-else-if="item.status == 'Under Review'"></i>
                    <i class="mdi mdi-timer-sand-complete" v-else-if="item.status == 'Negotiation'"></i>
                    <i class="mdi mdi-gavel" v-else-if="item.status == 'Court Proceeding'"></i>
                    <i class="mdi mdi-timer-sand-paused" v-else-if="item.status == 'On Hold'"></i>
                    <i class="mdi mdi-check-circle-outline" v-else-if="item.status == 'Settled'"></i>
                    {{ item.status }}
                  </el-tag>
                  <v-card-actions
                    class="p-0 text-subtitle-2 fw-bold justify-content-center text-white"
                    style="background-color: rgb(43, 76, 101); min-height: 35px; border-top-right-radius: 10px; border-top-left-radius: 10px;"
                  >
                    {{ item.name }}
                  </v-card-actions>
                  <v-card-item class="pt-3">
                    <v-card-title class="text-subtitle-2 fw-bold">
                      Case Created Time
                    </v-card-title>
                    <v-card-subtitle class="text-subtitle-2">
                      <i class="mdi mdi-calendar-blank-outline me-1"></i>
                      {{ dayjs(item.createdTime).format("DD MMM YYYY, hh:mm A") }}
                    </v-card-subtitle>
                    <!--Upload Button-->
                    <ul class="m-0 list-inline hstack">
                      <li>
                        <v-tooltip text="Upload Document" activator="parent" location="top" offset="2"/>
                      <v-btn
                        icon
                        color="primary"
                        @click="createDocument"
                      >
                        <v-icon>mdi-upload</v-icon>
                      </v-btn>
                      </li>
                    </ul>
                    <input
                      ref="fileInput"
                      type="file"
                      accept=".pdf, .doc, .docx"
                      style="display:none"
                      @change="handleFileUpload"
                    />
                    <v-divider class="my-3"/>
                    <el-scrollbar class="text-body-1" height="60px">
                      <div class="d-flex pt-sm-2 align-center">
                        <v-avatar
                          class="mb-0"
                          image="/images/users/avatar.jpg"
                          size="35"
                        />
                        <strong class="ms-5">{{ item.partnerName }}</strong>
                      </div>
                    </el-scrollbar>
                  </v-card-item>
                </v-card>
              </v-col>
            </template>
            <template v-slot:bottom>
              <div class="d-flex justify-content-end pt-2">
                <el-pagination
                  layout="total, prev, pager, next"
                  v-model:current-page="currentPage"
                  :page-size="caseList.length/pageCount()"
                  :total="caseList.length"
                  background
                />
              </div>
            </template>
        </v-data-table>
      </v-col>
    </v-row>
  </template>
  
  <script setup>
  import { ref, shallowRef } from 'vue';
  import dayjs from 'dayjs';
  import { BriefcaseIcon } from "vue-tabler-icons"
  
  const filter = ref({
    clientId: null,
    partnerId: null,
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
  const { data: caseList } = await fetchData.$get("/Case/ClientPerspectiveCaseList", filter.value)
  const { data: partnerList } = await fetchData.$get("/Partner")
  
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
  </script>