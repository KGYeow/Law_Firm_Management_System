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
              image="/images/users/avatar.jpg"
              size="140"
              style="border-width: 5px !important; border-color: white !important;"
            />
            <v-btn
              class="position-absolute"
              color="grey100"
              icon="mdi-camera-outline"
              size="small"
              style="right: 0; bottom: 15px;"
              flat
              @click="uploadProfilePhotoModal = true"
            />
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

  <SharedUiModal v-model="uploadProfilePhotoModal" title="Upload Profile Photo" width="500">
    <v-card-text class="px-8 py-4 text-body-1 text-justify">
      <el-upload
        class="avatar-uploader"
        :show-file-list="false"
        :on-success="handleAvatarSuccess"
        :before-upload="uploadProfilePhoto"
      >
        <img v-if="imageUrl" :src="imageUrl" class="avatar"/>
        <el-icon v-else class="avatar-uploader-icon"><i class="mdi mdi-image-plus-outline"/></el-icon>
      </el-upload>
    </v-card-text>
    <v-card-actions class="p-3 justify-content-end">
      <v-btn color="primary" type="submit">Submit</v-btn>
    </v-card-actions>
  </SharedUiModal>
</template>

<script setup>
import { UserIcon } from "vue-tabler-icons"
import { useField, useForm } from 'vee-validate'

// Data
const tab = ref(null)
const imageUrl = ref('')
const uploadProfilePhotoModal = ref(false)
const { data: user } = await fetchData.$get("/User/Me")
const { data: userRole } = await fetchData.$get("/UserRole/RoleName")
const { data: userRoleInfo } = await fetchData.$get("/User/Me/Info/Role")
const { handleSubmit } = useForm({
  validationSchema: {
    attachment(value) {
      if (!value)
        return 'Document is required'

      const fileSize = (value.length * 3) / 4 / 1024 // Convert base64 size to KB
      if (fileSize > 28000)
        return 'Document size cannot exceeds 28MB'

      const fileType = getFileType(addDocumentDetails.value.name)
      return fileType ? true : 'The document type must be in PDF, Word, or Excel'
    }
  }
})

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
const handleAvatarSuccess = (response, uploadFile) => {
  imageUrl.value = URL.createObjectURL(uploadFile.raw)
  console.log(uploadFile)
}
const uploadProfilePhoto = (rawFile) => {
  
  if (rawFile.type !== 'image/jpeg') {
    ElMessage.error('Avatar picture must be JPG format!')
    return false 
  } else if (rawFile.size / 1024 / 1024 > 2) {
    ElMessage.error('Avatar picture size can not exceed 2MB!')
    return false
  }
  return true
}
// const uploadProfilePhoto = async() => {
  // const file = addDocumentDetails.value.attachmentInfo[0]
  // if (file) {
  //   // Read file as DataURL using a promise-based approach
  //   const reader = new FileReader()
  //   reader.readAsDataURL(file)
  //   try {
  //     const base64Data = await new Promise((resolve, reject) => {
  //       reader.onload = () => resolve(reader.result)
  //       reader.onerror = reject
  //     })
  //     addDocumentDetails.value.attachment.value = base64Data.replace(/^.+?;base64,/, '')
  //     addDocumentDetails.value.name = file.name
  //     addDocumentDetails.value.type = getFileType(file.name)
  //   } catch(e) { ElNotification.error({ message: `Error reading file: ${e}` }) }
  // }
  // else
  // {
  //   addDocumentDetails.value.name = null
  //   addDocumentDetails.value.type = null
  //   addDocumentDetails.value.attachment.value = null
  // }
// }
</script>

<style scoped>
.avatar-uploader .avatar {
  width: 178px;
  height: 178px;
  display: block;
}
</style>

<style>
.avatar-uploader .el-upload {
  border: 1px dashed var(--el-border-color);
  border-radius: 6px;
  cursor: pointer;
  position: relative;
  overflow: hidden;
  transition: var(--el-transition-duration-fast);
}

.avatar-uploader .el-upload:hover {
  border-color: var(--el-color-primary);
}

.el-icon.avatar-uploader-icon {
  font-size: 28px;
  color: #8c939d;
  width: 178px;
  height: 178px;
  text-align: center;
}
</style>