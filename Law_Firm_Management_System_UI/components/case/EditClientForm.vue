<template>
    <form @submit.prevent="editClient">
      <v-card-item class="px-8 py-4 text-body-1">
        <v-row>
          <v-col>
            <v-label class="text-caption">Select Client</v-label>
            <!-- Use a v-select or any other appropriate component for selecting a client -->
            <v-select
              :items="clientList"
              item-title="fullName"
              item-value="id"
              placeholder="Select client"
              variant="outlined"
              density="compact"
              v-model="editedClientDetails.clientId.value"
              hide-details="auto"
            />
          </v-col>
        </v-row>
      </v-card-item>
      <v-card-actions class="p-3 justify-content-end">
        <v-btn color="primary" type="submit">Submit</v-btn>
      </v-card-actions>
    </form>
  </template>
  
  <script setup>
  import { useField, useForm } from 'vee-validate';
  import { ref, defineProps, defineEmits } from 'vue';
  
  const { data: clientList } = await fetchData.$get("/Client")

  const props = defineProps({
    clientId: Number,
    caseId: Number, // Add this line
});
  const emit = defineEmits(['close-modal']);
  
  const { handleSubmit } = useForm({
    initialValues: {
      clientId: props.clientId,
    },
    validationSchema: {
      clientId(value) {
        return value ? true : 'Client selection is required';
      },
    },
  });
  
  const editedClientDetails = ref({
    clientId: useField('clientId'),
});
  
const editClient = handleSubmit(async (values) => {
    console.log("Form Values:", values);

    try {
        // Call your API to update the client associated with the case
        const result = await fetchData.$put("/Case/EditClient", {
            caseId: props.caseId,
            clientId: values.clientId,
        });

        console.log("API Response:", result);

        if (!result.error) {
            emit('close-modal', false);
            editedClientDetails.value.clientId.resetField();
            ElNotification.success({ message: result.message });

            // Log the updated case details to the console
            console.log("Updated Case Details:", result.updatedCase);

            nuxtRefreshData();
            //refreshNuxtData();
        } else {
            ElNotification.error({ message: result.message });
        }
    } catch (error) {
        console.error("Error during editClient:", error);
        ElNotification.error({ message: "There is a problem with the server. Please try again later." });
    }
});
  </script>