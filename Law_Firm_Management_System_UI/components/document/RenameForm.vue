<template>
  <form @submit.prevent="renameDocument">
    <v-card-item class="px-8 py-4 text-body-1">
      <v-row>
        <v-col>
          <v-label class="text-caption">Document Name</v-label>
          <v-text-field
            variant="outlined"
            density="compact"
            :error-messages="renameDocumentDetails.name.errorMessage"
            v-model="renameDocumentDetails.name.value"
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
  docId: Number,
  docName: String,
})
const emit = defineEmits(['close-modal'])

// Data
const { handleSubmit } = useForm({
  initialValues: {
    name: props.docName,
  },
  validationSchema: {
    name(value) {
      return value ? true : 'Document name is required'
    }
  }
})
const renameDocumentDetails = ref({
  docId: props.docId,
  name: useField('name'),
})

// Methods
const renameDocument = handleSubmit(async(values) => {
  try {
    const result = await fetchData.$put("/Document/Rename", {
      docId: renameDocumentDetails.value.docId,
      name: values.name
    })
    
    if (!result.error) {
      emit('close-modal', false)
      renameDocumentDetails.value.docId = null
      renameDocumentDetails.value.name.resetField()
      ElNotification.success({ message: result.message })
      refreshNuxtData()
    }
    else {
      ElNotification.error({ message: result.message })
    }
  } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
})
</script>