<template>
  <v-row>
    <v-col cols="12" md="12">
      <UiParentCard title="Clients">
        <div class="pa-7 pt-1 text-body-1">
          <v-data-table
            density="comfortable"
            v-model:page="currentPage"
            :headers="headers"
            :items="clientList"
            :items-per-page="itemsPerPage"
            hover
          >
            <template #item="{ item }">
              <tr>
                <td>{{ clientList.indexOf(item) + 1 }}</td>
                <td>
                  <a :href="`/contacts/clients/${item.id}`" class="row-link">{{ item.fullName }}</a>
                </td>
                <td>{{ item.email }}</td>
                <td>{{ item.phoneNumber ?? '-' }}</td>
                <td>{{ item.address ?? '-' }}</td>
                <td>
                  <ul class="m-0 list-inline hstack">
                    <li>
                      <v-tooltip text="View Details" activator="parent" location="top" offset="2"/>
                      <v-btn icon="mdi-open-in-new" size="small" variant="text" :href="`/contacts/clients/${item.id}`"/>
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
                  :page-size="clientList.length/pageCount()"
                  :total="clientList.length"
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
import UiParentCard from "@/components/shared/UiParentCard.vue"

// Data
const currentPage = ref(1)
const itemsPerPage = ref(10)
const headers = ref([
  { key: "number", title: "No." },
  { key: "fullName", title: "Full Name" },
  { key: "email" , title: "Email" },
  { key: "phoneNumber" , title: "Phone No." },
  { key: "address" , title: "Address" },
  { key: "actions", sortable: false, width: 0 },
])
const { data: clientList } = await fetchData.$get("/Client")

// Head
useHead({
  title: "Clients | CaseCraft",
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
      title: 'Clients',
      disabled: false,
    },
  ],
})

// Methods
const pageCount = () => {
  return Math.ceil(clientList.value.length / itemsPerPage.value)
}
</script>