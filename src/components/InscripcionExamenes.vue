<template>
  <div class="inscripcion-container" :class="{ 'ms-sidebar': isSidebarVisible && !isMobile }">
    <!-- Header -->
    <Header :carrera="getAlumno?.carrera" :nombre="getAlumno?.nombre" />

    <h3>Selecciona los Exámenes a rendir</h3>

    <!-- Contenedor del frame de pestañas -->
    <div class="tabs-wrapper">
      <div class="tabs-frame">
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
import { ref, computed, onMounted } from "vue";
import { mapState, mapActions, mapGetters } from "vuex";
import Header from "../components/Header.vue";

export default {
  components: { Header },
  setup() {
    const currentTab = ref("Primer");
    const anios = ref(["Primer", "Segundo"]);
    const isMobile = ref(window.innerWidth <= 768);
    const isSidebarVisible = ref(!isMobile.value);

    const handleResize = () => {
      isMobile.value = window.innerWidth <= 768;
      isSidebarVisible.value = !isMobile.value;
    };

    onMounted(() => {
      window.addEventListener("resize", handleResize);
    });

    return {
      currentTab,
      anios,
      isMobile,
      isSidebarVisible,
    };
  },
  computed: {
    ...mapState({
      getMaterias: "materias",
      materiasSeleccionadas: "materiasSeleccionadas",
    }),
    ...mapGetters({
      getAlumno: "getAlumno",
    }),
    filteredMaterias() {
      return this.getMaterias
        .filter((materia) => materia.estado === "-")  // Filtramos por estado "-"
        .filter((materia) => materia.anio === (this.currentTab === "Primer" ? 1 : 2));  // Filtramos por año
    },
  },
  methods: {
    ...mapActions({
      agregarMateriaSeleccionada: "agregarMateriaSeleccionada",
      eliminarMateriaSeleccionada: "eliminarMateriaSeleccionada",
    }),
    selectTab(anio) {
      this.currentTab = anio;
    },
    toggleMateria(materia) {
      if (this.isMateriaSelected(materia)) {
        this.eliminarMateriaSeleccionada(materia.id); // Llamamos a la acción para eliminar
      } else {
        this.agregarMateriaSeleccionada(materia); // Llamamos a la acción para agregar
      }
    },
    isMateriaSelected(materia) {
      return this.materiasSeleccionadas.some((m) => m.id === materia.id);
    },
    volver() {
      this.$router.push("/dashboard");
    },
    continuar() {
      console.log("Exámenes seleccionados antes de navegar:", this.materiasSeleccionadas);
      this.$router.push({
        name: "confirmar-inscripcion-Examenes",
        query: { materiasSeleccionadas: JSON.stringify(this.materiasSeleccionadas) },
      });
    },
  },
};
</script>


<style scoped>
.inscripcion-container {
  padding: 15px;
  max-width: 1000px; /* Ajustado al ancho que quieres */
  margin: 0 auto;
}

.ms-sidebar {
  margin-left: 350px;
}

.header {
  width: 100%; /* Igual que el content-wrapper */
  padding: 2rem 1.5rem; /* Mantiene un diseño compacto */
  margin: 0 auto 1rem; /* Centrado y con separación inferior */
}

h1 {
  margin-bottom: 1rem;
}

.tabs-wrapper {
  background: white;
  border-radius: 1rem;
  padding: 1.5rem;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.05);
  margin-bottom: 1rem;
}

.tabs {
  display: flex;
  border-bottom: 1px solid #ccc;
  position: sticky;
  top: 0;
  background: white;
  z-index: 10;
}

.tab {
  padding: 10px 20px;
  border: none;
  background: none;
  cursor: pointer;
  font-size: 18px;
  color: #666;
}

.tab.active {
  font-weight: bold;
  color: #000;
  border-bottom: 3px solid #4040ff;
}

.materias-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
  gap: 15px;
  padding: 15px;
}

.materia-frame {
  background-color: #e0e4ff;
  border-radius: 8px;
  padding: 12px;
  text-align: center;
  cursor: pointer;
  transition: all 0.3s ease;
  display: flex;
  flex-direction: column;
  justify-content: center;
  min-height: 80px;
}

.materia-frame.selected {
  border: 2px solid #6d63f1;
}

.actions {
  margin-top: 20px;
  display: flex;
  justify-content: flex-end;
  gap: 10px;
}

.btn {
  padding: 10px 20px;
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

@media (max-width: 768px) {
  .inscripcion-container {
    padding: 1rem;
  }

  .tabs-wrapper {
    padding: 1rem;
  }

  .materias-grid {
    grid-template-columns: repeat(auto-fit, minmax(150px, 1fr));
    gap: 10px;
    padding: 10px;
  }
  .materias-tab {
  overflow-y: auto;
  max-height: calc(100vh - 200px); /* Ajusta este valor según sea necesario */
  padding-right: 5px; /* Espacio para la barra de desplazamiento */
}

/* Para navegadores webkit (Chrome, Safari) */
.materias-tab::-webkit-scrollbar {
  width: 8px;
}

.materias-tab::-webkit-scrollbar-thumb {
  background-color: #ccc;
  border-radius: 4px;
}

.materias-tab::-webkit-scrollbar-track {
  background-color: #f1f1f1;
}

.materia-frame:hover {
  transform: translateY(-2px);
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}

.materia-nombre {
  font-weight: 500;
  margin-bottom: 4px;
}

.materia-horario {
  font-size: 0.9em;
  color: #666;
}


}
</style>