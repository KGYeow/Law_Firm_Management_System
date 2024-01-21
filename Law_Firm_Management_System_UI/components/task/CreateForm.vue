<template>
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
        <v-col>
          <v-label class="text-caption">Due Time</v-label>
          <el-date-picker
            :class="{ 'error': addTaskDetails.dueTime.errorMessage }"
            placeholder="Select due date and time"
            type="datetime"
            format="DD MMM YYYY, hh:mm A"
            date-format="DD MMM YYYY"
            time-format="HH:mm"
            :teleported="false"
            :disabled-date="(time) => { return time.getTime() < new Date() }"
            v-model="addTaskDetails.dueTime.value"
            style="height: 40px;"
          />
          <v-messages
            :messages="addTaskDetails.dueTime.errorMessage"
            :active="addTaskDetails.dueTime.errorMessage ? true : false"
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
            v-model="addTaskDetails.caseId"
            hide-details="auto"
          >
            <template #prepend-item>
              <v-list-item title="None" @click="addTaskDetails.caseId = null"/>
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
            v-model="addTaskDetails.eventId"
            hide-details="auto"
          >
            <template #prepend-item>
              <v-list-item title="None" @click="addTaskDetails.eventId = null"/>
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
            v-model="addTaskDetails.docId"
            hide-details="auto"
          >
            <template #prepend-item>
              <v-list-item title="None" @click="addTaskDetails.docId = null"/>
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
            :error-messages="addTaskDetails.description.errorMessage"
            v-model="addTaskDetails.description.value"
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
            v-model="addTaskDetails.isAssignedParalegal"
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
  caseInputList: Array,
  eventInputList: Array,
  docInputList: Array,
})

// Emit
const emit = defineEmits(['close-modal'])

// Data
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
  isAssignedParalegal: false,
})

// Methods
const addTask = handleSubmit(async(values) => {
  try {
    const result = await fetchData.$post("/Task", {
      title: values.title,
      description: values.description,
      dueTime: dayjs(values.dueTime).format("DD MMM YYYY, h:mm A"),
      caseId: addTaskDetails.value.caseId,
      eventId: addTaskDetails.value.eventId,
      documentId: addTaskDetails.value.docId,
      isAssignedParalegal: addTaskDetails.value.isAssignedParalegal,
    })

    if (!result.error) {
      emit('close-modal', false)
      addTaskDetails.value.title.resetField()
      addTaskDetails.value.description.resetField()
      addTaskDetails.value.dueTime.resetField()
      addTaskDetails.value.caseId = null
      addTaskDetails.value.eventId = null
      addTaskDetails.value.docId = null
      addTaskDetails.value.isAssignedParalegal = false
      ElNotification.success({ message: result.message })
      refreshNuxtData()
    }
    else {
      ElNotification.error({ message: result.message })
    }
  } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
})
</script>