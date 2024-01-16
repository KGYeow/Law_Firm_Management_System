<template>
  <v-row>
    <v-col cols="12" md="12">
      <UiParentCard title="Repositories">
        <v-row class="px-7">
          <!-- Filters -->
          <v-col class="pe-0" cols="3">
            <v-autocomplete
              :items="documentList"
              item-title="name"
              item-value="id"
              placeholder="Documents"
              density="compact"
              variant="outlined"
              v-model="filter.docId"
              hide-details
            />     
          </v-col>
          <v-col class="pe-0" cols="3">
            <v-autocomplete
              :items="caseList"
              item-title="name"
              item-value="id"
              placeholder="Cases"
              density="compact"
              variant="outlined"
              v-model="filter.caseId"
              hide-details
            />
          </v-col>
          <v-col class="pe-0" cols="2">
            <v-select
              :items="categoryList"
              item-title="name"
              item-value="id"
              placeholder="Categories"
              density="compact"
              variant="outlined"
              v-model="filter.categoryId"
              hide-details
            >     
              <template #prepend-item>
                <v-list-item title="All Categories" @click="filter.categoryId = null"/>
              </template>
            </v-select>
          </v-col>
        </v-row>

        <!-- Document List Table -->
        <div class="pa-7 pt-3 text-body-1">
          <v-data-table
            density="comfortable"
            v-model:page="currentPage"
            :headers="headers"
            :items="documentFilterList"
            :items-per-page="itemsPerPage"
            hover
          >
            <template #item="{ item }">
              <tr>
                <td>
                  <a :href="`/documents/repositories/${item.id}`" target="_blank" class="row-link">{{ item.name }}</a>
                </td>
                <td>{{ item.categoryName }}</td>
                <td>{{ item.caseName ?? '-' }}</td>
                <td>{{ item.modifiedBy }}</td>
                <td>{{ dayjs(item.modifiedTime).format("DD MMM YYYY") }}</td>
                <td>
                  <ul class="m-0 list-inline hstack">
                    <li>
                      <v-tooltip text="Download" activator="parent" location="top" offset="2"/>
                      <v-btn icon="mdi-download-outline" size="small" variant="text" @click="downloadDocument(item.id, item.name, item.type)"/>
                    </li>
                    <li>
                      <v-tooltip text="Update" activator="parent" location="top" offset="2"/>
                      <v-btn icon="mdi-upload-outline" size="small" variant="text" @click="getEditAttachmentInfo(item.id)"/>
                    </li>
                    <li>
                      <v-tooltip text="Edit Info" activator="parent" location="top" offset="2"/>
                      <v-btn icon="mdi-file-edit-outline" size="small" variant="text" @click="getEditDocumentInfo(item.id, item.name, item.categoryId, item.caseId)"/>
                    </li>
                    <li>
                      <v-tooltip text="Archive" activator="parent" location="top" offset="2"/>
                      <el-popconfirm
                        title="Are you sure to archive this document?"
                        icon-color="orange"
                        width="190"
                        @confirm="archiveDocument(item.id)"
                      >
                        <template #reference>
                          <v-btn icon="mdi-archive-outline" size="small" variant="text"/>
                        </template>
                      </el-popconfirm>
                    </li>
                    <li>
                      <v-tooltip text="View Details" activator="parent" location="top" offset="2"/>
                      <v-btn icon="mdi-open-in-new" size="small" variant="text" :href="`/documents/repositories/${item.id}`"/>
                    </li>
                  </ul>
                </td>
              </tr>
            </template>
            <template #bottom>
              <div class="d-flex justify-content-end pt-2">
                <el-pagination
                  layout="total, prev, pager, next"
                  v-model:current-page="currentPage"
                  :page-size="documentFilterList.length/pageCount()"
                  :total="documentFilterList.length"
                />
              </div>
            </template>
          </v-data-table>
        </div>
      </UiParentCard>
    </v-col>
  </v-row>

  <!-- Add New Document Button -->
  <el-affix
    class="position-absolute"
    position="bottom"
    :offset="30"
    style="right: 30px; bottom: 100px;"
  >
    <v-tooltip text="Upload Document" activator="parent" location="left" offset="2"/>
    <v-btn icon="mdi-file-document-plus-outline" color="primary" size="large" @click="addDocumentModal = true"/>
  </el-affix>

  <!-- Add New Document Modal -->
  <SharedUiModal v-model="addDocumentModal" title="Add New Document" width="500">
    <DocumentCreateForm @close-modal="(e) => addDocumentModal = e"/>
  </SharedUiModal>

  <!-- Edit Document Information Modal -->
  <SharedUiModal v-model="editDocumentInfoModal" title="Edit Document Information" width="500">
    <DocumentEditInfoForm
      :edit-document-info="editDocumentInfoDetails"
      :doc-category-list="categoryList"
      :case-list="caseList"
      @close-modal="(e) => editDocumentInfoModal = e"
    />
  </SharedUiModal>

  <!-- Edit Document Attachment Modal -->
  <SharedUiModal v-model="editAttachmentModal" title="Update Document File" width="500">
    <DocumentEditAttachmentForm
      :document-id="editAttachmentInfoId"
      @close-modal="(e) => editAttachmentModal = e"
    />
  </SharedUiModal>
</template>

<script setup>
import { FileDescriptionIcon } from "vue-tabler-icons"
import { Buffer } from 'buffer'
import dayjs from 'dayjs'
import UiParentCard from '@/components/shared/UiParentCard.vue'

// Data
const currentPage = ref(1)
const itemsPerPage = ref(10)
const headers = ref([
  { key: "name", title: "Name" },
  { key: "category", title: "Category" },
  { key: "case", title: "Case" },
  { key: "modifiedBy", title: "Modified By" },
  { key: "modifiedTime", title: "Modified Time" },
  { key: "actions", sortable: false, width: 0 },
])
const filter = ref({
  docId: null,
  categoryId: null,
  caseId: null,
})
const editDocumentInfoDetails = ref({
  docId: null,
  name: null,
  categoryId: null,
  caseId: null,
})
const editAttachmentInfoId = ref(null)
const addDocumentModal = ref(false)
const editDocumentInfoModal = ref(false)
const editAttachmentModal = ref(false)
const { data: caseList } = await fetchData.$get("/Case")
const { data: categoryList } = await fetchData.$get("/Document/Category")
const { data: documentList } = await fetchData.$get("/Document")
const { data: documentFilterList } = await fetchData.$get("/Document/Filter", filter.value)

// Head
useHead({
  title: "Repositories | CaseCraft",
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
    },
  ],
})

// Methods
const pageCount = () => {
  return Math.ceil(documentFilterList.value.length / itemsPerPage.value)
}
const getEditDocumentInfo = (docId, name, categoryId, caseId) => {
  editDocumentInfoDetails.value.docId = docId
  editDocumentInfoDetails.value.name = name
  editDocumentInfoDetails.value.categoryId = categoryId
  editDocumentInfoDetails.value.caseId = caseId
  editDocumentInfoModal.value = true
}
const getEditAttachmentInfo = (docId) => {
  editAttachmentInfoId.value = docId
  editAttachmentModal.value = true
}
const downloadDocument = async(docId, docName, type) => {
  const { data: attachment } = await fetchData.$get(`/Document/GetAttachment/${docId}`)
  const mimeType = {
    "PDF": "application/pdf",
    "Word": "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
    "Excel": "application/vnd.ms-excel",
  }
  const arrayBuffer = Buffer.from(attachment.value, 'base64');
  const blob = new Blob([arrayBuffer], { type: mimeType[type] })
  const url = URL.createObjectURL(blob)

  if (type == 'PDF')
    window.open(url, '_blank')
  else {
    const link = document.createElement('a')
    link.href = url
    link.download = docName
    link.click()
    document.body.removeChild(link)
  }
}
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
</script>