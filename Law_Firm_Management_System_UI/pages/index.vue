<template>
  <div>
    <v-parallax class="background-part" src="/images/background/login-cover.jpg">
    <v-container fill-height class="title-part">
      <v-row>
        <v-col>
          <v-typography class="title-text">
            Law Firm Management System
          </v-typography>
          <v-typography class="subtitle-text">
            CaseCraft
          </v-typography>
        </v-col>
      </v-row>
    </v-container>
  </v-parallax>

  <v-container class="choose-us-container" fluid>
      <v-row align="center">
        <v-col class ="choose-us-row">
          <v-typography class="choose-us-title">
            Why Choose Us
          </v-typography>
          
          <v-row class="choose-us-section">
          <!-- Icon 1 -->
          <v-col class="icon-col">
            <v-icon class="icon" size="70">mdi-check-circle-outline</v-icon>
            <v-typography class="icon-title">Quality Service</v-typography>
          </v-col>

          <!-- Icon 2 -->
          <v-col class="icon-col">
            <v-icon class="icon" size="70">mdi-clock-outline</v-icon>
            <v-typography class="icon-title">Timely Delivery</v-typography>
          </v-col>

          <!-- Icon 3 -->
          <v-col class="icon-col">
            <v-icon class="icon" size="70">mdi-account-group-outline</v-icon>
            <v-typography class="icon-title">Experienced Team</v-typography>
          </v-col>

          <!-- Icon 4 -->
          <v-col class="icon-col">
            <v-icon class="icon" size="70">mdi-check-circle-outline</v-icon>
            <v-typography class="icon-title">Quality Service</v-typography>
          </v-col>

          <!-- Icon 5 -->
          <v-col class="icon-col">
            <v-icon class="icon" size="70">mdi-clock-outline</v-icon>
            <v-typography class="icon-title">Timely Delivery</v-typography>
          </v-col>

          <!-- Icon 6 -->
          <v-col class="icon-col">
            <v-icon class="icon" size="70">mdi-account-group-outline</v-icon>
            <v-typography class="icon-title">Experienced Team</v-typography>
          </v-col>
        </v-row>
        </v-col>
      </v-row>
    </v-container>
  </div>
</template>

<style scoped>
.background-part {
  position: relative;
}
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

.title-part {
  text-align: center;
  padding-top: 300px;
  font-weight: 900;
  margin-bottom: 20px;
  z-index: 1;
}

.title-text {
  font-size: 110px;
  color: #2b4c65;
}

.subtitle-text {
  font-size: 90px;
  color: #95b4cc;
}
.choose-us-container {
  background-color: #2b4c65; /* Dark blue background color */
  color: #ffffff; /* White text color for better contrast */
  padding: 50px 0; /* Adjusted padding for spacing */
}
.choose-us-row {
  text-align: center;
  padding: 0px 300px;
}
.choose-us-title {
  font-size: 55px;
  font-weight: 900;
  text-align: center;
}
.choose-us-section {
  padding-top: 35px;
}
.icon-col {
  display: flex;
  justify-content: center;
  align-items: center;
  flex-direction: column;
}
.icon {
  margin-bottom: 10px;
}
.icon-title {
  font-size: 25px;
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