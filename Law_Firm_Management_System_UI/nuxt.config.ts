// https://nuxt.com/docs/api/configuration/nuxt-config
export default defineNuxtConfig({
  // ssr: https://nuxt.com/docs/api/configuration/nuxt-config#ssr
  ssr: false,

  app: {
    // Global page headers: https://nuxt.com/docs/api/configuration/nuxt-config#head
    head: {
      title: 'CaseCraft',
      htmlAttrs: { lang: 'en' },
      meta: [
        { charset: 'utf-8' },
        { name: 'viewport', content: 'width=device-width, initial-scale=1' },
        { hid: 'description', name: 'description', content: '' },
      ],
    },
  },

  // Global css: https://nuxt.com/docs/api/configuration/nuxt-config#css
  css: [
    '@/assets/scss/config/app/main.scss',
  ],

  // devServer: https://nuxt.com/docs/api/nuxt-config#devserver
  // devServer: {
  //   host: '0.0.0.0',
  //   port: 3000,
  // },
  
  // modules: https://nuxt.com/docs/api/configuration/nuxt-config#modules
  modules: [
    '@sidebase/nuxt-auth',
    '@element-plus/nuxt',
  ],

  // runtimeConfig: https://nuxt.com/docs/api/nuxt-config#runtimeconfig
  runtimeConfig: {
    public: {
      baseURL: "https://localhost:7248/api",
    }
  },
  
  // typescript: https://nuxt.com/docs/api/nuxt-config#typescript
  typescript: {
    shim: false
  },

  // vite: https://nuxt.com/docs/api/nuxt-config#vite
  vite: {
    define: {
      "process.env.DEBUG": false,
    },
  },

  // nitro: https://nuxt.com/docs/api/nuxt-config#nitro
  nitro: {
    serveStatic: true,
  },

  // sidebase: https://sidebase.io/nuxt-auth/v0.6/getting-started/quick-start
  auth: {
    isEnabled: true,
    globalAppMiddleware: {
      isEnabled: true,
      allow404WithoutAuth: false,
      addDefaultCallbackUrl: false,
    },
    provider: {
      type: 'local',
      endpoints: {
        signIn: { path: '/Authenticate/Login', method: 'post' },
        signOut: { path: '/Authenticate/Logout', method: 'get' },
        signUp: { path: '/Authenticate/Register', method: 'post' },
        getSession: { path: '/Authenticate/Me', method: 'get' },
      },
      token: {
        maxAgeInSeconds: 60 * 60 * 24
      },
      sessionDataType: { id: 'number', username: 'string', password: 'string', fullName: 'string', email: 'string', userRoleId: 'number' }
    },
    baseURL: 'https://localhost:7248/api',
  }
})