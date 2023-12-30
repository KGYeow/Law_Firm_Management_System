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
            <template v-slot:item.number="{ index }">
              <span>{{ index + 1 }}</span>
            </template>
            <template v-slot:item.appointmentTime="{ item }">
              {{ dayjs(item.appointmentTime).format("DD MMM YYYY, hh:mm A") }}
            </template>
            <template v-slot:item.actions="{ item }">
              <el-popconfirm
                title="Are you sure to approve this appointment?"
                icon-color="green"
                width="190"
                @confirm="appointmentApproval(item.id, 'Approved')"
              >
                <template #reference>
                  <v-btn icon="mdi-check" size="small" variant="text" :disabled="item.status != 'Pending'"/>
                </template>
              </el-popconfirm>
              <el-popconfirm
                title="Are you sure to reject this appointment?"
                icon-color="red"
                width="190"
                @confirm="appointmentApproval(item.id, 'Rejected')"
              >
                <template #reference>
                  <v-btn icon="mdi-close" size="small" variant="text" :disabled="item.status != 'Pending'"/>
                </template>
              </el-popconfirm>
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
import dayjs from 'dayjs'
import { Book2Icon } from "vue-tabler-icons"
import UiParentCard from "@/components/shared/UiParentCard.vue"

// Data
const { data: appointmentList } = await fetchData.$get("/Appointment/PartnerPerspectiveList")
const currentPage = ref(1)
const itemsPerPage = ref(10)
const headers = ref([
  { key: "number", title: "No." },
  { key: "fullName", title: "Full Name" },
  { key: "category", title: "Category" },
  { key: "appointmentTime" , title: "Appointment Time" },
  { key: "status" , title: "Status" },
  { key: "actions", title: "Actions", sortable: false }
])

// Head
useHead({
  title: "Appointments | CaseCraft",
})

// Page Meta
definePageMeta({
  breadcrumbsIcon: shallowRef(Book2Icon),
  breadcrumbs: [
    {
      title: 'Appointments',
      disabled: false,
    },
  ],
})

// Methods
const pageCount = () => {
  return Math.ceil(appointmentList.value.length / itemsPerPage.value)
}
const appointmentApproval = async(appointmentId, approvalStatus) => {
  try {
    const result = await fetchData.$put("/Appointment/PartnerApproval", {
      appointmentId: appointmentId,
      status: approvalStatus,
    })

    if (!result.error) {
      ElNotification.success({ message: result.message })
      refreshNuxtData()
    }
    else {
      ElNotification.error({ message: result.message })
    }
  } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
}
</script>