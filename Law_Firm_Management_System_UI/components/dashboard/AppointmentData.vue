<template>
  <v-card elevation="10" class="withbg h-100">
    <v-card-item>
      <div class="d-flex align-center justify-space-between pt-sm-2 pb-2">
        <v-card-title class="text-h6 d-flex align-center">
          <v-avatar class="me-2" color="rgb(243, 244, 248)" size="small">
            <Book2Icon color="gray" size="18"/>
          </v-avatar>
          Appointments
        </v-card-title>
      </div>
      <v-row>
        <v-col cols="12" sm="12">
          <h3 class="text-h3">{{ appointmentTotal }}</h3>
          <div class="d-flex align-center flex-shrink-0" v-if="appointmentTotal != 0">
            <apexchart type="pie" height="145" :options="chartOptions" :series="chartSeries"/>
          </div>
          <el-empty class="p-0 pb-2" :image-size="110" description="No appointment" v-else/>
        </v-col>
        <v-col cols="12" sm="12" v-if="appointmentTotal != 0">
          <div class="d-flex flex-column">
            <h6 class="text-subtitle-1 text-muted">
              <v-icon icon="mdi mdi-checkbox-blank-circle" class="mr-1" size="10" color="#0D47A1"/> Pending
            </h6>
            <h6 class="text-subtitle-1 text-muted">
              <v-icon icon="mdi mdi-checkbox-blank-circle" class="mr-1" size="10" color="#2196F3"/> Approved
            </h6>
            <h6 class="text-subtitle-1 text-muted">
              <v-icon icon="mdi mdi-checkbox-blank-circle" class="mr-1" size="10" color="#64B5F6"/> Rejected
            </h6>
            <h6 class="text-subtitle-1 text-muted">
              <v-icon icon="mdi mdi-checkbox-blank-circle" class="mr-1" size="10" color="#BBDEFB"/> Cancelled
            </h6>
          </div>
        </v-col>
      </v-row>
    </v-card-item>
  </v-card>
</template>

<script setup lang="ts">
import apexchart from 'vue3-apexcharts'

// Properties
const props = defineProps({
  appointmentTotal: { type: Number, default: 0 },
  appointmentPending: { type: Number, default: 0 },
  appointmentApproved: { type: Number, default: 0 },
  appointmentRejected: { type: Number, default: 0 },
  appointmentCancelled: { type: Number, default: 0 },
})

// Data
const chartSeries = [props.appointmentPending, props.appointmentApproved, props.appointmentRejected, props.appointmentCancelled]
const chartOptions = computed(() => {
  return {
    labels: ['Pending', 'Approved', 'Rejected', 'Cancelled'],
    chart: {
      type: 'donut',
      fontFamily: `inherit`,
      foreColor: '#a1aab2',
      toolbar: {
        show: false
      }
    },
    // https://coolors.co/palette/e3f2fd-bbdefb-90caf9-64b5f6-42a5f5-2196f3-1e88e5-1976d2-1565c0-0d47a1
    colors: ['#0D47A1', '#2196F3', '#64B5F6', '#BBDEFB'],
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
</script>