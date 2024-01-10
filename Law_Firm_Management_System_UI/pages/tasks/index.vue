<template>
  <v-row>
    <!-- <BackLog> -->
    <v-col cols="3">
      <v-card elevation="10" class="withbg">
        <v-card-item class="pa-0">
          <div class="d-sm-flex align-center justify-space-between">
            <h5 class="text-h5 mb-3 pl-7 pt-3 d-flex align-center">
              BackLog
            </h5>
          </div>
        </v-card-item>
      </v-card>
    <!-- <Task Card> -->      
      <el-scrollbar max-height="200px" class="pt-5">
        <v-card elevation="10" class="withbg">
          <v-card-item class="pa-0">
            <div class="d-sm-flex align-center justify-space-between">
              <h5 class="text-h5 mb-3 pl-7 pt-3 d-flex align-center">
                Create styleguide foundation
              </h5>
            </div>
            <div class="pl-7 mb-2 pt-2 text justify">
                This is the first task description 
            </div>
            <div class="pl-7 mb-2 pt-2 text justify">
                Aug 20 2001
            </div>
            <v-btn
              color="primary"
              class="ma-2 ml-7" 
              >Details</v-btn>
              <v-btn
              color="primary"
              class="ma-2 ml-7" 
              >Update</v-btn>
          </v-card-item>
        </v-card>
      </el-scrollbar>
    </v-col>
    <!-- <To Do> -->
    <v-col cols="3">
      <v-card elevation="10" class="withbg">
          <v-card-item class="pa-0">
            <div class="d-sm-flex align-center justify-space-between">
              <h5 class="text-h5 mb-3 pl-7 pt-3 d-flex align-center">
                To Do
              </h5>
            </div>
          </v-card-item>
      </v-card>
    </v-col>
    <!-- <In Progress> -->
    <v-col cols="3">
      <v-card elevation="10" class="withbg">
          <v-card-item class="pa-0">
            <div class="d-sm-flex align-center justify-space-between">
              <h5 class="text-h5 mb-3 pl-7 pt-3 d-flex align-center">
                In Progress
              </h5>
            </div>
          </v-card-item>
      </v-card>
    </v-col>
    <!-- <Complete> -->
    <v-col cols="3">
      <v-card elevation="10" class="withbg">
          <v-card-item class="pa-0">
            <div class="d-sm-flex align-center justify-space-between">
              <h5 class="text-h5 mb-3 pl-7 pt-3 d-flex align-center">
                Complete
              </h5>
            </div>
          </v-card-item>
      </v-card>
    </v-col>
  </v-row>
</template>


<script setup>
import { ClipboardTextIcon } from "vue-tabler-icons"

// Head
useHead({
  title: "Tasks | CaseCraft",
})

// Page Meta
definePageMeta({
  breadcrumbsIcon: shallowRef(ClipboardTextIcon),
  breadcrumbs: [
    {
      title: 'Tasks',
      disabled: false,
    },
  ],
})
</script>