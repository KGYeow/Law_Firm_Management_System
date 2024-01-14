<template>
  <v-row>
    <!-- Backlog -->
    <v-col>
      <!-- Backlog Title -->
      <v-card elevation="10" class="withbg">
        <v-card-item class="pa-0">
          <v-card-title class="text-h6 px-4 py-2 d-sm-flex align-center" style="white-space: unset;">
            Backlog
          </v-card-title>
        </v-card-item>
      </v-card>
      <!-- Backlog Task Card -->
      <el-scrollbar max-height="70vh" class="mt-5">
        <v-card elevation="10" class="withbg mb-5" link @click="viewTaskModal = true" v-for="n in 10">
          <v-card-item class="pa-0">
            <v-card-title class="px-4 pt-5 pb-1 d-sm-flex align-center text-body-1 font-weight-bold" style="white-space: unset;">
              Create styleguide foundation
            </v-card-title>
          </v-card-item>
          <v-card-text class="px-4 pt-0 pb-5">
            <div class="pb-2 text-body-1">
                This is the first task description 
            </div>
            <v-card-subtitle class="p-0 text-caption">
              DUE: Aug 20 2001
            </v-card-subtitle>
          </v-card-text>
        </v-card>
      </el-scrollbar>
    </v-col>

    <!-- To Do -->
    <v-col>
      <v-card elevation="10" class="withbg">
        <v-card-item class="pa-0">
          <v-card-title class="text-h6 px-4 py-2 d-sm-flex align-center" style="white-space: unset;">
            To Do
          </v-card-title>
        </v-card-item>
      </v-card>
      <!-- To Do Task Card -->
      <el-scrollbar max-height="70vh" class="mt-5">
        <v-card elevation="10" class="withbg mb-5" link @click="viewTaskModal = true" v-for="n in 10">
          <v-card-item class="pa-0">
            <v-card-title class="px-4 pt-5 pb-1 d-sm-flex align-center text-body-1 font-weight-bold" style="white-space: unset;">
              Create styleguide foundation
            </v-card-title>
          </v-card-item>
          <v-card-text class="px-4 pt-0 pb-5">
            <div class="pb-2 text-body-1">
                This is the first task description
            </div>
            <v-card-subtitle class="p-0 text-caption">
              DUE: Aug 20 2001
            </v-card-subtitle>
          </v-card-text>
        </v-card>
      </el-scrollbar>
    </v-col>

    <!-- In Progress -->
    <v-col>
      <v-card elevation="10" class="withbg">
        <v-card-item class="pa-0">
          <v-card-title class="text-h6 px-4 py-2 d-sm-flex align-center" style="white-space: unset;">
            In Progress
          </v-card-title>
        </v-card-item>
      </v-card>
      <!-- In Progress Task Card -->
      <el-scrollbar max-height="70vh" class="mt-5">
        <v-card elevation="10" class="withbg mb-5" link @click="viewTaskModal = true" v-for="n in 10">
          <v-card-item class="pa-0">
            <v-card-title class="px-4 pt-5 pb-1 d-sm-flex align-center text-body-1 font-weight-bold" style="white-space: unset;">
              Create styleguide foundation
            </v-card-title>
          </v-card-item>
          <v-card-text class="px-4 pt-0 pb-5">
            <div class="pb-2 text-body-1">
                This is the first task description
            </div>
            <v-card-subtitle class="p-0 text-caption">
              DUE: Aug 20 2001
            </v-card-subtitle>
          </v-card-text>
        </v-card>
      </el-scrollbar>
    </v-col>

    <!-- Complete -->
    <v-col>
      <v-card elevation="10" class="withbg">
        <v-card-item class="pa-0">
          <v-card-title class="text-h6 px-4 py-2 d-sm-flex align-center" style="white-space: unset;">
            Complete
          </v-card-title>
        </v-card-item>
      </v-card>
      <!-- Complete Task Card -->
      <el-scrollbar max-height="70vh" class="mt-5">
        <v-card elevation="10" class="withbg mb-5" link @click="viewTaskModal = true" v-for="n in 10">
          <v-card-item class="pa-0">
            <v-card-title class="px-4 pt-5 pb-1 d-sm-flex align-center text-body-1 font-weight-bold" style="white-space: unset;">
              Create styleguide foundation
            </v-card-title>
          </v-card-item>
          <v-card-text class="px-4 pt-0 pb-5">
            <div class="pb-2 text-body-1">
                This is the first task description
            </div>
            <v-card-subtitle class="p-0 text-caption">
              DUE: Aug 20 2001
            </v-card-subtitle>
          </v-card-text>
        </v-card>
      </el-scrollbar>
    </v-col>
  </v-row>

  <!-- Add New Task Button -->
  <el-affix
    class="position-absolute"
    position="bottom"
    :offset="30"
    style="right: 30px; bottom: 100px;"
  >
    <v-tooltip text="Create Task" activator="parent" location="left" offset="2"/>
    <v-btn icon="mdi-clipboard-plus-outline" color="primary" size="large" @click="addTaskModal = true"/>
  </el-affix>

  <!-- Add New Task Modal -->
  <SharedUiModal v-model="addTaskModal" title="Add New Task" width="700">
    <el-scrollbar max-height="400px">
      <form @submit.prevent="addTask">
        <v-card-item class="px-8 py-4 text-body-1">
          <v-row>
            <v-col>
              <v-label class="text-caption">Title</v-label>
              <v-text-field
                variant="outlined"
                density="compact"
                :error-messages="addTaskDetails.title.errorMessage"
                v-model="addTaskDetails.title.value"
                hide-details="auto"
              />
            </v-col>
          </v-row>
          <v-row>
            <v-col>
              <v-label class="text-caption">Description</v-label>
              <v-textarea
                variant="outlined"
                density="compact"
                :error-messages="addTaskDetails.description.errorMessage"
                v-model="addTaskDetails.description.value"
                hide-details="auto"
              />
            </v-col>
          </v-row>
        </v-card-item>
        <v-card-actions class="p-3 justify-content-end">
          <v-btn color="primary" type="submit">Submit</v-btn>
        </v-card-actions>
      </form>
    </el-scrollbar>
  </SharedUiModal>

  <!-- Add New Task Modal -->
  <SharedUiModal v-model="viewTaskModal" title="Selected Task Title" width="500">
  </SharedUiModal>
</template>


<script setup>
import { ClipboardTextIcon } from "vue-tabler-icons"
import { useField, useForm } from 'vee-validate'

// Data
const addTaskModal = ref(false)
const viewTaskModal = ref(false)
const { handleSubmit } = useForm({
  validationSchema: {
    title(value) {
      return value ? true : 'Task title is required'
    },
    description(value) {
      return value ? true : 'Task description is required'
    },
    dueTime(value) {
      return value ? true : 'Due Time is required'
    }
  }
})
const addTaskDetails = ref({
  title: useField('title'),
  description: useField('description'),
  dueTime: useField('dueTime'),
  caseId: null,
  eventId: null,
  docId: null,
  isAssignedParalegal: null,
})

// Head
useHead({
  title: "Tasks | CaseCraft",
})

// Page Meta
definePageMeta({
  breadcrumbsIcon: shallowRef(ClipboardTextIcon),
  breadcrumbs: [
    {
      title: 'Tasks',
      disabled: false,
    },
  ],
})

// Methods
const addTask = handleSubmit(async(values) => {
  try {
    const result = await fetchData.$put(`/Task/AssignedParalegal/Add/${values.paralegalUserId}`)
    if (!result.error) {
      addAssignedParalegalModal.value = false
      ElNotification.success({ message: result.message })
      refreshNuxtData()
    }
    else {
      ElNotification.error({ message: result.message })
    }
  } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
})
</script>