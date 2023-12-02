<template>
  <v-row>
    <v-col cols="12" md="12">
      <UiParentCard title="User Account List"> 
        <div class="pa-7 pt-1 text-body-1">
          <v-data-table
            v-model:page="currentPage"
            :headers="headers"
            :items="userList.data.value"
            :items-per-page="itemsPerPage"
          >
            <!-- <template v-slot:item.phoneNumber="{ item }">
              <span v-if="item.phoneNumber">{{ item.phoneNumber }}</span>
              <span v-else>-</span>
            </template> -->
            <template v-slot:bottom>
              <div class="d-flex justify-content-end pt-2">
                <el-pagination
                  layout="total, prev, pager, next"
                  v-model:current-page="currentPage"
                  :page-size="userList.data.value.length/pageCount()"
                  :total="userList.data.value.length"
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
  { key: "id", title: "User ID" },
  { key: "username", title: "Username" },
  { key: "fullName", title: "Full Name" },
  { key: "email" , title: "Email" },
  { key: "role", title: "User Type" },
])
const userList = await fetchData.$get("/User")

// Head
useHead({
  title: "User Settings | CaseCraft",
})

// Methods
const pageCount = () => {
  return Math.ceil(userList.data.value.length / itemsPerPage.value)
}
</script>