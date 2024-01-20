<template>
  <v-row>
    <v-col cols="12" md="12">
      <v-card elevation="10" class="withbg overflow-hidden">
        <div class="position-relative">
          <!-- Cover Image -->
          <v-img
            height="220"
            src="/images/background/default-cover.jpg"
            cover
          />
          <div class="position-absolute" style="left: 40px; bottom: -50px;">
            <!-- Profile Image -->
            <v-avatar
              class="border"
              :image="user.profilePhoto ? getProfilePhoto(user.profilePhoto) : '/images/users/avatar.jpg'"
              size="140"
              style="border-width: 5px !important; border-color: white !important;"
            />
            <ProfileEditProfilePhoto/>
          </div>
        </div>
        <v-card-item class="pt-2 pb-6">
          <v-card-title class="text-h4 font-weight-black text-wrap" style="margin-left: 175px;">{{ user.fullName }}</v-card-title>
        </v-card-item>
      </v-card>
    </v-col>
  </v-row>
  <v-row>
    <v-col cols="12" md="12">
      <v-card elevation="10" class="withbg">
        <v-tabs
          class="mx-8 mb-5 border-bottom"
          selected-class="text-secondary"
          v-model="tab"
        >
          <v-tab value="1">Profile</v-tab>
          <v-tab value="2">{{ userRole }}</v-tab>
          <v-tab value="3">Password</v-tab>
        </v-tabs>
        <v-window v-model="tab">
          <v-window-item value="1">
            <ProfileEditForm :user-info="user" :role="userRole"/>
          </v-window-item>
          <v-window-item value="2">
            <ProfileEditPartnerForm :role-info="userRoleInfo" v-if="userRole == 'Partner'"/>
            <ProfileEditParalegalForm :role-info="userRoleInfo" v-else-if="userRole == 'Paralegal'"/>
            <ProfileEditClientForm :role-info="userRoleInfo" v-else/>
          </v-window-item>
          <v-window-item value="3">
            <ProfileEditPasswordForm :user-info="user"/>
          </v-window-item>
        </v-window>
      </v-card>
    </v-col>
  </v-row>
</template>

<script setup>
import { UserIcon } from "vue-tabler-icons"
import { Buffer } from 'buffer'

// Data
const tab = ref(null)
const { data: user } = await fetchData.$get("/User/Me")
const { data: userRole } = await fetchData.$get("/UserRole/RoleName")
const { data: userRoleInfo } = await fetchData.$get("/User/Me/Info/Role")

// Head
useHead({
  title: `${user.value.fullName} | CaseCraft`,
})

// Page Meta
definePageMeta({
  breadcrumbsIcon: shallowRef(UserIcon),
  breadcrumbs: [
    {
      title: 'My Profile',
      disabled: false,
    },
  ],
})

// Methods
const getProfilePhoto = (attachment) => {
  const arrayBuffer = Buffer.from(attachment, 'base64');
  const blob = new Blob([arrayBuffer], { type: 'image/jpeg' })
  return URL.createObjectURL(blob)
}
</script>