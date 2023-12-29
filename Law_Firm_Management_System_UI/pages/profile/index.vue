<template>
  <v-row>
    <v-col cols="12" md="12">
      <v-card elevation="0" class="bg-transparent">
        <!-- Cover Image -->
        <div>
          <v-img
            height="200"
            src="/images/background/default-cover.jpg"
            cover
          ></v-img>
        </div>
        <form @submit.prevent="editUser">
          <v-card-item class="position-relative pt-4">
            <!-- Profile Image -->
            <v-avatar
              class="border position-absolute"
              image="/images/users/avatar.jpg"
              size="110"
              style="border-width: 5px !important; border-color: white !important; left: 40px; bottom: 0px;"
            />
            <div class="d-flex justify-content-between">
              <v-card-title class="fs-3 fw-bold" style="margin-left: 155px;">{{ user.fullName }}</v-card-title>
              <div>
                <v-btn color="primary" elevation="0" @click="isEdit = !isEdit" v-if="!isEdit">Edit Profile</v-btn>
                <div v-else>
                  <v-btn color="primary" variant="outlined" @click="editUserCancel">Cancel</v-btn>
                  <v-btn color="primary" class="ms-2" elevation="0" type="submit">Save Profile</v-btn>
                </div>
              </div>
            </div>
          </v-card-item>
          <v-card-text class="pb-7 pt-10 px-10 text-body-1">
            <div style="width: 500px;">
              <div class="row">
                <div class="col">
                  <label class="my-1 fw-bold">User ID</label>
                  <v-text-field variant="outlined" density="compact" v-model="user.id" disabled/>
                </div>
                <div class="col">
                  <label class="my-1 fw-bold">Role</label>
                  <v-text-field variant="outlined" density="compact" v-model="role" disabled/>
                </div>
              </div>
              <div class="row">
                <label class="my-1 fw-bold">Full Name</label>
                <v-text-field variant="outlined" density="compact" v-model="editUserDetails.fullName" :disabled="!isEdit"/>
              </div>
              <div class="row">
                <label class="my-1 fw-bold">Username</label>
                <v-text-field variant="outlined" density="compact" v-model="editUserDetails.username" :disabled="!isEdit"/>
              </div>
              <div class="row">
                <label class="my-1 fw-bold">Email Address</label>
                <v-text-field variant="outlined" density="compact" v-model="editUserDetails.email" :disabled="!isEdit"/>
              </div>
            </div>
          </v-card-text>
        </form>
      </v-card>
    </v-col>
  </v-row>
</template>

<script setup>
// Data
const { data: user } = useAuth()
const { data: role } = fetchData.$get(`/UserRole/RoleName/${user.value?.userRoleId}`)
const isEdit = ref(false)
const editUserDetails = ref({
  username: user.value.username,
  fullName: user.value.fullName,
  email: user.value.email
})

// Head
useHead({
  title: `${user.value.fullName} | CaseCraft`,
})

// Methods
const editUser = async() => {
  try {
    const result = await fetchData.$put(`/User/Me/${user.value.id}`, editUserDetails.value)
    if (!result.error) {
      isEdit.value = !isEdit.value
      ElNotification.success({ message: result.message })
    }
    else {
      ElNotification.error({ message: "The user profile has been edited unsuccessfully." })
    }
  } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
}
const editUserCancel = () => {
  isEdit.value = !isEdit.value
  editUserDetails.value.username = user.value.username
  editUserDetails.value.fullName = user.value.fullName
  editUserDetails.value.email = user.value.email
}
</script>