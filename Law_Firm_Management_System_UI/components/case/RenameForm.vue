<template>
    <form @submit.prevent="renameCase">
      <v-card-item class="px-8 py-4 text-body-1">
        <v-row>
          <v-col>
            <v-label class="text-caption">Case Name</v-label>
            <v-text-field
              variant="outlined"
              density="compact"
              :error-messages="renameCaseDetails.name.errorMessage"
              v-model="renameCaseDetails.name.value"
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
  import { useField, useForm } from 'vee-validate'
  
  // Properties, Emit & Model
  const props = defineProps({
    caseId: Number,
    caseName: String,
  })
  const emit = defineEmits(['close-modal'])
  
  // Data
  const { handleSubmit } = useForm({
    initialValues: {
      name: props.caseName,
    },
    validationSchema: {
      name(value) {
        return value ? true : 'Case name is required'
      }
    }
  })
  const renameCaseDetails = ref({
    caseId: props.caseId,
    name: useField('name'),
  })
  
  // Methods
  const renameCase = handleSubmit(async (values) => {
  try {
    const result = await fetchData.$put("/Case/Rename", {
      CaseId: renameCaseDetails.value.caseId,
      CaseName: values.name,
    });
    
    if (!result.error) {
      emit('close-modal', false);
      renameCaseDetails.value.caseId = null;
      renameCaseDetails.value.name.resetField();
      ElNotification.success({ message: result.message });
      refreshNuxtData();
    } else {
      ElNotification.error({ message: result.message });
    }
  } catch {
    ElNotification.error({ message: "There is a problem with the server. Please try again later." });
  }
});
  </script>

