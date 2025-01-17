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
      path: '/passform',
      name: 'Passform',
      component: PassView,
    },   
    {
      path: '/dashboard',
      name: 'Dasboard',
      component: DashBoard,
    },  
  ],
});

export default router;
