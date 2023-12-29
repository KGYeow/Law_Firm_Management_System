<template>
  <v-row>
    <v-col cols="12" md="12">
      <v-card elevation="10" class="withbg bg-primary">
        <v-card-item>
          <div class="d-flex align-center justify-space-between">
            <v-card-title class="text-h6 d-flex align-center">
              <v-avatar class="me-2" color="rgb(243, 244, 248)" size="small">
                <UserIcon color="gray" size="18"/>
              </v-avatar>
              Your Assigned Paralegal
              <v-divider
                vertical
                class="mx-5 my-0"
                style="border-color: white !important; opacity: 0.5;"
              />
              <div class="text-h4">
                <span class="text-subtitle-1">
                  <strong>Full Name: </strong>{{ assignedParalegal.fullName }}
                </span>
                <br/>
                <span class="text-subtitle-1">
                  <strong>Email Address: </strong>{{ assignedParalegal.email }}
                </span>
                <br/>
                <span class="text-subtitle-1">
                  <strong>Phone Number: </strong>
                  <span v-if="assignedParalegal.phoneNumber">{{ assignedParalegal.phoneNumber }}</span>
                  <span v-else>-</span>
                </span>
              </div>
            </v-card-title>
            <div>
              <v-btn variant="outlined" class="me-2">Change</v-btn>
              <v-btn variant="outlined">Delete</v-btn>
            </div>
          </div>
        </v-card-item>
      </v-card>
    </v-col>
  </v-row>
  <v-row>
    <v-col cols="12" md="12">
      <UiParentCard title="Paralegals">
        <div class="pa-7 pt-1 text-body-1">
          <v-data-table
            v-model:page="currentPage"
            :headers="headers"
            :items="paralegalList"
            :items-per-page="itemsPerPage"
          >
            <template v-slot:item.number="{ index }">
              <span>{{ index + 1 }}</span>
            </template>
            <template v-slot:item.assignedPartner="{ item }">
              <span v-if="item.assignedPartner">{{ item.assignedPartner }}</span>
              <span v-else>-</span>
            </template>
            <template v-slot:item.phoneNumber="{ item }">
              <span v-if="item.phoneNumber">{{ item.phoneNumber }}</span>
              <span v-else>-</span>
            </template>
            <template v-slot:item.isActive="{ item }">
              <el-tag type="success" v-if="item.isActive">Active</el-tag>
              <el-tag type="danger" v-else>Inactive</el-tag>
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
const { data: user } = useAuth()
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
const { data: assignedParalegal } = await fetchData.$get(`/Paralegal/AssignedParalegal/${user.value.id}`)

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