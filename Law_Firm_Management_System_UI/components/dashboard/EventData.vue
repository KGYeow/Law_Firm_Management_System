<template>
  <v-card elevation="10" class="withbg h-100">
    <v-card-item>
      <div class="d-flex align-center justify-space-between pt-sm-2 pb-2">
        <v-card-title class="text-h6 d-flex align-center">
          <v-avatar class="me-2" color="rgb(243, 244, 248)" size="small">
            <CalendarIcon color="gray" size="18"/>
          </v-avatar>
          Events
        </v-card-title>
      </div>
      <v-row>
        <v-col cols="12" sm="12">
          <h3 class="text-h3">{{ eventTotal }}</h3>
          <div class="d-flex align-center flex-shrink-0">
            <apexchart type="donut" height="145" :options="chartOptions" :series="chartSeries"/>
          </div>
        </v-col>
        <v-col cols="12+" sm="12">
          <div class="d-flex flex-column">
            <h6 class="text-subtitle-1 text-muted">
              <v-icon icon="mdi mdi-checkbox-blank-circle" class="mr-1" size="10" color="#0D47A1"></v-icon> Completed
            </h6>
            <h6 class="text-subtitle-1 text-muted">
              <v-icon icon="mdi mdi-checkbox-blank-circle" class="mr-1" size="10" color="#64B5F6"></v-icon> Incompleted
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
  eventTotal: { type: Number, default: 0 },
  eventCompleted: { type: Number, default: 0 },
  eventIncompleted: { type: Number, default: 0 },
})

// Data
const chartSeries = [props.eventCompleted, props.eventIncompleted]
const chartOptions = computed(() => {
  return {
    labels: ['Complete', 'Incomplete'],
    chart: {
      type: 'donut',
      fontFamily: `inherit`,
      foreColor: '#a1aab2',
      toolbar: {
        show: false
      }
    },
    colors: ['#0D47A1', '#64B5F6'],
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