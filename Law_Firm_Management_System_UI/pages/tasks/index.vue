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
      <div class="mt-5">
        <v-card elevation="10" class="withbg mb-5" link @click="getSelectedTask(task.id, 'Backlog')" v-for="task in backlogTasks">
          <v-card-item class="pa-0">
            <v-card-title class="px-4 py-2 d-sm-flex align-center text-h6 fw-bold" style="white-space: unset;">
              {{ task.title }}
            </v-card-title>
          </v-card-item>
          <v-card-text class="px-5 py-2 pb-0 text-body-1">
            {{ task.description }}
          </v-card-text>
          <v-card-actions class="px-5 py-0 justify-space-between">
            <v-card-subtitle class="p-0 text-caption">
              Due: {{ dayjs(task.dueTime).format("DD MMM YYYY") }}
            </v-card-subtitle>
            <div>
              <v-icon size="small" icon="mdi-briefcase-variant-outline" class="me-1 text-disabled" v-if="task.caseId"/>
              <v-icon size="small" icon="mdi-calendar-outline" class="me-1 text-disabled" v-if="task.eventId"/>
              <v-icon size="small" icon="mdi-file-document-outline" class="text-disabled" v-if="task.documentId"/>
            </div>
          </v-card-actions>
        </v-card>
      </div>
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
      <div class="mt-5">
        <v-card elevation="10" class="withbg mb-5" link @click="getSelectedTask(task.id, 'ToDo')" v-for="task in toDoTasks">
          <v-card-item class="pa-0">
            <v-card-title class="px-4 py-2 d-sm-flex align-center text-h6 fw-bold" style="white-space: unset;">
              {{ task.title }}
            </v-card-title>
          </v-card-item>
          <v-card-text class="px-5 py-2 pb-0 text-body-1">
            {{ task.description }}
          </v-card-text>
          <v-card-actions class="px-5 py-0 justify-space-between">
            <v-card-subtitle class="p-0 text-caption">
              Due: {{ dayjs(task.dueTime).format("DD MMM YYYY") }}
            </v-card-subtitle>
            <div>
              <v-icon size="small" icon="mdi-briefcase-variant-outline" class="me-1 text-disabled" v-if="task.caseId"/>
              <v-icon size="small" icon="mdi-calendar-outline" class="me-1 text-disabled" v-if="task.eventId"/>
              <v-icon size="small" icon="mdi-file-document-outline" class="text-disabled" v-if="task.documentId"/>
            </div>
          </v-card-actions>
        </v-card>
      </div>
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
      <div class="mt-5">
        <v-card elevation="10" class="withbg mb-5" link @click="getSelectedTask(task.id, 'InProgress')" v-for="task in inProgressTasks">
          <v-card-item class="pa-0">
            <v-card-title class="px-4 py-2 d-sm-flex align-center text-h6 fw-bold" style="white-space: unset;">
              {{ task.title }}
            </v-card-title>
          </v-card-item>
          <v-card-text class="px-5 py-2 pb-0 text-body-1">
            {{ task.description }}
          </v-card-text>
          <v-card-actions class="px-5 py-0 justify-space-between">
            <v-card-subtitle class="p-0 text-caption">
              Due: {{ dayjs(task.dueTime).format("DD MMM YYYY") }}
            </v-card-subtitle>
            <div>
              <v-icon size="small" icon="mdi-briefcase-variant-outline" class="me-1 text-disabled" v-if="task.caseId"/>
              <v-icon size="small" icon="mdi-calendar-outline" class="me-1 text-disabled" v-if="task.eventId"/>
              <v-icon size="small" icon="mdi-file-document-outline" class="text-disabled" v-if="task.documentId"/>
            </div>
          </v-card-actions>
        </v-card>
      </div>
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
      <div class="mt-5">
        <v-card elevation="10" class="withbg mb-5" link @click="getSelectedTask(task.id, 'Complete')" v-for="task in completeTasks">
          <v-card-item class="pa-0">
            <v-card-title class="px-4 py-2 d-sm-flex align-center text-h6 fw-bold" style="white-space: unset;">
              {{ task.title }}
            </v-card-title>
          </v-card-item>
          <v-card-text class="px-5 py-2 pb-0 text-body-1">
            {{ task.description }}
          </v-card-text>
          <v-card-actions class="px-5 py-0 justify-space-between">
            <v-card-subtitle class="p-0 text-caption">
              Due: {{ dayjs(task.dueTime).format("DD MMM YYYY") }}
            </v-card-subtitle>
            <div>
              <v-icon size="small" icon="mdi-briefcase-variant-outline" class="me-1 text-disabled" v-if="task.caseId"/>
              <v-icon size="small" icon="mdi-calendar-outline" class="me-1 text-disabled" v-if="task.eventId"/>
              <v-icon size="small" icon="mdi-file-document-outline" class="text-disabled" v-if="task.documentId"/>
            </div>
          </v-card-actions>
        </v-card>
      </div>
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

  <!-- Back to Top Button -->
  <el-backtop :bottom="100">
    <v-btn icon="mdi-arrow-left" color="primary" size="large"/>
  </el-backtop>

  <!-- Add New Task Modal -->
  <SharedUiModal v-model="addTaskModal" title="Add New Task" width="700">
    <el-scrollbar max-height="400px">
      <TaskCreateForm
        :case-input-list="caseList"
        :event-input-list="eventList"
        :doc-input-list="docList"
        :close-modal="(e) => addTaskModal = e"
      />
    </el-scrollbar>
  </SharedUiModal>

  <!-- Edit Task Modal -->
  <SharedUiModal v-model="editTaskModal" title="Edit Task" width="700">
    <el-scrollbar max-height="400px">
      <TaskEditForm
        :initial-values="selectedTask"
        :case-input-list="caseList"
        :event-input-list="eventList"
        :doc-input-list="docList"
        :close-modal="(e) => editTaskModal = e"
      />
    </el-scrollbar>
  </SharedUiModal>

  <!-- View Task Information Modal -->
  <SharedUiModal v-model="viewTaskModal" title="Task Information" width="500">
    <el-scrollbar max-height="450px">
      <v-card-item class="px-8 py-4 text-body-1">
        <v-card-title class="text-h6 fw-bold" style="white-space: unset;">
          {{ selectedTask.title }}
        </v-card-title>
        <v-card-subtitle class="text-subtitle-2">
          <div v-if="selectedTask.paralegalUserId">Task Assignment: Your paralegal, {{ selectedTask.paralegalFullName }}</div>
          <div v-else>Task Assignment: Yourself</div>
          <div>
            Due Time: {{ dayjs(selectedTask.dueTime).format("DD MMM YYYY, h:mm A") }}
          </div>
        </v-card-subtitle>
        <v-divider class="mt-3 mb-0"/>
      </v-card-item>
      <v-card-text class="px-8 pt-0 text-body-1 text-justify">
        {{ selectedTask.description }}
        <v-list class="pt-3 pb-0" v-if="selectedTask.caseId || selectedTask.eventId || selectedTask.docId">
          <v-list-item
            class="mb-2 rounded-3 bg-background list-link"
            density="compact"
            prepend-icon="mdi-briefcase-variant-outline"
            :to="``"
            link
            v-if="selectedTask.caseId"
          >
            {{ selectedTask.caseName }}
          </v-list-item>
          <v-list-item
            class="mb-2 rounded-3 bg-background list-link"
            density="compact"
            prepend-icon="mdi-calendar-outline"
            :to="``"
            link
            v-if="selectedTask.eventId"
          >
            {{ selectedTask.eventName }}
          </v-list-item>
          <v-list-item
            class="mb-2 rounded-3 bg-background list-link"
            density="compact"
            prepend-icon="mdi-file-document-outline"
            link
            @click="downloadDocument(selectedTask.docId)"
            v-if="selectedTask.docId"
          >
            {{ selectedTask.docName }}
          </v-list-item>
        </v-list>
      </v-card-text>
      <v-card-actions class="p-3 justify-content-end">
        <el-popconfirm
          title="Are you sure to accept this task?"
          icon-color="green"
          width="190"
          :teleported="false"
          @confirm="acceptTask(selectedTask.id)"
          v-if="selectedTask.status == 'ToDo'"
        >
          <template #reference>
            <v-btn color="primary">Accept</v-btn>
          </template>
        </el-popconfirm>
        <el-popconfirm
          title="Are you sure to edit this task?"
          icon-color="orange"
          width="190"
          :teleported="false"
          @confirm="editTask"
          v-if="userRole == 'Partner' && (selectedTask.status == 'ToDo' || selectedTask.status == 'InProgress')"
        >
          <template #reference>
            <v-btn color="primary">Edit</v-btn>
          </template>
        </el-popconfirm>
        <el-popconfirm
          title="Are you sure to delete this task?"
          icon-color="red"
          width="190"
          :teleported="false"
          @confirm="deleteTask(selectedTask.id)"
          v-if="userRole == 'Partner'"
        >
          <template #reference>
            <v-btn color="danger">Delete</v-btn>
          </template>
        </el-popconfirm>
      </v-card-actions>
    </el-scrollbar>
  </SharedUiModal>
</template>


<script setup>
import { ClipboardTextIcon } from "vue-tabler-icons"
import dayjs from 'dayjs'

// Data
const addTaskModal = ref(false)
const editTaskModal = ref(false)
const viewTaskModal = ref(false)
const selectedTask = ref({})
const { data: userRole } = await fetchData.$get("/UserRole/RoleName")
const { data: caseList } = await fetchData.$get("/Case")
const { data: eventList } = await fetchData.$get("/Event")
const { data: docList } = await fetchData.$get("/Document")
const { data: backlogTasks } = await fetchData.$get("/Task/Backlog")
const { data: toDoTasks } = await fetchData.$get("/Task/ToDo")
const { data: inProgressTasks } = await fetchData.$get("/Task/InProgress")
const { data: completeTasks } = await fetchData.$get("/Task/Complete")

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
const getSelectedTask = async(taskId, status) => {
  const { data } = await fetchData.$get(`/Task/Info/${taskId}`)
  selectedTask.value = data.value
  selectedTask.value.status = status
  selectedTask.value.isAssignedParalegal = data.value.paralegalUserId ? true : false
  viewTaskModal.value = true
  console.log(selectedTask.value)
}
const editTask = () => {
  viewTaskModal.value = false
  editTaskModal.value = true
}
const acceptTask = async(taskId) => {
  try {
    const result = await fetchData.$put(`/Task/StartProgress/${taskId}`)
    if (!result.error) {
      viewTaskModal.value = false
      ElNotification.success({ message: result.message })
      refreshNuxtData()
    }
    else {
      ElNotification.error({ message: result.message })
    }
  } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
}
const deleteTask = async(taskId) => {
  try {
    const result = await fetchData.$delete(`/Task/Delete/${taskId}`)
    if (!result.error) {
      viewTaskModal.value = false
      ElNotification.success({ message: result.message })
      refreshNuxtData()
    }
    else {
      ElNotification.error({ message: result.message })
    }
  } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
}
const downloadDocument = async(docId) => {
  const { data: doc } = await fetchData.$get(`/Document/Info/${docId}`)
  const mimeType = {
    "PDF": "application/pdf",
    "Word": "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
    "Excel": "application/vnd.ms-excel",
  }
  const arrayBuffer = atob(doc.value.attachment)
  const blob = new Blob([arrayBuffer], { type: mimeType[doc.value.type] })
  const url = URL.createObjectURL(blob)
  const link = document.createElement('a')
  link.href = url
  link.download = doc.value.name
  link.click()
  document.body.removeChild(link)
}
</script>