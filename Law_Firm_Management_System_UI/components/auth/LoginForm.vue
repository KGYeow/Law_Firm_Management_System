<template>
  <v-row class="d-flex mb-3">
    <v-col cols="12">
      <v-label class="font-weight-bold mb-1">Username</v-label>
      <v-text-field variant="outlined" hide-details color="primary" v-model="loginDetails.username"></v-text-field>
    </v-col>
    <v-col cols="12">
      <v-label class="font-weight-bold mb-1">Password</v-label>
      <v-text-field variant="outlined" type="password" hide-details color="primary" v-model="loginDetails.password"></v-text-field>
    </v-col>
    <v-col cols="12" class="pt-0">
      <div class="d-flex flex-wrap align-center ml-n2">
        <v-checkbox v-model="checkbox"  color="primary" hide-details>
          <template v-slot:label class="text-body-1">Remeber this Device</template>
        </v-checkbox>
        <div class="ml-sm-auto">
          <NuxtLink to="/"
            class="text-primary text-decoration-none text-body-1 opacity-1 font-weight-medium">Forgot
            Password ?</NuxtLink>
        </div>
      </div>
    </v-col>
    <v-col cols="12" class="pt-0">
      <v-btn color="primary" size="large" block flat @click="login">Sign in</v-btn>
    </v-col>
  </v-row>
</template>

<script setup>
// Data
const { signIn } = useAuth()
const checkbox = ref(true);
const loginDetails = ref({
  username: null,
  password: null,
})

// Methods
const login = async() => {
  try {
    const result = await fetchData.$post("/Authenticate/LoginInfo", loginDetails.value)
    if (!result.error) {
      await signIn(loginDetails.value, { callbackUrl: "/" })
      ElNotification.success({ message: result.message })
    }
    else {
      ElNotification.error({ message: result.message })
    }
  } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
}
</script>