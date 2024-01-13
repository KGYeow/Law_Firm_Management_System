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
            <template #item="{ item }">
              <tr>
                <td>{{ item.id }}</td>
                <td>
                  <a :href="`/configuration/user-settings/${item.id}`" class="row-link">{{ item.username }}</a>
                </td>
                <td>{{ item.fullName }}</td>
                <td>{{ item.email }}</td>
                <td>{{ item.role }}</td>
                <td>
                  <ul class="m-0 list-inline hstack">
                    <li>
                      <v-tooltip text="View Details" activator="parent" location="top" offset="2"/>
                      <v-btn icon="mdi-open-in-new" size="small" variant="text" :href="`/configuration/user-settings/${item.id}`"/>
                    </li>
                  </ul>
                </td>
              </tr>
            </template>
            <template #bottom>
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
import UiParentCard from "@/components/shared/UiParentCard.vue"

// Data
const currentPage = ref(1)
const itemsPerPage = ref(10)
const headers = ref([
  { key: "id", title: "User ID" },
  { key: "username", title: "Username" },
  { key: "fullName", title: "Full Name" },
  { key: "email" , title: "Email" },
  { key: "role", title: "User Role" },
  { key: "actions", sortable: false, width: 0 },
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