<template>
  <v-row>
    <v-col cols="12" md="12">
      <UiParentCard title="Documents">
        <v-row class="px-7">
          <!-- Filters -->
          <v-col class="pe-0" cols="2">
            <v-select
              :items="categoryList"
              item-title="name"
              placeholder="Category"
              density="compact"
              variant="outlined"
              v-model="filter.category"
              hide-details
            >     
              <template v-slot:prepend-item>
                <v-list-item title="All Categories" @click="filter.category = null"/>
              </template>
            </v-select>
          </v-col>
        </v-row>

        <!-- Document List Table -->
        <div class="pa-7 pt-1 text-body-1">
          <v-data-table
            density="comfortable"
            v-model:page="currentPage"
            :headers="headers"
            :items="documentList"
            :items-per-page="itemsPerPage"
          >
            <template v-slot:item="{ item }">
              <tr>
                <td>{{ documentList.indexOf(item) + 1 }}</td>
                <td>{{ item.name }}</td>
                <td>{{ item.category }}</td>
                <td>{{ item.case }}</td>
                <td>{{ dayjs(item.modifiedDate).format("DD MMM YYYY") }}</td>
                <td>{{ item.modifiedBy }}</td>
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
                      @confirm=""
                    >
                      <template #reference>
                        <v-btn icon="mdi-archive-outline" size="small" variant="text"/>
                      </template>
                    </el-popconfirm>
                  </li>
                </td>
              </tr>
            </template>
            <template v-slot:bottom>
              <div class="d-flex justify-content-end pt-2">
                <el-pagination
                  layout="total, prev, pager, next"
                  v-model:current-page="currentPage"
                  :page-size="documentList.length/pageCount()"
                  :total="documentList.length"
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
</template>

<script setup>
import { useField, useForm } from 'vee-validate'
import { FileDescriptionIcon } from "vue-tabler-icons"
import dayjs from 'dayjs'
import UiParentCard from '@/components/shared/UiParentCard.vue';

// Data
const { handleSubmit } = useForm({
  validationSchema: {
    name(value) {
      return value ? true : 'Document name is required'
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
  category: null,
})
const addDocumentModal = ref(false)
const renameDocumentModal = ref(false)
const addDocumentDetails = ref({
  name: useField('name'),
  categoryId: useField('categoryId'),
  caseId: useField('caseId'),
})
const renameDocumentDetails = ref({
  name: useField('name'),
})
const currentPage = ref(1)
const itemsPerPage = ref(10)
const headers = ref([
  { key: "number", title: "No." },
  { key: "name", title: "Name" },
  { key: "category", title: "Category" },
  { key: "case", title: "Case" },
  { key: "modifiedDate", title: "Modified Date" },
  { key: "modifiedBy", title: "Modified By" },
  { key: "actions", sortable: false },
])
const { data: categoryList } = await fetchData.$get("/Document/Category")
const { data: documentList } = await fetchData.$get("/Document", filter.value)

// Head
useHead({
  title: "Documents | CaseCraft",
})

// Page Meta
definePageMeta({
  breadcrumbsIcon: shallowRef(FileDescriptionIcon),
  breadcrumbs: [
    {
      title: 'Documents',
      disabled: false,
    },
  ],
})

// Methods
const pageCount = () => {
  return Math.ceil(documentList.value.length / itemsPerPage.value)
}
const addDocument = handleSubmit(async(values) => {
  try {
    const result = await fetchData.$post("/Document", values)
    if (!result.error) {
      addDocumentModal.value = false
      addDocumentDetails.value.name.resetField()
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