<template>
  <v-row>
    <v-col cols="12">
      <h5 class="text-h5 mb-6 pl-7 pt-7 ps-0 d-flex align-center">
        Upcoming Appointments
      </h5>
      <div class="d-flex justify-content-between align-center">
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
        <v-btn color="primary" elevation="0" @click="addAppointmentModal = true">New Appointment</v-btn>
      </div>
    </v-col>
  </v-row>
  <v-row class="justify-content-between">
    <v-col cols="3" v-for="item in appointmentList">
      <v-card elevation="10" class="withbg">
        <el-tag
          :type="item.status == 'Approved' ? 'success' : item.status == 'Pending' ? 'warning' : 'danger'"
          class="position-absolute fw-bold"
          style="bottom: -10px; right: -15px; border-radius: 10px;"
        >
          <i class="mdi mdi-check"></i>
          <i class="mdi mdi-timer-sand-complete"></i>
          <i class="mdi mdi-close"></i>
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

  <!-- Add Appointment Modal -->
  <v-dialog v-model="addAppointmentModal" width="auto" persistent>
    <v-card elevation="10" class="withbg" style="border-radius: 10px;">
      <v-card-title class="px-4 py-4 d-sm-flex align-center justify-space-between">
        <h5 class="text-h5 mb-0 d-flex align-center">
          Add New Appointment
        </h5>
        <v-btn density="compact" variant="plain" icon="mdi-close" @click="addAppointmentModal = false"/>
      </v-card-title>
      <v-divider class="m-0"/>
      <v-card-item class="p-3 text-body-1">
        <form @submit.prevent="addAppointment">
          Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.
        </form>
      </v-card-item>
      <v-divider class="m-0"/>
      <v-card-actions class="p-3 justify-content-end">
        <v-btn color="primary" variant="tonal">Save</v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script setup>
import dayjs from 'dayjs'

// Data
const { data: user } = useAuth()
const { data: appointmentList } = await fetchData.$get(`/Appointment/ClientAppointmentsToLawyer/${user.value.id}`)
const addAppointmentModal = ref(false)
const addAppointmentDetails = ref({
  fullName: null,
  appointmentTime: null,
  category: null
})

// Head
useHead({
  title: "Appointments | CaseCraft",
})

// Methods
const addAppointment = async() => {
  try {
    const result = await fetchData.$put(`/Appointment/${user.value.id}`, addAppointmentDetails.value)
    if (!result.error) {
      addAppointmentDetails.value.fullName = null
      addAppointmentDetails.value.appointmentTime = null
      addAppointmentDetails.value.category = null
      ElNotification.success({ message: result.message })
    }
    else {
      ElNotification.error({ message: result.message })
    }
  } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
}
</script>