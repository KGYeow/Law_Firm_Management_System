<template>
  <v-row>
    <v-col cols="12" md="12">
      <UiParentCard title="Appointment"> 
        <div class="pa-7 pt-1 text-body-1">
          <v-data-table
            v-model:page="currentPage"
            :headers="headers"
            :items="desserts"
            :items-per-page="itemsPerPage"
          >
            <template v-slot:item.actions>
              <v-btn icon="mdi-eye-outline" size="small" color="#68058d" variant="text" class="me-1"/>
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
import UiParentCard from "@/components/shared/UiParentCard.vue";
import { VDataTable } from "vuetify/lib/labs/components.mjs";

// Data
const currentTime = new Intl.DateTimeFormat('en-US', { dateStyle: 'short', timeStyle: 'short' }).format(new Date())
const currentPage = ref(1)
const itemsPerPage = ref(10)
const headers = ref([
  { key: "fullName", title: "Full Name" },
  { key: "category", title: "Appointment Category" },
  { key: "creationTime" , title: "Creation Time" },
  { key: "appointmentTime" , title: "Appointment Time" },
  { key: "actions", sortable: false }
])
const desserts = ref([
  {
    fullName: "Yeow Kok Guan",
    category: "Consulting",
    creationTime: currentTime,
    appointmentTime: currentTime,
  },
  {
    fullName: "Kaedehara Kazuha",
    category: "Consulting",
    creationTime: currentTime,
    appointmentTime: currentTime,
  },
  {
    fullName: "Zhongli",
    category: "Consulting",
    creationTime: currentTime,
    appointmentTime: currentTime,
  },
  {
    fullName: "Eula",
    category: "Consulting",
    creationTime: currentTime,
    appointmentTime: currentTime,
  },
  {
    fullName: "Nahida",
    category: "Raise Case",
    creationTime: currentTime,
    appointmentTime: currentTime,
  },
  {
    fullName: "Venti",
    category: "Consulting",
    creationTime: currentTime,
    appointmentTime: currentTime,
  },
  {
    fullName: "Raiden Ei",
    category: "Raise Case",
    creationTime: currentTime,
    appointmentTime: currentTime,
  },
  {
    fullName: "Furina",
    category: "Raise Case",
    creationTime: currentTime,
    appointmentTime: currentTime,
  },
  {
    fullName: "Yelan",
    category: "Consulting",
    creationTime: currentTime,
    appointmentTime: currentTime,
  },
  {
    fullName: "Cyno",
    category: "Raise Case",
    creationTime: currentTime,
    appointmentTime: currentTime,
  }
])

// Head
useHead({
  title: "Appointment | CaseCraft",
})

// Methods
const pageCount = () => {
  return Math.ceil(desserts.value.length / itemsPerPage.value)
}
const uploadDocument = () => {
  let input = document.createElement("input");
  input.type = "file";
  input.onchange = function () {
    input.files[0].arrayBuffer().then(function (arrayBuffer) {
        console.log(new TextDecoder().decode(arrayBuffer));
    });
  }
  input.click();
}
</script>