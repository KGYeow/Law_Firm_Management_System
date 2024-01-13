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
              {{ clientInfo.fullName }}
            </div>
            <div class="text-body-1 d-sm-flex align-center justify-content-center" v-if="clientInfo.userId != null">
              <a :href="`/configuration/user-settings/${clientInfo.userId}`" class="text-secondary">
                <i class="mdi mdi-open-in-new"></i>
                View User Information
              </a>
            </div>
          </div>
          <v-divider vertical/>
          <div class="p-4 w-75">
            <v-card-title class="text-h6 text-body-tertiary">
              Client Information
            </v-card-title>
            <form @submit.prevent="editClient">
              <v-card-item>
                <v-row class="pb-2">
                  <v-col cols="4">
                    <v-label class="text-h6 pb-3">Client ID</v-label>
                    <v-card-subtitle>{{ clientInfo.clientId }}</v-card-subtitle>
                  </v-col>
                  <v-col cols="4">
                    <v-label class="text-h6 pb-3">User Account ID</v-label>
                    <v-card-subtitle>{{ clientInfo.userId ?? '-' }}</v-card-subtitle>
                  </v-col>
                </v-row>
                <v-row class="pb-2">
                  <v-col>
                    <v-label class="text-h6 pb-3">Full Name</v-label>
                    <v-card-subtitle v-if="!isEdit">{{ clientInfo.fullName }}</v-card-subtitle>
                    <v-text-field
                      variant="outlined"
                      density="compact"
                      :error-messages="editClientDetails.fullName.errorMessage"
                      v-model="editClientDetails.fullName.value"
                      hide-details="auto"
                      v-else
                    />
                  </v-col>
                  <v-col>
                    <v-label class="text-h6 pb-3">Phone Number</v-label>
                    <v-card-subtitle v-if="!isEdit">{{ clientInfo.phoneNum ?? '-' }}</v-card-subtitle>
                    <v-text-field
                      variant="outlined"
                      density="compact"
                      :error-messages="editClientDetails.phoneNum.errorMessage"
                      v-model="editClientDetails.phoneNum.value"
                      hide-details="auto"
                      v-else
                    />
                  </v-col>
                  <v-col>
                    <v-label class="text-h6 pb-3">Email</v-label>
                    <v-card-subtitle v-if="!isEdit">{{ clientInfo.email ?? '-' }}</v-card-subtitle>
                    <v-text-field
                      variant="outlined"
                      density="compact"
                      :error-messages="editClientDetails.email.errorMessage"
                      v-model="editClientDetails.email.value"
                      hide-details="auto"
                      v-else
                    />
                  </v-col>
                </v-row>
                <v-row>
                  <v-col>
                    <v-label class="text-h6 pb-3">Address</v-label>
                    <v-card-subtitle v-if="!isEdit">{{ clientInfo.address ?? '-' }}</v-card-subtitle>
                    <v-text-field
                      variant="outlined"
                      density="compact"
                      :error-messages="editClientDetails.address.errorMessage"
                      v-model="editClientDetails.address.value"
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
                  <v-btn color="primary" variant="outlined" @click="editClientCancel">Cancel</v-btn>
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
import { AddressBookIcon } from "vue-tabler-icons"

// Data
const isEdit = ref(false)
const routeParameter = ref(useRoute().params)
const { data: clientInfo } = await fetchData.$get(`/Client/Info/${routeParameter.value.clientID}`)
const { handleSubmit } = useForm({
  initialValues: {
    fullName: clientInfo.value.fullName,
    phoneNum: clientInfo.value.phoneNum,
    email: clientInfo.value.email,
    address: clientInfo.value.address,
  },
  validationSchema: {
    fullName(value) {
      return value ? true : 'Full name is required'
    },
    phoneNum(value) {
      return true
    },
    email(value) {
      return true
    },
    address(value) {
      return true
    },
  }
})
const editClientDetails = ref({
  fullName: useField('fullName'),
  phoneNum: useField('phoneNum'),
  email: useField('email'),
  address: useField('address'),
})

// Head
useHead({
  title: 'Client Details | CaseCraft',
})

// Page Meta
definePageMeta({
  breadcrumbsIcon: shallowRef(AddressBookIcon),
  breadcrumbs: [
    {
      title: 'Contacts',
      disabled: false,
    },
    {
      title: 'Clients',
      disabled: false,
      href: '/contacts/clients',
    },
    {
      title: 'Client Details',
      disabled: false,
    },
  ],
})

// Methods
const editClientCancel = () => {
  isEdit.value = false
  editClientDetails.value.fullName.resetField()
  editClientDetails.value.phoneNum.resetField()
  editClientDetails.value.email.resetField()
  editClientDetails.value.address.resetField()
}
const editClient = handleSubmit(async(values) => {
  try {
    const result = await fetchData.$put(`/Client/Info/${clientInfo.value.clientId}/Edit`, values)
    if (!result.error) {
      isEdit.value = false
      editClientDetails.value.fullName.setValue(values.fullName)
      editClientDetails.value.phoneNum.setValue(values.phoneNum)
      editClientDetails.value.email.setValue(values.email)
      editClientDetails.value.address.setValue(values.address)
      ElNotification.success({ message: result.message })
      refreshNuxtData()
    }
    else {
      ElNotification.error({ message: result.message })
    }
  } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
})
// const deleteClient = async(clientId) => {
//   try {
//     const result = await fetchData.$delete(`/Client/Delete/${clientId}`)
//     if (!result.error) {
//       ElNotification.success({ message: result.message })
//       navigateTo('/contacts/clients')
//     }
//     else {
//       ElNotification.error({ message: result.message })
//     }
//   } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
// }
</script>