<template>
  <v-btn icon variant="text" class="custom-hover-primary me-md-3 me-sm-5 me-3 text-muted">
    <v-badge dot color="primary" offset-x="-5" offset-y="-3">
      <BellRingingIcon stroke-width="1.5" size="22" />
    </v-badge>
  </v-btn>
</template>

<script setup lang="ts">
import { BellRingingIcon } from 'vue-tabler-icons';
</script>