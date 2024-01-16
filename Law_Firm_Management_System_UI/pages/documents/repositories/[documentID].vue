<template>
  <v-row>
    <v-col cols="12" md="12">
      <v-card elevation="10" class="withbg">
        <div class="hstack justify-content-center align-items-stretch">
          <div class="p-4 w-100">
            <v-card-title class="text-h6 text-body-tertiary">
              Document Information
            </v-card-title>
            <form @submit.prevent="editDocument">
              <v-card-item>
                <v-row class="pb-2">
                  <v-col>
                    <v-label class="text-h6 pb-3">Name</v-label>
                    <v-card-subtitle>{{ docInfo.name }}</v-card-subtitle>
                  </v-col>
                </v-row>
                <v-row class="pb-2">
                  <v-col>
                    <v-label class="text-h6 pb-3">Category</v-label>
                    <v-card-subtitle v-if="!isEdit">{{ docInfo.categoryName }}</v-card-subtitle>
                    <v-select
                      :items="categoryList"
                      item-title="name"
                      item-value="id"
                      placeholder="Select category"
                      variant="outlined"
                      density="compact"
                      :error-messages="editDocumentDetails.categoryId.errorMessage"
                      v-model="editDocumentDetails.categoryId.value"
                      hide-details="auto"
                      v-else
                    />
                  </v-col>
                  <v-col>
                    <v-label class="text-h6 pb-3">Related Case</v-label>
                    <v-card-subtitle v-if="!isEdit">{{ docInfo.caseName }}</v-card-subtitle>
                    <v-select
                      :items="caseList"
                      item-title="name"
                      item-value="id"
                      placeholder="Select case"
                      variant="outlined"
                      density="compact"
                      :error-messages="editDocumentDetails.caseId.errorMessage"
                      v-model="editDocumentDetails.caseId.value"
                      hide-details="auto"
                      v-else
                    />
                  </v-col>
                  <v-col>
                    <v-label class="text-h6 pb-3">Attachment</v-label>
                    <v-card-subtitle v-if="!isEdit">{{ docInfo.attachment }}</v-card-subtitle>
                    <v-file-input
                      variant="outlined"
                      density="compact"
                      prepend-icon=""
                      clear-icon="mdi-close-circle-outline fs-5"
                      :accept="acceptedDocInput"
                      :error-messages="editDocumentDetails.attachment.errorMessage"
                      v-model:model-value="editDocumentDetails.attachmentInfo"
                      @update:model-value="uploadFile"
                      messages="Accepted document types: PDF, Word and Excel"
                      hide-details="auto"
                      show-size
                      v-else
                    />
                  </v-col>
                </v-row>
                <v-row>
                  <v-col>
                    <v-label class="text-h6 pb-3">Attachment</v-label>
                    <v-card-subtitle v-if="!isEdit">{{ docInfo.attachment ?? '-' }}</v-card-subtitle>
                    <v-text-field
                      variant="outlined"
                      density="compact"
                      :error-messages="editDocumentDetails.attachment.errorMessage"
                      v-model="editDocumentDetails.attachment.value"
                      hide-details="auto"
                      v-else
                    />
                  </v-col>
                </v-row>
              </v-card-item>
              <v-card-actions class="justify-content-end p-3">
                <div v-if="!isEdit">
                  <v-btn color="primary" variant="tonal" flat @click="isEdit = true">Edit</v-btn>
                </div>
                <div v-else>
                  <v-btn color="primary" variant="outlined" @click="editDocumentCancel">Cancel</v-btn>
                  <v-btn color="primary" variant="tonal" class="ms-2" flat type="submit">Save Details</v-btn>
                </div>
              </v-card-actions>
            </form>
          </div>
        </div> 
      </v-card>
    </v-col>
  </v-row>
</template>

<script setup>
import { FileDescriptionIcon } from "vue-tabler-icons"
import { useField, useForm } from 'vee-validate'

// Data
const isEdit = ref(false)
const routeParameter = ref(useRoute().params)
const { data: docInfo } = await fetchData.$get(`/Document/Info/${routeParameter.value.documentID}`)
const { data: attachment } = await fetchData.$get(`/Document/GetAttachment/${routeParameter.value.documentID}`)
const { data: caseList } = await fetchData.$get("/Case")
const { data: categoryList } = await fetchData.$get("/Document/Category")
const { handleSubmit } = useForm({
  initialValues: {
    categoryId: docInfo.value.categoryId,
    caseId: docInfo.value.caseId,
    attachment: attachment.value
  },
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
      const fileType = getFileType(editDocumentDetails.value.name)
      return fileType ? true : 'The document type must be in PDF, Word, or Excel'
    },
  }
})
const editDocumentDetails = ref({
  docId: null,
  name: null,
  categoryId: useField('categoryId'),
  caseId: useField('caseId'),
  attachment: useField('attachment'),
  type: null,
  attachmentInfo: null,
})

// Head
useHead({
  title: 'Client Details | CaseCraft',
})

// Page Meta
definePageMeta({
  breadcrumbsIcon: shallowRef(FileDescriptionIcon),
  breadcrumbs: [
  {
      title: 'Documents',
      disabled: false,
    },
    {
      title: 'Repositories',
      disabled: false,
      href: '/documents/repositories',
    },
    {
      title: 'Document Details',
      disabled: false,
    },
  ],
})

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
const editDocumentCancel = () => {
  isEdit.value = false
  editDocumentDetails.value.categoryId.resetField()
  editDocumentDetails.value.caseId.resetField()
  editDocumentDetails.value.attachment.resetField()
}
const editDocument = handleSubmit(async(values) => {
  try {
    const result = await fetchData.$put("/Document", {
    })

    if (!result.error) {
      isEdit.value = false
      editDocumentDetails.value.categoryId.setValue(values.categoryId)
      editDocumentDetails.value.caseId.setValue(values.caseId)
      editDocumentDetails.value.attachment.setValue(values.attachment)
      ElNotification.success({ message: result.message })
      refreshNuxtData()
    }
    else {
      ElNotification.error({ message: result.message })
    }
  } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
})
const archiveDocument = async(docId) => {
  try {
    const result = await fetchData.$put(`/Document/Archive/${docId}`)
    if (!result.error) {
      ElNotification.success({ message: result.message })
      refreshNuxtData()
    }
    else {
      ElNotification.error({ message: result.message })
    }
  } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
}
const downloadDocument = () => {
  const mimeType = {
    "PDF": "application/pdf",
    "Word": "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
    "Excel": "application/vnd.ms-excel",
  }
  const arrayBuffer = Buffer.from(attachment.value, 'base64');
  const blob = new Blob([arrayBuffer], { type: mimeType[docInfo.value.type] })
  const url = URL.createObjectURL(blob)

  if (docInfo.value.type == 'PDF')
    window.open(url, '_blank')
  else {
    const link = document.createElement('a')
    link.href = url
    link.download = docInfo.value.name
    link.click()
    document.body.removeChild(link)
  }
}
const uploadFile = async() => {
  const file = editDocumentDetails.value.attachmentInfo[0]
  if (file) {
    // Read file as DataURL using a promise-based approach
    const reader = new FileReader()
    reader.readAsDataURL(file)
    try {
      const base64Data = await new Promise((resolve, reject) => {
        reader.onload = () => resolve(reader.result)
        reader.onerror = reject
      })
      editDocumentDetails.value.attachment.value = base64Data.replace(/^.+?;base64,/, '')
      editDocumentDetails.value.name = file.name
      editDocumentDetails.value.type = getFileType(file.name)
    } catch(e) { ElNotification.error({ message: `Error reading file: ${e}` }) }
  }
  else
  {
    editDocumentDetails.value.name == null
    editDocumentDetails.value.type == null
    editDocumentDetails.value.attachment.value = null
  }
}
</script>