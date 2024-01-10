<template>
    <v-row>
      <!-- BackLog -->
      <v-col cols="3">
        <v-card elevation="10" class="withbg">
          <v-card-title class="pa-0">
            <div class="d-flex justify-space-between align-center">
              <h5 class="text-h5 mb-3 pl-7 pt-3">
                Backlog
              </h5>
              <!-- Add button or other actions here if necessary -->
            </div>
          </v-card-title>
          <v-card-text>
            <!-- Task Card Example -->
            <v-card outlined class="mb-4">
              <v-card-title>
                <!-- Task Type Icon and Title -->
                <v-icon left>mdi-briefcase-outline</v-icon>
                Create styleguide foundation
              </v-card-title>
              <v-card-subtitle>
                <!-- Task Details -->
                Aug 20, 2021
              </v-card-subtitle>
              <v-card-actions>
                <!-- Task Progress and Actions -->
                <v-progress-linear
                  value="25"
                  color="deep-purple accent-4"
                  height="10"
                ></v-progress-linear>
              </v-card-actions>
            </v-card>
            <!-- Repeat for each task card -->
          </v-card-text>
        </v-card>
      </v-col>
      <!-- Repeat similar structure for To Do, In Progress, and Complete columns -->
    </v-row>
  </template>
  
  <script>
  export default {
    // Component logic goes here
  }
  </script>
  
  <style>
  .withbg {
    background-color: #F5F5F5; /* Adjust to match your UI */
  }
  /* Additional CSS */
  </style>
  