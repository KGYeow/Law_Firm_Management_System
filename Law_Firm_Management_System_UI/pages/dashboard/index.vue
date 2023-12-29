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
        <v-col cols="12">
          <DashboardTodayTask/>
        </v-col>
        <v-col cols="12" lg="6">
          <DashboardCaseCompleted/>
        </v-col>
        <v-col cols="12" lg="6">
          <div class="mb-6">
            <DashboardCaseNew/>
          </div>
          <div>
            <DashboardTaskCompleted/>
          </div>
        </v-col>
      </v-row>
    </v-col>
  </v-row>
</template>

<script setup>
import { LayoutDashboardIcon } from "vue-tabler-icons"

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