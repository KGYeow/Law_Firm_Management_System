<template>
  <div>
    <h5 class="text-h5 mb-6 pl-7 pt-7 d-flex align-center">
      <LayoutFullVerticalSidebarIcon class="me-3" :item="SpeakerphoneIcon"/>
      Announcements
    </h5>
    <el-scrollbar height="50vh">
      <div class="pa-7 py-1 text-body-1">
        <v-card
          v-for="item in announcement"
          class="mb-3 p-3"
          color="rgb(243, 244, 248)"
          elevation="2"
        >
          <v-card-item class="p-0">
            <v-card-title class="fs-6 fw-bold">
              {{ item.title }}
            </v-card-title>
            <v-card-subtitle style="font-size: smaller;">
              {{ dayjs(item.createdTime).format("D MMM YYYY, h:mm A") }}
            </v-card-subtitle>
          </v-card-item>
          <v-card-text class="p-0 pt-2">
            {{ item.description }}
          </v-card-text>
        </v-card>
      </div>
    </el-scrollbar>
  </div>
</template>

<script setup>
import { SpeakerphoneIcon } from 'vue-tabler-icons'
import dayjs from 'dayjs';

// Data
const { data: announcement } = fetchData.$get("/Dashboard/Announcement")
</script>