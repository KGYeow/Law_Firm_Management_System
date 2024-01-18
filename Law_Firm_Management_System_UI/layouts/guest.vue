<template>
  <v-locale-provider>
    <v-app>
      <LayoutGuestMain/>
      <v-main class="m-0">
        <v-container fluid class="p-0">
          <div class="maxWidth">
            <RouterView/>
          </div>
        </v-container>
      </v-main>
    </v-app>
  </v-locale-provider>
</template>