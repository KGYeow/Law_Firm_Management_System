<template>
  <v-row>
    <v-col>
      <UiParentCard title="Case Management"> 
        <div class="pa-7 pt-1 text-body-1">
          <v-data-table
            density="comfortable"
            v-model:page="currentPage"
            :headers="headers"
            :items="caseList"
            :items-per-page="itemsPerPage"
          >
            <template v-slot:item="{ item }">
              <tr>
                <td>{{ caseList.indexOf(item) + 1 }}</td>
                <td>{{ item.name }}</td>
                <td>{{ item.clientName }}</td>
                <td>{{ dayjs(item.createdTime).format("DD MMM YYYY, hh:mm A") }}</td>
                <td>{{ dayjs(item.updatedTime).format("DD MMM YYYY, hh:mm A") }}</td>
                <td>{{ dayjs(item.closedTime).format("DD MMM YYYY, hh:mm A") }}</td>
                <td>{{ item.status }}</td>
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
import dayjs from 'dayjs';
import { BriefcaseIcon } from "vue-tabler-icons"
import UiParentCard from "@/components/shared/UiParentCard.vue";

// Data
const { data: user } = useAuth()
const currentPage = ref(1)
const itemsPerPage = ref(10)
const headers = ref([
  { key: "number", title: "No." },
  { key: "clientName", title: "Client Name" },
  { key: "name", title: "Case Name" },
  { key: "createdTime" , title: "Created Time" },
  { key: "updatedTime" , title: "Updated Time" },
  { key: "closedTime", title: "Closed Time"},
  { key: "status", title: "Case Status"},
])
const { data: caseList } = await fetchData.$get("/Case/PartnerPerspectiveCaseList")
//const { data: caseList } = await fetchData.$get("/Case")

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