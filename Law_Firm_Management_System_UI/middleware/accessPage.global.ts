import sidebarItems from '@/data/sidebarItem'
export default defineNuxtRouteMiddleware(async(to, from) => {
  const { data: user, status } = useAuth()
  
  if (status.value == "authenticated") {
    // Restricted and unrestricted authenticated user access page list (Not from sidebar)
    if (to.path == "/profile")
      return

    // Restricted and unrestricted authenticated user access page list (From sidebar)
    const userAccessPageList = await useFetch(`https://localhost:7204/api/Page/AccessPageList/${user.value?.id}`)
    const accessList: string[] = userAccessPageList.data.value as string[]
    const sidebarMenu = shallowRef(sidebarItems)
    const filteredSidebarMenu = sidebarMenu.value.filter(item => accessList.includes(item.title as string) || item.auth == null)
    const isAuthorized = filteredSidebarMenu.some(path => path.to == to.path)
    if (!isAuthorized) {
      return navigateTo('/dashboard')
    }
  }
})