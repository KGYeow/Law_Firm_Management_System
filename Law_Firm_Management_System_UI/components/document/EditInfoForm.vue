<template>
  <form @submit.prevent="editDocInfo">
    <v-card-item class="px-8 py-4 text-body-1">
      <v-row>
        <v-col>
          <v-label class="text-caption">Document Name</v-label>
          <v-text-field
            variant="outlined"
            density="compact"
            :suffix="editDocumentInfoDetails.extension"
            :error-messages="editDocumentInfoDetails.nameWithoutExt.errorMessage"
            v-model="editDocumentInfoDetails.nameWithoutExt.value"
            hide-details="auto"
          />
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <v-label class="text-caption">Category</v-label>
          <v-select
            :items="docCategoryList"
            item-title="name"
            item-value="id"
            placeholder="Select category"
            variant="outlined"
            density="compact"
            v-model="editDocumentInfoDetails.categoryId"
            hide-details
          />
        </v-col>
        <v-col>
          <v-label class="text-caption">Case</v-label>
          <v-autocomplete
            :items="caseList"
            item-title="name"
            item-value="id"
            placeholder="Select case"
            variant="outlined"
            density="compact"
            v-model="editDocumentInfoDetails.caseId"
            hide-details
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
  editDocumentInfo: Object,
  docCategoryList: Array,
  caseList: Array,
})
const emit = defineEmits(['close-modal'])

// Data
const { handleSubmit } = useForm({
  initialValues: {
    nameWithoutExt: props.editDocumentInfo.name.slice(0, props.editDocumentInfo.name.lastIndexOf(".")),
  },
  validationSchema: {
    nameWithoutExt(value) {
      return value ? true : 'Document name is required'
    }
  }
})
const editDocumentInfoDetails = ref({
  nameWithoutExt: useField('nameWithoutExt'),
  categoryId: props.editDocumentInfo.categoryId,
  caseId: props.editDocumentInfo.caseId,
  extension: props.editDocumentInfo.name.slice(props.editDocumentInfo.name.lastIndexOf(".")) ,
})

// Methods
const editDocInfo = handleSubmit(async(values) => {
  try {
    const result = await fetchData.$put("/Document/Edit/Info", {
      docId: props.editDocumentInfo.docId,
      name: `${values.nameWithoutExt}${editDocumentInfoDetails.value.extension}`,
      categoryId: editDocumentInfoDetails.value.categoryId,
      caseId: editDocumentInfoDetails.value.caseId,
    })
    
    if (!result.error) {
      emit('close-modal', false)
      editDocumentInfoDetails.value.nameWithoutExt.resetField()
      editDocumentInfoDetails.value.categoryId = null
      editDocumentInfoDetails.value.caseId = null
      editDocumentInfoDetails.value.extension = null
      ElNotification.success({ message: result.message })
      refreshNuxtData()
    }
    else {
      ElNotification.error({ message: result.message })
    }
  } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
})
</script>