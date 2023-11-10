<template>
  <v-row>
    <v-col cols="12" md="12">
      <UiParentCard title="My Profile"> 
        <div class="pa-7 pt-1 text-body-1">
          <form @submit.prevent="editUser">
            <div class="row">
              <label class="col-2 my-2 fw-bold">Full Name</label>
              <div class="col my-2" v-if="!isEdit">{{ user.fullName }}</div>
              <div class="col-5" v-else>
                <input class="form-control my-1" type="text" v-model="editUserDetails.fullName">
              </div>
            </div>
            <div class="row">
              <label class="col-2 my-2 fw-bold">Username</label>
              <div class="col my-2" v-if="!isEdit">{{ user.username }}</div>
              <div class="col-5" v-else>
                <input class="form-control my-1" type="text" v-model="editUserDetails.username">
              </div>
            </div>
            <div class="row">
              <label class="col-2 my-2 fw-bold">User ID</label>
              <div class="col my-2">{{ user.id }}</div>
            </div>
            <div class="row">
              <label class="col-2 my-2 fw-bold">Email Address</label>
              <div class="col my-2" v-if="!isEdit">{{ user.email }}</div>
              <div class="col-5" v-else>
                <input class="form-control my-1" type="text" v-model="editUserDetails.email">
              </div>
            </div>
            <div class="mt-2">
              <v-btn class="bg-primary" @click="isEdit = !isEdit" v-if="!isEdit">Edit Profile</v-btn>
              <v-btn class="bg-primary" type="submit" v-else>Update Profile</v-btn>
            </div>
          </form>
        </div>
      </UiParentCard>
    </v-col>
  </v-row>
</template>

<script setup>
import UiParentCard from '@/components/shared/UiParentCard.vue'

// Data
const { data: user } = useAuth()
const isEdit = ref(false)
const editUserDetails = ref({
  username: user.value.username,
  fullName: user.value.fullName,
  email: user.value.email
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
</script>