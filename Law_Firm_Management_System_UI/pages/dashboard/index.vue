<template>
  <!-- Rightbar -->
  <v-navigation-drawer
    class=""
    location="right"
    width="300"
    permanent
    :absolute="true"
  >
    <perfect-scrollbar class="">
      <!-- Announcements -->
      <DashboardAnnouncement/>
      <v-divider class="mx-7"/>
      <!-- Events -->
      <DashboardEvent/>
    </perfect-scrollbar>
  </v-navigation-drawer>
  <!-- Dashboard Items -->
  <v-row>
    <v-col cols="12" lg="12">
      <v-row>
        <v-col cols="6" lg="6">
          <DashboardCaseCompleted/>
        </v-col>
        <v-col cols="6" lg="6">
          <DashboardCaseNew/>
        </v-col>
      </v-row>
      <v-row>
        <v-col cols="12" lg="12">
          <!-- Calendar -->
          <DashboardCalendar/>
        </v-col>
      </v-row>
    </v-col>
  </v-row>
</template>

<script setup>
</script>

<style scoped lang="scss">
</style>