<template>
  <v-row>
    <v-col cols="12" md="12">
      <v-card elevation="10" class="withbg">
        <div class="hstack justify-content-center align-items-stretch">
          <div class="p-4 w-100">
            <v-card-title class="text-h6 text-body-tertiary">
              Document Information
            </v-card-title>
            <form @submit.prevent="editDocInfo">
              <v-card-item>
                <v-row class="pb-2">
                  <v-col>
                    <v-label class="text-h6 pb-3">Name</v-label>
                    <v-card-subtitle class="text-wrap" v-if="!isEdit">{{ docInfo.name }}</v-card-subtitle>
                    <v-text-field
                      variant="outlined"
                      density="compact"
                      :suffix="editDocumentInfoDetails.extension"
                      :error-messages="editDocumentInfoDetails.nameWithoutExt.errorMessage"
                      v-model="editDocumentInfoDetails.nameWithoutExt.value"
                      hide-details="auto"
                      v-else
                    />
                  </v-col>
                </v-row>
                <v-row class="pb-2">
                  <v-col cols="4">
                    <v-label class="text-h6 pb-3">Category</v-label>
                    <v-card-subtitle class="text-wrap" v-if="!isEdit">{{ docInfo.categoryName }}</v-card-subtitle>
                    <v-select
                      :items="categoryList"
                      item-title="name"
                      item-value="id"
                      placeholder="Select category"
                      variant="outlined"
                      density="compact"
                      v-model="editDocumentInfoDetails.categoryId"
                      hide-details="auto"
                      v-else
                    />
                  </v-col>
                  <v-col cols="4">
                    <v-label class="text-h6 pb-3">Related Case</v-label>
                    <v-card-subtitle class="text-wrap" v-if="!isEdit">{{ docInfo.caseName ?? '-' }}</v-card-subtitle>
                    <v-select
                      :items="caseList"
                      item-title="name"
                      item-value="id"
                      placeholder="Select case"
                      variant="outlined"
                      density="compact"
                      v-model="editDocumentInfoDetails.caseId"
                      hide-details="auto"
                      v-else
                    />
                  </v-col>
                </v-row>
                <v-row>
                  <v-col cols="4">
                    <v-label class="text-h6 pb-3">Created Time</v-label>
                    <v-card-subtitle class="text-wrap">{{ dayjs(docInfo.createdTime).format("DD MMM YYYY, h:mm A") }}</v-card-subtitle>
                  </v-col>
                  <v-col cols="4">
                    <v-label class="text-h6 pb-3">Latest Modified Time</v-label>
                    <v-card-subtitle class="text-wrap">{{ dayjs(docInfo.modifiedTime).format("DD MMM YYYY, h:mm A") }}</v-card-subtitle>
                  </v-col>
                  <v-col cols="4">
                    <v-label class="text-h6 pb-3">Modified By</v-label>
                    <v-card-subtitle class="text-wrap">{{ docInfo.modifiedBy }}</v-card-subtitle>
                  </v-col>
                </v-row>
              </v-card-item>
              <v-card-actions class="justify-content-end p-3">
                <div v-if="!isEdit">
                  <v-btn color="primary" variant="tonal" flat @click="downloadDocument">Download</v-btn>
                  <v-btn color="primary" variant="tonal" flat @click="editAttachmentModal = true">Update</v-btn>
                  <v-btn color="primary" variant="tonal" flat @click="isEdit = true">Edit</v-btn>
                </div>
                <div v-else>
                  <v-btn color="primary" variant="outlined" @click="editDocumentCancel">Cancel</v-btn>
                  <v-btn color="primary" variant="tonal" class="ms-2" flat type="submit">Save</v-btn>
                </div>
              </v-card-actions>
            </form>
          </div>
        </div>
      </v-card>
    </v-col>
  </v-row>

  <!-- Edit Document Attachment Modal -->
  <SharedUiModal v-model="editAttachmentModal" title="Update Document File" width="500">
    <DocumentEditAttachmentForm
      :document-id="docInfo.id"
      @close-modal="(e) => editAttachmentModal = e"
    />
  </SharedUiModal>
</template>

<script setup>
import { FileDescriptionIcon } from "vue-tabler-icons"
import { useField, useForm } from 'vee-validate'
import { Buffer } from 'buffer'
import dayjs from 'dayjs'

// Data
const isEdit = ref(false)
const editAttachmentModal = ref(false)
const routeParameter = ref(useRoute().params)
const { data: docInfo } = await fetchData.$get(`/Document/Info/${routeParameter.value.documentID}`)
const { data: caseList } = await fetchData.$get("/Case")
const { data: categoryList } = await fetchData.$get("/Document/Category")
const { handleSubmit } = useForm({
  initialValues: {
    nameWithoutExt: docInfo.value.name.slice(0, docInfo.value.name.lastIndexOf(".")),
  },
  validationSchema: {
    nameWithoutExt(value) {
      return value ? true : 'Document name is required'
    }
  }
})
const editDocumentInfoDetails = ref({
  nameWithoutExt: useField('nameWithoutExt'),
  categoryId: docInfo.value.categoryId,
  caseId: docInfo.value.caseId,
  extension: docInfo.value.name.slice(docInfo.value.name.lastIndexOf(".")),
})

// Head
useHead({
  title: 'Document Details | CaseCraft',
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
const editDocumentCancel = () => {
  isEdit.value = false
  editDocumentInfoDetails.value.nameWithoutExt.resetField()
  editDocumentInfoDetails.value.categoryId = docInfo.value.categoryId
  editDocumentInfoDetails.value.caseId = docInfo.value.caseId
}
const editDocInfo = handleSubmit(async(values, { resetForm }) => {
  try {
    const result = await fetchData.$put("/Document/Edit/Info", {
      docId: docInfo.value.id,
      name: `${values.nameWithoutExt}${editDocumentInfoDetails.value.extension}`,
      categoryId: editDocumentInfoDetails.value.categoryId,
      caseId: editDocumentInfoDetails.value.caseId,
    })
    
    if (!result.error) {
      isEdit.value = false
      resetForm({ values: values })
      ElNotification.success({ message: result.message })
      refreshNuxtData()
    }
    else {
      ElNotification.error({ message: result.message })
    }
  } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
})
const downloadDocument = async() => {
  const { data: attachment } = await fetchData.$get(`/Document/GetAttachment/${routeParameter.value.documentID}`)
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
</script>