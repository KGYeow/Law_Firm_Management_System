<template>
  <v-row>
    <v-col cols="12" lg="8">
      <SalesOverview />
    </v-col>
    <v-col cols="12" lg="4">
      <Announcement/>
    </v-col>
  </v-row>
</template>

<script setup>
import SalesOverview from '@/components/dashboard/SalesOverview.vue';
import Announcement from '@/components/dashboard/Announcement.vue';
</script>