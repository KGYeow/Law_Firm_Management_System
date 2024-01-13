<template>
    <form @submit.prevent="renameEvent">
      <v-card-item class="px-8 py-4 text-body-1">
        <v-row>
          <v-col>
            <v-label class="text-caption">Event Name</v-label>
            <v-text-field
              variant="outlined"
              density="compact"
              :error-messages="renameEventDetails.name.errorMessage"
              v-model="renameEventDetails.name.value"
              hide-details="auto"
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
    eventName: String,
  })
  const emit = defineEmits(['close-modal'])
  
  // Data
  const { handleSubmit } = useForm({
    initialValues: {
      name: props.eventName,
    },
    validationSchema: {
      name(value) {
        return value ? true : 'Event name is required'
      }
    }
  })
  const renameEventDetails = ref({
    eventId: props.eventId,
    name: useField('name'),
  })
  
  // Methods
  const renameEvent = handleSubmit(async(values) => {
    try {
      const result = await fetchData.$put("/Event/Rename", {
        eventId: renameEventDetails.value.eventId,
        name: values.name
      })
      
      if (!result.error) {
        emit('close-modal', false)
        renameEventDetails.value.eventId = null
        renameEventDetails.value.name.resetField()
        ElNotification.success({ message: result.message })
        refreshNuxtData()
      }
      else {
        ElNotification.error({ message: result.message })
      }
    } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
  })
  </script>