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
  const { data: caseList } = await fetchData.$get("/Case/ParalegalPerspectiveCaseList", filter.value)
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
  </script>