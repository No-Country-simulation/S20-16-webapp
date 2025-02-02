<template>
  <div class="inscripcion-view">
    <!-- Botón de hamburguesa (solo visible en pantallas pequeñas) -->
    <button
      class="toggle-btn"
      @click="toggleSidebar"
      type="button"
      aria-label="Toggle sidebar"
      v-if="isMobile"
    >
      <span class="navbar-toggler-icon"></span>
    </button>

    <!-- Sidebar -->
    <Sidebar :isSidebarVisible="isSidebarVisible" />

    <!-- Contenido principal -->
    <div class="content" :class="{ 'content-expanded': isSidebarVisible && !isMobile }">
      <Inscripcion v-if="step === 1" @continuar="nextStep" />
      <ConfirmacionInscripcion v-if="step === 2" />
    </div>
  </div>
</template>

<script>
import Sidebar from '@/components/Sidebar.vue';
import Inscripcion from '@/components/InscripcionesForm.vue';
import ConfirmacionInscripcion from '@/components/ConfirmarInscripcion.vue';

export default {
  components: {
    Sidebar,
    Inscripcion,
    ConfirmacionInscripcion,
  },
  data() {
    return {
      isSidebarVisible: false, // Controla la visibilidad del sidebar
      isMobile: false, // Detecta si el dispositivo es móvil
      step: 1, // Controla qué componente mostrar
    };
  },
  mounted() {
    this.isMobile = window.innerWidth <= 768;
    if (!this.isMobile) {
      this.isSidebarVisible = true; // Mostrar el sidebar en pantallas grandes
    }
    window.addEventListener('resize', this.handleResize);
  },
  beforeDestroy() {
    window.removeEventListener('resize', this.handleResize);
  },
  methods: {
    toggleSidebar() {
      this.isSidebarVisible = !this.isSidebarVisible;
    },
    handleResize() {
      this.isMobile = window.innerWidth <= 768;
      this.isSidebarVisible = !this.isMobile; // Mostrar solo en pantallas grandes
    },
    nextStep() {
      console.log("Evento 'continuar' recibido: cambiando a paso 2...");
      this.step = 2; // Cambiar a la vista de confirmación
    },
  },
};
</script>

<style scoped>
.inscripcion-view {
  display: flex;
  height: 100vh; /* Asegura que la vista ocupe toda la altura */
  background-color: #f9f5fd;
}

.toggle-btn {
  position: fixed;
  top: 20px;
  left: 20px;
  z-index: 20;
  background: none;
  border: none;
  font-size: 24px;
  cursor: pointer;
}

.content {
  flex-grow: 1;
  padding: 20px;
  transition: margin-left 0.3s ease;
}

.content-expanded {
  margin-left: 250px; /* Espacio para el sidebar */
}

@media (max-width: 768px) {
  .content-expanded {
    margin-left: 0; /* Sin margen en pantallas móviles */
  }
}
</style>
