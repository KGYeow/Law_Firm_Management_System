import sidebarItems from '@/data/sidebarItem'

const isAuthorizedRecursive = (menuItems: any, path: any) => {
  return menuItems.some((item: any) => {
    if (item.to === path) {
      return true
    }
    if (item.children) {
      return isAuthorizedRecursive(item.children, path)
    }
    return false
  })
}

export default defineNuxtRouteMiddleware(async(to, from) => {
  const { data: user, status } = useAuth()
  
  // If user is authenticated user
  if (status.value == "authenticated") {
    // Restricted and unrestricted access page list (Not from sidebar)
    if (to.path == "/profile") return

    // Restricted and unrestricted access page list (From sidebar)
    // Get the user's access page list
    const userAccessPageList = await useFetch(`https://localhost:7248/api/Page/AccessPageList/${user.value?.id}`)
    const accessList = userAccessPageList.data.value as string[]

    // Filter the sidebar menu based on the user's access
    const sidebarMenu = shallowRef(sidebarItems)
    const filteredSidebarMenu = sidebarMenu.value.filter(item => accessList.includes(item.title as string) || item.auth == null)

    // Check if the user is authorized to access the requested page
    const isAuthorized = isAuthorizedRecursive(filteredSidebarMenu, to.path)

    // Redirect to the dashboard if not authorized
    if (!isAuthorized) {
      return navigateTo('/dashboard')
    }
  }
})