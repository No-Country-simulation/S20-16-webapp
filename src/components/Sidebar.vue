<template>
  <aside :class="['sidebar', { 'sidebar-hidden': !isSidebarVisible }]">
    <div class="sidebar-header text-center">
      <img src="../assets/img/Logotipo1.png" alt="Edulink" class="logo-image" />
      <label class="text-muted">Nombre de la Institución</label>
    </div>
    <nav>
      <ul class="nav flex-column">
        <li class="nav-item">
          <router-link to="/dashboard" class="nav-link" :class="{ active: activeMenu === 'dashboard' }"
            @click="setActive('dashboard')">
            <i class="fas fa-home me-2"></i> Home
          </router-link>
        </li>
        <li class="nav-item">
          <router-link to="/inscripciones" class="nav-link" :class="{ active: activeMenu === 'inscripciones' }"
            @click="setActive('inscripciones')">
            <i class="fas fa-edit me-2"></i> Inscripciones
          </router-link>
        </li>
        <li class="nav-item">
          <router-link to="/cancelar" class="nav-link" :class="{ active: activeMenu === 'cancelar' }"
            @click="setActive('cancelar')">
            <i class="fas fa-times-circle me-2"></i> Cancelar Inscripciones
          </router-link>
        </li>
        <li class="nav-item">
          <router-link to="/comprobantes" class="nav-link" :class="{ active: activeMenu === 'comprobantes' }"
            @click="setActive('comprobantes')">
            <i class="fas fa-file-invoice me-2"></i> Comprobantes
          </router-link>
        </li>
        <!-- Link para abrir el HelpModal con Vue Router -->
        <li class="nav-item">
          <!-- Usamos router-link para que la URL cambie, pero se sigue abriendo el modal -->
          <router-link to="/ayuda" class="nav-link" :class="{ active: activeMenu === 'ayuda' }"
  @click="setActive('ayuda')">
  <i class="fas fa-question-circle me-2"></i> Ayuda
</router-link>
        </li>
        <li class="nav-item">
          <a href="#" class="nav-link" :class="{ active: activeMenu === 'cerrar' }" @click="logout">
            <i class="fas fa-sign-out-alt me-2"></i> Cerrar Sesión
          </a>
        </li>
      </ul>
    </nav>
  </aside>
</template>

<script>
export default {
  props: {
    isSidebarVisible: {
      type: Boolean,
      required: true,
    },
  },
  data() {
    return {
      activeMenu: 'dashboard',
    };
  },
  methods: {
    setActive(menu) {
      this.activeMenu = menu;
    },
    logout() {
      // Limpiar el estado de Vuex
      this.$store.commit('clearUser');
      
      // Eliminar el usuario del localStorage
      localStorage.removeItem('user');
  
      // Redirigir al login
      this.$router.push({ name: 'Login' });
    }
  },
};
</script>

<style scoped>
.sidebar {
  width: 250px;
  padding: 1rem;
  height: 100vh;
  position: fixed;
  left: 1.5rem;
  top: 1.5rem;
  border-radius: 20px;
  border-color: black;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  background-color: #f8f9fa;
  transition: transform 0.3s ease;
  transform: translateX(0);
}

.sidebar-hidden {
  transform: translateX(-100%);
}

.nav-link.active {
  background-color: #0d6efd;
  color: white !important;
  font-weight: bold;
  border-radius: 5px;
}

.nav-link {
  color: black;
}

.nav-link:hover {
  color: #0d6efd;
}
</style>
