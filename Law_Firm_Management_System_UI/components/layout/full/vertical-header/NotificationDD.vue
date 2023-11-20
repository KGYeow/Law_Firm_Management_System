<template>
  <v-menu :close-on-content-click="false">
    <template v-slot:activator="{ props }">
      <v-btn icon variant="text" class="custom-hover-primary me-md-3 me-sm-5 me-3 text-muted" v-bind="props">
        <v-badge
          dot
          color="primary"
          offset-x="-5"
          offset-y="-3"
          v-if="notifications"
        >
          <BellRingingIcon stroke-width="1.5" size="22" />
        </v-badge>
        <BellRingingIcon stroke-width="1.5" size="22" v-else/>
      </v-btn>
    </template>
    <v-sheet rounded="md" width="300" elevation="10" class="mt-2">
      <el-scrollbar height="200px">
        <v-list class="py-0">
          <v-list-item v-for="item in notifications">
            <v-alert variant="outlined">
              <v-alert-title class="fs-6 fw-bold">{{ item.title }}</v-alert-title>
              <span style="font-size: smaller;">{{ item.description }}</span>
            </v-alert>
          </v-list-item>
        </v-list>
      </el-scrollbar>
    </v-sheet>
  </v-menu>
</template>

<script setup lang="ts">
import { BellRingingIcon } from 'vue-tabler-icons';

// Data
const { data: user } = useAuth()
const notifications = fetchData.$get(`/Notification/UserNotifications/${user.value?.id}`).data
</script>