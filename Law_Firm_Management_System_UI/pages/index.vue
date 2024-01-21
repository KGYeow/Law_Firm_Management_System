<template>
  <div>
    <v-parallax class="background-part" src="/images/background/login-cover.jpg">
    <v-container fill-height class="title-part text-center mb-20" style="padding-top: 300px; font-weight: 900;">
      <v-row>
        <v-col>
          <v-col class="text-primary" style="font-size: 110px;">
            Law Firm Management System
          </v-col>
          <v-col class="text-secondary" style="font-size: 90px;">
            CaseCraft
          </v-col>
        </v-col>
      </v-row>
    </v-container>
  </v-parallax>

  <v-container class="bg-primary text-white pt-16 pb-16" fluid>
      <v-row align="center">
        <v-col class ="text-center" style ="padding: 0px 300px;">
          <v-col class="text-center" style="font-size: 55px; font-weight: 900;">
            Why Choose Us
          </v-col>
          
          <v-row class="pt-10">
          <!-- Icon 1 -->
          <v-col class="d-flex justify-center flex-column align-center">
            <v-icon class="mb-4" size="70">mdi-check-circle-outline</v-icon>
            <v-col style ="font-size: 25px;">Quality Service</v-col>
          </v-col>

          <!-- Icon 2 -->
          <v-col class="d-flex justify-center flex-column align-center">
            <v-icon class="mb-4" size="70">mdi-clock-outline</v-icon>
            <v-col style ="font-size: 25px;">Timely Delivery</v-col>
          </v-col>

          <!-- Icon 3 -->
          <v-col class="d-flex justify-center flex-column align-center">
            <v-icon class="mb-4" size="70">mdi-account-group-outline</v-icon>
            <v-col style ="font-size: 25px;">Experienced Team</v-col>
          </v-col>

          <!-- Icon 4 -->
          <v-col class="d-flex justify-center flex-column align-center">
            <v-icon class="mb-4" size="70">mdi-trophy</v-icon>
            <v-col style ="font-size: 25px;">We Are The Best</v-col>
          </v-col>
        </v-row>
        </v-col>
      </v-row>
    </v-container>
  </div>
</template>

<style scoped>
.background-part::before {
  content: "";
  position: absolute;
  height: 100%;
  width: 100%;
  opacity: 0.3;
  left: 0;
  top: 0;
  background: radial-gradient(rgb(210, 241, 223), rgb(211, 215, 250), rgb(186, 216, 244)) 0% 0%/400% 400%;
}

</style>

<script setup>
definePageMeta({
  layout: "guest",
  auth: { unauthenticatedOnly: true },
})

// Head
useHead({
  title: "CaseCraft",
})
</script>