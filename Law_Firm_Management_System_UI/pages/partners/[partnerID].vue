<template>
  <v-row>
    <v-col cols="12" md="12">
      <v-card elevation="10" class="withbg">
        <div class="hstack justify-content-center align-items-stretch">
          <div class="p-4 w-25 text-center">
            <!-- Client Profile Image -->
            <v-avatar
              class="border my-5"
              :image="partnerInfo.profilePhoto ? getProfilePhoto(partnerInfo.profilePhoto) : '/images/users/avatar.jpg'"
              size="110"
              style="border-width: 3px !important; border-color: lightgrey !important;"
            />
            <div class="mb-2 text-h5 d-sm-flex align-center justify-content-center">
              {{ partnerInfo.fullName }}
            </div>
          </div>
          <v-divider vertical/>
          <div class="p-4 w-75">
            <v-card-title class="text-h6 text-body-tertiary">
              Partner Information
            </v-card-title>
            <v-card-item>
              <v-row>
                <v-col cols="3" class="d-flex align-center">
                  <v-label class="text-h6 pb-3">Full Name</v-label>
                </v-col>
                <v-col>
                  <v-card-subtitle class="p-0">{{ partnerInfo.fullName }}</v-card-subtitle>
                </v-col>
              </v-row>
              <v-row>
                <v-col cols="3" class="d-flex align-center">
                  <v-label class="text-h6 pb-3">Email</v-label>
                </v-col>
                <v-col>
                  <v-card-subtitle class="p-0">{{ partnerInfo.email }}</v-card-subtitle>
                </v-col>
              </v-row>
              <v-row>
                <v-col cols="3" class="d-flex align-center">
                  <v-label class="text-h6 pb-3">Phone Number</v-label>
                </v-col>
                <v-col>
                  <v-card-subtitle class="p-0">{{ partnerInfo.phoneNum ?? '-' }}</v-card-subtitle>
                </v-col>
              </v-row>
              <v-row>
                <v-col cols="3" class="d-flex align-center">
                  <v-label class="text-h6 pb-3">Address</v-label>
                </v-col>
                <v-col>
                  <v-card-subtitle class="p-0">{{ partnerInfo.address ?? '-' }}</v-card-subtitle>
                </v-col>
              </v-row>
            </v-card-item>
          </div>
        </div> 
      </v-card>
    </v-col>
  </v-row>
</template>

<script setup>
import { SettingsIcon } from 'vue-tabler-icons'
import { Buffer } from 'buffer'

// Data
const routeParameter = ref(useRoute().params)
const { data: partnerInfo } = await fetchData.$get(`/Partner/Info/${routeParameter.value.partnerID}`)

// Head
useHead({
  title: 'Partner Details | CaseCraft',
})

// Page Meta
definePageMeta({
  breadcrumbsIcon: shallowRef(SettingsIcon),
  breadcrumbs: [
    {
      title: 'Partners',
      disabled: false,
      href: '/partners',
    },
    {
      title: 'Partner Details',
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