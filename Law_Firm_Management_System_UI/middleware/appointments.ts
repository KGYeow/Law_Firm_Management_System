export default defineNuxtRouteMiddleware(async(to, from) => {
  const { data: user } = useAuth()

  if (user.value?.role == "Partner") {
    return navigateTo('/dashboard')
  }
  else {
    return navigateTo('/dashboard')
  }
})