<template>
  <div class="dashboard-view">
    <!-- Botón de hamburguesa (solo visible en pantallas pequeñas) -->
    <button class="btn btn-light toggle-btn" @click="toggleSidebar" type="button" aria-label="Toggle sidebar" v-if="isMobile">
      <span class="navbar-toggler-icon"></span>
    </button>

    <!-- Componente DashboardLayout que contiene el Sidebar y el contenido principal -->
    <DashboardLayout :isSidebarVisible="isSidebarVisible" />
  </div>
</template>

<script>
import DashboardLayout from '../components/DashboardLayout.vue';

export default {
  components: {
    DashboardLayout
  },
  data() {
    return {
      isSidebarVisible: false,  // Controla la visibilidad del sidebar (offcanvas)
      isMobile: false  // Detecta si el dispositivo es móvil
    };
  },
  mounted() {
    // Detectar si es un dispositivo móvil o de escritorio
    this.isMobile = window.innerWidth <= 768;
    
    // Si es móvil, aseguramos que el sidebar esté oculto
    if (!this.isMobile) {
      this.isSidebarVisible = true;
    }
    
    // Escuchar cambios de tamaño de la ventana
    window.addEventListener('resize', this.handleResize);
  },
  beforeDestroy() {
    // Eliminar el listener cuando el componente se destruya
    window.removeEventListener('resize', this.handleResize);
  },
  methods: {
    toggleSidebar() {
      this.isSidebarVisible = !this.isSidebarVisible;  // Alternar visibilidad
    },
    handleResize() {
      this.isMobile = window.innerWidth <= 768;
      // En pantallas pequeñas, ocultar el sidebar
      if (this.isMobile) {
        this.isSidebarVisible = false;
      } else {
        this.isSidebarVisible = true;
      }
    }
  }
};
</script>

<style scoped>
.dashboard-view {
  display: flex;
}

.toggle-btn {
  position: fixed;
  top: 20px;
  left: 20px;
  z-index: 20;
  background: none;
  border: none;
  color: white;
  font-size: 24px;
}
</style>
