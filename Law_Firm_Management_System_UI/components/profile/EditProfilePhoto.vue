<template>
  <v-menu location="right" location-strategy="connected" :offset="10">
    <template #activator="{ props }">
      <v-btn
        class="position-absolute"
        color="grey100"
        icon="mdi-camera-outline"
        size="small"
        style="right: 0; bottom: 15px;"
        flat
        v-bind="props"
      />
    </template>
    <v-sheet rounded="md" width="230" elevation="10" :border="true">
      <v-list class="" lines="one" density="compact">
        <v-list-item color="primary" @click="uploadProfilePhotoModal = true">
          <v-list-item-title class="text-body-1">Change Profile Photo</v-list-item-title>
        </v-list-item>
        <v-list-item color="primary" @click="deleteProfilePhoto">
          <v-list-item-title class="text-body-1">Delete Profile Photo</v-list-item-title>
        </v-list-item>
      </v-list>
    </v-sheet>
  </v-menu>

  <SharedUiModal v-model="uploadProfilePhotoModal" title="Upload Profile Photo" width="500">
    <form @submit.prevent="addProfilePhoto">
      <v-card-text class="px-8 pt-4 pb-2 text-body-1 text-justify">
        <v-label class="text-caption">Choose Profile Photo:</v-label>
        <el-upload
          class="avatar-uploader"
          :show-file-list="false"
          :on-change="handleAvatarSuccess"
          :before-upload="uploadProfilePhoto"
        >
          <v-img height="180px" width="180px" :src="imageUrl" v-if="imageUrl"/>
          <v-icon class="avatar-uploader-icon" size="large" icon="mdi-image-plus-outline" v-else/>
        </el-upload>
        <v-messages
          class="ps-0"
          :messages="addProfilePhotoDetails.attachment.errorMessage"
          :active="addProfilePhotoDetails.attachment.errorMessage ? true : false"
          color="#fa896b"
          :transition="false"
          style="padding: 3px 16px 3px; opacity: unset;"
        />
      </v-card-text>
      <v-card-actions class="p-3 justify-content-end">
        <v-btn color="primary" type="submit">Submit</v-btn>
      </v-card-actions>
    </form>
  </SharedUiModal>
</template>

<script setup>
import { useField, useForm } from 'vee-validate'

// Data
const imageUrl = ref('')
const uploadProfilePhotoModal = ref(false)
const { handleSubmit } = useForm({
  validationSchema: {
    attachment(value) {
      if (!value)
        return 'Profile photo is required'
      if (addProfilePhotoDetails.value.type != 'image/jpeg')
        return 'Profile photo must be JPG format'
      else if (addProfilePhotoDetails.value.size / 1024 / 1024 > 5)
        return 'Profile photo size can not exceed 5MB'
      return true
    }
  }
})
const addProfilePhotoDetails = ref({
  attachment: useField('attachment'),
  size: null,
  type: null,
})

// Methods
const handleAvatarSuccess = (uploadFile) => {
  imageUrl.value = URL.createObjectURL(uploadFile.raw)
}
const uploadProfilePhoto = async(file) => {
  if (file) {
    // Read file as DataURL using a promise-based approach
    const reader = new FileReader()
    reader.readAsDataURL(file)
    try {
      const base64Data = await new Promise((resolve, reject) => {
        reader.onload = () => resolve(reader.result)
        reader.onerror = reject
      })
      addProfilePhotoDetails.value.attachment.value = base64Data.replace(/^.+?;base64,/, '')
      addProfilePhotoDetails.value.size = file.size
      addProfilePhotoDetails.value.type = file.type
    } catch(e) { ElNotification.error({ message: `Error reading file: ${e}` }) }
  }
  else
  {
    addProfilePhotoDetails.value.attachment.value = null
    addProfilePhotoDetails.value.type = null
  }
}
const addProfilePhoto = handleSubmit(async(values) => {
  try {
    const result = await fetchData.$put("/User/Me/ProfilePhoto", {
      profilePhoto: values.attachment
    })

    if (!result.error) {
      uploadProfilePhotoModal.value = false
      imageUrl.value = null
      addProfilePhotoDetails.value.attachment.resetField()
      addProfilePhotoDetails.value.type = null
      ElNotification.success({ message: result.message })
      refreshNuxtData()
    }
    else {
      ElNotification.error({ message: result.message })
    }
  } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
})
const deleteProfilePhoto = async() => {
  try {
    const result = await fetchData.$put("/User/Me/ProfilePhoto/Delete")
    if (!result.error) {
      ElNotification.success({ message: result.message })
      refreshNuxtData()
    }
    else {
      ElNotification.error({ message: result.message })
    }
  } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
}
</script>