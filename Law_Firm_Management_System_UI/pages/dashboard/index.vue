<template>
  <v-row>
    <v-col cols="12" lg="8">
      <SalesOverview />
    </v-col>
    <v-col cols="12" lg="4">
      <UiParentCard title="Announcement"> 
        <div class="pa-7 pt-1">
          <p class="text-body-1">This is an announcement section</p>
        </div>  
      </UiParentCard>
    </v-col>
  </v-row>
</template>

<script setup>
import UiParentCard from '@/components/shared/UiParentCard.vue';
import SalesOverview from '@/components/dashboard/SalesOverview.vue';
</script>