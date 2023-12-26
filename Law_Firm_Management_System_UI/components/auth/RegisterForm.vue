<template>
  <v-form @submit.prevent="register">
    <v-row class="d-flex mb-3">
      <v-col cols="12">
        <v-label class="font-weight-bold mb-1">Username</v-label>
        <v-text-field
          density="compact"
          variant="outlined"
          hide-details="auto"
          color="primary"
          :error-messages="registrationDetails.username.errorMessage"
          v-model="registrationDetails.username.value"
        ></v-text-field>
      </v-col>
      <v-col cols="12">
        <v-label class="font-weight-bold mb-1">Email Address</v-label>
        <v-text-field
          density="compact"
          variant="outlined"
          type="email"
          hide-details="auto"
          color="primary"
          :error-messages="registrationDetails.email.errorMessage"
          v-model="registrationDetails.email.value"
        ></v-text-field>
      </v-col>
      <v-col cols="12">
        <v-label class="font-weight-bold mb-1">Password</v-label>
        <v-text-field
          :append-inner-icon="passwordVisible ? 'mdi-eye-off' : 'mdi-eye'"
          :type="passwordVisible ? 'text' : 'password'"
          density="compact"
          variant="outlined"
          hide-details="auto"
          color="primary"
          :error-messages="registrationDetails.password.errorMessage"
          v-model="registrationDetails.password.value"
          @click:append-inner="passwordVisible = !passwordVisible"
        ></v-text-field>
      </v-col>
      <v-col cols="12" >
        <v-btn color="primary" size="large" type="submit" block flat>Sign up</v-btn>
      </v-col>
    </v-row>
  </v-form>
</template>

<script setup>
import { useField, useForm } from 'vee-validate'

// Data
const { signIn } = useAuth()
const passwordVisible = ref(false)
const { handleSubmit } = useForm({
  validationSchema: {
    username(value) {
      return value ? true : 'Username is required'
    },
    email(value) {
      return value ? true : 'Email address is required'
    },
    password(value){
      return value ? true : 'Password is required'
    }
  }
})
const registrationDetails = ref({
  username: useField('username'),
  email: useField('email'),
  password: useField('password'),
})

// Methods
const register = handleSubmit(async(values) => {
  try {
    const result = await fetchData.$post("/Authenticate/Register", values)
    if (!result.error) {
      await signIn({
        username: values.username,
        password: values.password
      }, { callbackUrl: "/" })
      ElNotification.success({ message: result.message })
    }
    else {
      ElNotification.error({ message: result.message })
    }
  } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
})
</script>