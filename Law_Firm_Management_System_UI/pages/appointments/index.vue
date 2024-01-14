<template>
  <v-row>
    <v-col cols="12" md="12">
      <UiParentCard title="Appointments">
        <v-row class="px-7">
          <!-- Filters -->
          <v-col class="pe-0" cols="2">
            <v-select
              :items="clientList"
              item-title="fullName"
              item-value="id"
              placeholder="Client"
              density="compact"
              bg-color="white"
              variant="outlined"
              v-model="filter.clientId"
              hide-details
              :single-line="true"
            >
              <template v-slot:prepend-item>
                <v-list-item title="All Clients" @click="filter.clientId = null"/>
              </template>
            </v-select>
          </v-col>
          <v-col class="pe-0" cols="2">
            <v-select
              :items="categoryList"
              item-title="name"
              placeholder="Category"
              density="compact"
              bg-color="white"
              variant="outlined"
              v-model="filter.category"
              hide-details
            >     
              <template v-slot:prepend-item>
                <v-list-item title="All Categories" @click="filter.category = null"/>
              </template>
            </v-select>
          </v-col>
          <v-col cols="2">
            <v-select
              :items="['Approved', 'Pending', 'Rejected', 'Cancelled']"
              placeholder="Status"
              density="compact"
              bg-color="white"
              variant="outlined"
              v-model="filter.status"
              hide-details
            >
              <template v-slot:prepend-item>
                <v-list-item title="All Statuses" @click="filter.status = null"/>
              </template>
            </v-select>
          </v-col>
          <v-col>
            <!-- Add New Appointment -->
            <v-btn class="float-end" color="primary" prepend-icon="mdi-plus" flat @click="addAppointmentModal = true">New Appointment</v-btn>
          </v-col>
        </v-row>

        <!-- Appointment List Table -->
        <div class="pa-7 pt-3 text-body-1">
          <v-data-table
            density="comfortable"
            v-model:page="currentPage"
            :headers="headers"
            :items="appointmentList"
            :items-per-page="itemsPerPage"
            hover
          >
            <template #item="{ item }">
              <tr>
                <td>{{ appointmentList.indexOf(item) + 1 }}</td>
                <td>{{ item.fullName }}</td>
                <td>{{ item.category }}</td>
                <td>{{ dayjs(item.appointmentTime).format("DD MMM YYYY, hh:mm A") }}</td>
                <td>{{ item.status }}</td>
                <td>
                  <ul class="m-0 list-inline hstack justify-content-end" style="width: 100px;">
                    <li>
                      <v-tooltip text="Approve" activator="parent" location="top" offset="2"/>
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
                    </li>
                    <li>
                      <v-tooltip text="Reject" activator="parent" location="top" offset="2"/>
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
                    </li>
                    <li>
                      <v-tooltip text="Cancel" activator="parent" location="top" offset="2"/>
                      <el-popconfirm
                        title="Are you sure to cancel this appointment?"
                        icon-color="orange"
                        width="190"
                        @confirm="appointmentApproval(item.id, 'Cancelled')"
                      >
                        <template #reference>
                          <v-btn icon="mdi-cancel" size="small" variant="text" :disabled="item.status != 'Approved'"/>
                        </template>
                      </el-popconfirm>
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

  <!-- Add New Appointment Modal -->
  <SharedUiModal v-model="addAppointmentModal" title="Add New Appointment" width="550">
    <form @submit.prevent="addAppointment">
      <v-card-item class="px-8 py-4 text-body-1">
        <v-row>
          <v-col>
            <v-label class="text-caption">Client</v-label>
            <v-select
              :items="clientList"
              item-title="fullName"
              item-value="id"
              placeholder="Select client"
              variant="outlined"
              density="compact"
              :error-messages="addAppointmentDetails.clientId.errorMessage"
              v-model="addAppointmentDetails.clientId.value"
              hide-details="auto"
            />
          </v-col>
        </v-row>
        <v-row>
          <v-col>
            <v-label class="text-caption">Category</v-label>
            <v-select
              :items="categoryList"
              item-title="name"
              item-value="id"
              placeholder="Select category"
              variant="outlined"
              density="compact"
              :error-messages="addAppointmentDetails.categoryId.errorMessage"
              v-model="addAppointmentDetails.categoryId.value"
              hide-details="auto"
            />
          </v-col>
          <v-col>
            <v-label class="text-caption">Time</v-label>
            <el-date-picker
              :class="{ 'error': addAppointmentDetails.appointmentTime.errorMessage }"
              placeholder="Select date and time"
              type="datetime"
              format="DD MMM YYYY, hh:mm A"
              date-format="DD MMM YYYY"
              time-format="HH:mm"
              :teleported="false"
              v-model="addAppointmentDetails.appointmentTime.value"
              style="height: 40px;"
            />
            <v-messages
              :messages="addAppointmentDetails.appointmentTime.errorMessage"
              :active="addAppointmentDetails.appointmentTime.errorMessage ? true : false"
              color="#fa896b"
              :transition="false"
              style="padding: 3px 16px 3px; opacity: unset;"
            />
          </v-col>
        </v-row>
      </v-card-item>
      <v-card-actions class="p-3 justify-content-end">
        <v-btn color="primary" type="submit">Submit</v-btn>
      </v-card-actions>
    </form>
  </SharedUiModal>
</template>

<script setup>
import { useField, useForm } from 'vee-validate'
import { Book2Icon } from "vue-tabler-icons"
import dayjs from 'dayjs'
import UiParentCard from "@/components/shared/UiParentCard.vue"

// Data
const { handleSubmit } = useForm({
  validationSchema: {
    clientId(value) {
      return value ? true : 'Client is required'
    },
    categoryId(value) {
      return value ? true : 'Category is required'
    },
    appointmentTime(value) {
      return value ? true : 'Appointment time is required'
    }
  }
})
const filter = ref({
  clientId: null,
  category: null,
  status: null,
})
const addAppointmentModal = ref(false)
const addAppointmentDetails = ref({
  clientId: useField('clientId'),
  categoryId: useField('categoryId'),
  appointmentTime: useField('appointmentTime'),
})
const currentPage = ref(1)
const itemsPerPage = ref(10)
const headers = ref([
  { key: "number", title: "No.", width: 0 },
  { key: "fullName", title: "Full Name" },
  { key: "category", title: "Category", width: 240 },
  { key: "appointmentTime", title: "Appointment Time", width: 200 },
  { key: "status", title: "Status", width: 120 },
  { key: "actions", sortable: false, width: 120 },
])
const { data: clientList } = await fetchData.$get("/Client")
const { data: categoryList } = await fetchData.$get("/Appointment/Category")
const { data: appointmentList } = await fetchData.$get("/Appointment/PartnerPerspectiveList", filter.value)

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
const addAppointment = handleSubmit(async(values) => {
  try {
    const result = await fetchData.$post(`/Appointment/PartnerCreate`, values)
    if (!result.error) {
      addAppointmentModal.value = false
      addAppointmentDetails.value.clientId.resetField()
      addAppointmentDetails.value.categoryId.resetField()
      addAppointmentDetails.value.appointmentTime.resetField()
      ElNotification.success({ message: result.message })
      refreshNuxtData()
    }
    else {
      ElNotification.error({ message: result.message })
    }
  } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
})
</script>