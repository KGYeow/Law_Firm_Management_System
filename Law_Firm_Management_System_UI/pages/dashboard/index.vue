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
          <DashboardTaskToday/>
        </v-col>

        <v-col cols="4" lg="4">
          <!-- Appointments Data -->
          <DashboardAppointmentData/>
        </v-col>
        <v-col cols="4" lg="4">
          <!-- Cases Data -->
          <DashboardCaseData/>
        </v-col>
        <v-col cols="4" lg="4">
          <!-- Events Data -->
          <DashboardEventData/>
        </v-col>
      </v-row>
    </v-col>
  </v-row>
</template>

<script setup>
import { LayoutDashboardIcon } from "vue-tabler-icons"

// Data
const { data: dashboardData } = await fetchData.$get("/Dashboard/Partner/DashboardData")

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