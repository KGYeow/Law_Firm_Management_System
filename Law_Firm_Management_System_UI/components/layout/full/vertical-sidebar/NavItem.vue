<script setup>
import Icon from './Icon.vue';
const props = defineProps({ item: Object, level: Number });
</script>

<template>
  <!--Single Item-->
  <v-list-item
    :to="item.to"
    rounded
    class="mb-1"
    color="primary"
    :disabled="item.disabled"
    :target="item.type === 'external' ? '_blank' : ''"
    v-if="!item.children"
  >
    <!---If icon-->
    <template v-slot:prepend>
      <Icon :item="item.icon" :level="level" />
    </template>
    <v-list-item-title>{{ item.title }}</v-list-item-title>

    <!---If Caption-->
    <v-list-item-subtitle v-if="item.subCaption" class="text-caption mt-n1 hide-menu">
      {{ item.subCaption }}
    </v-list-item-subtitle>
    
    <!---If any chip or label-->
    <template v-slot:append v-if="item.chip">
      <v-chip
        :color="item.chipColor"
        class="sidebarchip hide-menu"
        :size="'small'"
        :variant="item.chipVariant"
        :prepend-icon="item.chipIcon"
      >
        {{ item.chip }}
      </v-chip>
    </template>
  </v-list-item>

  <!--Item Group-->
  <v-list-group v-else>
    <template v-slot:activator="{ props }">
      <v-list-item
        v-bind="props"
        rounded
        class="mb-1"
        :disabled="item.disabled"
      >
        <!---If icon-->
        <template v-slot:prepend>
          <Icon :item="item.icon" :level="level" />
        </template>
        <v-list-item-title>{{ item.title }}</v-list-item-title>

        <!---If Caption-->
        <v-list-item-subtitle v-if="item.subCaption" class="text-caption mt-n1 hide-menu">
          {{ item.subCaption }}
        </v-list-item-subtitle>
        
        <!---If any chip or label-->
        <template v-slot:append v-if="item.chip">
          <v-chip
            :color="item.chipColor"
            class="sidebarchip hide-menu"
            :size="'small'"
            :variant="item.chipVariant"
            :prepend-icon="item.chipIcon"
          >
            {{ item.chip }}
          </v-chip>
        </template>
      </v-list-item>
    </template>

    <v-list-item
      v-for="(subItem, i) in item.children"
      :to="subItem.to"
      rounded
      class="mb-1"
      color="primary"
      :disabled="subItem.disabled"
      :target="subItem.type === 'external' ? '_blank' : ''"
    >
      <!---If icon-->
      <template v-slot:prepend>
        <Icon :item="subItem.icon" :level="level" />
      </template>
      <v-list-item-title>{{ subItem.title }}</v-list-item-title>
      
      <!---If Caption-->
      <v-list-item-subtitle v-if="subItem.subCaption" class="text-caption mt-n1 hide-menu">
        {{ subItem.subCaption }}
      </v-list-item-subtitle>

      <!---If any chip or label-->
      <template v-slot:append v-if="subItem.chip">
        <v-chip
          :color="subItem.chipColor"
          class="sidebarchip hide-menu"
          :size="'small'"
          :variant="subItem.chipVariant"
          :prepend-icon="subItem.chipIcon"
        >
          {{ subItem.chip }}
        </v-chip>
      </template>
    </v-list-item>
  </v-list-group>
</template>