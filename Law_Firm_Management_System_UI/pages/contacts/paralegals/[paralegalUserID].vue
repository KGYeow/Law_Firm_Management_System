<template>
  <v-row>
    <v-col cols="12" md="12">
      <v-card elevation="10" class="withbg">
        <div class="hstack justify-content-center align-items-stretch">
          <div class="p-4 w-25 text-center">
            <!-- Client Profile Image -->
            <v-avatar
              class="border my-5"
              :image="info.paralegalInfo.profilePhoto ? getProfilePhoto(info.paralegalInfo.profilePhoto) : '/images/users/avatar.jpg'"
              size="110"
              style="border-width: 3px !important; border-color: lightgrey !important;"
            />
            <div class="mb-2 text-h5 d-sm-flex align-center justify-content-center">
              {{ info.paralegalInfo.fullName }}
            </div>
            <div>
              <el-tag type="success" v-if="info.paralegalInfo.isActive">Active</el-tag>
              <el-tag type="danger" v-else>Inactive</el-tag>
            </div>
          </div>
          <v-divider vertical/>
          <div class="p-4 w-75">
            <v-card-title class="text-h6 text-body-tertiary">
              Paralegal Information
            </v-card-title>
            <v-card-item>
              <v-row>
                <v-col cols="3" class="d-flex align-center">
                  <v-label class="text-h6 pb-3">Full Name</v-label>
                </v-col>
                <v-col>
                  <v-card-subtitle class="p-0">{{ info.paralegalInfo.fullName }}</v-card-subtitle>
                </v-col>
              </v-row>
              <v-row>
                <v-col cols="3" class="d-flex align-center">
                  <v-label class="text-h6 pb-3">Email</v-label>
                </v-col>
                <v-col>
                  <v-card-subtitle class="p-0">{{ info.paralegalInfo.email }}</v-card-subtitle>
                </v-col>
              </v-row>
              <v-row>
                <v-col cols="3" class="d-flex align-center">
                  <v-label class="text-h6 pb-3">Phone Number</v-label>
                </v-col>
                <v-col>
                  <v-card-subtitle class="p-0">{{ info.paralegalInfo.phoneNum ?? '-' }}</v-card-subtitle>
                </v-col>
              </v-row>
              <v-row>
                <v-col cols="3" class="d-flex align-center">
                  <v-label class="text-h6 pb-3">Address</v-label>
                </v-col>
                <v-col>
                  <v-card-subtitle class="p-0">{{ info.paralegalInfo.address ?? '-' }}</v-card-subtitle>
                </v-col>
              </v-row>
              <v-row>
                <v-col cols="3" class="d-flex align-center">
                  <v-label class="text-h6 pb-3">Assigned Partner</v-label>
                </v-col>
                <v-col>
                  <v-card-subtitle class="p-0">
                    <a :href="`/contacts/partners/${info.assignedPartner.userId}`" target="_blank" class="text-secondary" v-if="info.assignedPartner">{{ info.assignedPartner.fullName }}</a>
                    <span v-else>-</span>
                  </v-card-subtitle>
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
const { data: info } = await fetchData.$get(`/Paralegal/Info/${routeParameter.value.paralegalUserID}`)

// Head
useHead({
  title: 'Paralegal Details | CaseCraft',
})

// Page Meta
definePageMeta({
  breadcrumbsIcon: shallowRef(SettingsIcon),
  breadcrumbs: [
    {
      title: 'Contacts',
      disabled: false,
    },
    {
      title: 'Paralegals',
      disabled: false,
      href: '/contacts/paralegals',
    },
    {
      title: 'Paralegal Details',
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