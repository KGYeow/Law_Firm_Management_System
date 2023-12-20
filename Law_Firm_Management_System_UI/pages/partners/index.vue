<template>
  <v-row>
    <v-col cols="3" v-for="partner in partnerList">
      <v-card elevation="10" class="withbg">
        <v-card-item>
          <div class="d-flex justify-content-center pt-sm-2">
            <v-card-title class="text-h5 d-flex flex-column align-center">
              <v-avatar
                class="mb-3"
                image="/images/users/avatar.jpg"
                size="80"
              />
              {{ partner.fullName }}
            </v-card-title>
          </div>
          <v-divider/>
            <el-scrollbar class="text-body-1" height="150px">
              <v-list density="compact">
                <v-list-item>
                  <template v-slot:prepend>
                    <i class="mdi mdi-phone me-5"></i>
                  </template>
                  <span v-if="partner.phoneNumber">{{ partner.phoneNumber }}</span>
                  <span v-else>-</span>
                </v-list-item>
                <v-list-item>
                  <template v-slot:prepend>
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
// Data
const { data: partnerList } = await fetchData.$get("/Partner")

// Head
useHead({
  title: "Partners | CaseCraft",
})
</script>