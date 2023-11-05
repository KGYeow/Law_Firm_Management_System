<template>
  <v-row class="d-flex mb-3">
    <v-col cols="12">
      <v-label class="font-weight-bold mb-1">Username</v-label>
      <v-text-field variant="outlined" hide-details color="primary" v-model="registrationDetails.username"></v-text-field>
    </v-col>
    <v-col cols="12">
      <v-label class="font-weight-bold mb-1">Email Address</v-label>
      <v-text-field variant="outlined" type="email" hide-details color="primary" v-model="registrationDetails.email"></v-text-field>
    </v-col>
    <v-col cols="12">
      <v-label class="font-weight-bold mb-1">Password</v-label>
      <v-text-field variant="outlined" type="password"  hide-details color="primary" v-model="registrationDetails.password"></v-text-field>
    </v-col>
    <v-col cols="12" >
      <v-btn color="primary" size="large" block flat @click="register">Sign up</v-btn>
    </v-col>
  </v-row>
</template>

<script setup>
// Data
const { signIn } = useAuth()
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