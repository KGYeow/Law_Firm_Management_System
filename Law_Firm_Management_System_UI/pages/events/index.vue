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
      

        
        
        <!---If Caption-->
       
        
        <v-col cols="12">
          
          

          <!-- Upcoming Event -->
          <div>
            <h5 class="text-h5 pl-7 d-flex align-center">
              Upcoming Events
            </h5>
            <el-scrollbar height="0vh">
              <div class="pa-7 py-1 text-body-1">
              </div>
            </el-scrollbar>
          </div>
        
        <v-list items="events" style="background-color: rgb(243, 244, 248);">
            <v-row>
              <v-col v-for="n in 3" :key="n" cols="12" lg="6">
                <EventItem />
              </v-col>
            </v-row>
          </v-list>
        </v-col>
        
        <v-col cols="12">
         <!-- Past Event -->
         <div>
          <h5 class="text-h5 pl-7 d-flex align-center">
            Past Events
          </h5>
          <el-scrollbar height="0vh">
            <div class="pa-7 py-1 text-body-1">
            </div>
          </el-scrollbar>
        </div>
        
        
        
        <v-list items="events" style="background-color: rgb(243, 244, 248);">
            <v-row>
              <v-col v-for="n in 3" :key="n" cols="12" lg="6">
                <EventItem />
              </v-col>
            </v-row>
          </v-list>
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

