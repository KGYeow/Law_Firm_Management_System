<template>
  <h5 class="text-h5 mb-6 pl-7 pt-7 ps-0 d-flex align-center">
    Partners
  </h5>
  <v-row>
    <v-col cols="3" v-for="partner in partnerList">
      <v-card elevation="10" class="withbg" link :href="`/partners/${partner.userId}`" target="_blank">
        <v-card-item>
          <div class="d-flex justify-content-center pt-sm-2">
            <v-card-title class="text-h5 d-flex flex-column align-center">
              <v-avatar
                class="mb-3"
                :image="partner.profilePhoto ? getProfilePhoto(partner.profilePhoto) : '/images/users/avatar.jpg'"
                size="80"
              />
              {{ partner.fullName }}
            </v-card-title>
          </div>
          <v-divider/>
          <el-scrollbar class="text-body-1" height="150px">
            <v-list density="compact">
              <v-list-item>
                <template #prepend>
                  <i class="mdi mdi-phone me-5"></i>
                </template>
                <span v-if="partner.phoneNumber">{{ partner.phoneNumber }}</span>
                <span v-else>-</span>
              </v-list-item>
              <v-list-item>
                <template #prepend>
                  <i class="mdi mdi-email me-5"></i>
                </template>
                {{ partner.email }}
              </v-list-item>
            </v-list>
          </el-scrollbar>
        </v-card-item>
      </v-card>
    </v-col>
  </v-row>
</template>

<script setup>
import { ScaleIcon } from "vue-tabler-icons"
import { Buffer } from 'buffer'

// Data
const { data: partnerList } = await fetchData.$get("/Partner")

// Head
useHead({
  title: "Partners | CaseCraft",
})

// Page Meta
definePageMeta({
  breadcrumbsIcon: shallowRef(ScaleIcon),
  breadcrumbs: [
    {
      title: 'Partners',
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