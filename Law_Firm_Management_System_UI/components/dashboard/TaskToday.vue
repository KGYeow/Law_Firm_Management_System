<template>
  <v-card elevation="10" class="withbg">
    <v-card-item>
      <v-row>
        <v-col>
          <v-card-title class="fs-3 fw-bold">
            Today Task
          </v-card-title>
          <v-card-text class="p-0 pt-8 text-justify">
            <div>Check your daily tasks</div>
            <v-btn color="primary" elevation="0" class="mt-5">Today's Task</v-btn>
          </v-card-text>
        </v-col>
        <v-col>
          <v-table class="month-table">
            <thead>
              <tr>
                <th class="text-subtitle-1 font-weight-bold">Id</th>
                <th class="text-subtitle-1 font-weight-bold">Assigned</th>
                <th class="text-subtitle-1 font-weight-bold">Name</th>
                <th class="text-subtitle-1 font-weight-bold">Priority</th>
                <th class="text-subtitle-1 font-weight-bold text-right">Budget</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="item in productPerformance" :key="item.name" class="month-item">
                <td>
                  <p class="text-15 font-weight-medium">{{ item.id }}</p>
                </td>
                <td>
                  <div class="">
                    <h6 class="text-subtitle-1 font-weight-bold">{{ item.name }}</h6>
                    <div class="text-13 mt-1 text-muted">{{ item.post }}</div>
                  </div>
                </td>
                <td>
                  <h6 class="text-body-1 text-muted">{{ item.pname }}</h6>
                </td>
                <td>
                  <v-chip  :class="'text-body-1 bg-' + item.statuscolor " color="white"  size="small" >{{ item.status }}</v-chip>
                </td>
                <td>
                  <h6 class="text-h6 text-right">{{ item.budget }}</h6>
                </td>
              </tr>
            </tbody>
          </v-table>
        </v-col>
      </v-row>
    </v-card-item>
  </v-card>
</template>

<script setup>
import { productPerformance } from '@/data/dashboard/dashboardData';
</script>