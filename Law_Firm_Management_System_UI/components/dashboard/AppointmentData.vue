<template>
  <v-card elevation="10" class="withbg">
    <v-card-item>
      <div class="d-flex align-center justify-space-between pt-sm-2">
        <v-card-title class="text-h6 d-flex align-center">
          <v-avatar class="me-2" color="rgb(243, 244, 248)" size="small">
            <Book2Icon color="gray" size="18"/>
          </v-avatar>
          Appointments
        </v-card-title>
      </div>
      <v-divider/>
      <v-row>
        <v-col cols="7" sm="12">
          <div>
            <h3 class="text-h3">36,358</h3>
            <div class="d-flex align-center flex-shrink-0">
              <apexchart type="donut" height="145" :options="chartOptions" :series="Chart"> </apexchart>
            </div>
          </div>
        </v-col>
        <v-col cols="5" sm="12" class="pt-0">
          <div class="d-flex flex-column">
            <h6 class="text-subtitle-1 text-muted">
              <v-icon icon="mdi mdi-checkbox-blank-circle" class="mr-1" size="10" color="primary"></v-icon> 2022
            </h6>
            <h6 class="text-subtitle-1 text-muted">
              <v-icon icon="mdi mdi-checkbox-blank-circle" class="mr-1" size="10" color="lightprimary"></v-icon> 2023
            </h6>
          </div>
        </v-col>
      </v-row>
    </v-card-item>
  </v-card>
</template>

<script setup lang="ts">
// Properties
const props = defineProps({
  appointmentTotal: Number,
  appointmentPending: Number,
  appointmentApproved: Number,
  appointmentRejected: Number,
  appointmentCancelled: Number,
})

// Data
const chartOptions = computed(() => {
  return {
    labels: ['Pending', 'Approved', 'Rejected'],
    chart: {
      type: 'donut',
      fontFamily: `inherit`,
      foreColor: '#a1aab2',
      toolbar: {
        show: false
      }
    },
    colors: ['#0d47a1', '#64b5f6', '#bbdefb'],
    plotOptions: {
      pie: {
        startAngle: 0,
        endAngle: 360,
        donut: {
          size: '75%',
          background: 'transparent'
        }
      }
    },
    stroke: {
      show: false
    },
    dataLabels: {
      enabled: false
    },
    legend: {
      show: false
    },
    tooltip: { theme: "light", fillSeriesColor: false },
  }
})
const Chart = [38, 40, 25]
</script>