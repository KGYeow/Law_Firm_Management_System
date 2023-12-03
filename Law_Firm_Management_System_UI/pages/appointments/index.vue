<template>
  <v-row>
    <v-col cols="12" md="12">
      <UiParentCard title="Appointment"> 
        <div class="pa-7 pt-1 text-body-1">
          <v-data-table
            v-model:page="currentPage"
            :headers="headers"
            :items="appointmentList"
            :items-per-page="itemsPerPage"
          >
            <template v-slot:item.appointmentTime="{ item }">
              {{ dayjs(item.appointmentTime).format("DD MMM YYYY, hh:mm A") }}
            </template>
            <template v-slot:item.actions>
              <v-btn icon="mdi-check" size="small" color="#68058d" variant="text" class="me-1"/>
              <v-btn icon="mdi-close" size="small" color="#68058d" variant="text"/>
            </template>
            <template v-slot:bottom>
              <div class="d-flex justify-content-end pt-2">
                <el-pagination
                  layout="total, prev, pager, next"
                  v-model:current-page="currentPage"
                  :page-size="appointmentList.length/pageCount()"
                  :total="appointmentList.length"
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
import { VDataTable } from "vuetify/lib/labs/components.mjs";

// Data
const { data: user } = useAuth()
const currentPage = ref(1)
const itemsPerPage = ref(10)
const headers = ref([
  { key: "id", title: "No." },
  { key: "fullName", title: "Full Name" },
  { key: "category", title: "Category" },
  { key: "appointmentTime" , title: "Appointment Time" },
  { key: "status" , title: "Status" },
  { key: "actions", title: "Actions", sortable: false }
])
const { data: appointmentList } = await fetchData.$get(`/Appointment/ClientAppointments/${user.value.id}`)

// Head
useHead({
  title: "Appointments | CaseCraft",
})

// Methods
const pageCount = () => {
  return Math.ceil(appointmentList.value.length / itemsPerPage.value)
}
</script>