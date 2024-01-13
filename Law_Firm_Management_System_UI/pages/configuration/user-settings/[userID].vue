<template>
  <v-row>
    <v-col cols="12" md="12">
      <v-card elevation="10" class="withbg">
        <div class="hstack justify-content-center align-items-stretch">
          <div class="p-4 w-25 text-center">
            <!-- Client Profile Image -->
            <v-avatar
              class="border my-5"
              image="/images/users/avatar.jpg"
              size="110"
              style="border-width: 3px !important; border-color: lightgrey !important;"
            />
            <div class="mb-2 text-h5 d-sm-flex align-center justify-content-center">
              {{ userInfo.fullName }}
            </div>
            <div class="text-body-1 d-sm-flex align-center justify-content-center" v-if="userInfo.role == 'Client'">
              <a :href="`/contacts/clients/${userInfo.clientId}`" class="text-secondary">
                <i class="mdi mdi-open-in-new"></i>
                View Client Information
              </a>
            </div>
          </div>
          <v-divider vertical/>
          <div class="p-4 w-75">
            <v-card-title class="text-h6 text-body-tertiary">
              User Account Information
            </v-card-title>
            <form @submit.prevent="editUser">
              <v-card-item>
                <v-row class="pb-2">
                  <v-col cols="4">
                    <v-label class="text-h6 pb-3">User Account ID</v-label>
                    <v-card-subtitle>{{ userInfo.id }}</v-card-subtitle>
                  </v-col>
                  <v-col cols="4" v-if="userInfo.role == 'Client'">
                    <v-label class="text-h6 pb-3">Client ID</v-label>
                    <v-card-subtitle>{{ userInfo.clientId }}</v-card-subtitle>
                  </v-col>
                </v-row>
                <v-row class="pb-2">
                  <v-col>
                    <v-label class="text-h6 pb-3">Full Name</v-label>
                    <v-card-subtitle v-if="!isEdit">{{ userInfo.fullName }}</v-card-subtitle>
                    <v-text-field
                      variant="outlined"
                      density="compact"
                      :error-messages="editUserDetails.fullName.errorMessage"
                      v-model="editUserDetails.fullName.value"
                      hide-details="auto"
                      v-else
                    />
                  </v-col>
                  <v-col>
                    <v-label class="text-h6 pb-3">Username</v-label>
                    <v-card-subtitle v-if="!isEdit">{{ userInfo.username ?? '-' }}</v-card-subtitle>
                    <v-text-field
                      variant="outlined"
                      density="compact"
                      :error-messages="editUserDetails.username.errorMessage"
                      v-model="editUserDetails.username.value"
                      hide-details="auto"
                      v-else
                    />
                  </v-col>
                  <v-col>
                    <v-label class="text-h6 pb-3">Email</v-label>
                    <v-card-subtitle v-if="!isEdit">{{ userInfo.email ?? '-' }}</v-card-subtitle>
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
                  <v-col cols="4">
                    <v-label class="text-h6 pb-3">User Role</v-label>
                    <v-card-subtitle v-if="!isEdit">{{ userInfo.role }}</v-card-subtitle>
                    <v-select
                      :items="['Partner', 'Paralegal', 'Client']"
                      placeholder="Role"
                      variant="outlined"
                      density="compact"
                      :error-messages="editUserDetails.role.errorMessage"
                      v-model="editUserDetails.role.value"
                      hide-details="auto"
                      v-else
                    />
                  </v-col>
                </v-row>
              </v-card-item>
              <v-card-actions class="justify-content-end p-3">
                <div v-if="!isEdit">
                  <v-btn color="primary" variant="tonal" flat @click="isEdit = true">Edit</v-btn>
                </div>
                <div v-else>
                  <v-btn color="primary" variant="outlined" @click="editUserCancel">Cancel</v-btn>
                  <v-btn color="primary" variant="tonal" class="ms-2" flat type="submit">Save Details</v-btn>
                </div>
              </v-card-actions>
            </form>
          </div>
        </div> 
      </v-card>
    </v-col>
  </v-row>
</template>

<script setup>
import { useField, useForm } from 'vee-validate'
import { SettingsIcon } from 'vue-tabler-icons'

// Data
const isEdit = ref(false)
const routeParameter = ref(useRoute().params)
const { data: userInfo } = await fetchData.$get(`/User/Info/${routeParameter.value.userID}`)
const { handleSubmit } = useForm({
  initialValues: {
    fullName: userInfo.value.fullName,
    username: userInfo.value.username,
    email: userInfo.value.email,
    role: userInfo.value.role,
  },
  validationSchema: {
    fullName(value) {
      return value ? true : 'Full name is required'
    },
    username(value) {
      return value ? true : 'Username is required'
    },
    email(value) {
      return value ? true : 'Email is required'
    },
    role(value) {
      return value ? true : 'User role is required'
    },
  }
})
const editUserDetails = ref({
  fullName: useField('fullName'),
  username: useField('username'),
  email: useField('email'),
  role: useField('role'),
})

// Head
useHead({
  title: 'User Account Details | CaseCraft',
})

// Page Meta
definePageMeta({
  breadcrumbsIcon: shallowRef(SettingsIcon),
  breadcrumbs: [
    {
      title: 'Contacts',
      disabled: false,
    },
    {
      title: 'User Settings',
      disabled: false,
      href: '/configuration/user-settings',
    },
    {
      title: 'User Account Details',
      disabled: false,
    },
  ],
})

// Methods
const editUserCancel = () => {
  isEdit.value = false
  editUserDetails.value.fullName.resetField()
  editUserDetails.value.username.resetField()
  editUserDetails.value.email.resetField()
  editUserDetails.value.role.resetField()
}
const editUser = handleSubmit(async(values) => {
  try {
    const result = await fetchData.$put(`/User/Info/${userInfo.value.id}/Edit`, values)
    if (!result.error) {
      isEdit.value = false
      editUserDetails.value.fullName.setValue(values.fullName)
      editUserDetails.value.username.setValue(values.username)
      editUserDetails.value.email.setValue(values.email)
      editUserDetails.value.role.setValue(values.role)
      ElNotification.success({ message: result.message })
      refreshNuxtData()
    }
    else {
      ElNotification.error({ message: result.message })
    }
  } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
})
// const deleteUser = async(userId) => {
//   try {
//     const result = await fetchData.$delete(`/User/Delete/${userId}`)
//     if (!result.error) {
//       ElNotification.success({ message: result.message })
//       navigateTo('/configuration/user-settings')
//     }
//     else {
//       ElNotification.error({ message: result.message })
//     }
//   } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
// }
</script>