<template>
  <div class="inscripcion-container">
    <!-- Header -->
<!-- Header -->
  <Header :carrera="getAlumno?.carrera" :nombre="getAlumno?.nombre" />

    <h1>Seleccioná las materias a cursar</h1>

    <!-- Contenedor del frame de pestañas -->
    <div class="tabs-wrapper">
      <div class="tabs-frame">
        <!-- Pestañas para los años -->
        <div class="tabs">
          <button
            v-for="(anio, index) in anios"
            :key="index"
            :class="['tab', { active: currentTab === anio }]"
            @click="selectTab(anio)"
          >
            {{ anio }} año
          </button>
        </div>

        <!-- Contenido de las pestañas -->
        <div class="materias-tab">
          <div class="materias-grid">
            <div
              v-for="materia in filteredMaterias"
              :key="materia.id"
              class="materia-frame"
              :class="{ selected: isMateriaSelected(materia) }"
              @click="toggleMateria(materia)"
            >
              <span class="materia-nombre">{{ materia.nombre }}</span>
              <span class="materia-horario">{{ materia.horario }}</span>
            </div>
          </div>
        </div>
      </div>
    </div>

    <!-- Botones de acción -->
    <div class="actions">
      <button @click="volver" class="btn btn-volver">Volver</button>
      <button @click="continuar" class="btn btn-continuar">Continuar</button>
    </div>
  </div>
</template>

<script>
import { mapState, mapActions, mapGetters } from 'vuex';
import Header from '../components/Header.vue';

export default {
  components: { Header },
  data() {
    return {
      currentTab: 'Primer',
      anios: ['Primer', 'Segundo'],
    };
  },
  computed: {
    ...mapState({
      getMaterias: 'materias',
      materiasSeleccionadas: 'materiasSeleccionadas',
    }),
    ...mapGetters({
      getAlumno: 'getAlumno',
    }),
    filteredMaterias() {
      return this.getMaterias.filter((materia) => materia.anio === (this.currentTab === 'Primer' ? 1 : 2));
    },
  },
  methods: {
    ...mapActions(['agregarMateriaSeleccionada', 'eliminarMateriaSeleccionada']),
    selectTab(anio) {
      this.currentTab = anio;
    },
    toggleMateria(materia) {
      if (this.isMateriaSelected(materia)) {
        this.eliminarMateria(materia.id);
      } else {
        this.agregarMateriaSeleccionada(materia);
      }
    },
    isMateriaSelected(materia) {
      return this.materiasSeleccionadas.some((m) => m.id === materia.id);
    },
    volver() {
      this.$router.push('/dashboard');
    },
    continuar() {
      this.$emit('continuar');
      this.$router.push({
        name: 'confirmar-inscripcion', // Nombre de la ruta de la página de confirmación
        query: {
          materiasSeleccionadas: JSON.stringify(this.materiasSeleccionadas) // Pasamos las materias como JSON en los parámetros de la URL
        }
      });
    },
    eliminarMateria(materiaId) {
      this.eliminarMateriaSeleccionada(materiaId);
    },
  },
};
</script>
<style scoped>
.inscripcion-container {
  padding: 20px;
  max-width: 900px;
  margin: 0 auto; 
}

.header {
  /* border: 1px solid #ccc; */
  padding: 15px;
  font-size: 24px;
  margin-bottom: 20px;
}

.tabs-wrapper {
  margin-top: 20px;
}

.tabs-frame {
  background-color: white;
  border: 1px solid #ccc;
  border-radius: 8px;
  overflow: hidden;
}

.tabs {
  display: flex;
  border-bottom: 1px solid #ccc;
}

.tab {
  padding: 15px 30px;
  background: none;
  border: none;
  cursor: pointer;
  font-size: 18px;
  color: #666;
}

.tab.active {
  color: #000;
  font-weight: bold;
  position: relative;
}

.tab.active::after {
  content: '';
  position: absolute;
  bottom: -1px;
  left: 0;
  width: 100%;
  height: 3px;
  background-color: #4040ff;
}

.materias-grid {
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  gap: 20px;
  padding: 20px;
}

.materia-frame {
  background-color: #f8e6ff;
  border: 2px solid #f8e6ff;
  border-radius: 8px;
  padding: 15px;
  text-align: center;
  cursor: pointer;
  transition: all 0.3s ease;
}

.materia-frame:hover {
  border-color: #4040ff;
  background-color: #f0e6ff;
}

.materia-frame.selected {
  border-color: #4040ff;
  background-color: #f0e6ff;
}

.materia-nombre {
  display: block;
  font-weight: bold;
  margin-bottom: 5px;
}

.materia-horario {
  display: block;
  font-size: 14px;
  color: #666;
}

.actions {
  margin-top: 20px;
  display: flex;
  justify-content: flex-end;
  gap: 10px;
}

.btn {
  padding: 10px 30px;
  border: none;
  border-radius: 8px;
  cursor: pointer;
  font-size: 16px;
  font-weight: bold;
  transition: background-color 0.3s ease;
}

.btn-volver {
  background-color: white;
  color: #4040ff;
  border: 2px solid #4040ff;
}

.btn-continuar {
  background-color: #4040ff;
  color: white;
}

.btn-volver:hover {
  background-color: #f0f0ff;
}

.btn-continuar:hover {
  background-color: #3030ff;
}
</style>