<template>
  <form @submit.prevent="editUser">
    <v-card-text class="px-8 py-4 text-body-1">
      <v-row>
        <v-col cols="2" class="d-flex align-center">
          <v-label class="fw-bold">User ID</v-label>
        </v-col>
        <v-col cols="4">
          <v-card-subtitle class="p-0 text-wrap">{{ userInfo.id }}</v-card-subtitle>
        </v-col>
      </v-row>
      <v-row>
        <v-col cols="2" class="d-flex align-center">
          <v-label class="fw-bold">Username</v-label>
        </v-col>
        <v-col cols="4">
          <v-card-subtitle class="p-0 text-wrap" v-if="!isEdit">{{ editUserDetails.username.value }}</v-card-subtitle>
          <v-text-field
            variant="outlined"
            density="compact"
            :error-messages="editUserDetails.username.errorMessage"
            v-model="editUserDetails.username.value"
            hide-details="auto"
            v-else
          />
        </v-col>
      </v-row>
      <v-row>
        <v-col cols="2" class="d-flex align-center">
          <v-label class="fw-bold">Full Name</v-label>
        </v-col>
        <v-col cols="4">
          <v-card-subtitle class="p-0 text-wrap" v-if="!isEdit">{{ editUserDetails.fullName.value }}</v-card-subtitle>
          <v-text-field
            variant="outlined"
            density="compact"
            :error-messages="editUserDetails.fullName.errorMessage"
            v-model="editUserDetails.fullName.value"
            hide-details="auto"
            v-else
          />
        </v-col>
      </v-row>
      <v-row>
        <v-col cols="2" class="d-flex align-center">
          <v-label class="fw-bold">Email Address</v-label>
        </v-col>
        <v-col cols="4">
          <v-card-subtitle class="p-0 text-wrap" v-if="!isEdit">{{ editUserDetails.email.value }}</v-card-subtitle>
          <v-text-field
            variant="outlined"
            density="compact"
            :error-messages="editUserDetails.email.errorMessage"
            v-model="editUserDetails.email.value"
            hide-details="auto"
            v-else
          />
        </v-col>
      </v-row>
      <v-row>
        <v-col cols="2" class="d-flex align-center">
          <v-label class="fw-bold">Role</v-label>
        </v-col>
        <v-col cols="4">
          <v-card-subtitle class="p-0 text-wrap">{{ role }}</v-card-subtitle>
        </v-col>
      </v-row>
    </v-card-text>
    <v-card-actions class="p-3 justify-content-end">
      <v-btn color="primary" elevation="0" @click="isEdit = !isEdit" v-if="!isEdit">Edit</v-btn>
      <div v-else>
        <v-btn color="primary" variant="outlined" @click="editUserCancel">Cancel</v-btn>
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
  role: String,
})

// Data
const isEdit = ref(false)
const { handleSubmit } = useForm({
  initialValues: {
    username: props.userInfo.username,
    fullName: props.userInfo.fullName,
    email: props.userInfo.email
  },
  validationSchema: {
    username(value) {
      return value ? true : 'Username is required'
    },
    fullName(value) {
      return value ? true : 'Full name is required'
    },
    email(value) {
      return value ? true : 'Email address is required'
    }
  }
})
const editUserDetails = ref({
  username: useField('username'),
  fullName: useField('fullName'),
  email: useField('email'),
})

// Methods
const editUserCancel = () => {
  isEdit.value = !isEdit.value
  editUserDetails.value.username.resetField()
  editUserDetails.value.fullName.resetField()
  editUserDetails.value.email.resetField()
}
const editUser = handleSubmit(async(values, { resetForm }) => {
  try {
    const result = await fetchData.$put("/User/Me", values)
    if (!result.error) {
      isEdit.value = !isEdit.value
      resetForm({ values: values })
      ElNotification.success({ message: result.message })
      refreshNuxtData()
    }
    else {
      ElNotification.error({ message: "Your user profile has been edited unsuccessfully" })
    }
  } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
})
</script>