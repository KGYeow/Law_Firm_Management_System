<template>
  <form @submit.prevent="editPassword">
    <v-card-text class="px-8 py-4 text-body-1">
      <v-row>
        <v-col cols="2" class="d-flex align-center">
          <v-label class="fw-bold">New Password</v-label>
        </v-col>
        <v-col cols="4">
          <v-text-field
            variant="outlined"
            density="compact"
            :error-messages="editPasswordDetails.newPassword.errorMessage"
            v-model="editPasswordDetails.newPassword.value"
            :disabled="!isEdit"
            hide-details="auto"
          />
        </v-col>
      </v-row>
      <v-row>
        <v-col cols="2" class="d-flex align-center">
          <v-label class="fw-bold">Confirm Password</v-label>
        </v-col>
        <v-col cols="4">
          <v-text-field
            variant="outlined"
            density="compact"
            :error-messages="editPasswordDetails.confirmPassword.errorMessage"
            v-model="editPasswordDetails.confirmPassword.value"
            :disabled="!isEdit"
            hide-details="auto"
          />
        </v-col>
      </v-row>
    </v-card-text>
    <v-card-actions class="p-3 justify-content-end">
      <v-btn color="primary" elevation="0" @click="isEdit = !isEdit" v-if="!isEdit">Renew Password</v-btn>
      <div v-else>
        <v-btn color="primary" variant="outlined" @click="editPasswordCancel">Cancel</v-btn>
        <v-btn color="primary" class="ms-2" elevation="0" type="submit">Save</v-btn>
      </div>
    </v-card-actions>
  </form>
</template>

<script setup>
import { useField, useForm } from 'vee-validate'

// Properties
const props = defineProps({
  userInfo: Object,
})

// Data
const isEdit = ref(false)
const { handleSubmit } = useForm({
  validationSchema: {
    newPassword(value) {
      return value ? true : 'New password is required'
    },
    confirmPassword(value) {
      if (value) {
        if (value != editPasswordDetails.value.newPassword.value)
          return 'The confirmed password does not match the new password'
        return true
      }
      else
        return 'Confirm password is required'
    },
  }
})
const editPasswordDetails = ref({
  newPassword: useField('newPassword'),
  confirmPassword: useField('confirmPassword'),
})

// Methods
const editPasswordCancel = () => {
  isEdit.value = !isEdit.value
  editPasswordDetails.value.newPassword.resetField()
  editPasswordDetails.value.confirmPassword.resetField()
}
const editPassword = handleSubmit(async(values) => {
  try {
    const result = await fetchData.$put(`/User/Me/Password/${values.newPassword}`)
    if (!result.error) {
      isEdit.value = !isEdit.value
      editPasswordDetails.value.newPassword.resetField()
      editPasswordDetails.value.confirmPassword.resetField()
      ElNotification.success({ message: result.message })
    }
    else {
      ElNotification.error({ message: result.message })
    }
  } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
})
</script>