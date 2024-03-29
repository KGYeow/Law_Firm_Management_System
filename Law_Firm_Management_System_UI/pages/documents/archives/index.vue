<template>
  <v-row>
    <v-col cols="12" md="12">
      <UiParentCard title="Archives">
        <v-row class="px-7">
          <!-- Filters -->
          <v-col class="pe-0" cols="3">
            <v-autocomplete
              :items="archiveList"
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

        <!-- Archive List Table -->
        <div class="pa-7 pt-3 text-body-1">
          <v-data-table
            density="comfortable"
            v-model:page="currentPage"
            :headers="headers"
            :items="archiveFilterList"
            :items-per-page="itemsPerPage"
            hover
          >
            <template #item="{ item }">
              <tr>
                <td>{{ item.name }}</td>
                <td>{{ item.categoryName }}</td>
                <td>{{ item.caseName ?? '-' }}</td>
                <td>{{ item.modifiedBy }}</td>
                <td>{{ dayjs(item.modifiedTime).format("DD MMM YYYY") }}</td>
                <td>
                  <ul class="m-0 list-inline hstack">
                    <li>
                      <v-tooltip text="Restore" activator="parent" location="top" offset="2"/>
                      <el-popconfirm
                        title="Are you sure to restore this document?"
                        icon-color="green"
                        width="190"
                        @confirm="restoreDocument(item.id)"
                      >
                        <template #reference>
                          <v-btn icon="mdi-restore" size="small" variant="text"/>
                        </template>
                      </el-popconfirm>
                    </li>
                    <li>
                      <v-tooltip text="Delete Forever" activator="parent" location="top" offset="2"/>
                      <el-popconfirm
                        title="Are you sure to delete this document forever?"
                        icon-color="red"
                        width="190"
                        @confirm="deleteDocument(item.id)"
                      >
                        <template #reference>
                          <v-btn icon="mdi-trash-can-outline" size="small" variant="text"/>
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
                  :page-size="archiveFilterList.length/pageCount()"
                  :total="archiveFilterList.length"
                />
              </div>
            </template>
          </v-data-table>
        </div>
      </UiParentCard>
    </v-col>
  </v-row>
</template>

<script setup>
import { FileDescriptionIcon } from "vue-tabler-icons"
import dayjs from 'dayjs'
import UiParentCard from '@/components/shared/UiParentCard.vue'

// Data
const filter = ref({
  docId: null,
  categoryId: null,
  caseId: null,
})
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
const { data: caseList } = await fetchData.$get("/Case")
const { data: categoryList } = await fetchData.$get("/Document/Category")
const { data: archiveList } = await fetchData.$get("/Archive")
const { data: archiveFilterList } = await fetchData.$get("/Archive/Filter", filter.value)

// Head
useHead({
  title: "Archives | CaseCraft",
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
      title: 'Archives',
      disabled: false,
    },
  ],
})

// Methods
const pageCount = () => {
  return Math.ceil(archiveFilterList.value.length / itemsPerPage.value)
}
const restoreDocument = async(docId) => {
  try {
    const result = await fetchData.$put(`/Archive/Restore/${docId}`)
    if (!result.error) {
      ElNotification.success({ message: result.message })
      refreshNuxtData()
    }
    else {
      ElNotification.error({ message: result.message })
    }
  } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
}
const deleteDocument = async(docId) => {
  try {
    const result = await fetchData.$delete(`/Archive/${docId}`)
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