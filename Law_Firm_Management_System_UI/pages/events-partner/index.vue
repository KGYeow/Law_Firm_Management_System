<template>
  <v-row>
    <v-col cols="12" md="12">
      <UiParentCard title="Event Management"> 
        <div class="pa-7 pt-1 text-body-1">
          <v-data-table
            v-model:page="currentPage"
            :headers="headers"
            :items="eventList"
            :items-per-page="itemsPerPage"
          >
            <template v-slot:item.number="{ index }">
              <span>{{ index + 1 }}</span>
            </template>
            <template v-slot:item.createdTime="{ item }">
              {{ dayjs(item.createdTime).format("DD MMM YYYY, hh:mm A") }}
            </template>
            <template v-slot:item.eventTime="{ item }">
              {{ dayjs(item.eventTime).format("DD MMM YYYY, hh:mm A") }}
            </template>
            <template v-slot:bottom>
              <div class="d-flex justify-content-end pt-2">
                <el-pagination
                  layout="total, prev, pager, next"
                  v-model:current-page="currentPage"
                  :page-size="eventList.length/pageCount()"
                  :total="eventList.length"
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
  { key: "caseID", title: "Case ID" },
  { key: "name", title: "Event Name" },
  { key: "createdTime" , title: "Created Time" },
  { key: "eventTime", title: "Event Time"},
  { key: "isCompleted", title: "Is Complete"},
])
const { data: eventList } = await fetchData.$get("/Event")

// Head
useHead({
  title: "Events | CaseCraft",
})

// Methods
const pageCount = () => {
  return Math.ceil(eventList.value.length / itemsPerPage.value)
}
</script>