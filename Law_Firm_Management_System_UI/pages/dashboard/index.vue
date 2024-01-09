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
      <div class="d-flex flex-column">
        <!-- Calendar -->
        <DashboardCalendar/>
        <v-divider class="mx-7"/>
        <!-- Events -->
        <DashboardUpcomingEvent/>
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

        <!-- Tasks Data -->
        <v-col cols="12">
          <DashboardTaskData
            :task-to-do="dashboardData.numTasksToDo"
            :task-in-progress="dashboardData.numTasksInProgress"
            :task-completed="dashboardData.numTasksCompleted"
          />
        </v-col>

        <v-col cols="4" lg="4" v-if="userRole == 'Partner'">
          <!-- Appointments Data -->
          <DashboardAppointmentData
            :appointment-total="dashboardData.numAppointmentsTotal"
            :appointment-pending="dashboardData.numAppointmentsPending"
            :appointment-approved="dashboardData.numAppointmentsApproved"
            :appointment-rejected="dashboardData.numAppointmentsRejected"
            :appointment-cancelled="dashboardData.numAppointmentsCancelled"
          />
        </v-col>
        <v-col cols="4" lg="4" v-if="userRole == 'Partner'">
          <!-- Cases Data -->
          <DashboardCaseData
            :case-total="dashboardData.numCasesTotal"
            :case-completed="dashboardData.numCasesCompleted"
            :case-incompleted="dashboardData.numCasesIncompleted"
          />
        </v-col>
        <v-col cols="4" lg="4" v-if="userRole == 'Partner'">
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
const { data: userRole } = await fetchData.$get("/UserRole/RoleName")
const { data: dashboardData } = await fetchData.$get(`/Dashboard/Data/${userRole.value}`)

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