<template>
  <div class="confirmacion-container" :class="{ 'ms-sidebar': isSidebarVisible && !isMobile }">
    <!-- Header -->
    <Header :carrera="getAlumno?.carrera" :nombre="getAlumno?.nombre" />

    <!-- Título -->
    <h3>Revisa los exámenes seleccionadas</h3>

    <!-- Contenedor del frame -->
    <div class="confirmacion-frame">
      <div class="year-section">
        <h2 class="year-title">Primer año</h2>

        <div v-if="materiasSeleccionadas && materiasSeleccionadas.length > 0" class="materias-list">
          <div v-for="materia in materiasSeleccionadas" :key="materia.id" class="materia-item">
            <div class="fecha-box">
              {{ materia.fecha || '10/03' }}
            </div>
            <div class="materia-info">
              <div class="materia-nombre">{{ materia.nombre }}</div>
              <div class="materia-horario">{{ materia.horario }}</div>
            </div>
            <button 
              class="delete-btn" 
              @click.stop="eliminarMateria(materia.id)" 
              aria-label="Eliminar materia"
            >
              <svg width="20" height="20" viewBox="0 0 15 15" fill="none" xmlns="http://www.w3.org/2000/svg">
                <path d="M11.7816 4.03157C12.0062 3.80702 12.0062 3.44295 11.7816 3.2184C11.5571 2.99385 11.193 2.99385 10.9685 3.2184L7.50005 6.68682L4.03164 3.2184C3.80708 2.99385 3.44301 2.99385 3.21846 3.2184C2.99391 3.44295 2.99391 3.80702 3.21846 4.03157L6.68688 7.49999L3.21846 10.9684C2.99391 11.193 2.99391 11.557 3.21846 11.7816C3.44301 12.0061 3.80708 12.0061 4.03164 11.7816L7.50005 8.31316L10.9685 11.7816C11.193 12.0061 11.5571 12.0061 11.7816 11.7816C12.0062 11.557 12.0062 11.193 11.7816 10.9684L8.31322 7.49999L11.7816 4.03157Z" fill="currentColor"/>
              </svg>
            </button>
          </div>
        </div>
        <div v-else class="no-materias">
          <p>No has seleccionado materias.</p>
        </div>
      </div>
    </div>

    <!-- Botones de acción -->
    <div class="actions">
      <button @click="volver" class="btn btn-volver">Volver</button>
      <button 
        @click="mostrarModal" 
        class="btn btn-confirmar"
        :disabled="!materiasSeleccionadas || materiasSeleccionadas.length === 0"
      >
        Confirmar
      </button>
    </div>

    <!-- Modal de confirmación -->
    <div v-if="modalVisible" class="modal-backdrop" @click="cerrarModal">
      <div class="modal-container" @click.stop>
        <div class="modal-content">
          <div class="modal-header">
            <div class="header-content">
              <div class="info-icon">
                <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                  <path d="M22 11.08V12a10 10 0 1 1-5.93-9.14"></path>
                  <polyline points="22 4 12 14.01 9 11.01"></polyline>
                </svg>
              </div>
              <h2>Confirmación de Inscripción</h2>
            </div>
            <button class="close-button" @click="cerrarModal" aria-label="Cerrar">
              <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                <line x1="18" y1="6" x2="6" y2="18"></line>
                <line x1="6" y1="6" x2="18" y2="18"></line>
              </svg>
            </button>
          </div>
          <div class="modal-body">
            <p>¿Estás seguro de que deseas finalizar la inscripción?</p>
          </div>
          <div class="modal-footer">
            <button @click="cerrarModal" class="btn btn-cancelar">Cancelar</button>
            <button @click="finalizarInscripcion" class="btn btn-confirmar">Confirmar</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex';
import Header from '../components/Header.vue';

export default {
  name: 'ConfirmacionInscripcion',
  components: { 
    Header 
  },
  data() {
    return {
      materiasSeleccionadas: [],
      modalVisible: false,
      isMobile: window.innerWidth <= 768,
      isSidebarVisible: window.innerWidth > 768
    };
  },
  computed: {
    ...mapGetters({
      getAlumno: 'getAlumno',
    }),
  },
  created() {
    this.materiasSeleccionadas = this.obtenerMateriasSeleccionadas();
    window.addEventListener('resize', this.handleResize);
  },
  beforeDestroy() {
    window.removeEventListener('resize', this.handleResize);
  },
  methods: {
    obtenerMateriasSeleccionadas() {
      try {
        const materiasString = this.$route.query.materiasSeleccionadas;
        return materiasString ? JSON.parse(materiasString) : [];
      } catch (error) {
        console.error('Error al parsear materias:', error);
        return [];
      }
    },
    handleResize() {
      this.isMobile = window.innerWidth <= 768;
      this.isSidebarVisible = window.innerWidth > 768;
    },
    volver() {
      this.$router.push('/inscripcionExamenes');
    },
    mostrarModal() {
      this.modalVisible = true;
    },
    cerrarModal() {
      this.modalVisible = false;
    },
    finalizarInscripcion() {
      if (this.materiasSeleccionadas && this.materiasSeleccionadas.length > 0) {
        // Lógica para finalizar inscripción
        this.cerrarModal();
        this.$router.push('/dashboard');
      }
    },
    eliminarMateria(id) {
      if (this.materiasSeleccionadas) {
        this.materiasSeleccionadas = this.materiasSeleccionadas.filter(
          materia => materia.id !== id
        );
      }
    },
  },
};
</script>

<style scoped>
.confirmacion-container {
  padding: 20px;
  max-width: 1000px;
  margin: 40px auto;
  overflow: hidden;
}

.ms-sidebar {
  margin-left: 300px; 
  transition: margin 0.3s ease;
}

.header {
  max-width: 900px;
  width: 100%;
}

.confirmacion-frame {
  background-color: white;
  border-radius: 16px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.05);
  padding: 30px;
  margin-bottom: 30px;
}

.year-title {
  font-size: 24px;
  font-weight: 500;
  margin-bottom: 20px;
}

.materias-list {
  display: flex;
  flex-direction: column;
  gap: 16px;
}

.materia-item {
  display: grid;
  grid-template-columns: auto 1fr auto;
  align-items: center;
  gap: 16px;
  padding: 8px;
  background-color: white;
  border-radius: 8px;
  transition: all 0.2s ease;
}

.fecha-box {
  background-color: #E0E4FF;
  padding: 10px;
  border-radius: 8px;
  font-weight: 500;
  min-width: 55px;
  text-align: center;
  
}

.materia-nombre {
  font-weight: 500;
  color: #4C5EF7;
}

.materia-horario {
  font-size: 12px;
  color: #6A6A6A;
}

.delete-btn {
  background: none;
  border: none;
  cursor: pointer;
}

.actions {
  display: flex;
  justify-content: space-between;
  margin-top: 30px;
}

.btn {
  padding: 12px 24px;
  border-radius: 8px;
  font-weight: 500;
  cursor: pointer;
}

.btn-volver {
  background-color: #E0E4FF;
  color: #4C5EF7;
}

.btn-confirmar {
  background-color: #4C5EF7;
  color: white;
}

.modal-backdrop {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: rgba(0, 0, 0, 0.5);
  display: flex;
  align-items: center;
  justify-content: center;
}

.modal-container {
  background-color: white;
  border-radius: 8px;
  width: 400px;
  padding: 20px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}

.modal-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.close-button {
  background: none;
  border: none;
  cursor: pointer;
}

.modal-footer {
  display: flex;
  justify-content: space-between;
  margin-top: 20px;
}
</style>
