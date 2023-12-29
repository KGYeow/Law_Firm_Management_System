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
					<template v-for="(item, i) in filteredSidebarMenu">
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
				<LayoutFullVerticalHeaderNotificationDD/>
				<!-- User Profile -->
				<LayoutFullVerticalHeaderProfileDD/>
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

// Data
const sDrawer = ref(true);
const accessList = await fetchData.$get(`/Page/AccessPageList/${useAuth().data.value.id}`)
const filteredSidebarMenu = filterSidebarItems(accessList.data.value, sidebarItems)

// Functions
function filterSidebarItems(accessPages, menuItems) {
  const temp = menuItems.filter(item => {
		if (item.header) {
			return true; // Include header
		}
    if (item.to && (!item.auth || accessPages.includes(item.accessName))) {
      return true // Include parent item if it's accessible
    }
		else if (item.children) {
      const filteredChildren = filterSidebarItems(accessPages, item.children)
      if (filteredChildren.length > 0) {
				item.children = filteredChildren // Update parent item's children with filtered children
        return true // Include parent item if any child is accessible
      } else {
        return false // Exclude parent item if no child is accessible
      }
    }
		else {
      return false // Exclude item if it's not a header, parent, or has children
    }
  })
  return temp.filter((item, index) => {
		if (item.header) {
			const nextItem = temp[index + 1]
			if (!nextItem || !nextItem.title) {
				return false // Exclude header that does not followed by item 
			}
		}
		return true
	})
}
</script>