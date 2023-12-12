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
        
      </div>
    </el-scrollbar>
  </v-navigation-drawer>
  <!-- Events Items -->
  <v-row>
    <v-col cols="12">
      <v-row>
      
        
        <v-col cols="12">
          <!-- Breadcrumbs -->
          <EventBreadcrumb/>
        </v-col>

        <v-col cols="12">
          <!-- Upcoming Event -->
          <EventUpcomingEventTitle/>
        </v-col>
        <v-col cols="12" lg="6">
          <EventItem/>
        </v-col>
        <v-col cols="12" lg="6">
          <EventItem/>
        </v-col>
        
        <v-col cols="12">
         <!-- Past Event -->
         <EventPastEventTitle/>
        </v-col>
        <v-col cols="12" lg="6">
          <EventItem/>
        </v-col>
        <v-col cols="12" lg="6">
          <EventItem/>
        </v-col>
        
    
    </v-row>
      
    </v-col>
  </v-row>
</template>

<script setup>
// Head
useHead({
  title: "Events | CaseCraft",
})

</script>

