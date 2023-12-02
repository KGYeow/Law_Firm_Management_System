<template>
  <v-row>
    <v-col cols="12" md="12">
      <UiParentCard title="Paralegals"> 
        <div class="pa-7 pt-1 text-body-1">
          <v-data-table
            v-model:page="currentPage"
            :headers="headers"
            :items="paralegalList.data.value"
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
                  :page-size="paralegalList.data.value.length/pageCount()"
                  :total="paralegalList.data.value.length"
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
const paralegalList = await fetchData.$get("/Paralegal")

// Head
useHead({
  title: "Paralegals | CaseCraft",
})

// Methods
const pageCount = () => {
  return Math.ceil(paralegalList.data.value.length / itemsPerPage.value)
}
</script>