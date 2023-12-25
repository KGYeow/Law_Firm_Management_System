<template>
  <v-row class="d-flex mb-3">
    <v-col cols="12">
      <v-label class="font-weight-bold mb-1">Username</v-label>
      <v-text-field density="compact" variant="outlined" hide-details color="primary" v-model="registrationDetails.username"></v-text-field>
    </v-col>
    <v-col cols="12">
      <v-label class="font-weight-bold mb-1">Email Address</v-label>
      <v-text-field density="compact" variant="outlined" type="email" hide-details color="primary" v-model="registrationDetails.email"></v-text-field>
    </v-col>
    <v-col cols="12">
      <v-label class="font-weight-bold mb-1">Password</v-label>
      <v-text-field
        :append-inner-icon="passwordVisible ? 'mdi-eye-off' : 'mdi-eye'"
        :type="passwordVisible ? 'text' : 'password'"
        density="compact"
        variant="outlined"
        hide-details
        color="primary"
        v-model="registrationDetails.password"
        @click:append-inner="passwordVisible = !passwordVisible"
      ></v-text-field>
    </v-col>
    <v-col cols="12" >
      <v-btn color="primary" size="large" block flat @click="register">Sign up</v-btn>
    </v-col>
  </v-row>
</template>

<script setup>
// Data
const { signIn } = useAuth()
const passwordVisible = ref(false)
const registrationDetails = ref({
  username: null,
  email: null,
  password: null,
})

// Methods
const register = async() => {
  try {
    const result = await fetchData.$post("/Authenticate/Register", registrationDetails.value)

    if (!result.error) {
      await signIn({
        username: registrationDetails.value.username,
        password: registrationDetails.value.password
      }, { callbackUrl: "/" })
      ElNotification.success({ message: result.message })
    }
    else {
      ElNotification.error({ message: result.message })
    }
  } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
}
</script>