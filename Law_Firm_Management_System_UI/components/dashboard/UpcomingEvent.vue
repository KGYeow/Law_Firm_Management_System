<template>
  <div>
    <h5 class="text-h5 mb-6 pl-7 d-flex align-center">
      <LayoutFullVerticalSidebarIcon class="me-3" :item="CalendarEventIcon"/>
      Upcoming Events
    </h5>
    <el-scrollbar height="60vh">
      <div class="pa-7 py-1 text-body-1">
        <div class="recent-transaction mt-10 px-3">
          <div v-for="list in recentTransaction" :key="list.title">
            <v-row class="d-flex mb-4">
              <v-col cols="4" lg="3" md="auto" sm="auto" class="px-0 pt-0 pb-1 d-flex align-start">
                <h6 class="text-body-1 textSecondary text-no-wrap">{{ list.title }}</h6>
              </v-col>
              <v-col cols="1" sm="1" class="px-0 text-center pt-0 pb-1">
                <CircleIcon size="13" :class="'text-' + list.textcolor" />
                <div v-if="list.line" class="line mx-auto bg-grey100"></div>
              </v-col>
              <v-col cols="7" sm="8" class="pt-0">
                <h6 v-if="list.boldtext" class="text-body-1 font-weight-bold">{{ list.subtitle }}</h6>
                <h6 v-else class="text-body-1 textSecondary">{{ list.subtitle }}</h6>
                <div class="mt-n1">
                  <RouterLink :to="list.url" class="text-body-1 text-primary text-decoration-none" v-if="list.link">
                    {{ list.link }}
                  </RouterLink>
                </div>
              </v-col>
            </v-row>
          </div>
        </div>
      </div>
    </el-scrollbar>
  </div>
</template>

<script setup>
import { CalendarEventIcon } from 'vue-tabler-icons';
import { recentTransaction } from '@/data/dashboard/dashboardData';

// Data
</script>

<style lang="scss">
.recent-transaction {
  .line {
    width: 2px;
    height: 35px;
  }
}
</style>