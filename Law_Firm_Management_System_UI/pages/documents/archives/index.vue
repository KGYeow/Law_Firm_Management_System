<template>
  <v-row>
    <v-col cols="12" md="12">
      <UiParentCard title="Archives">
        <v-row class="px-7">
          <!-- Filters -->
          <v-col class="pe-0" cols="2">
            <v-select
              :items="archiveList"
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
                <td>{{ dayjs(item.modifiedDate).format("DD MMM YYYY") }}</td>
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
                      @confirm="deleteDocument(item.id)"
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
import UiParentCard from '@/components/shared/UiParentCard.vue';

// Data
const filter = ref({
  docId: null,
  categoryId: null,
  caseId: null,
  type: null,
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