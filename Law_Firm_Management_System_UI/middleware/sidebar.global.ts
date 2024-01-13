import sidebarItems from '@/data/sidebarItem'

export default defineNuxtRouteMiddleware(async(to, from) => {
  const baseURL = useRuntimeConfig().public.baseURL
  const { data: user, status } = useAuth()

  // If user is authenticated user
  if (status.value == "authenticated") {
    // Restricted and unrestricted access page list (Not from sidebar)
    if (to.path.match("/profile")) return

    // Restricted and unrestricted access page list (From sidebar)
    // Filter the sidebar menu based on the user's access
    const accessList = await useFetch(`${baseURL}/Page/AccessPageList/${user.value?.id}`)
    const filteredSidebarMenu = filterSidebarItems(accessList.data.value, sidebarItems)

    // Check if the user is authorized to access the requested page
    const isAuthorized = checkAuthorization(filteredSidebarMenu, to.path)
    
    // Redirect to the dashboard if not authorized
    if (!isAuthorized) {
      if (user.value?.userRoleId == 3) {
        return navigateTo('/dashboard-client')
      }
      else {
        if (to.path.match("/contacts/clients")) return
        if (user.value?.userRoleId == 1 && to.path.match("/configuration/user-settings")) return 
        return navigateTo('/dashboard')
      }
    }
  }
})

const filterSidebarItems = (accessPages: any, menuItems: any) => {
  const temp = menuItems.filter((item: any) => {
		if (item.header) {
			return true; // Include header
		}
    if (item.to && (!item.auth || accessPages.includes(item.accessName))) {
      return true; // Include parent item if it's accessible
    }
		else if (item.children) {
      const filteredChildren = filterSidebarItems(accessPages, item.children);
      if (filteredChildren.length > 0) {
				item.children = filteredChildren; // Update parent item's children with filtered children
        return true; // Include parent item if any child is accessible
      } else {
        return false; // Exclude parent item if no child is accessible
      }
    }
		else {
      return false; // Exclude item if it's not a header, parent, or has children
    }
  })
  return temp.filter((item: any, index: any) => {
		if (item.header != null) {
			const nextItem = temp[index + 1];
			if (!nextItem || !nextItem.title) {
				return false; // Exclude header that does not followed by item 
			}
		}
		return true;
	})
}

const checkAuthorization = (menuItems: any, path: any) => {
  return menuItems.some((item: any) => {
    if (item.to === path) {
      return true
    }
    if (item.children) {
      return checkAuthorization(item.children, path)
    }
    return false
  })
}