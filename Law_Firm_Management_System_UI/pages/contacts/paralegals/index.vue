<template>
  <v-row>
    <v-col cols="12" md="12">
      <v-card elevation="10" class="withbg bg-primary">
        <v-card-item>
          <v-row v-if="assignedParalegal.userId != null">
            <v-col cols="9" class="hstack">
              <v-avatar class="me-2" color="rgb(243, 244, 248)" size="small">
                <UserIcon color="gray" size="18"/>
              </v-avatar>
              <v-card-title class="text-h6">Your Assigned Paralegal</v-card-title>
              <v-divider
                vertical
                class="mx-5 my-0"
                style="border-color: white !important; opacity: 0.5;"
              />
              <div class="d-flex flex-column text-subtitle-1">
                <span class="mb-1"><strong>Full Name: </strong>{{ assignedParalegal.fullName }}</span>
                <span class="mb-1"><strong>Email Address: </strong>{{ assignedParalegal.email }}</span>
                <span><strong>Phone Number: </strong>{{ assignedParalegal.phoneNumber ?? '-' }}</span>
              </div>
            </v-col>
            <v-col cols="3" class="hstack justify-content-end">
              <v-btn variant="outlined" class="me-2" @click="changeAssignedParalegalModal = true">Change</v-btn>
              <v-btn variant="outlined" @click="deleteAssignedParalegal">Delete</v-btn>
            </v-col>
          </v-row>
          <v-row v-else>
            <v-col class="hstack justify-content-between">
              <v-card-title class="text-h6">No Assigned Paralegal</v-card-title>
              <v-btn variant="outlined" @click="addAssignedParalegalModal = true">Assign Paralegal</v-btn>
            </v-col>
          </v-row>
        </v-card-item>
      </v-card>
    </v-col>
  </v-row>
  <v-row>
    <v-col cols="12" md="12">
      <UiParentCard title="Paralegals">
        <div class="pa-7 pt-1 text-body-1">
          <v-data-table
            density="comfortable"
            v-model:page="currentPage"
            :headers="headers"
            :items="paralegalList"
            :items-per-page="itemsPerPage"
            hover
          >
            <template v-slot:item="{ item }">
              <tr>
                <td>{{ paralegalList.indexOf(item) + 1 }}</td>
                <td>{{ item.fullName }}</td>
                <td>{{ item.assignedPartner ?? '-' }}</td>
                <td>{{ item.email }}</td>
                <td>{{ item.phoneNumber ?? '-' }}</td>
                <td>{{ item.address ?? '-' }}</td>
                <td>
                  <el-tag type="success" v-if="item.isActive">Active</el-tag>
                  <el-tag type="danger" v-else>Inactive</el-tag>
                </td>
                <td>
                  <ul class="m-0 list-inline hstack">
                    <li v-if="!item.isActive">
                      <v-tooltip text="Activate" activator="parent" location="top" offset="2"/>
                      <el-popconfirm
                        title="Are you sure to activate this paralegal?"
                        icon-color="green"
                        width="190"
                        @confirm="changeParalegalStatus(item.userId, 'Activate')"
                      >
                        <template #reference>
                          <v-btn icon="mdi-account-reactivate-outline" size="small" variant="text"/>
                        </template>
                      </el-popconfirm>
                    </li>
                    <li v-else>
                      <v-tooltip text="Inactivate" activator="parent" location="top" offset="2"/>
                      <el-popconfirm
                        title="Are you sure to inactivate this paralegal?"
                        icon-color="orange"
                        width="190"
                        @confirm="changeParalegalStatus(item.userId, 'Inactivate')"
                      >
                        <template #reference>
                          <v-btn icon="mdi-account-off-outline" size="small" variant="text"/>
                        </template>
                      </el-popconfirm>
                    </li>
                    <li>
                      <v-tooltip text="View Details" activator="parent" location="top" offset="2"/>
                      <v-btn icon="mdi-open-in-new" size="small" variant="text" :href="`/contacts/paralegals/${item.userId}`"/>
                    </li>
                  </ul>
                </td>
              </tr>
            </template>
            <template v-slot:bottom>
              <div class="d-flex justify-content-end pt-2">
                <el-pagination
                  layout="total, prev, pager, next"
                  v-model:current-page="currentPage"
                  :page-size="paralegalList.length/pageCount()"
                  :total="paralegalList.length"
                />
              </div>
            </template>
          </v-data-table>
        </div>
      </UiParentCard>
    </v-col>
  </v-row>

  <!-- Add Assigned Paralegal Modal -->
  <SharedUiModal v-model="addAssignedParalegalModal" title="Assign Paralegal" width="500">
    <form @submit.prevent="addAssignedParalegal">
      <v-card-item class="px-8 py-4 text-body-1">
        <v-row>
          <v-col>
            <v-label class="text-caption">Paralegal</v-label>
            <v-autocomplete
              :items="activeParalegalList"
              item-title="fullName"
              item-value="userId"
              placeholder="Select active paralegal"
              variant="outlined"
              density="compact"
              :error-messages="addAssignedParalegalUserId.errorMessage.value"
              v-model="addAssignedParalegalUserId.value.value"
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

  <!-- Change Assigned Paralegal Modal -->
  <SharedUiModal v-model="changeAssignedParalegalModal" title="Change Assigned Paralegal" width="500">
    <form @submit.prevent="changeAssignedParalegal">
      <v-card-item class="px-8 py-4 text-body-1">
        <v-row>
          <v-col>
            <v-label class="text-caption">Paralegal</v-label>
            <v-autocomplete
              :items="activeParalegalList"
              item-title="fullName"
              item-value="userId"
              placeholder="Select active paralegal"
              variant="outlined"
              density="compact"
              v-model="newAssignedParalegalUserId"
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
import { useField, useForm } from 'vee-validate'
import { AddressBookIcon } from "vue-tabler-icons"
import UiParentCard from "@/components/shared/UiParentCard.vue"

// Data
const { handleSubmit } = useForm({
  validationSchema: {
    paralegalUserId(value) {
      return value ? true : 'Paralegal is required'
    }
  }
})
const { data: paralegalList } = await fetchData.$get("/Paralegal")
const { data: activeParalegalList } = await fetchData.$get("/Paralegal/ActiveParalegalList")
const { data: assignedParalegal } = await fetchData.$get("/Partner/AssignedParalegal")
const addAssignedParalegalModal = ref(false)
const changeAssignedParalegalModal = ref(false)
const addAssignedParalegalUserId = useField('paralegalUserId')
const newAssignedParalegalUserId = ref(assignedParalegal.value.userId)
const currentPage = ref(1)
const itemsPerPage = ref(10)
const headers = ref([
  { key: "number", title: "No." },
  { key: "fullName", title: "Full Name" },
  { key: "assignedPartner", title: "Assigned Partner" },
  { key: "email" , title: "Email" },
  { key: "phoneNumber" , title: "Phone No." },
  { key: "address" , title: "Address" },
  { key: "isActive" , title: "Status" },
  { key: "actions", sortable: false, width: 0 },
])

// Head
useHead({
  title: "Paralegals | CaseCraft",
})

// Page Meta
definePageMeta({
  breadcrumbsIcon: shallowRef(AddressBookIcon),
  breadcrumbs: [
    {
      title: 'Contacts',
      disabled: false,
    },
    {
      title: 'Paralegals',
      disabled: false,
    },
  ],
})

// Methods
const pageCount = () => {
  return Math.ceil(paralegalList.value.length / itemsPerPage.value)
}
const changeParalegalStatus = async(paralegalUserId, status) => {
  try {
    const result = await fetchData.$put(`/Paralegal/ChangeStatus/${paralegalUserId}/${status}`)
    if (!result.error) {
      ElNotification.success({ message: result.message })
      refreshNuxtData()
    }
    else {
      ElNotification.error({ message: result.message })
    }
  } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
}
const addAssignedParalegal = handleSubmit(async(values) => {
  try {
    const result = await fetchData.$put(`/Partner/AssignedParalegal/Add/${values.paralegalUserId}`)
    if (!result.error) {
      addAssignedParalegalModal.value = false
      ElNotification.success({ message: result.message })
      refreshNuxtData()
    }
    else {
      ElNotification.error({ message: result.message })
    }
  } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
})
const changeAssignedParalegal = async() => {
  try {
    const result = await fetchData.$put(`/Partner/AssignedParalegal/Change/${newAssignedParalegalUserId.value}`)
    if (!result.error) {
      changeAssignedParalegalModal.value = false
      ElNotification.success({ message: result.message })
      refreshNuxtData()
    }
    else {
      ElNotification.error({ message: result.message })
    }
  } catch { ElNotification.error({ message: "There is a problem with the server. Please try again later." }) }
}
const deleteAssignedParalegal = async() => {
  try {
    const result = await fetchData.$put(`/Partner/AssignedParalegal/Delete`)
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