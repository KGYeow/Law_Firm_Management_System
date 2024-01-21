<template>
  <v-menu :close-on-content-click="false">
    <template v-slot:activator="{ props }">
      <v-btn class="profileBtn custom-hover-primary" variant="text" v-bind="props" icon>
        <v-avatar size="35" :image="user.profilePhoto ? getProfilePhoto(user.profilePhoto) : '/images/users/avatar.jpg'"/>
      </v-btn>
    </template>
    <v-sheet rounded="md" width="200" elevation="10" class="mt-2" :border="true">
      <v-list class="pb-0" lines="one" density="compact">
        <v-list-item value="item1" color="primary" to="/profile">
          <template v-slot:prepend>
            <UserIcon stroke-width="1.5" size="20"/>
          </template>
          <v-list-item-title class="pl-4 text-body-1">My Profile</v-list-item-title>
        </v-list-item>
      </v-list>
      <div class="pt-4 pb-4 px-5 text-center">
        <v-btn color="primary" variant="outlined" block @click="logout">Logout</v-btn>
      </div>
    </v-sheet>
  </v-menu>
</template>

<script setup>
import { Buffer } from 'buffer'

// Data
const { signOut } = useAuth()
const { data: user } = await fetchData.$get("/User/Me")

// Methods
const getProfilePhoto = (attachment) => {
  const arrayBuffer = Buffer.from(attachment, 'base64');
  const blob = new Blob([arrayBuffer], { type: 'image/jpeg' })
  return URL.createObjectURL(blob)
}
const logout = async() => {
  try {
    const result = await fetchData.$get("/Authenticate/Logout")
    if (!result.error.value) {
      signOut({ callbackUrl: "/login", redirect: true })
      ElNotification.success({ message: "User logout successfully!" })
    }
  } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
}
</script>