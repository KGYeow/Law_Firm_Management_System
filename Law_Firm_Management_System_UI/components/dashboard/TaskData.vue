<template>
  <v-card elevation="10" class="withbg">
    <v-card-item>
      <v-row class="text-center">
        <v-col cols="3">
          <div>
            <v-card-title class="fs-6 text-black-50">Today's Tasks</v-card-title>
            <v-card-text class="p-0 pt-2">
              <RouterLink to="/tasks" class="text-secondary">
                <i class="mdi mdi-arrow-right"></i>
                Check your daily tasks
              </RouterLink>
            </v-card-text>
          </div>
        </v-col>
        <v-divider vertical/>
        <v-col cols="3">
          <div>
            <v-card-title class="fs-6 text-black-50">To Do Tasks</v-card-title>
            <v-card-text class="p-0 pt-2 text-h3 fw-bold">{{ taskToDo }}</v-card-text>
          </div>
        </v-col>
        <v-divider vertical/>
        <v-col cols="3">
          <div>
            <v-card-title class="fs-6 text-black-50">In-Progress Tasks</v-card-title>
            <v-card-text class="p-0 pt-2 text-h3 fw-bold">{{ taskInProgress }}</v-card-text>
          </div>
        </v-col>
        <v-divider vertical/>
        <v-col cols="3">
          <div>
            <v-card-title class="fs-6 text-black-50">Completed Tasks</v-card-title>
            <v-card-text class="p-0 pt-2 text-h3 fw-bold">{{ taskCompleted }}</v-card-text>
          </div>
        </v-col>
      </v-row>
    </v-card-item>
  </v-card>
</template>

<script setup lang="ts">
const props = defineProps({
  taskToDo: { type: Number, default: 0 },
  taskInProgress: { type: Number, default: 0 },
  taskCompleted: { type: Number, default: 0 },
})
</script>