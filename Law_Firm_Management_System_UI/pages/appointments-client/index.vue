<template>
  <v-row>
    <v-col cols="12">
      <h5 class="text-h5 mb-6 pl-7 pt-7 ps-0 d-flex align-center">
        Upcoming Appointments
      </h5>
      <v-row>
        <!-- Filters -->
        <v-col class="pe-0">
          <v-select
            :items="partnerList"
            item-title="fullName"
            item-value="userId"
            placeholder="Partner"
            density="compact"
            bg-color="white"
            variant="outlined"
            v-model="filter.partnerUserId"
            hide-details
            :single-line="true"
          >
            <template v-slot:prepend-item>
              <v-list-item title="All Partners" @click="filter.partnerUserId = null"/>
            </template>
          </v-select>
        </v-col>
        <v-col class="pe-0">
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
        <v-col class="pe-0">
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
        <v-col cols="6">
          <!-- Add New Appointment -->
          <v-btn class="float-end" color="primary" prepend-icon="mdi-plus" flat @click="addAppointmentModal = true">New Appointment</v-btn>
        </v-col>
      </v-row>
    </v-col>
  </v-row>
  <v-divider/>
  <!-- Appointment Item List -->
  <v-data-table
    class="clientAppointmentList bg-transparent"
    v-model:page="currentPage"
    :items="appointmentList"
    :items-per-page="itemsPerPage"
  >
    <template #headers/>
    <template #body v-if="appointmentList.length == 0">
      <div class="p-3 w-100 fs-6 text-center">
        No Appointments
      </div>
    </template>
    <template v-slot:item="{ item }">
      <v-col class="pb-8" cols="3">
        <v-card elevation="10" class="withbg">
          <el-tag
            :type="item.status == 'Approved' ? 'success' : item.status == 'Pending' ? 'warning' : item.status == 'Rejected' ? 'danger' : 'info'"
            class="position-absolute"
            effect="dark"
            style="bottom: -10px; right: -15px; border-radius: 10px;"
          >
            <i class="mdi mdi-check" v-if="item.status == 'Approved'"></i>
            <i class="mdi mdi-timer-sand-complete" v-else-if="item.status == 'Pending'"></i>
            <i class="mdi mdi-close" v-else-if="item.status == 'Rejected'"></i>
            <i class="mdi mdi-cancel" v-else-if="item.status == 'Cancelled'"></i>
            {{ item.status }}
          </el-tag>
          <v-card-actions
            class="p-0 text-subtitle-2 fw-bold justify-content-center text-white"
            style="background-color: rgb(43, 76, 101); min-height: 35px; border-top-right-radius: 10px; border-top-left-radius: 10px;"
          >
            {{ item.category }}
          </v-card-actions>
          <v-card-item class="pt-3">
            <v-card-title class="text-subtitle-2 fw-bold">
              Appointment Time
            </v-card-title>
            <v-card-subtitle class="text-subtitle-2">
              <i class="mdi mdi-calendar-blank-outline me-1"></i>
              {{ dayjs(item.appointmentTime).format("DD MMM YYYY, hh:mm A") }}
            </v-card-subtitle>
            <v-divider class="my-3"/>
            <el-scrollbar class="text-body-1" height="60px">
              <div class="d-flex pt-sm-2 align-center">
                <v-avatar
                  class="mb-3"
                  image="/images/users/avatar.jpg"
                  size="40"
                />
                <strong class="ms-5">{{ item.fullName }}</strong>
              </div>
            </el-scrollbar>
          </v-card-item>
        </v-card>
      </v-col>
    </template>
    <template v-slot:bottom>
      <div class="d-flex justify-content-end pt-2">
        <el-pagination
          layout="total, prev, pager, next"
          v-model:current-page="currentPage"
          :page-size="appointmentList.length/pageCount()"
          :total="appointmentList.length"
          background
        />
      </div>
    </template>
  </v-data-table>

  <!-- Add New Appointment Modal -->
  <SharedUiModal v-model="addAppointmentModal" title="Add New Appointment" width="500">
    <form @submit.prevent="addAppointment">
      <v-card-item class="px-8 py-4 text-body-1">
        <v-row>
          <v-col class="pb-0">
            <v-label class="text-caption">Partner</v-label>
            <v-select
              :items="partnerList"
              item-title="fullName"
              item-value="userId"
              placeholder="Select partner"
              variant="outlined"
              density="compact"
              :error-messages="addAppointmentDetails.partnerUserId.errorMessage"
              v-model="addAppointmentDetails.partnerUserId.value"
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

// Data
const { handleSubmit } = useForm({
  validationSchema: {
    partnerUserId(value) {
      return value ? true : 'Partner is required'
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
  partnerUserId: null,
  category: null,
  status: null,
})
const currentPage = ref(1)
const itemsPerPage = ref(8)
const addAppointmentModal = ref(false)
const addAppointmentDetails = ref({
  partnerUserId: useField('partnerUserId'),
  categoryId: useField('categoryId'),
  appointmentTime: useField('appointmentTime'),
})
const { data: partnerList } = await fetchData.$get("/Partner")
const { data: categoryList } = await fetchData.$get("/Appointment/Category")
const { data: appointmentList } = await fetchData.$get("/Appointment/ClientPerspectiveList", filter.value)

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
const addAppointment = handleSubmit(async(values) => {
  try {
    const result = await fetchData.$post(`/Appointment/ClientCreate`, values)
    if (!result.error) {
      addAppointmentModal.value = false
      addAppointmentDetails.value.partnerUserId.resetField()
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