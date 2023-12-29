<template>
  <v-row>
    <v-col cols="12">
      <h5 class="text-h5 mb-6 pl-7 pt-7 ps-0 d-flex align-center">
        Upcoming Appointments
      </h5>
      <div class="d-flex justify-content-between align-center">
        <!-- Search Bar -->
        <div class="w-25">
          <v-text-field
            label="Search appointments"
            density="compact"
            variant="solo"
            append-inner-icon="mdi-magnify"
            single-line
            hide-details
          ></v-text-field>
        </div>
        <!-- Add New Appointment -->
        <v-btn color="primary" flat @click="addAppointmentModal = true">New Appointment</v-btn>
      </div>
    </v-col>
  </v-row>

  <!-- Appointment Item List -->
  <v-row class="justify-content-start">
    <div class="w-100 text-center pt-5" v-if="appointmentList.length == 0">
      <div>No Appointments</div>
    </div>
    <v-col class="pb-8" cols="3" v-for="item in appointmentList" v-else>
      <v-card elevation="10" class="withbg">
        <el-tag
          :type="item.status == 'Approved' ? 'success' : item.status == 'Pending' ? 'warning' : 'danger'"
          class="position-absolute"
          effect="dark"
          style="bottom: -10px; right: -15px; border-radius: 10px;"
        >
          <i class="mdi mdi-check" v-if="item.status == 'Approved'"></i>
          <i class="mdi mdi-timer-sand-complete" v-else-if="item.status == 'Pending'"></i>
          <i class="mdi mdi-close" v-else-if="item.status == 'Rejected'"></i>
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
  </v-row>

  <!-- Add New Appointment Modal -->
  <v-dialog v-model="addAppointmentModal" width="auto">
    <v-card elevation="10" class="withbg rounded-3 overflow-visible" width="500px">
      <v-card-title class="px-4 py-4 d-sm-flex align-center justify-space-between bg-background rounded-top-3">
        <h5 class="text-h5 mb-0 d-flex align-center">
          Add New Appointment
        </h5>
        <v-btn density="compact" variant="plain" icon="mdi-close" @click="addAppointmentModal = false"/>
      </v-card-title>
      <v-divider class="m-0"/>
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
                color="primary"
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
                color="primary"
                :error-messages="addAppointmentDetails.categoryId.errorMessage"
                v-model="addAppointmentDetails.categoryId.value"
                hide-details="auto"
              />
            </v-col>
            <v-col>
              <v-label class="text-caption">Date</v-label>
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
    </v-card>
  </v-dialog>
</template>

<script setup>
import { useField, useForm } from 'vee-validate'
import dayjs from 'dayjs'

// Data
const { data: user } = useAuth()
const { data: partnerList } = await fetchData.$get("/Partner")
const { data: categoryList } = await fetchData.$get("/Appointment/Category")
const { data: appointmentList } = await fetchData.$get(`/Appointment/List/ClientPerspective/${user.value.id}`)
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
const addAppointmentModal = ref(false)
const addAppointmentDetails = ref({
  partnerUserId: useField('partnerUserId'),
  categoryId: useField('categoryId'),
  appointmentTime: useField('appointmentTime'),
})

// Head
useHead({
  title: "Appointments | CaseCraft",
})

// Methods
const addAppointment = handleSubmit(async(values) => {
  try {
    const result = await fetchData.$post(`/Appointment/ClientCreate`, values)
    if (!result.error) {
      addAppointmentModal.value = false
      addAppointmentDetails.value.partnerUserId.value = null
      addAppointmentDetails.value.categoryId.value = null
      addAppointmentDetails.value.appointmentTime.value = null
      ElNotification.success({ message: result.message })
      refreshNuxtData()
    }
    else {
      ElNotification.error({ message: result.message })
    }
  } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
})
</script>