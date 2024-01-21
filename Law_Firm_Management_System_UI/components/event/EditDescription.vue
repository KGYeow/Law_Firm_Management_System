<template>
    <form @submit.prevent="editDescription">
      <v-card-item class="px-8 py-4 text-body-1">
        <v-row>
          <v-col>
              <v-label class="text-caption">Event Description</v-label>
              <v-textarea
                variant="outlined"
                density="compact"
                :error-messages="editDescriptionDetails.description.errorMessage"
                v-model="editDescriptionDetails.description.value"
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
    description: String,
  })
  const emit = defineEmits(['close-modal'])
  
  // Data
  const { handleSubmit } = useForm({
    initialValues: {
      description: props.description,
    },
    validationSchema: {
      description(value) {
        return value ? true : 'Event description is required'
      }
    }
  })
  const editDescriptionDetails = ref({
    eventId: props.eventId,
    description: useField('description'),
  })
  
  // Methods
  const editDescription = handleSubmit(async(values) => {
    try {
      const result = await fetchData.$put("/Event/EditDescription", {
        eventId: editDescriptionDetails.value.eventId,
        description: values.description
      })
      
      if (!result.error) {
        emit('close-modal', false)
        editDescriptionDetails.value.eventId = null
        editDescriptionDetails.value.description.resetField()
        ElNotification.success({ message: result.message })
        refreshNuxtData()
      }
      else {
        ElNotification.error({ message: result.message })
      }
    } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
  })
  </script>