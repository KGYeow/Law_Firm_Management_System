<template>
  <v-row>
    <v-col cols="12" md="12">
      <UiParentCard title="User Account List"> 
        <div class="pa-7 pt-1 text-body-1">
          <v-data-table
            density="comfortable"
            v-model:page="currentPage"
            :headers="headers"
            :items="userList"
            :items-per-page="itemsPerPage"
            hover
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
                  :page-size="userList.length/pageCount()"
                  :total="userList.length"
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
import { SettingsIcon } from "vue-tabler-icons"
import UiParentCard from "@/components/shared/UiParentCard.vue";

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
const { data: userList } = await fetchData.$get("/User")

// Head
useHead({
  title: "User Settings | CaseCraft",
})

// Page Meta
definePageMeta({
  breadcrumbsIcon: shallowRef(SettingsIcon),
  breadcrumbs: [
    {
      title: 'Configuration',
      disabled: false,
    },
    {
      title: 'User Settings',
      disabled: false,
    },
  ],
})

// Methods
const pageCount = () => {
  return Math.ceil(userList.value.length / itemsPerPage.value)
}
</script>