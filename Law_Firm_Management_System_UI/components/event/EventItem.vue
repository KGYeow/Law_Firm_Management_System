<template>
    <v-card elevation="10" class="withbg">
      <v-card-item>
        <div class="d-flex align-center justify-space-between pt-sm-2">
          <v-card-title class="text-h6 d-flex align-center">
            <v-avatar class="me-2" color="rgb(243, 244, 248)" size="small">
              <CalendarEventIcon color="gray" size="18"/>
            </v-avatar>
            Event Time
          </v-card-title>
          
        </div>
        <v-divider/>
        <v-row>
          <v-col cols="12">
            <div class="mt-2">
              <div class="mt-1">
                <v-avatar class="bg-lighterror text-accent" size="40">
                    <v-img
                        height="200"
                        src="/images/lawyer/lawyer-potrait.jpg"
                        cover>
                    </v-img>
                </v-avatar>
                Event Details
              </div>
            </div>
          </v-col>
        </v-row>
      </v-card-item>
    
    </v-card>
  </template>
  
  <script setup lang="ts">
  import { CalendarEventIcon } from 'vue-tabler-icons';
  import { useTheme } from 'vuetify';
  const theme = useTheme();
  const primary = theme.current.value.colors.primary;
  const secondary = theme.current.value.colors.secondary;
  
  </script>