<template>
  <v-form @submit.prevent="login">
    <v-row class="d-flex mb-3">
      <v-col class="pb-0" cols="12">
        <v-text-field
          placeholder="Username"
          prepend-inner-icon="mdi-account fs-5"
          density="compact"
          variant="outlined"
          hide-details="auto"
          color="primary"
          :error-messages="loginDetails.username.errorMessage"
          v-model="loginDetails.username.value"
        ></v-text-field>
      </v-col>
      <v-col cols="12">
        <v-text-field
          placeholder="Password"
          prepend-inner-icon="mdi-lock fs-5"
          :append-inner-icon="passwordVisible ? 'mdi-eye-off fs-5' : 'mdi-eye fs-5'"
          :type="passwordVisible ? 'text' : 'password'"
          density="compact"
          variant="outlined"
          hide-details="auto"
          color="primary"
          :error-messages="loginDetails.password.errorMessage"
          v-model="loginDetails.password.value"
          @click:append-inner="passwordVisible = !passwordVisible"
        ></v-text-field>
      </v-col>
      <v-col cols="12" class="pt-0">
        <div class="d-flex flex-wrap align-center ml-n2">
          <v-checkbox v-model="checkbox"  color="primary" hide-details>
            <template v-slot:label class="text-body-1">Remember this Device</template>
          </v-checkbox>
          <div class="ml-sm-auto">
            <NuxtLink to="/"
              class="text-primary text-decoration-none text-body-1 opacity-1 font-weight-medium">Forgot
              Password ?</NuxtLink>
          </div>
        </div>
      </v-col>
      <v-col cols="12" class="pt-0">
        <v-btn color="primary" size="large" type="submit" block flat>Sign in</v-btn>
      </v-col>
    </v-row>
  </v-form>
</template>

<script setup>
import { useField, useForm } from 'vee-validate'

// Data
const { signIn } = useAuth()
const passwordVisible = ref(false)
const checkbox = ref(true)
const { handleSubmit } = useForm({
  validationSchema: {
    username(value) {
      return value ? true : 'Username is required'
    },
    password(value){
      return value ? true : 'Password is required'
    }
  }
})
const loginDetails = ref({
  username: useField('username'),
  password: useField('password'),
})

// Methods
const login = handleSubmit(async(values) => {
  try {
    const result = await fetchData.$post("/Authenticate/LoginInfo", values)
    if (!result.error) {
      await signIn(values, { callbackUrl: "/" })
      ElNotification.success({ message: result.message })
    }
    else {
      ElNotification.error({ message: result.message })
    }
  } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
})
</script>