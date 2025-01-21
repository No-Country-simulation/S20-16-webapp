import { createRouter, createWebHistory } from 'vue-router';
import LoginView from '../views/LoginView.vue';
import PassView from '../views/PassView.vue'
import DashBoard from '@/views/DashBoard.vue';
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'login',
      component: LoginView,
    },
    {
<<<<<<< HEAD

       path: '/passform',
       name: 'passform',
       component: () => import('../views/PassView.vue'),
    },
    {
      path: '/dashboard',
      name: 'Dashboard',
      component: () => import('../views/DasboardView.vue'),
   },
  ],
})
=======
>>>>>>> 8b14e6f917594963ae67f5a1b4807c03053de7b8




export default router;
