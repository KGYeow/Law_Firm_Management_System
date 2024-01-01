<template>
  <v-row>
    <v-col cols="12" md="12">
      <v-card elevation="10" class="withbg bg-primary">
        <v-card-item>
          <v-row v-if="assignedParalegal.userId != null">
            <v-col cols="9" class="hstack">
              <v-avatar class="me-2" color="rgb(243, 244, 248)" size="small">
                <UserIcon color="gray" size="18"/>
              </v-avatar>
              <v-card-title class="text-h6">Your Assigned Paralegal</v-card-title>
              <v-divider
                vertical
                class="mx-5 my-0"
                style="border-color: white !important; opacity: 0.5;"
              />
              <div class="d-flex flex-column text-subtitle-1">
                <span class="mb-1"><strong>Full Name: </strong>{{ assignedParalegal.fullName }}</span>
                <span class="mb-1"><strong>Email Address: </strong>{{ assignedParalegal.email }}</span>
                <span><strong>Phone Number: </strong>{{ assignedParalegal.phoneNumber ?? '-' }}</span>
              </div>
            </v-col>
            <v-col cols="3" class="hstack justify-content-end">
              <v-btn variant="outlined" class="me-2">Change</v-btn>
              <v-btn variant="outlined">Delete</v-btn>
            </v-col>
          </v-row>
          <v-row v-else>
            <v-col class="hstack justify-content-between">
              <v-card-title class="text-h6">No Assigned Paralegal</v-card-title>
              <v-btn variant="outlined">Assign Paralegal</v-btn>
            </v-col>
          </v-row>
        </v-card-item>
      </v-card>
    </v-col>
  </v-row>
  <v-row>
    <v-col cols="12" md="12">
      <UiParentCard title="Paralegals">
        <div class="pa-7 pt-1 text-body-1">
          <v-data-table
            density="comfortable"
            v-model:page="currentPage"
            :headers="headers"
            :items="paralegalList"
            :items-per-page="itemsPerPage"
          >
            <template v-slot:item="{ item }">
              <tr>
                <td>{{ paralegalList.indexOf(item) + 1 }}</td>
                <td>{{ item.fullName }}</td>
                <td>{{ item.assignedPartner ?? '-' }}</td>
                <td>{{ item.email }}</td>
                <td>{{ item.phoneNumber ?? '-' }}</td>
                <td>{{ item.address ?? '-' }}</td>
                <td>
                  <el-tag type="success" v-if="item.isActive">Active</el-tag>
                  <el-tag type="danger" v-else>Inactive</el-tag>
                </td>
              </tr>
            </template>
            <template v-slot:bottom>
              <div class="d-flex justify-content-end pt-2">
                <el-pagination
                  layout="total, prev, pager, next"
                  v-model:current-page="currentPage"
                  :page-size="paralegalList.length/pageCount()"
                  :total="paralegalList.length"
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
import { AddressBookIcon } from "vue-tabler-icons"
import UiParentCard from "@/components/shared/UiParentCard.vue";

// Data
const currentPage = ref(1)
const itemsPerPage = ref(10)
const headers = ref([
  { key: "number", title: "No." },
  { key: "fullName", title: "Full Name" },
  { key: "assignedPartner", title: "Assigned Partner" },
  { key: "email" , title: "Email" },
  { key: "phoneNumber" , title: "Phone No." },
  { key: "address" , title: "Address" },
  { key: "isActive" , title: "Status" },
])
const { data: paralegalList } = await fetchData.$get("/Paralegal")
const { data: assignedParalegal } = await fetchData.$get("/Paralegal/AssignedParalegal")

// Head
useHead({
  title: "Paralegals | CaseCraft",
})

// Page Meta
definePageMeta({
  breadcrumbsIcon: shallowRef(AddressBookIcon),
  breadcrumbs: [
    {
      title: 'Contacts',
      disabled: false,
    },
    {
      title: 'Paralegals',
      disabled: false,
    },
  ],
})

// Methods
const pageCount = () => {
  return Math.ceil(paralegalList.value.length / itemsPerPage.value)
}
</script>