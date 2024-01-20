<template>
    <form @submit.prevent="editTime">
      <v-card-item class="px-8 py-4 text-body-1">
        <v-row>
          <v-col>
              <v-label class="text-caption">Time</v-label>
              <el-date-picker
                :class="{ 'error': editTimeDetails.eventTime.errorMessage }"
                placeholder="Select date and time"
                type="datetime"
                format="DD MMM YYYY, hh:mm A"
                date-format="DD MMM YYYY"
                time-format="HH:mm"
                :teleported="false"
                :disabled-date="(time) => { return time.getTime() < new Date() }"
                v-model="editTimeDetails.eventTime.value"
                style="height: 40px;"
              />
            </v-col>
          </v-row>
      </v-card-item>
      <v-card-actions class="p-3 justify-content-end">
        <v-btn color="primary" type="submit">Submit</v-btn>
      </v-card-actions>
    </form>
  </template>
  
  <script setup>
  import { useField, useForm } from 'vee-validate'

  // Properties, Emit & Model
  const props = defineProps({
    eventId: Number,
    eventTime: Date,
  })
  const emit = defineEmits(['close-modal'])
  
  // Data
  const { handleSubmit } = useForm({
    initialValues: {
      eventTime: props.eventTime,
    },
    validationSchema: {
      eventTime(value) {
        return value ? true : 'Event time selection is required'
      }
    }
  })
  const editTimeDetails = ref({
    eventId: props.eventId,
    eventTime: useField('eventTime'),
  })
  
  // Methods
  const editTime = handleSubmit(async(values) => {
    try {
      const result = await fetchData.$put("/Event/EditTime", {
        eventId: editTimeDetails.value.eventId,
        eventTime: values.eventTime
      })
      
      if (!result.error) {
        emit('close-modal', false)
        editTimeDetails.value.eventId = null
        editTimeDetails.value.eventTime.resetField()
        ElNotification.success({ message: result.message })
        refreshNuxtData()
      }
      else {
        ElNotification.error({ message: result.message })
      }
    } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
  })
  </script>