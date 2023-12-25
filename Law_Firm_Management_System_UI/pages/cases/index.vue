<template>
  <v-row>
    <v-col cols="12" md="12">
      <UiParentCard title="Case Management"> 
        <div class="pa-7 pt-1 text-body-1">
          <v-data-table
            v-model:page="currentPage"
            :headers="headers"
            :items="clientList"
            :items-per-page="itemsPerPage"
          >
            <template v-slot:item.number="{ index }">
              <span>{{ index + 1 }}</span>
            </template>
            <template v-slot:item.updatedTime="{ item }">
              {{ dayjs(item.updatedTime).format("DD MMM YYYY, hh:mm A") }}
            </template>
            <template v-slot:item.createdTime="{ item }">
              {{ dayjs(item.createdTime).format("DD MMM YYYY, hh:mm A") }}
            </template>
            <template v-slot:item.closedTime="{ item }">
              {{ dayjs(item.closedTime).format("DD MMM YYYY, hh:mm A") }}
            </template>
            <template v-slot:bottom>
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
import dayjs from 'dayjs';
import UiParentCard from "@/components/shared/UiParentCard.vue";

// Data
const { data: user } = useAuth()
const currentPage = ref(1)
const itemsPerPage = ref(10)
const headers = ref([
  { key: "number", title: "No." },
  { key: "name", title: "Case Name" },
  { key: "updatedBy", title: "UpdatedBy" },
  { key: "createdTime" , title: "Created Time" },
  { key: "updatedTime" , title: "Updated Time" },
  { key: "closedTime", title: "Closed Time"},
])
const { data: clientList } = await fetchData.$get("/Case")
//const { data: caseList } = await fetchData.$get("/Case")

// Head
useHead({
  title: "Cases | CaseCraft",
})

// Methods
const pageCount = () => {
  return Math.ceil(clientList.value.length / itemsPerPage.value)
}
</script>