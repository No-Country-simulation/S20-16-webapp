<template>
  <aside :class="['sidebar', { 'sidebar-hidden': !isSidebarVisible }]">
    <div class="sidebar-header text-center">
      <img src="../assets/img/Logotipo1.png" alt="Edulink" class="logo-image" />
      <label class="text-muted">Insituto Superior Técnico</label>
    </div>
    
    <nav>
      <ul class="nav flex-column">
        <li class="nav-item">
          <router-link 
            to="/dashboard" 
            class="nav-link" 
            :class="{ active: activeMenu === 'dashboard' }"
            @click="setActive('dashboard')"
          >
            <i class="fas fa-home me-2"></i> Home
          </router-link>
        </li>

        <!-- Inscripciones con Submenú -->
        <li class="nav-item">
          <a 
            href="#" 
            class="nav-link d-flex justify-content-between align-items-center"
            :class="{ active: isInscripcionesActive }" 
            @click.prevent="toggleSubmenu"
          >
            <span>
              <i class="fas fa-edit me-2"></i> Inscripciones
            </span>
            <i class="fas" :class="{ 
              'fa-chevron-down': !isSubmenuOpen, 
              'fa-chevron-up': isSubmenuOpen 
            }"></i>
          </a>
          <ul class="submenu" :class="{ 'submenu-open': isSubmenuOpen }">
            <li class="nav-item">
              <router-link 
                to="/inscripcionExamenes" 
                class="nav-link"
                :class="{ active: activeMenu === 'inscripcionExamenes' }"
                @click="setActive('inscripcionExamenes')"
              >
                <i class="fas fa-file-alt me-2"></i> Exámenes
              </router-link>
            </li>
            <li class="nav-item">
              <router-link 
                to="/inscripciones" 
                class="nav-link"
                :class="{ active: activeMenu === 'inscripciones' }"
                @click="setActive('inscripciones')"
              >
                <i class="fas fa-book me-2"></i> Materias
              </router-link>
            </li>
          </ul>
        </li>

        <li class="nav-item">
          <router-link 
            to="/cancelar" 
            class="nav-link" 
            :class="{ active: activeMenu === 'cancelar' }"
            @click="setActive('cancelar')"
          >
            <i class="fas fa-times-circle me-2"></i> Cancelar Inscripciones
          </router-link>
        </li>
        
        <li class="nav-item">
          <router-link 
            to="/comprobantes" 
            class="nav-link" 
            :class="{ active: activeMenu === 'comprobantes' }"
            @click="setActive('comprobantes')"
          >
            <i class="fas fa-file-invoice me-2"></i> Comprobantes
          </router-link>
        </li>
        
        <li class="nav-item">
          <router-link 
            to="/ayuda" 
            class="nav-link" 
            :class="{ active: activeMenu === 'ayuda' }"
            @click="setActive('ayuda')"
          >
            <i class="fas fa-question-circle me-2"></i> Ayuda
          </router-link>
        </li>
        
        <li class="nav-item">
          <a 
            href="#" 
            class="nav-link" 
            :class="{ active: activeMenu === 'cerrar' }" 
            @click.prevent="logout"
          >
            <i class="fas fa-sign-out-alt me-2"></i> Cerrar Sesión
          </a>
        </li>
      </ul>
    </nav>
  </aside>
</template>

<script>
export default {
  name: 'Sidebar',
  props: {
    isSidebarVisible: {
      type: Boolean,
      required: true,
    },
  },
  data() {
    return {
      activeMenu: '',
      isSubmenuOpen: false,
      lastNonSubmenuRoute: '',
    };
  },
  computed: {
    isInscripcionesActive() {
      return this.activeMenu === 'inscripciones' || 
             this.activeMenu === 'inscripcionExamenes' ||
             this.isSubmenuOpen;
    }
  },
  created() {
    // Establecer el menú activo inicial basado en la ruta actual
    this.setInitialActiveMenu();
  },
  methods: {
    setInitialActiveMenu() {
      const path = this.$route.path;
      const routeToMenu = {
        '/dashboard': 'dashboard',
        '/inscripcionExamenes': 'inscripcionExamenes',
        '/inscripciones': 'inscripciones',
        '/cancelar': 'cancelar',
        '/comprobantes': 'comprobantes',
        '/ayuda': 'ayuda'
      };
      
      this.activeMenu = routeToMenu[path] || '';
      
      // Abrir el submenú si estamos en una ruta de inscripciones
      if (path === '/inscripciones' || path === '/inscripcionExamenes') {
        this.isSubmenuOpen = true;
      }
    },
    setActive(menu) {
      // Si el menú seleccionado no es parte del submenú, cerrar el submenú
      if (menu !== 'inscripciones' && menu !== 'inscripcionExamenes') {
        this.isSubmenuOpen = false;
        this.lastNonSubmenuRoute = menu;
      }
      this.activeMenu = menu;
    },
    toggleSubmenu() {
      this.isSubmenuOpen = !this.isSubmenuOpen;
    },
    logout() {
      this.$store.commit('clearUser');
      localStorage.removeItem('user');
      this.$router.push({ name: 'Login' });
    },
  },
  watch: {
    '$route'(newRoute) {
      const path = newRoute.path;
      // Mantener el submenú abierto solo si estamos navegando entre rutas de inscripciones
      if (path === '/inscripciones' || path === '/inscripcionExamenes') {
        this.isSubmenuOpen = true;
      }
      // Actualizar el menú activo
      this.setInitialActiveMenu();
    }
  }
};
</script>

<style scoped>
.sidebar {
  width: 250px;
  padding: 1rem;
  height: calc(100vh - 3rem);
  position: fixed;
  left: 1.5rem;
  top: 1.5rem;
  border-radius: 20px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  background-color: #f8f9fa;
  transition: transform 0.3s ease;
  transform: translateX(0);
  overflow-y: auto;
}

.sidebar-hidden {
  transform: translateX(-100%);
}

.sidebar-header {
  margin-bottom: 2rem;
}

.logo-image {
  max-width: 150px;
  margin-bottom: 0.5rem;
  display: block;
  margin-left: auto;
  margin-right: auto;
}

.nav-link {
  color: #666;
  padding: 0.75rem 1rem;
  border-radius: 8px;
  transition: all 0.2s ease;
  margin-bottom: 0.25rem;
  font-weight: 500;
}

.nav-link:hover {
  color: #4040ff;
  background-color: #f0f0ff;
}

.nav-link.active {
  background-color: #4040ff;
  color: white;
  font-weight: 600;
}

/* Estilos para el submenú */
.submenu {
  list-style: none;
  padding-left: 1rem;
  max-height: 0;
  overflow: hidden;
  transition: max-height 0.3s ease-in-out;
}

.submenu-open {
  max-height: 200px;
}

.submenu .nav-link {
  padding-left: 2.5rem;
  font-size: 0.95rem;
}

/* Ajustes para los íconos */
.fas {
  width: 20px;
  text-align: center;
  margin-right: 0.5rem;
}

/* Estilo para el ícono de chevron */
.fa-chevron-down,
.fa-chevron-up {
  margin-left: auto;
  transition: transform 0.3s ease;
}

/* Animación suave para el hover */
.nav-item {
  position: relative;
}

.nav-item::after {
  content: '';
  position: absolute;
  bottom: 0;
  left: 50%;
  width: 0;
  height: 2px;
  background-color: #4040ff;
  transition: all 0.3s ease;
}

.nav-item:hover::after {
  width: 100%;
  left: 0;
}
</style>