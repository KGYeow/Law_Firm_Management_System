<template>
  <div>
    <h5 class="text-h5 mb-6 pl-7 pt-7 d-flex align-center">
      <LayoutFullVerticalSidebarIcon class="me-3" :item="SpeakerphoneIcon"/>
      Announcements
    </h5>
    <div class="text-body-1">
      <v-card elevation="10">
        <v-carousel height="200px" color="primary" hide-delimiter-background="" delimiter-icon="mdi-minus">
          <v-carousel-item v-for="item in announcement" height="200px">
            <v-card-item class="px-16 mx-3">
              <v-card-title class="fs-6 fw-bold">
                {{ item.title }}
              </v-card-title>
              <v-card-subtitle style="font-size: smaller;">
                {{ dayjs(item.createdTime).format("D MMM YYYY, h:mm A") }}
              </v-card-subtitle>
              <v-card-text class="p-0 pt-2 text-justify">
                {{ item.description }}
              </v-card-text>
            </v-card-item>
          </v-carousel-item>
        </v-carousel>
      </v-card>
    </div>
  </div>
</template>

<script setup>
import { SpeakerphoneIcon } from 'vue-tabler-icons'
import dayjs from 'dayjs';

// Data
const { data: announcement } = fetchData.$get("/Dashboard/Announcement")
</script>