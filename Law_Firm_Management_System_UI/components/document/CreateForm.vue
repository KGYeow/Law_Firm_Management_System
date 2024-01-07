<template>
  <form @submit.prevent="addDocument">
    <v-card-item class="px-8 py-4 text-body-1">
      <v-row>
        <v-col class="pb-0">
          <v-label class="text-caption">Category</v-label>
          <v-select
            :items="categoryList"
            item-title="name"
            item-value="id"
            placeholder="Select category"
            variant="outlined"
            density="compact"
            :error-messages="addDocumentDetails.categoryId.errorMessage"
            v-model="addDocumentDetails.categoryId.value"
            hide-details="auto"
          />
        </v-col>
        <v-col class="pb-0">
          <v-label class="text-caption">Case</v-label>
          <v-autocomplete
            :items="caseList"
            item-title="name"
            item-value="id"
            placeholder="Select case"
            variant="outlined"
            density="compact"
            :error-messages="addDocumentDetails.caseId.errorMessage"
            v-model="addDocumentDetails.caseId.value"
            hide-details="auto"
          />
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <v-label class="text-caption">Document</v-label>
          <v-file-input
            variant="outlined"
            density="compact"
            prepend-icon=""
            clear-icon="mdi-close-circle-outline fs-5"
            :accept="acceptedDocInput"
            :error-messages="addDocumentDetails.attachment.errorMessage"
            v-model:model-value="addDocumentDetails.attachmentInfo"
            @update:model-value="uploadFile"
            messages="Accepted document types: PDF, Word and Excel"
            hide-details="auto"
            show-size
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

// Emit
const emit = defineEmits(['close-modal'])

// Data
const { handleSubmit } = useForm({
  validationSchema: {
    categoryId(value) {
      return value ? true : 'Category is required'
    },
    caseId(value) {
      return value ? true : 'Case is required'
    },
    attachment(value) {
      if (!value)
        return 'Document is required'
      const fileType = getFileType(addDocumentDetails.value.name)
      return fileType ? true : 'The document type must be in PDF, Word, or Excel'
    }
  }
})
const addDocumentDetails = ref({
  name: null,
  attachment: useField('attachment'),
  categoryId: useField('categoryId'),
  caseId: useField('caseId'),
  type: null,
  attachmentInfo: null,
})
const acceptedDocInput = ref(
  `application/pdf,
  .doc,.docx,.xml,
  application/msword,
  application/vnd.openxmlformats-officedocument.wordprocessingml.document,
  application/vnd.ms-excel
`)
const { data: caseList } = await fetchData.$get("/Case")
const { data: categoryList } = await fetchData.$get("/Document/Category")

// Methods
const getFileType = (docName) => {
  const extensions = {
    "pdf": "PDF",
    "doc": "Word",
    "docx": "Word",
    "xls": "Excel",
    "xlsx": "Excel",
  }
  const extensionIndex = docName.lastIndexOf(".")
  const ext = docName.slice(extensionIndex + 1) // Get the extension
  return extensions[ext] || null // Check for extension in map, return null if not found
}
const uploadFile = async() => {
  const file = addDocumentDetails.value.attachmentInfo[0]
  if (file) {
    // Read file as DataURL using a promise-based approach
    const reader = new FileReader()
    reader.readAsDataURL(file)
    try {
      const base64Data = await new Promise((resolve, reject) => {
        reader.onload = () => resolve(reader.result)
        reader.onerror = reject
      })
      addDocumentDetails.value.attachment.value = base64Data.replace(/^.+?;base64,/, '')
      addDocumentDetails.value.name = file.name
      addDocumentDetails.value.type = getFileType(file.name)
    } catch(e) { ElNotification.error({ message: `Error reading file: ${e}` }) }
  }
  else
  {
    addDocumentDetails.value.name == null
    addDocumentDetails.value.type == null
    addDocumentDetails.value.attachment.value = null
  }
}
const addDocument = handleSubmit(async(values) => {
  try {
    const result = await fetchData.$post("/Document", {
      name: addDocumentDetails.value.name,
      categoryId: values.categoryId,
      caseId: values.caseId,
      attachment: values.attachment,
      type: addDocumentDetails.value.type,
    })

    if (!result.error) {
      emit('close-modal', false)
      addDocumentDetails.value.name = null
      addDocumentDetails.value.categoryId.resetField()
      addDocumentDetails.value.caseId.resetField()
      addDocumentDetails.value.attachment.resetField()
      addDocumentDetails.value.type = null
      ElNotification.success({ message: result.message })
      refreshNuxtData()
    }
    else {
      ElNotification.error({ message: result.message })
    }
  } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
})
</script>