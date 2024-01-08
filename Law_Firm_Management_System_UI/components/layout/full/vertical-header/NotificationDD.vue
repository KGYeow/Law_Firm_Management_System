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
      <v-card elevation="0">
        <v-card-item class="px-3 py-2" style="background-color: rgb(243, 244, 248);">
          <v-card-title class="fs-6 fw-bold">
            <v-row>
              <v-col cols="9" class="d-flex align-center">
                <span v-if="!showNotificationLog">Notifications</span>
                <span v-else>Notification History</span>
                <v-chip
                  class="ms-1"
                  variant="flat"
                  color="primary"
                  density="compact"
                  style="font-size: x-small;"
                  v-if="notifications && !showNotificationLog"
                >
                  {{ notifications.length }}
                </v-chip>
              </v-col>
              <v-col cols="3">
                <v-tooltip text="View Notification History" location="top" offset="2" v-if="!showNotificationLog">
                  <template #activator="{ props }">
                    <v-btn class="float-end" icon="mdi-history" size="small" variant="text" v-bind="props" @click="showNotificationLog = true"/>
                  </template>
                </v-tooltip>
                <v-tooltip text="New Notifications" location="top" offset="2" v-else>
                  <template #activator="{ props }">
                    <v-btn class="float-end" icon="mdi-arrow-left" size="small" variant="text" v-bind="props" @click="showNotificationLog = false"/>
                  </template>
                </v-tooltip>
              </v-col>
            </v-row>
          </v-card-title>
        </v-card-item>
        <v-divider class="m-0"/>
        <v-card-text class="p-0" v-if="!showNotificationLog">
          <el-scrollbar height="200px" v-if="notifications.length > 0">
            <v-list class="pt-0 pb-3 newNotification">
              <v-list-item
                class="m-0 p-0 px-3"
                v-for="item in notifications"
              >
                <v-alert class="my-1 p-2" @click="readNotification(item.id)">
                  <v-alert-title class="fw-bold" style="font-size: small;">{{ item.title }}</v-alert-title>
                  <span style="font-size: smaller;">{{ item.description }}</span>
                </v-alert>
                <v-divider class="m-0"/>  
              </v-list-item>
            </v-list>
          </el-scrollbar>
          <div class="p-3 text-center" style="font-size: smaller;" v-else>No new notifications.</div>
        </v-card-text>
        <v-card-text class="p-0" v-else>
          <el-scrollbar height="300px" v-if="notificationLog.length > 0">
            <v-list class="pt-0 pb-3 notificationLog">
              <v-list-item
                class="m-0 p-0 px-3"
                v-for="item in notificationLog"
              >
                <v-alert class="my-1 p-2">
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
const showNotificationLog = ref(false)
const { data: notifications } = await fetchData.$get("/Notification/UnreadList")
const { data: notificationLog } = await fetchData.$get("/Notification/Log")

// Methods
const readNotification = async(notificationId) => {
  try {
    const result = await fetchData.$put(`/Notification/Read/${notificationId}`)
    if (!result.error) {
      refreshNuxtData()
    }
    else {
      ElNotification.error({ message: result.message })
    }
  } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
}

</script>