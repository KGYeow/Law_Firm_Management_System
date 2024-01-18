<template>
  <form @submit.prevent="editRole">
    <v-card-text class="px-8 py-4 text-body-1">
      <v-row>
        <v-col cols="2" class="d-flex align-center">
          <v-label class="fw-bold">Assigned Paralegal</v-label>
        </v-col>
        <v-col cols="4">
          <v-card-subtitle class="p-0 text-wrap">{{ roleInfo.assignedParalegal ?? '-'  }}</v-card-subtitle>
        </v-col>
      </v-row>
      <v-row>
        <v-col cols="2" class="d-flex align-center">
          <v-label class="fw-bold">Phone Number</v-label>
        </v-col>
        <v-col cols="4">
          <v-card-subtitle class="p-0 text-wrap" v-if="!isEdit">{{ roleInfo.phoneNum ?? '-' }}</v-card-subtitle>
          <v-text-field
            variant="outlined"
            density="compact"
            :error-messages="editRoleDetails.phoneNum.errorMessage"
            v-model="editRoleDetails.phoneNum.value"
            hide-details="auto"
            v-else
          />
        </v-col>
      </v-row>
      <v-row>
        <v-col cols="2" class="d-flex align-center">
          <v-label class="fw-bold">Address</v-label>
        </v-col>
        <v-col>
          <v-card-subtitle class="p-0 text-wrap" v-if="!isEdit">{{ roleInfo.address ?? '-' }}</v-card-subtitle>
          <v-text-field
            variant="outlined"
            density="compact"
            :error-messages="editRoleDetails.address.errorMessage"
            v-model="editRoleDetails.address.value"
            hide-details="auto"
            v-else
          />
        </v-col>
      </v-row>
    </v-card-text>
    <v-card-actions class="p-3 justify-content-end">
      <v-btn color="primary" elevation="0" @click="isEdit = !isEdit" v-if="!isEdit">Edit</v-btn>
      <div v-else>
        <v-btn color="primary" variant="outlined" @click="editRoleCancel">Cancel</v-btn>
        <v-btn color="primary" class="ms-2" elevation="0" type="submit">Save</v-btn>
      </div>
    </v-card-actions>
  </form>
</template>

<script setup>
import { useField, useForm } from 'vee-validate'

// Properties
const props = defineProps({
  roleInfo: Object,
})

// Data
const isEdit = ref(false)
const { handleSubmit } = useForm({
  initialValues: {
    phoneNum: props.roleInfo.phoneNum,
    address: props.roleInfo.address
  },
  validationSchema: {
    phoneNum(value) {
      return true
    },
    address(value) {
      return true
    }
  }
})
const editRoleDetails = ref({
  phoneNum: useField('phoneNum'),
  address: useField('address'),
})

// Methods
const editRoleCancel = () => {
  isEdit.value = !isEdit.value
  editRoleDetails.value.phoneNum.resetField()
  editRoleDetails.value.address.resetField()
}
const editRole = handleSubmit(async(values, { resetForm }) => {
  try {
    const result = await fetchData.$put("/User/Me/Info/Role/Partner", values)
    if (!result.error) {
      isEdit.value = !isEdit.value
      resetForm({ values: values })
      ElNotification.success({ message: result.message })
      refreshNuxtData()
    }
    else {
      ElNotification.error({ message: "Your partner information has been edited unsuccessfully" })
    }
  } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
})
</script>