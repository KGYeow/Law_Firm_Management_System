<template>
  <v-row>
    <v-col cols="12" md="12">
      <UiParentCard title="Sample Page"> 
        <div class="pa-7 pt-1">
          <p class="text-body-1">This is a sample page</p>
          <i class="ri-admin-line"></i>
          <i class="las la-location-arrow"></i>
          <i class="mdi mdi-adjust"></i>
          <Menu2Icon size="20" stroke-width="1.5" />
        </div>
      </UiParentCard>
    </v-col>
  </v-row>
</template>

<script setup lang="ts">
import UiParentCard from '@/components/shared/UiParentCard.vue';
</script>