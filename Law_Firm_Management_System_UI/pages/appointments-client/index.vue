<template>
  <v-row>
    <v-col cols="12">
      <h5 class="text-h5 mb-6 pl-7 pt-7 d-flex align-center">
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
        <v-btn color="primary" elevation="0" @click="">New Appointment</v-btn>
      </div>
    </v-col>
  </v-row>
  <v-row>
    <v-col cols="3" v-for="item in appointmentList">
      <v-card elevation="10" class="withbg">
        <v-card-item>
          <div class="d-flex flex-column justify-content-center pt-sm-2">
            <v-card-title class="text-subtitle-1 fw-bold d-flex align-center">
              {{ item.category }}
            </v-card-title>
            <v-card-subtitle class="text-subtitle-2">
              <i class="mdi mdi-calendar-blank-outline me-1"></i>
              {{ dayjs(item.appointmentTime).format("DD MMM YYYY, hh:mm A") }}
            </v-card-subtitle>
          </div>
          <v-divider/>
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
</template>

<script setup>
import dayjs from 'dayjs'

// Data
const { data: user } = useAuth()
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
const { data: appointmentList } = await fetchData.$get(`/Appointment/ClientAppointmentsToLawyer/${user.value.id}`)

// Head
useHead({
  title: "Appointments | CaseCraft",
})

// Methods
const pageCount = () => {
  return Math.ceil(appointmentList.value.length / itemsPerPage.value)
}
</script>