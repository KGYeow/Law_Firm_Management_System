<template>
  <v-row>
    <v-col cols="12" md="12">
      <UiParentCard title="Document Management">
        <div class="pa-7 pt-1 text-body-1">
          <v-data-table
            v-model:page="currentPage"
            :headers="headers"
            :items="desserts"
            :items-per-page="itemsPerPage"
          >
            <template v-slot:item.actions>
              <v-btn icon="mdi-eye-outline" size="small" color="#68058d" variant="text" class="me-1"/>
              <v-btn icon="mdi-download" size="small" color="#68058d" variant="text" class="me-1"/>
              <v-btn icon="mdi-rename-outline" size="small" color="#68058d" variant="text" class="me-1"/>
              <v-btn icon="mdi-update" size="small" color="#68058d" variant="text" class="me-1"/>
              <v-btn icon="mdi-archive-outline" size="small" color="#68058d" variant="text"/>
            </template>
            <template v-slot:bottom>
              <div class="d-flex justify-content-center pt-2">
                <el-pagination
                  layout="total, prev, pager, next"
                  v-model:current-page="currentPage"
                  :page-size="desserts.length/pageCount()"
                  :total="desserts.length"
                />
              </div>
            </template>
          </v-data-table>
        </div>
      </UiParentCard>
    </v-col>
  </v-row>
  <el-affix
    class="position-absolute"
    position="bottom"
    :offset="30"
    style="right: 30px; bottom: 100px;"
  >
    <v-btn icon="mdi-file-document-plus-outline" size="large" color="#68058d" @click="uploadDocument"/>
  </el-affix>
</template>

<script setup>
import UiParentCard from '@/components/shared/UiParentCard.vue';
import { VDataTable } from 'vuetify/lib/labs/components.mjs';

// Data
const currentPage = ref(1)
const itemsPerPage = ref(10)
const headers = ref([
  { key: 'name', title: 'Name' },
  { key: 'version', title: 'Version' },
  { key: 'category', title: 'Category' },
  { key: 'modifiedBy' , title: 'Modified By' },
  { key: 'modifiedDate' , title: 'Modified Date' },
  { key: 'actions', sortable: false }
])
const desserts = ref([
  {
    name: 'Frozen Yogurt',
    version: 159,
    category: 6.0,
    modifiedBy: 24,
    modifiedDate: 24,
  },
  {
    name: 'Ice cream sandwich',
    version: 237,
    category: 9.0,
    modifiedBy: 24,
    modifiedDate: 37,
  },
  {
    name: 'Eclair',
    version: 262,
    category: 16.0,
    modifiedBy: 24,
    modifiedDate: 23,
  },
  {
    name: 'Cupcake',
    version: 305,
    category: 3.7,
    modifiedDate: 67,
  },
  {
    name: 'Gingerbread',
    version: 356,
    category: 16.0,
    modifiedDate: 49,
  },
  {
    name: 'Jelly bean',
    version: 375,
    category: 0.0,
    modifiedDate: 94,
  },
  {
    name: 'Lollipop',
    version: 392,
    category: 0.2,
    modifiedDate: 98,
  },
  {
    name: 'Honeycomb',
    version: 408,
    category: 3.2,
    modifiedDate: 87,
  },
  {
    name: 'Donut',
    version: 452,
    category: 25.0,
    modifiedDate: 51
  },
  {
    name: 'KitKat',
    version: 518,
    category: 26.0,
    modifiedDate: 65,
  }
])

// Head
useHead({
  title: "Repository | USM Document Management System",
})

// Methods
const pageCount = () => {
  return Math.ceil(desserts.value.length / itemsPerPage.value)
}
const uploadDocument = () => {
  let input = document.createElement('input');
  input.type = 'file';
  input.onchange = function () {
    input.files[0].arrayBuffer().then(function (arrayBuffer) {
        console.log(new TextDecoder().decode(arrayBuffer));
    });
  }
  input.click();
}
</script>