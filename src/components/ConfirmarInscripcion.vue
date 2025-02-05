<template>
  <div class="confirmacion-container" :class="{ 'ms-sidebar': isSidebarVisible && !isMobile }">
    <!-- Header -->
    <Header :carrera="getAlumno?.carrera" :nombre="getAlumno?.nombre" />

    <!-- Título -->
    <h3>Revisá las materias seleccionadas</h3>

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
        @click="finalizarInscripcion" 
        class="btn btn-confirmar"
        :disabled="!materiasSeleccionadas || materiasSeleccionadas.length === 0"
      >
        Confirmar
      </button>
    </div>

    <!-- Modal de confirmación -->
    <div class="modal-backdrop" v-if="isModalVisible" @click="closeModal">
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
              <h2>Inscripción Finalizada</h2>
            </div>
            <button class="close-button" @click="closeModal" aria-label="Cerrar">
              <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                <line x1="18" y1="6" x2="6" y2="18"></line>
                <line x1="6" y1="6" x2="18" y2="18"></line>
              </svg>
            </button>
          </div>
          <div class="modal-body">
            <p>Tu inscripción ha sido procesada exitosamente.</p>
          </div>
          <div class="modal-footer">
            <button class="action-button" @click="confirmarYCerrar">
              Entendido
            </button>
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
      isMobile: window.innerWidth <= 768,
      isSidebarVisible: window.innerWidth > 768,
      isModalVisible: false
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
      this.$router.push('/inscripciones');
    },
    finalizarInscripcion() {
      if (this.materiasSeleccionadas && this.materiasSeleccionadas.length > 0) {
        this.isModalVisible = true;
      }
    },
    eliminarMateria(id) {
      if (this.materiasSeleccionadas) {
        this.materiasSeleccionadas = this.materiasSeleccionadas.filter(
          materia => materia.id !== id
        );
      }
    },
    closeModal() {
      this.isModalVisible = false;
    },
    confirmarYCerrar() {
      this.closeModal();
      this.$router.push('/dashboard');
    }
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

.header {
  max-width: 900px;   
  margin-bottom: 20px;
}


.ms-sidebar {
  margin-left: 300px; 
  transition: margin 0.3s ease;
}


.confirmacion-frame {
  background-color: white;
  border-radius: 16px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.05);
  padding: 20px;
  margin-bottom: 20px;
}

.year-title {
  font-size: 22px;
  font-weight: 500;
  margin-bottom: 15px;
}

.materias-list {
  display: flex;
  flex-direction: column;
  gap: 12px;
}

.materia-item {
  display: grid;
  grid-template-columns: auto 1fr auto;
  align-items: center;
  gap: 12px;
  padding: 10px;
  background-color: #f9f9f9;
  border-radius: 8px;
  transition: all 0.2s ease;
}

.fecha-box {
  background-color: #f9ecfb;
  padding: 10px;
  border-radius: 8px;
  font-weight: 500;
  min-width: 55px;
  text-align: center;
}

.materia-info {
  display: flex;
  flex-direction: column;
  gap: 6px;
}

.materia-nombre {
  font-weight: 500;
  font-size: 14px;
}

.materia-horario {
  color: #666;
  font-size: 12px;
}

.delete-btn {
  background: none;
  border: none;
  cursor: pointer;
  padding: 6px;
  color: #4040ff;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
  transition: background-color 0.2s;
}

.delete-btn:hover {
  background-color: #f0f0ff;
}

.delete-btn:focus {
  outline: none;
  box-shadow: 0 0 0 2px #4040ff33;
}

.actions {
  display: flex;
  justify-content: flex-end;
  gap: 12px;
  margin-top: 20px;
}

.btn {
  padding: 10px 18px;
  border-radius: 8px;
  font-size: 14px;
  font-weight: 500;
  cursor: pointer;
  transition: all 0.2s;
  min-width: 180px;
}

.btn:disabled {
  opacity: 0.5;
  cursor: not-allowed;
}

.btn-volver {
  background-color: white;
  border: 2px solid #4040ff;
  color: #4040ff;
}

.btn-volver:hover:not(:disabled) {
  background-color: #f8f9ff;
}

.btn-confirmar {
  background-color: #4040ff;
  border: none;
  color: white;
}

.btn-confirmar:hover:not(:disabled) {
  background-color: #3333cc;
}

.no-materias {
  text-align: center;
  color: #666;
  padding: 20px;
}

/* Estilos para el modal */
.modal-backdrop {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000;
}

.modal-container {
  background: white;
  border-radius: 12px;
  width: 90%;
  max-width: 500px;
  padding: 24px;
  position: relative;
}

.modal-header {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  margin-bottom: 20px;
}

.header-content {
  display: flex;
  align-items: center;
  gap: 16px;
}

.info-icon {
  background-color: #f0f0ff;
  color: #4040ff;
  width: 40px;
  height: 40px;
  border-radius: 8px;
  display: flex;
  align-items: center;
  justify-content: center;
}

.close-button {
  background: none;
  border: none;
  padding: 8px;
  cursor: pointer;
  color: #666;
  border-radius: 4px;
  transition: all 0.2s;
}

.close-button:hover {
  background-color: #f0f0f0;
}

.modal-content{
  font-size: 24px;
  margin: 0;
  font-weight: 500;
}

.modal-body {
  margin-bottom: 24px;
}

.modal-body p {
  margin: 0;
  line-height: 1.5;
  color: #333;
}

.modal-footer {
  display: flex;
  justify-content: center;
}

.action-button {
  background-color: #4040ff;
  color: white;
  border: none;
  padding: 12px 32px;
  border-radius: 8px;
  font-size: 16px;
  font-weight: 500;
  cursor: pointer;
  transition: background-color 0.2s;
  min-width: 200px;
}

.action-button:hover {
  background-color: #3333cc;
}

/* Animaciones */
.modal-backdrop {
  animation: fadeIn 0.2s ease-out;
}

.modal-container {
  animation: slideIn 0.2s ease-out;
}

@keyframes fadeIn {
  from { opacity: 0; }
  to { opacity: 1; }
}

@keyframes slideIn {
  from {
    transform: translateY(20px);
    opacity: 0;
  }
  to {
    transform: translateY(0);
    opacity: 1;
  }
}

@media (max-width: 768px) {
  .confirmacion-container {
    padding: 10px;
    margin: 20px auto;
  }

  .ms-sidebar {
    margin-left: 0;
  }

  .actions {
    flex-direction: column;
  }

  .btn {
    width: 100%;
  }
}
</style>

