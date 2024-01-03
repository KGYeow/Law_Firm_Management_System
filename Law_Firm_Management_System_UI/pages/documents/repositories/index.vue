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
              <template v-slot:prepend-item>
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
              <template v-slot:prepend-item>
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
              <template v-slot:prepend-item>
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
              <template v-slot:prepend-item>
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
            c
          >
            <template #item="{ item }">
              <tr>
                <td style="max-width: 300px;">
                  <el-text truncated>
                    <v-tooltip :text="item.name" activator="parent" location="top" offset="2"/>
                    {{ item.name }}aaaaaaaaaaaaaaaaaaaaaaaaaaa
                  </el-text>
                </td>
                <td style="max-width: 150px;">
                  <el-text truncated>
                    <v-tooltip :text="item.categoryName" activator="parent" location="top" offset="2"/>
                    {{ item.categoryName }}aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                  </el-text>
                </td>
                <td style="max-width: 150px;">
                  <el-text truncated>
                    <v-tooltip :text="item.caseName" activator="parent" location="top" offset="2" v-if="item.caseName"/>
                    {{ item.caseName ?? '-' }}aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                  </el-text>
                </td>
                <td class="flex">
                  <el-text truncated>
                    <v-tooltip :text="item.modifiedBy" activator="parent" location="top" offset="2"/>
                    {{ item.modifiedBy }}
                  </el-text>
                </td>
                <td style="max-width: 0;">
                  <el-text truncated>
                    <v-tooltip :text="dayjs(item.modifiedDate).format('DD MMM YYYY')" activator="parent" location="top" offset="2"/>
                    {{ dayjs(item.modifiedDate).format("DD MMM YYYY") }}aaaaaaaaaaaa
                  </el-text>
                </td>
                <td class="list-inline hstack">
                  <li>
                    <v-tooltip text="Download" activator="parent" location="top" offset="2"/>
                    <v-btn icon="mdi-download" size="small" variant="text"/>
                  </li>
                  <li>
                    <v-tooltip text="Rename" activator="parent" location="top" offset="2"/>
                    <v-btn icon="mdi-rename-outline" size="small" variant="text"/>
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

  <!-- Add New Document -->
  <el-affix
    class="position-absolute"
    position="bottom"
    :offset="30"
    style="right: 30px; bottom: 100px;"
  >
    <v-btn icon="mdi-file-document-plus-outline" color="primary" size="large" @click="addDocumentModal = true"/>
  </el-affix>

  <!-- Add New Document Modal -->
  <v-dialog v-model="addDocumentModal" width="auto">
    <v-card elevation="10" class="withbg rounded-3 overflow-visible" width="500px">
      <v-card-title class="px-4 py-4 d-sm-flex align-center justify-space-between bg-background rounded-top-3">
        <h5 class="text-h5 mb-0 d-flex align-center">
          Add New Document
        </h5>
        <v-btn density="compact" variant="plain" icon="mdi-close" @click="addDocumentModal = false"/>
      </v-card-title>
      <v-divider class="m-0"/>
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
                :error-messages="addDocumentDetails.attachment.errorMessage"
                v-model="addDocumentDetails.attachment.value"
                hide-details="auto"
              />
            </v-col>
          </v-row>
        </v-card-item>
        <v-card-actions class="p-3 justify-content-end">
          <v-btn color="primary" type="submit">Submit</v-btn>
        </v-card-actions>
      </form>
    </v-card>
  </v-dialog>
</template>

<script setup>
import { useField, useForm } from 'vee-validate'
import { FileDescriptionIcon } from "vue-tabler-icons"
import dayjs from 'dayjs'
import UiParentCard from '@/components/shared/UiParentCard.vue';

// Data
const { handleSubmit } = useForm({
  validationSchema: {
    attachment(value) {
      return value ? true : 'Document is required'
    },
    categoryId(value) {
      return value ? true : 'Category is required'
    },
    caseId(value) {
      return value ? true : 'Case is required'
    }
  }
})
const filter = ref({
  docId: null,
  categoryId: null,
  caseId: null,
  userId: null,
})
const addDocumentModal = ref(false)
const renameDocumentModal = ref(false)
const addDocumentDetails = ref({
  attachment: useField('attachment'),
  categoryId: useField('categoryId'),
  caseId: useField('caseId'),
})
const renameDocumentDetails = ref({
  name: useField('name'),
})
const currentPage = ref(1)
const itemsPerPage = ref(10)
const headers = ref([
  { key: "name", title: "Name" },
  { key: "category", title: "Category" },
  { key: "case", title: "Case" },
  { key: "modifiedBy", title: "Modified By" },
  { key: "modifiedDate", title: "Modified Date" },
  { key: "actions", sortable: false },
])
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
const addDocument = handleSubmit(async(values) => {
  try {
    const result = await fetchData.$post("/Document", values)
    if (!result.error) {
      addDocumentModal.value = false
      addDocumentDetails.value.attachment.resetField()
      addDocumentDetails.value.categoryId.resetField()
      addDocumentDetails.value.caseId.resetField()
      ElNotification.success({ message: result.message })
      refreshNuxtData()
    }
    else {
      ElNotification.error({ message: result.message })
    }
  } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
})
const renameDocument = handleSubmit(async(values) => {
  try {
    const result = await fetchData.$put("/Document/Rename", values)
    if (!result.error) {
      renameDocumentModal.value = false
      renameDocumentDetails.value.name.resetField()
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
</script>