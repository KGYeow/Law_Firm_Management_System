<template>
  <v-row v-if="user.role === 'Staff/Paralegal'">
    <v-col cols="12" md="12">
      <v-card elevation="10" class="withbg bg-primary">
        <v-card-item>
          <div class="d-flex align-center justify-space-between">
            <v-card-title class="text-h6 d-flex align-center">
              <v-avatar class="me-2" color="rgb(243, 244, 248)" size="small">
                <UserIcon color="gray" size="18"/>
              </v-avatar>
              Your Assigned Partner
              <v-divider
                vertical
                class="mx-5 my-0"
                style="border-color: white !important; opacity: 0.5;"
              />
              <div class="text-h4">
                <span class="text-subtitle-1">
                  <strong>Full Name: </strong>{{ assignedPartner.fullName }}
                </span>
                <br/>
                <span class="text-subtitle-1">
                  <strong>Email Address: </strong>{{ assignedPartner.email }}
                </span>
                <br/>
                <span class="text-subtitle-1">
                  <strong>Phone Number: </strong>
                  <span v-if="assignedPartner.phoneNumber">{{ assignedPartner.phoneNumber }}</span>
                  <span v-else>-</span>
                </span>
              </div>
            </v-card-title>
          </div>
        </v-card-item>
      </v-card>
    </v-col>
  </v-row>
  <v-row>
    <v-col cols="12" md="12">
      <UiParentCard title="Partners"> 
        <div class="pa-7 pt-1 text-body-1">
          <v-data-table
            v-model:page="currentPage"
            :headers="headers"
            :items="partnerList"
            :items-per-page="itemsPerPage"
          >
            <template v-slot:item.number="{ index }">
              <span>{{ index + 1 }}</span>
            </template>
            <template v-slot:item.phoneNumber="{ item }">
              <span v-if="item.phoneNumber">{{ item.phoneNumber }}</span>
              <span v-else>-</span>
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
import UiParentCard from "@/components/shared/UiParentCard.vue";
import { VDataTable } from "vuetify/lib/labs/components.mjs";

// Data
const { data: user } = useAuth()
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
const { data: assignedPartner } = await fetchData.$get(`/Partner/AssignedPartner/${user.value.id}`)

// Head
useHead({
  title: "Partners | CaseCraft",
})

// Methods
const pageCount = () => {
  return Math.ceil(partnerList.value.length / itemsPerPage.value)
}
</script>