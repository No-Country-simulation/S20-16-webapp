<template>
  <div class="confirmacion-container">
    <!-- Header -->
    <Header :carrera="getAlumno?.carrera" :nombre="getAlumno?.nombre" />

    <!-- Título -->
    <h1 class="header">Revisá las materias seleccionadas</h1>

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
      materiasSeleccionadas: []
    };
  },
  computed: {
    ...mapGetters({
      getAlumno: 'getAlumno',
    }),
  },
  created() {
    this.materiasSeleccionadas = this.obtenerMateriasSeleccionadas();
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
    volver() {
      this.$router.push('/inscripciones');
    },
    finalizarInscripcion() {
      if (this.materiasSeleccionadas && this.materiasSeleccionadas.length > 0) {
        // Aquí puedes agregar la lógica para guardar en Vuex si es necesario
        alert('Inscripción finalizada');
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
  max-width: 900px;
  margin: 0 auto; 
}

.header {
  font-size: 28px;
  font-weight: 500;
  margin-bottom: 30px;
}

.header {
  max-width: 900px; /* Igual que el content-wrapper */
  width: 100%;  
  padding: 1rem 1.5rem; /* Mantiene un diseño compacto */  
  margin: 0 auto 1rem; /* Centrado y con separación inferior */
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
  background-color: #f9ecfb;
  padding: 12px;
  border-radius: 8px;
  font-weight: 500;
  min-width: 65px;
  text-align: center;
}

.materia-info {
  display: flex;
  flex-direction: column;
  gap: 4px;
}

.materia-nombre {
  font-weight: 500;
  font-size: 16px;
}

.materia-horario {
  color: #f9ecfb;
  font-size: 14px;
}

.delete-btn {
  background: none;
  border: none;
  cursor: pointer;
  padding: 8px;
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
  padding: 12px 24px;
  border-radius: 8px;
  font-size: 16px;
  font-weight: 500;
  cursor: pointer;
  transition: all 0.2s;
  min-width: 200px;
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
</style>