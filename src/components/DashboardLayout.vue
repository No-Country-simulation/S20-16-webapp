<template>
  <div class="dashboard">
    <!-- Sidebar -->
    <Sidebar :isSidebarVisible="isSidebarVisible" />

    <!-- Contenido principal -->
    <div class="content" :class="{'content-expanded': isSidebarVisible}">
      <h1>Dashboard Content</h1>
      <p>Here goes the main content of the dashboard.</p>
    </div>
  </div>
</template>

<script>
import Sidebar from './Sidebar.vue';

export default {
  components: {
    Sidebar
  },
  data() {
    return {
      isSidebarVisible: true,  // Sidebar visible por defecto en pantallas grandes
      isMobile: false  // Detecta si el dispositivo es móvil
    };
  },
  mounted() {
    // Detectar si es un dispositivo móvil o no
    this.isMobile = window.innerWidth <= 768;
    
    // Si no es móvil, mantener el sidebar visible
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
.dashboard {
  display: flex;
  height: 100vh;
}

.content {
  flex-grow: 1;
  padding: 1rem;
  transition: margin-left 0.3s ease;
}

.content-expanded {
  margin-left: 250px;  /* El contenido se desplaza cuando el sidebar está visible */
}

@media (max-width: 768px) {
  .content {
    margin-left: 0;  /* El contenido no tiene margen izquierdo cuando el sidebar está oculto */
  }
}
</style>
