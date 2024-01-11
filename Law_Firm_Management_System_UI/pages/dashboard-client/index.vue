<template>
  <!-- Rightbar -->
  <v-navigation-drawer
    elevation="0"
    location="right"
    width="350"
    absolute
    permanent
  >
    <el-scrollbar>
      <div class="mt-3 mb-7 d-flex flex-column">
        <!-- Calendar -->
        <DashboardCalendar :list="upcomingEventList"/>
        <v-divider class="mx-7"/>
        <!-- Events -->
        <DashboardUpcomingEvent :list="upcomingEventList"/>
      </div>
    </el-scrollbar>
  </v-navigation-drawer>

  <!-- Dashboard Items -->
  <v-row>
    <v-col cols="12">
      <v-row>
        <!-- Announcements -->
        <v-col cols="12">
          <DashboardAnnouncement/>
        </v-col>

        <v-col cols="4" lg="4">
          <!-- Appointments Data -->
          <DashboardAppointmentData
            :appointment-total="dashboardData.numAppointmentsTotal"
            :appointment-pending="dashboardData.numAppointmentsPending"
            :appointment-approved="dashboardData.numAppointmentsApproved"
            :appointment-rejected="dashboardData.numAppointmentsRejected"
            :appointment-cancelled="dashboardData.numAppointmentsCancelled"
          />
        </v-col>
        <v-col cols="4" lg="4">
          <!-- Cases Data -->
          <DashboardCaseData
            :case-total="dashboardData.numCasesTotal"
            :case-completed="dashboardData.numCasesCompleted"
            :case-incompleted="dashboardData.numCasesIncompleted"
          />
        </v-col>
        <v-col cols="4" lg="4">
          <!-- Events Data -->
          <DashboardEventData
            :event-total="dashboardData.numEventTotal"
            :event-completed="dashboardData.numEventCompleted"
            :event-incompleted="dashboardData.numEventIncompleted"
          />
        </v-col>
      </v-row>
    </v-col>
  </v-row>
</template>

<script setup>
import { LayoutDashboardIcon } from "vue-tabler-icons"

// Data
const { data: dashboardData } = await fetchData.$get("/Dashboard/Data/Client")
const { data: upcomingEventList } = await fetchData.$get("/Dashboard/UpcomingEvents/Client")

// Head
useHead({
  title: "Dashboard | CaseCraft",
})

// Page Meta
definePageMeta({
  breadcrumbsIcon: shallowRef(LayoutDashboardIcon),
  breadcrumbs: [
    {
      title: 'Dashboard',
      disabled: false,
    },
  ],
})
</script>