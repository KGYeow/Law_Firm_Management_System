<template>
    <v-card elevation="10" class="withbg">
      <v-card-item>
        <v-card-title class="fs-3 fw-bold">
          
          <h5 class="text-h5 mb-0 pl-0 d-flex align-center">
          <LayoutFullVerticalSidebarIcon class="me-3" :item="ChevronLeftIcon"/>
          <LayoutFullVerticalSidebarIcon class="me-3" :item="CalendarIcon"/>
          Events
          </h5>
        </v-card-title>
      </v-card-item>
    </v-card>

    
  

  </template>
  
  <script setup>
  import { CalendarIcon } from 'vue-tabler-icons';
  import { ChevronLeftIcon } from 'vue-tabler-icons';
  </script>