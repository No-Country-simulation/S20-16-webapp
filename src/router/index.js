import { createRouter, createWebHistory } from 'vue-router';
import store from '../../store/index'; // Asegúrate de que la ruta sea correcta

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'Login',
      component: () => import('../views/LoginView.vue'),
    },
    {
      path: '/passform',
      name: 'passform',
      component: () => import('../views/PassView.vue'),
    },
    {
      path: '/dashboard',
      name: 'Dashboard',
      component: () => import('@/views/DashboardView.vue'),
    },
    {
      path: '/inscripciones',
      name: 'Inscripcion',
      component: () => import('../views/InscripcionView.vue'),
    },
    {
      path: '/confirmar-inscripcion',
      name: 'confirmar-inscripcion',
      component: () => import('@/components/ConfirmarInscripcion.vue'),
    },
    {
      path: '/inscripcionExamenes',
      name: 'InscripcionExamenes',
      component: () => import('@/components/InscripcionExamenes.vue'),
    },
    {
      path: '/confirmar-inscripcion-Examenes',
      name: 'confirmar-inscripcion-Examenes',  // Corrección del nombre
      component: () => import ('@/components/ConfirmarInscripcionExamenes.vue') 
    },

    {
      path: '/cancelar',
      name: 'cancelar',
      component: () => import('@/components/CancelarInscripciones.vue'),
    },

   
    {
      path: '/comprobantes',
      name: 'comprobantes',
      component: () => import('@/components/ComprobantesIncripciones.vue'),
    },
    {
      path: '/ayuda',
      name: 'Ayuda',
      component: () => import('@/components/HelpModal.vue'),
    },
  ],
});

// Middleware de autenticación
router.beforeEach((to, from, next) => {
  console.log(store.state);
  const isAuthenticated = !!store.state.user;

  if (to.name !== "Login" && to.name !== "passform" && !isAuthenticated) {
    next({ name: "Login" });
  } else {
    next();
  }
});

export default router;
