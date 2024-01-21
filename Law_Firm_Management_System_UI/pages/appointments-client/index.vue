<template>
  <v-row>
    <v-col cols="12">
      <h5 class="text-h5 mb-6 pl-7 pt-7 ps-0 d-flex align-center">
        Upcoming Appointments
      </h5>
      <v-row>
        <!-- Filters -->
        <v-col class="pe-0">
          <v-autocomplete
            :items="partnerList"
            item-title="fullName"
            item-value="userId"
            placeholder="Partner"
            density="compact"
            bg-color="white"
            variant="outlined"
            v-model="filter.partnerUserId"
            hide-details
          />
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
        <v-card elevation="10" class="withbg overflow-hidden" link @click="getSelectedAppointment(item)">
          <v-card-item class="p-0 border-top border-5 border-primary">
            <v-card-title class="px-5 py-2 d-sm-flex align-center text-h6 fw-bold text-wrap" style="white-space: unset;">
              {{ item.category }}
            </v-card-title>
          </v-card-item>
          <v-card-text class="px-5 py-0 text-body-1 text-wrap">
            <v-card-subtitle class="p-0 text-subtitle-2">
              Appointment Time
            </v-card-subtitle>
            <span class="p-0 text-subtitle-2 fw-bold">
              <i class="mdi mdi-calendar-blank-outline me-1"></i>
              {{ dayjs(item.appointmentTime).format("DD MMM YYYY, hh:mm A") }}
            </span>
            <v-divider class="my-1"/>
            <div class="d-flex pt-sm-2 align-center overflow-hidden">
              <v-avatar
                class="mb-0"
                image="/images/users/avatar.jpg"
                size="40"
              />
              <el-text class="ms-3 fw-bold" truncated>{{ item.fullName }}</el-text>
            </div>
          </v-card-text>
          <v-card-actions class="px-5 py-2 justify-content-end">
            <el-tag type="success" effect="light" v-if="item.status == 'Approved'">
              <i class="mdi mdi-check"/>{{ item.status }}
            </el-tag>
            <el-tag type="warning" effect="light" v-if="item.status == 'Pending'">
              <i class="mdi mdi-timer-sand-complete"/>{{ item.status }}
            </el-tag>
            <el-tag type="danger" effect="light" v-if="item.status == 'Rejected'">
              <i class="mdi mdi-close"/>{{ item.status }}
            </el-tag>
            <el-tag type="info" effect="light" v-if="item.status == 'Cancelled'">
              <i class="mdi mdi-cancel"/>{{ item.status }}
            </el-tag>
          </v-card-actions>
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
  <SharedUiModal v-model="addAppointmentModal" title="Add New Appointment" width="600">
    <el-scrollbar max-height="400px" class="overflow-visible">
      <form @submit.prevent="addAppointment">
        <v-card-item class="px-8 py-4 text-body-1">
          <v-row>
            <v-col>
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
                :disabled-date="(time) => { return time.getTime() < new Date() }"
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
          <v-row>
            <v-col>
              <v-label class="text-caption">Description</v-label>
              <v-textarea
                variant="outlined"
                density="compact"
                v-model="addAppointmentDetails.description"
                hide-details
              />
            </v-col>
          </v-row>
        </v-card-item>
        <v-card-actions class="p-3 justify-content-end">
          <v-btn color="primary" type="submit">Submit</v-btn>
        </v-card-actions>
      </form>
    </el-scrollbar>
  </SharedUiModal>

  <!-- View Appointment Modal -->
  <SharedUiModal v-model="viewAppointmentModal" title="View Appointment Information" width="600">
    <el-scrollbar max-height="400px">
      <v-card-item class="px-8 py-4 text-body-1">
        <v-card-title class="text-h6 fw-bold" style="white-space: unset;">
          {{ selectedAppointmentDetails.category }}
        </v-card-title>
        <v-card-subtitle class="text-subtitle-2">
          <v-row>
            <v-col cols="3" class="pb-0">Partner</v-col>
            <v-col class="pb-0">: {{ selectedAppointmentDetails.fullName }}</v-col>
          </v-row>
          <v-row>
            <v-col cols="3" class="pt-0">Appointment Time</v-col>
            <v-col class="pt-0">: {{ dayjs(selectedAppointmentDetails.appointmentTime).format("DD MMM YYYY, h:mm A") }}</v-col>
          </v-row>
        </v-card-subtitle>
        <v-divider class="mt-3 mb-0"/>
      </v-card-item>
      <v-card-text class="px-8 pt-0 text-body-1 text-justify">
        <v-card-subtitle class="p-0 text-subtitle-2">Description:</v-card-subtitle>
        {{ selectedAppointmentDetails.description ?? '-' }}
      </v-card-text>
      <v-card-actions class="justify-content-end">
        <el-tag type="success" effect="light" v-if="selectedAppointmentDetails.status == 'Approved'">
          <i class="mdi mdi-check"/>{{ selectedAppointmentDetails.status }}
        </el-tag>
        <el-tag type="warning" effect="light" v-if="selectedAppointmentDetails.status == 'Pending'">
          <i class="mdi mdi-timer-sand-complete"/>{{ selectedAppointmentDetails.status }}
        </el-tag>
        <el-tag type="danger" effect="light" v-if="selectedAppointmentDetails.status == 'Rejected'">
          <i class="mdi mdi-close"/>{{ selectedAppointmentDetails.status }}
        </el-tag>
        <el-tag type="info" effect="light" v-if="selectedAppointmentDetails.status == 'Cancelled'">
          <i class="mdi mdi-cancel"/>{{ selectedAppointmentDetails.status }}
        </el-tag>
      </v-card-actions>
    </el-scrollbar>
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
const viewAppointmentModal = ref(false)
const addAppointmentDetails = ref({
  partnerUserId: useField('partnerUserId'),
  categoryId: useField('categoryId'),
  appointmentTime: useField('appointmentTime'),
  description: null,
})
const selectedAppointmentDetails = ref({})
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
const getSelectedAppointment = (appointmentInfo) => {
  selectedAppointmentDetails.value = appointmentInfo
  viewAppointmentModal.value = true
}
const addAppointment = handleSubmit(async(values) => {
  try {
    const result = await fetchData.$post(`/Appointment/ClientCreate`, {
      partnerUserId: values.partnerUserId,
      categoryId: values.categoryId,
      appointmentTime: dayjs(values.appointmentTime).format("DD MMM YYYY, h:mm A"),
      description: addAppointmentDetails.value.description,
    })

    if (!result.error) {
      addAppointmentModal.value = false
      addAppointmentDetails.value.partnerUserId.resetField()
      addAppointmentDetails.value.categoryId.resetField()
      addAppointmentDetails.value.appointmentTime.resetField()
      addAppointmentDetails.value.description = null
      ElNotification.success({ message: result.message })
      refreshNuxtData()
    }
    else {
      ElNotification.error({ message: result.message })
    }
  } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
})
</script>