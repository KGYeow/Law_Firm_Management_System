<template>
  <v-menu :close-on-content-click="false">
    <template v-slot:activator="{ props }">
      <v-btn icon variant="text" class="custom-hover-primary me-md-3 me-sm-5 me-3 text-muted" v-bind="props">
        <v-badge
          dot
          color="primary"
          offset-x="-5"
          offset-y="-3"
          v-if="notifications.length > 0"
        >
          <BellRingingIcon stroke-width="1.5" size="22" />
        </v-badge>
        <BellRingingIcon stroke-width="1.5" size="22" v-else/>
      </v-btn>
    </template>
    <v-sheet rounded="md" width="300" elevation="10" class="mt-2" :border="true">
      <v-card>
        <v-card-item class="px-3 py-2" style="background-color: rgb(243, 244, 248);">
          <v-card-title class="d-flex align-center fs-6 fw-bold">
            Notifications
            <v-chip
              class="ms-1"
              variant="flat"
              color="primary"
              density="compact"
              style="font-size: x-small;"
              v-if="notifications"
            >
              {{ notifications.length }}
            </v-chip>
          </v-card-title>
        </v-card-item>
        <v-divider class="m-0"/>
        <v-card-text class="p-0">
          <el-scrollbar height="200px" v-if="notifications.length > 0">
            <v-list class="pt-0 pb-3">
              <v-list-item
                class="p-0 px-3"
                v-for="item in notifications"
              >
                <v-alert class="p-2">
                  <v-alert-title class="fw-bold" style="font-size: small;">{{ item.title }}</v-alert-title>
                  <span style="font-size: smaller;">{{ item.description }}</span>
                </v-alert>
                <v-divider class="m-0"/>
              </v-list-item>
            </v-list>
          </el-scrollbar>
          <div class="p-3 text-center" style="font-size: smaller;" v-else>No notifications.</div>
        </v-card-text>
      </v-card>
    </v-sheet>
  </v-menu>
</template>

<script setup>
import { BellRingingIcon } from 'vue-tabler-icons';

// Data
const { data: notifications } = await fetchData.$get("/Notification/UserNotifications")
</script>