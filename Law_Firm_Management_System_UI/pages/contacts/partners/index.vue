<template>
  <v-row v-if="userRole === 'Paralegal'">
    <v-col cols="12" md="12">
      <v-card elevation="10" class="withbg bg-primary">
        <v-card-item>
          <v-row>
            <v-col class="hstack">
              <v-avatar class="me-2" color="rgb(243, 244, 248)" size="small">
                <UserIcon color="gray" size="18"/>
              </v-avatar>
              <v-card-title class="text-h6">Your Assigned Partner</v-card-title>
              <v-divider
                vertical
                class="mx-5 my-0"
                style="border-color: white !important; opacity: 0.5;"
              />
              <div class="d-flex flex-column text-subtitle-1">
                <span class="mb-1"><strong>Full Name: </strong>{{ assignedPartner.fullName }}</span>
                <span class="mb-1"><strong>Email Address: </strong>{{ assignedPartner.email }}</span>
                <span><strong>Phone Number: </strong>{{ assignedPartner.phoneNumber ?? '-' }}</span>
              </div>
            </v-col>
          </v-row>
        </v-card-item>
      </v-card>
    </v-col>
  </v-row>
  <v-row>
    <v-col cols="12" md="12">
      <UiParentCard title="Partners"> 
        <div class="pa-7 pt-1 text-body-1">
          <v-data-table
            density="comfortable"
            v-model:page="currentPage"
            :headers="headers"
            :items="partnerList"
            :items-per-page="itemsPerPage"
          >
            <template v-slot:item="{ item }">
              <tr>
                <td>{{ partnerList.indexOf(item) + 1 }}</td>
                <td>{{ item.fullName }}</td>
                <td>{{ item.assignedParalegal ?? '-' }}</td>
                <td>{{ item.email }}</td>
                <td>{{ item.phoneNumber ?? '-' }}</td>
                <td>{{ item.address ?? '-' }}</td>
              </tr>
            </template>
            <template v-slot:bottom>
              <div class="d-flex justify-content-end pt-2">
                <el-pagination
                  layout="total, prev, pager, next"
                  v-model:current-page="currentPage"
                  :page-size="partnerList.length/pageCount()"
                  :total="partnerList.length"
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
  { key: "assignedParalegal", title: "Assigned Paralegal" },
  { key: "email" , title: "Email" },
  { key: "phoneNumber" , title: "Phone No." },
  { key: "address" , title: "Address" },
])
const { data: partnerList } = await fetchData.$get("/Partner")
const { data: userRole } = await fetchData.$get("/UserRole/RoleName")
const { data: assignedPartner } = await fetchData.$get("/Partner/AssignedPartner")

// Head
useHead({
  title: "Partners | CaseCraft",
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
      title: 'Partners',
      disabled: false,
    },
  ],
})

// Methods
const pageCount = () => {
  return Math.ceil(partnerList.value.length / itemsPerPage.value)
}
</script>