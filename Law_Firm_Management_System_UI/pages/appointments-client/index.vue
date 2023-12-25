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
        <v-btn color="primary" elevation="0" @click="addAppointmentModal = true">New Appointment</v-btn>
      </div>
    </v-col>
  </v-row>

  <!-- Appointment Item List -->
  <v-row class="justify-content-between">
    <v-col cols="3" v-for="item in appointmentList">
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
  <v-dialog v-model="addAppointmentModal" width="auto" persistent>
    <v-card elevation="10" class="withbg" width="500px" style="border-radius: 10px;">
      <v-card-title class="px-4 py-4 d-sm-flex align-center justify-space-between">
        <h5 class="text-h5 mb-0 d-flex align-center">
          Add New Appointment
        </h5>
        <v-btn density="compact" variant="plain" icon="mdi-close" @click="addAppointmentModal = false"/>
      </v-card-title>
      <v-divider class="m-0"/>
      <form @submit.prevent="addAppointment">
        <v-card-item class="px-5 py-4 text-body-1">
          <v-row>
            <v-col>
              <v-label class="text-caption">Partner</v-label>
              <v-select
                :items="partnerList"
                item-title="fullName"
                item-value="fullName"
                placeholder="Select partner"
                variant="outlined"
                density="compact"
                color="primary"
                :rules="[!!addAppointmentDetails.fullName || 'Required.']"
                v-model="addAppointmentDetails.fullName"
              />
            </v-col>
          </v-row>
          <v-row>
            <v-col>
              <v-label class="text-caption">Category</v-label>
              <v-select
                :items="categoryList"
                item-title="name"
                item-value="name"
                placeholder="Select category"
                variant="outlined"
                density="compact"
                color="primary"
                v-model="addAppointmentDetails.category"
                hide-details
              />
            </v-col>
          </v-row>
          <v-row>
            <v-col>
              <v-label class="text-caption">Date</v-label>
              <v-menu :close-on-content-click="false">
                <template v-slot:activator="{ props }">
                  <v-text-field
                    v-bind="props"
                    placeholder="Select date and time"
                    variant="outlined"
                    density="compact"
                    color="primary"
                    append-inner-icon="mdi-calendar"
                    :model-value="addAppointmentDetails.appointmentTime ? dayjs(addAppointmentDetails.appointmentTime).format('YYYY-MM-DD, hh:mm A') : null"
                    hide-details
                    readonly
                  />
                </template>
                <v-sheet elevation="3">
                  <v-text-field
                    class="my-3 mx-5"
                    placeholder="Select time"
                    variant="outlined"
                    density="compact"
                    color="primary"
                    type="time"
                    v-model="addAppointmentDetails.time"
                    hide-details
                  />
                  <v-divider class="mx-5 my-0"/>
                  <v-date-picker
                    elevation="0"
                    color="primary"
                    :min="new Date(new Date().getTime() - 86400000)"
                    v-model="addAppointmentDetails.date"
                    show-adjacent-months
                    hide-header
                  />
                </v-sheet>
              </v-menu>
            </v-col>
          </v-row>
        </v-card-item>
        <v-divider class="m-0"/>
        <v-card-actions class="p-3 justify-content-end">
          <v-btn color="primary" type="submit">Submit</v-btn>
        </v-card-actions>
      </form>
    </v-card>
  </v-dialog>
</template>

<script setup>
import dayjs from 'dayjs'

// Data
const { data: user } = useAuth()
const { data: partnerList } = await fetchData.$get("/Partner")
const { data: categoryList } = await fetchData.$get("/Appointment/Category")
const { data: appointmentList } = await fetchData.$get(`/Appointment/List/ClientPerspective/${user.value.id}`)
const addAppointmentModal = ref(false)
const addAppointmentDetails = ref({
  fullName: null,
  category: null,
  appointmentTime: null,
  date: null,
  time: null
})
addAppointmentDetails.value.appointmentTime = computed(() => {
  if (!addAppointmentDetails.value.date && !addAppointmentDetails.value.time)
    return null
  const date = addAppointmentDetails.value.date ?? new Date()
  const time = addAppointmentDetails.value.time ?? "00:00"
  return new Date(`${date.toLocaleString().slice(0, 10)} ${time}`)
})

// Head
useHead({
  title: "Appointments | CaseCraft",
})

// Methods
const addAppointment = async() => {
  try {
    const result = await fetchData.$put(`/Appointment/ClientCreate/${user.value.id}`, {
      fullName: addAppointmentDetails.value.fullName,
      category: addAppointmentDetails.value.category,
      appointmentTime: addAppointmentDetails.value.appointmentTime,
    })

    if (!result.error) {
      addAppointmentModal.value = false
      addAppointmentDetails.value.fullName = null
      addAppointmentDetails.value.category = null
      addAppointmentDetails.value.appointmentTime = null
      addAppointmentDetails.value.date = null
      addAppointmentDetails.value.time = null
      ElNotification.success({ message: result.message })
    }
    else {
      ElNotification.error({ message: result.message })
    }
  } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
}
</script>