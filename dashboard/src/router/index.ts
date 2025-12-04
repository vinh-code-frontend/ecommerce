import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      component: () => import('@/layouts/DashboardLayout.vue'),
    },
    {
      path: '/auth',
      component: () => import('@/layouts/AuthLayout.vue'),
      children: [
        {
          path: 'login',
          component: () => import('@/features/auth/LoginPage.vue'),
        },
        {
          path: 'register',
          component: () => import('@/features/auth/RegisterPage.vue'),
        },
      ],
    },
  ],
})

export default router
