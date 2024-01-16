<template>
  <v-row>
    <v-col cols="12" md="12">
      <UiParentCard title="Repositories">
        <v-row class="px-7">
          <!-- Filters -->
          <v-col class="pe-0" cols="2">
            <v-select
              :items="documentList"
              item-title="name"
              item-value="id"
              placeholder="Documents"
              density="compact"
              variant="outlined"
              v-model="filter.docId"
              hide-details
            >     
              <template #prepend-item>
                <v-list-item title="All Documents" @click="filter.docId = null"/>
              </template>
            </v-select>
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
          <v-col class="pe-0" cols="2">
            <v-select
              :items="caseList"
              item-title="name"
              item-value="id"
              placeholder="Cases"
              density="compact"
              variant="outlined"
              v-model="filter.caseId"
              hide-details
            >     
              <template #prepend-item>
                <v-list-item title="All Cases" @click="filter.caseId = null"/>
              </template>
            </v-select>
          </v-col>
          <v-col class="pe-0" cols="2">
            <v-select
              :items="partnerList"
              item-title="fullName"
              item-value="userId"
              placeholder="Modified By"
              density="compact"
              variant="outlined"
              v-model="filter.userId"
              hide-details
            >     
              <template #prepend-item>
                <v-list-item title="All Partners" @click="filter.userId = null"/>
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
                <td class="row-link">{{ item.name }}</td>
                <td>{{ item.categoryName }}</td>
                <td>{{ item.caseName ?? '-' }}</td>
                <td>{{ item.modifiedBy }}</td>
                <td>{{ dayjs(item.modifiedDate).format("DD MMM YYYY") }}</td>
                <td>
                  <ul class="m-0 list-inline hstack">
                    <li>
                      <v-tooltip text="Download" activator="parent" location="top" offset="2"/>
                      <v-btn icon="mdi-download" size="small" variant="text" @click="downloadDocument(item.id, item.name, item.type)"/>
                    </li>
                    <li>
                      <v-tooltip text="Rename" activator="parent" location="top" offset="2"/>
                      <v-btn icon="mdi-rename-outline" size="small" variant="text" @click="renameDocumentGet(item.id, item.name)"/>
                    </li>
                    <li>
                      <v-tooltip text="Update" activator="parent" location="top" offset="2"/>
                      <v-btn icon="mdi-update" size="small" variant="text"/>
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

  <!-- Rename Document Modal -->
  <SharedUiModal v-model="renameDocumentModal" title="Rename Document" width="500">
    <DocumentRenameForm
      :docId="renameDocumentDetails.docId"
      :docName="renameDocumentDetails.name"
      @close-modal="(e) => renameDocumentModal = e"
    />
  </SharedUiModal>
</template>

<script setup>
import { FileDescriptionIcon } from "vue-tabler-icons"
import dayjs from 'dayjs'
import UiParentCard from '@/components/shared/UiParentCard.vue'
import { Buffer } from 'buffer'

// Data
const filter = ref({
  docId: null,
  categoryId: null,
  caseId: null,
  userId: null,
})
const renameDocumentDetails = ref({
  docId: null,
  name: null,
})
const currentPage = ref(1)
const itemsPerPage = ref(10)
const headers = ref([
  { key: "name", title: "Name" },
  { key: "category", title: "Category" },
  { key: "case", title: "Case" },
  { key: "modifiedBy", title: "Modified By" },
  { key: "modifiedDate", title: "Modified Date" },
  { key: "actions", sortable: false, width: 0 },
])
const addDocumentModal = ref(false)
const renameDocumentModal = ref(false)
const { data: caseList } = await fetchData.$get("/Case")
const { data: categoryList } = await fetchData.$get("/Document/Category")
const { data: partnerList } = await fetchData.$get("/Partner")
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
const renameDocumentGet = (docId, docName) => {
  renameDocumentDetails.value.docId = docId
  renameDocumentDetails.value.name = docName
  renameDocumentModal.value = true
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