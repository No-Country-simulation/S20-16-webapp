import { createRouter, createWebHistory } from 'vue-router';
import LoginView from '../views/LoginView.vue';
import PassView from '../views/PassView.vue';
import DashBoard from '@/views/DashboardView.vue';  // Asegúrate de que la ruta sea correcta
import Inscripcion from '../views/InscripcionView.vue';
import ConfirmarInscripcion from '@/components/ConfirmarInscripcion.vue';
import store from '../../store/index'; // Asegúrate de que la ruta sea correcta
import HelpModal from '@/components/HelpModal.vue'; // Importamos el modal
import Comprobantes from '@/components/ComprobantesIncripciones.vue';




const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'Login',
      component: LoginView,
    },
    {
      path: '/passform',
      name: 'passform',  // Cambio a minúsculas
      component: PassView,
    },
    {
      path: '/dashboard',
      name: 'Dashboard',  // Corrección del nombre
      component: DashBoard,
    },
    {
      path: '/inscripciones',
      name: 'Inscripcion',  // Corrección del nombre
      component: Inscripcion,
    },
    {
      path: '/confirmar-inscripcion',
      name: 'confirmar-inscripcion',  // Corrección del nombre
      component: ConfirmarInscripcion,
    },
    {
      path: '/comprobantes',
      name: 'comprobantes',  // Corrección del nombre
      component: Comprobantes,
    },
    {
      path: '/ayuda',
      name: 'Ayuda',
      component: HelpModal, // Asocia el modal como una ruta
    },
    

  ],

  
});

router.beforeEach((to, from, next) => {
  console.log(store.state); 
  // Esperamos que el estado del Vuex se haya cargado antes de realizar la verificación
  const isAuthenticated = !!store.state.user; // Aquí verificamos el estado de Vuex

  // Si el usuario no está autenticado y trata de acceder a una ruta protegida, lo redirigimos al login
  if (to.name !== "Login" && to.name !== "Passform" && !isAuthenticated) {
    next({ name: "Login" });
  } else {
    next(); // Continuamos con la navegación
  }
});



export default router;

