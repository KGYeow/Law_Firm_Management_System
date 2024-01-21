<template>
  <form @submit.prevent="editTask">
    <v-card-item class="px-8 py-4 text-body-1">
      <v-row>
        <v-col>
          <v-label class="text-caption">Title</v-label>
          <v-text-field
            variant="outlined"
            density="compact"
            :error-messages="editTaskDetails.title.errorMessage"
            v-model="editTaskDetails.title.value"
            hide-details="auto"
          />
        </v-col>
        <v-col>
          <v-label class="text-caption">Due Time</v-label>
          <el-date-picker
            :class="{ 'error': editTaskDetails.dueTime.errorMessage }"
            placeholder="Select due date and time"
            type="datetime"
            format="DD MMM YYYY, hh:mm A"
            date-format="DD MMM YYYY"
            time-format="HH:mm"
            :teleported="false"
            :disabled-date="(time) => { return time.getTime() < new Date() }"
            v-model="editTaskDetails.dueTime.value"
            style="height: 40px;"
          />
          <v-messages
            :messages="editTaskDetails.dueTime.errorMessage"
            :active="editTaskDetails.dueTime.errorMessage ? true : false"
            color="#fa896b"
            :transition="false"
            style="padding: 3px 16px 3px; opacity: unset;"
          />
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <v-label class="text-caption">Related Case</v-label>
          <v-autocomplete
            :items="caseInputList"
            item-title="name"
            item-value="id"
            placeholder="Select case"
            variant="outlined"
            density="compact"
            v-model="editTaskDetails.caseId"
            hide-details="auto"
          >
            <template #prepend-item>
              <v-list-item title="None" @click="editTaskDetails.caseId = null"/>
            </template>
          </v-autocomplete>
        </v-col>
        <v-col>
          <v-label class="text-caption">Related Event</v-label>
          <v-autocomplete
            :items="eventInputList"
            item-title="name"
            item-value="id"
            placeholder="Select event"
            variant="outlined"
            density="compact"
            v-model="editTaskDetails.eventId"
            hide-details="auto"
          >
            <template #prepend-item>
              <v-list-item title="None" @click="editTaskDetails.eventId = null"/>
            </template>
          </v-autocomplete>
        </v-col>
        <v-col>
          <v-label class="text-caption">Related Document</v-label>
          <v-autocomplete
            :items="docInputList"
            item-title="name"
            item-value="id"
            placeholder="Select document"
            variant="outlined"
            density="compact"
            v-model="editTaskDetails.docId"
            hide-details="auto"
          >
            <template #prepend-item>
              <v-list-item title="None" @click="editTaskDetails.docId = null"/>
            </template>
          </v-autocomplete>
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <v-label class="text-caption">Description</v-label>
          <v-textarea
            variant="outlined"
            density="compact"
            :error-messages="editTaskDetails.description.errorMessage"
            v-model="editTaskDetails.description.value"
            hide-details="auto"
          />
        </v-col>
      </v-row>
      <v-row>
        <v-col class="pt-0">
          <v-switch
            class=""
            color="secondary"
            density="compact"
            v-model="editTaskDetails.isAssignedParalegal"
            inset
            hide-details
          >
            <template #prepend>
              <v-label class="p-0 text-caption">Assign to Paralegal</v-label>
            </template>
          </v-switch>
        </v-col>
      </v-row>
    </v-card-item>
    <v-card-actions class="p-3 justify-content-end">
      <v-btn color="primary" type="submit">Submit</v-btn>
    </v-card-actions>
  </form>
</template>

<script setup>
import { useField, useForm } from 'vee-validate'
import dayjs from 'dayjs'

// Properties
const props = defineProps({
  initialValues: Object,
  caseInputList: Array,
  eventInputList: Array,
  docInputList: Array,
})

// Emit
const emit = defineEmits(['close-modal'])

// Data
const { handleSubmit } = useForm({
  initialValues: {
    title: props.initialValues.title,
    description: props.initialValues.description,
    dueTime: props.initialValues.dueTime,
  },
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
const editTaskDetails = ref({
  title: useField('title'),
  description: useField('description'),
  dueTime: useField('dueTime'),
  caseId: props.initialValues.caseId,
  eventId: props.initialValues.eventId,
  docId: props.initialValues.docId,
  isAssignedParalegal: props.initialValues.isAssignedParalegal,
})

// Methods
const editTask = handleSubmit(async(values) => {
  try {
    const result = await fetchData.$put("/Task", {
      taskId: props.initialValues.id,
      title: values.title,
      description: values.description,
      dueTime: dayjs(values.dueTime).format("DD MMM YYYY, h:mm A"),
      caseId: editTaskDetails.value.caseId,
      eventId: editTaskDetails.value.eventId,
      documentId: editTaskDetails.value.docId,
      isAssignedParalegal: editTaskDetails.value.isAssignedParalegal,
    })

    if (!result.error) {
      emit('close-modal', false)
      editTaskDetails.value.title.resetField()
      editTaskDetails.value.description.resetField()
      editTaskDetails.value.dueTime.resetField()
      editTaskDetails.value.caseId = null
      editTaskDetails.value.eventId = null
      editTaskDetails.value.docId = null
      editTaskDetails.value.isAssignedParalegal = false,
      ElNotification.success({ message: result.message })
      refreshNuxtData()
    }
    else {
      ElNotification.error({ message: result.message })
    }
  } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
})
</script>