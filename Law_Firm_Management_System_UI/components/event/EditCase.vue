<template>
    <form @submit.prevent="editCase">
      <v-card-item class="px-8 py-4 text-body-1">
        <v-row>
            <v-col >
              <v-label class="text-caption">Select Case</v-label>
              <v-select
                :items="caseList"
                item-title="name"
                item-value="id"
                placeholder="Select case"
                variant="outlined"
                density="compact"
                :error-messages="editCaseDetails.caseId.errorMessage"
                v-model="editCaseDetails.caseId.value"
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
  
  const { data: caseList } = await fetchData.$get("/Case")

  // Properties, Emit & Model
  const props = defineProps({
    eventId: Number,
    caseId: Number,
  })
  const emit = defineEmits(['close-modal'])
  
  // Data
  const { handleSubmit } = useForm({
    initialValues: {
      caseId: props.caseId,
    },
    validationSchema: {
      caseId(value) {
        return value ? true : 'Case selection is required'
      }
    }
  })
  const editCaseDetails = ref({
    eventId: props.eventId,
    caseId: useField('caseId'),
  })
  
  // Methods
  const editCase = handleSubmit(async(values) => {
    try {
      const result = await fetchData.$put("/Event/EditCase", {
        eventId: editCaseDetails.value.eventId,
        caseId: values.caseId
      })
      
      if (!result.error) {
        emit('close-modal', false)
        editCaseDetails.value.eventId = null
        editCaseDetails.value.caseId.resetField()
        ElNotification.success({ message: result.message })
        refreshNuxtData()
      }
      else {
        ElNotification.error({ message: result.message })
      }
    } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
  })
  </script>