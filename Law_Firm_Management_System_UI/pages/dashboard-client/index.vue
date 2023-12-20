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
      </v-row>
    </v-col>
  </v-row>
</template>

<script setup>
// Head
useHead({
  title: "Dashboard | CaseCraft",
})
</script>