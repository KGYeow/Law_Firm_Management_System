<template>
	<!------Sidebar-------->
	<v-navigation-drawer left elevation="0" app class="leftSidebar" v-model="sDrawer">
		<!---Logo part -->
		<div class="d-flex pa-5">
			<div class="logo mx-auto">
				<NuxtLink to="/">
				<img src="/images/logos/casecraftLogo2.png">
				</NuxtLink>
			</div>
		</div>
		<!-- ---------------------------------------------- -->
		<!-- Navigation -->
		<!-- ---------------------------------------------- -->
		<div>
			<el-scrollbar class="scrollnavbar">
				<v-list class="pa-6 pt-0">
					<!---Menu Loop -->
					<template v-for="(item, i) in fullyFilteredSidebarMenu">
						<!---Item Header -->
						<LayoutFullVerticalSidebarNavHeader :item="item" v-if="item.header" :key="item.title"/>
						<!---Single Item-->
						<LayoutFullVerticalSidebarNavItem :item="item" v-else/>
					</template>
				</v-list>
			</el-scrollbar>
		</div>
	</v-navigation-drawer>
	<!------Header-------->
	<v-app-bar elevation="0" height="70">
		<div class="d-flex align-center justify-space-between w-100">
			<div>
				<v-btn class="ms-md-3 ms-sm-5 ms-3 text-muted" @click="sDrawer = !sDrawer" icon variant="flat" size="small">
					<Menu2Icon size="20" stroke-width="1.5" />
				</v-btn>
			</div>
			<div>
        		<!-- Notification -->
				<LayoutFullVerticalHeaderNotificationDD />
				<!-- User Profile -->
				<LayoutFullVerticalHeaderProfileDD />
			</div>
		</div>
	</v-app-bar>
	<!------Footer-------->
	<v-footer :app="true" :absolute="true">
		<LayoutFullFooter/>
	</v-footer>
</template>

<script setup>
import sidebarItems from '@/data/sidebarItem';
import { Menu2Icon } from 'vue-tabler-icons';

// Data
const sDrawer = ref(true);
const sidebarMenu = shallowRef(sidebarItems);
const accessList = await fetchData.$get(`/Page/AccessPageList/${useAuth().data.value.id}`)
const filteredSidebarMenu = sidebarMenu.value.filter(item => accessList.data.value.includes(item.title) || item.header != null || item.auth == null)
const fullyFilteredSidebarMenu = filteredSidebarMenu.filter((item, index) => {
  if (item.header != null) {
    const nextItem = filteredSidebarMenu[index + 1];
    if (nextItem == null || nextItem.title == null) {
      return false;
    }
  }
  return true;
})
</script>