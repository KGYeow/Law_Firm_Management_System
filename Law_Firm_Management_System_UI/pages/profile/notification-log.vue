<template>
  <v-row>
    <v-col cols="12" md="12">
      <UiParentCard title="Notification Log">
        <div class="pa-7 pt-1">
          <p class="text-body-1">This is a notification log page</p>
        </div>
      </UiParentCard>
    </v-col>
  </v-row>
</template>

<script setup>
import UiParentCard from '@/components/shared/UiParentCard.vue';

// Head
useHead({
  title: "Notification Log | CaseCraft",
})
</script>