import { createRouter, createWebHistory } from 'vue-router';
import LoginView from '../views/LoginView.vue';
import PassView from '../views/PassView.vue';
import DashBoard from '@/views/DashboardView.vue';  // Asegúrate de que la ruta sea correcta




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
      name: 'passform',  // Cambio a minúsculas
      component: PassView,
    },
    {
      path: '/dashboard',
      name: 'dashboard',  // Corrección del nombre
      component: DashBoard,
    },

  ],
});

export default router;
