<template>
  <div>
    <h5 class="text-h5 mb-6 pl-7 pt-7 ps-0 d-flex align-center">
      <v-row>
        <v-col class="d-flex align-center">
          <LayoutFullVerticalSidebarIcon class="me-3" :item="SpeakerphoneIcon"/>
          Announcements
        </v-col>
        <v-col cols="8" v-if="userRole == 'Partner'">
          <v-btn class="float-end" color="primary" prepend-icon="mdi-plus" flat @click="addAnnouncementModal = true">New Announcement</v-btn>
        </v-col>
      </v-row>
    </h5>
    <div class="text-body-1">
      <v-card elevation="10">
        <v-carousel height="200px" color="primary" hide-delimiter-background delimiter-icon="mdi-minus">
          <template #prev="{ props }">
            <v-btn variant="text" v-bind="props" @click="props.onClick"/>
          </template>
          <template #next="{ props }">
            <v-btn variant="text" v-bind="props" @click="props.onClick"/>
          </template>
          <v-carousel-item v-for="item in announcement" height="200px" max-height="200px">
            <div class="px-16 pt-5 pb-6 h-100">
              <h6 class="m-0 fw-bold">
                {{ item.title }}
              </h6>
              <span class="text-subtitle-2">
                by {{ item.partnerFullName }} | {{ dayjs(item.createdTime).format("D MMM YYYY, h:mm A") }}
              </span>
              <div ref="announcementRef" class="p-0 pt-2 text-justify announcement">
                {{ item.description }}
              </div>
            </div>
            <v-tooltip text="See More" location="top" offset="2">
              <template #activator="{ props }">
                <v-btn class="m-2 position-absolute top-0 end-0" icon="mdi-open-in-new" size="small" variant="text" v-bind="props" @click="announcementDataGet(item)"/>
              </template>
            </v-tooltip>
          </v-carousel-item>
        </v-carousel>
      </v-card>
    </div>
  </div>

  <!-- Announcement See More Modal -->
  <SharedUiModal v-model="announcementModal" title="Announcement" width="600">
    <v-card-item class="px-8 py-4 text-body-1">
      <v-sheet>
        <h6 class="m-0 fw-bold">
          {{ announcementSeeMore.title }}
        </h6>
        <span class="text-subtitle-2">
          by {{ announcementSeeMore.partnerFullName }} | {{ dayjs(announcementSeeMore.createdTime).format("D MMM YYYY, h:mm A") }}
        </span>
        <div ref="announcementRef" class="p-0 pt-2 text-justify">
          {{ announcementSeeMore.description }}
        </div>
      </v-sheet>
    </v-card-item>
    <v-card-actions class="p-3 justify-content-end" v-if="announcementSeeMore.partnerUserId == user.id">
      <el-popconfirm
        title="Are you sure to delete this announcement?"
        icon-color="red"
        width="190"
        :teleported="false"
        @confirm="deleteAnnouncement(announcementSeeMore.id)"
      >
        <template #reference>
          <v-btn color="danger">Delete</v-btn>
        </template>
      </el-popconfirm>
    </v-card-actions>
  </SharedUiModal>

  <!-- Add Announcement Modal -->
  <SharedUiModal v-model="addAnnouncementModal" title="Add New Announcement" width="500">
    <form @submit.prevent="addAnnouncement">
      <v-card-item class="px-8 py-4 text-body-1">
        <v-row>
          <v-col>
            <v-label class="text-caption">Title</v-label>
            <v-text-field
              variant="outlined"
              density="compact"
              :error-messages="addAnnouncementDetails.title.errorMessage"
              v-model="addAnnouncementDetails.title.value"
              hide-details="auto"
            />
          </v-col>
        </v-row>
        <v-row>
          <v-col>
            <v-label class="text-caption">Description</v-label>
            <v-text-field
              variant="outlined"
              density="compact"
              :error-messages="addAnnouncementDetails.description.errorMessage"
              v-model="addAnnouncementDetails.description.value"
              hide-details="auto"
            />
          </v-col>
        </v-row>
      </v-card-item>
      <v-card-actions class="p-3 justify-content-end">
        <v-btn color="primary" type="submit">Submit</v-btn>
      </v-card-actions>
    </form>
  </SharedUiModal>
</template>

<script setup>
import { SpeakerphoneIcon } from 'vue-tabler-icons'
import { useField, useForm } from 'vee-validate'
import dayjs from 'dayjs'

// Data
const { data: user } = useAuth()
const { handleSubmit } = useForm({
  validationSchema: {
    title(value) {
      return value ? true : 'Announcement title is required'
    },
    description(value) {
      return value ? true : 'Announcement description is required'
    }
  }
})
const announcementModal = ref(false)
const addAnnouncementModal = ref(false)
const announcementSeeMore = ref({
  id: null,
  title: null,
  partnerUserId: null,
  partnerFullName: null,
  createdTime: null,
  description: null,
})
const addAnnouncementDetails = ref({
  title: useField('title'),
  description: useField('description'),
})
const { data: announcement } = fetchData.$get("/Dashboard/Announcement")
const { data: userRole } = await fetchData.$get("/UserRole/RoleName")

// Methods
const announcementDataGet = (data) => {
  announcementSeeMore.value.id = data.id
  announcementSeeMore.value.title = data.title
  announcementSeeMore.value.partnerUserId = data.partnerUserId
  announcementSeeMore.value.partnerFullName = data.partnerFullName
  announcementSeeMore.value.createdTime = data.createdTime
  announcementSeeMore.value.description = data.description
  announcementModal.value = true
}
const addAnnouncement = handleSubmit(async(values) => {
  try {
    const result = await fetchData.$post("/Dashboard/Announcement/Post", values)
    
    if (!result.error) {
      addAnnouncementModal.value = false
      addAnnouncementDetails.value.title.resetField()
      addAnnouncementDetails.value.description.resetField()
      ElNotification.success({ message: result.message })
      refreshNuxtData()
    }
    else {
      ElNotification.error({ message: result.message })
    }
  } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
})
const deleteAnnouncement = async(announcementId) => {
  try {
    const result = await fetchData.$delete(`/Dashboard/Announcement/Delete/${announcementId}`)
    if (!result.error) {
      announcementModal.value = false
      ElNotification.success({ message: result.message })
      refreshNuxtData()
    }
    else {
      ElNotification.error({ message: result.message })
    }
  } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
}
</script>