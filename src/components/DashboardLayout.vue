<template>
  <div class="dashboard">
    <div class="content" :class="{
      'ms-sidebar': isSidebarVisible && !isMobile,
      'content-expanded': isSidebarVisible && !isMobile
    }">
      <!-- Header -->
      <Header :carrera="getAlumno?.carrera" :nombre="getAlumno?.nombre" />

      <!-- Fila 2 -->
      <div class="row g-4 mb-4">
        <div class="col-md-7">
          <div class="frame">
            <h3 class="mb-4">¡Vas por el {{ porcentajeMateriasAprobadas }}% de la carrera!</h3>
            <div class="row align-items-center">
              <div class="col-md-6">
                <div class="progress-circle">
                  <div class="progress-circle-inner">
                    <svg viewBox="0 0 36 36" class="circular-chart">
                      <path d="M18 2.0845
                        a 15.9155 15.9155 0 0 1 0 31.831
                        a 15.9155 15.9155 0 0 1 0 -31.831" fill="none" stroke="#eee" stroke-width="3" />
                      <path d="M18 2.0845
                        a 15.9155 15.9155 0 0 1 0 31.831
                        a 15.9155 15.9155 0 0 1 0 -31.831" fill="none" stroke="url(#gradient)" stroke-width="3"
                        :stroke-dasharray="`${porcentajeMateriasAprobadas}, 100`" />
                      <defs>
                        <linearGradient id="gradient">
                          <stop offset="0%" stop-color="#4339F2" />
                          <stop offset="100%" stop-color="#7C3AED" />
                        </linearGradient>
                      </defs>
                    </svg>
                    <div class="progress-text">{{ porcentajeMateriasAprobadas }}%</div>
                  </div>
                </div>
              </div>
              <div class="col-md-6">
                <div class="status-box">
                  <div class="status-item approved">
                    <div class="status-icon">
                      <i class="fa fa-check"></i>
                    </div>
                    <div class="status-content">
                      <p class="status-label">Materias aprobadas</p>
                      <p class="status-number">{{ getMateriasAprobadas.length }}</p>
                    </div>
                  </div>
                  <div class="status-item pending">
                    <div class="status-icon">
                      <i class="fa fa-clock"></i>
                    </div>
                    <div class="status-content">
                      <p class="status-label">Materias pendientes</p>
                      <p class="status-number">{{ getMateriasPendientes.length }}</p>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>

        <div class="col-md-5">
          <div class="registration-cards">
            <div class="reg-card">
              <h3 class="text-primary">Inscripción a materias</h3>
              <p>{{ getFechaInicioInscripcionMaterias ? formatDate(getFechaInicioInscripcionMaterias) : 'N/A' }}</p>
            </div>
            <div class="reg-card">
              <h3 class="text-primary">Inscripción a exámenes</h3>
              <p>{{ getFechaInicioInscripcionExamenes ? formatDate(getFechaInicioInscripcionExamenes) : 'N/A' }}</p>
            </div>
            <div class="reg-card">
              <h3 class="text-primary">Comienzo de clases</h3>
              <p>{{ getFechaComienzoClases ? formatDate(getFechaComienzoClases) : 'N/A' }}</p>
            </div>
          </div>
        </div>
      </div>

      <!-- Fila 3 -->
      <div class="row g-4">
        <div class="col-md-6">
          <div class="frame">
            <div class="d-flex justify-content-between align-items-center mb-4">
              <h3>{{ activeTab1 === 'tab1-home' ? 'Primer Año' : 'Segundo Año' }}</h3>
              <div class="nav-buttons">
                <button @click="changeTab1('prev')" class="nav-btn">&lt;</button>
                <button @click="changeTab1('next')" class="nav-btn">&gt;</button>
              </div>
            </div>
            <div class="subjects-list">
              <template v-if="activeTab1 === 'tab1-home'">
                <div v-for="materia in getMateriasPrimerAnio" :key="materia.nombre" class="subject-item">
                  <span>{{ materia.nombre }}</span>
                  <span :class="['status-badge', getEstadoClass(materia.estado)]">
                    {{ materia.estado }}
                  </span>
                </div>
              </template>
              <template v-if="activeTab1 === 'tab2-home'">
                <div v-for="materia in getMateriasSegundoAnio" :key="materia.nombre" class="subject-item">
                  <span>{{ materia.nombre }}</span>
                  <span :class="['status-badge', getEstadoClass(materia.estado)]">
                    {{ materia.estado }}
                  </span>
                </div>
              </template>
            </div>
          </div>
        </div>

        <div class="col-md-6">
          <div class="frame">
            <div class="d-flex justify-content-between align-items-center mb-4">
              <h3>{{ activeTab2 === 'tab2-home' ? 'Tus materias' : 'Próximos exámenes finales' }}</h3>
              <div class="nav-buttons">
                <button @click="changeTab2('prev')" class="nav-btn">&lt;</button>
                <button @click="changeTab2('next')" class="nav-btn">&gt;</button>
              </div>
            </div>
            <div class="exam-list">
              <!-- Tab de materias en curso -->
              <template v-if="activeTab2 === 'tab2-home'">
                <div v-for="materia in getMateriasEnCurso" :key="materia.nombre"
                  class="exam-item d-flex align-items-center">
                  <div class="date-box"
                    style="background-color: #f9ecfb; padding: 10px; border-radius: 5px; min-width: 80px; text-align: center;">
                    {{ materia.fecha }}
                  </div>
                  <div class="exam-info ms-3">
                    <h4>{{ materia.nombre }}</h4>
                    <p>{{ materia.diaHorario }}</p>
                  </div>
                </div>
              </template>
              <!-- Tab de materias desaprobadas -->
              <template v-if="activeTab2 === 'tab2-profile'">
                <div v-for="materia in getMateriasDesaprobadas" :key="materia.nombre"
                  class="exam-item d-flex justify-content-between">
                  <!-- Materia a la izquierda -->
                  <div class="exam-info">
                    <h4>{{ materia.nombre }}</h4>                   
                  </div>

                  <!-- Día y horario a la derecha -->
                  <div class="dia-horario" style="text-align: right;">
                    <p>{{ materia.diaHorario }}</p>
                  </div>
                </div>
              </template>
            </div>
          </div>
        </div>


      </div>
    </div>
  </div>
</template>

<script>
import Sidebar from '../components/Sidebar.vue';
import Header from '../components/Header.vue'; // Importamos el nuevo header
import { mapGetters, mapActions } from 'vuex';

export default {
  components: { Sidebar, Header },
  data() {
    return {
      isSidebarVisible: true,
      isMobile: false,
      activeTab1: 'tab1-home',
      activeTab2: 'tab2-home',
    };
  },
  computed: {
    ...mapGetters({
      getAlumno: 'getAlumno',
      getMaterias: 'getMaterias',
      getProximosExamenes: 'getProximosExamenes',
      getFechasInscripcion: 'getFechasInscripcion',
    }),

    getFechaInicioInscripcionMaterias() {
      return this.getFechasInscripcion?.inscripcionMaterias || 'N/A';
    },

    getFechaInicioInscripcionExamenes() {
      return this.getFechasInscripcion?.inscripcionExamenes || 'N/A';
    },

    getFechaComienzoClases() {
      return this.getFechasInscripcion?.comienzoClases || 'N/A';
    },

    // Calcular el porcentaje de materias aprobadas
    porcentajeMateriasAprobadas() {
      const totalMaterias = this.getMaterias.length;
      const materiasAprobadas = this.getMateriasAprobadas.length;
      if (totalMaterias === 0) return 0; // Evitar dividir por 0
      return Math.round((materiasAprobadas / totalMaterias) * 100);
    },

    getMateriasPrimerAnio() {
      return this.getMaterias.filter(materia => materia.anio === 1);
    },

    getMateriasSegundoAnio() {
      return this.getMaterias.filter(materia => materia.anio === 2);
    },

    getMateriasEnCurso() {
      return this.getMaterias.filter(materia => materia.estado === 'EnCurso');
    },

    getMateriasAprobadas() {
      return this.getMaterias.filter(materia => materia.estado === 'Aprobada');
    },

    getMateriasPendientes() {
      return this.getMaterias.filter(materia => materia.estado === 'EnCurso' || materia.estado === 'Desaprobada' || materia.estado === '-');
    },
    getMateriasDesaprobadas() {
      return this.getMaterias.filter(materia => materia.estado === 'Desaprobada');
    },


    getEstadoClass() {
      return (estado) => ({
        'estado-aprobada': estado === 'Aprobada',
        'estado-desaprobada': estado === 'Desaprobada',
        'estado-en-curso': estado === 'EnCurso',
      });
    },
  },
  methods: {
    changeTab1(action) {
      if (action === 'prev') {
        this.activeTab1 = this.activeTab1 === 'tab1-home' ? 'tab2-home' : 'tab1-home';
      } else {
        this.activeTab1 = this.activeTab1 === 'tab1-home' ? 'tab2-home' : 'tab1-home';
      }
    },

    changeTab2(action) {
      if (action === 'prev') {
        this.activeTab2 = this.activeTab2 === 'tab2-home' ? 'tab2-profile' : 'tab2-home';
      } else {
        this.activeTab2 = this.activeTab2 === 'tab2-home' ? 'tab2-profile' : 'tab2-home';
      }
    },

    formatDate(date) {
      const options = { year: 'numeric', month: 'long', day: 'numeric' };
      return new Date(date).toLocaleDateString(undefined, options);
    },
  },
};
</script>
<style scoped>
.header {
  max-width: 900px;
  /* Igual que el content-wrapper */
  width: 100%;
}

.dashboard {
  display: flex;
  height: 100vh;
  width: 100%;
  overflow: auto;
  background-color: #f9f5fd;
  margin-left: 50px;
  padding: 2rem;
}

.content {
  transition: margin-left 0.3s ease-in-out;
}

.ms-sidebar {
  margin-left: 250px;
  /* Ajusta este valor al ancho de tu sidebar */
}

.frame {
  background: white;
  border-radius: 1rem;
  padding: 1.5rem;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.05);
  margin-bottom: 1rem;
}

.exam-item {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 15px;
  /* Espaciado entre elementos */
}

.exam-info {
  flex: 1;
  /* Esto asegurará que ocupe todo el espacio restante */
}

.dia-horario {
  flex: 0 0 auto;
  /* Esto hará que el campo de "Día y horario" no se expanda */
  text-align: right;
  /* Asegura que el texto esté alineado a la derecha */
}

/* Círculo de progreso */
.progress-circle {
  position: relative;
  width: 200px;
  height: 200px;
  margin: 0 auto;
}

.progress-circle-inner {
  position: relative;
  width: 100%;
  height: 100%;
}

.circular-chart {
  width: 100%;
  height: 100%;
  transform: rotate(-90deg);
}

.progress-text {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  font-size: 2.5rem;
  font-weight: bold;
  color: #333;
}

/* Estado de materias */
.status-box {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.status-item {
  background: #f8f9fd;
  border-radius: 0.75rem;
  padding: 1rem;
  display: flex;
  align-items: center;
  gap: 1rem;
}

.status-icon {
  width: 40px;
  height: 40px;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
}

.approved .status-icon {
  background: #e6f7ed;
  color: #28a745;
}

.pending .status-icon {
  background: #fff1f1;
  color: #dc3545;
}

.status-content {
  flex: 1;
}

.status-label {
  margin: 0;
  font-size: 0.9rem;
  color: #666;
}

.status-number {
  margin: 0;
  font-size: 1.5rem;
  font-weight: bold;
  color: #333;
}

/* Tarjetas de inscripción */
.registration-cards {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.reg-card {
  background: white;
  border-radius: 1rem;
  padding: 1.25rem;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.05);
}

.reg-card h3 {
  color: #4339F2;
  margin-bottom: 0.5rem;
  font-size: 1.1rem;
}

.reg-card p {
  margin: 0;
  color: #666;
}

/* Lista de materias */
.subjects-list {
  display: flex;
  flex-direction: column;
  gap: 0.75rem;
}

.subject-item {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 0.5rem 0;
  border-bottom: 1px solid #eee;
}

/* Badges de estado */
.status-badge {
  padding: 0.5rem 1rem;
  border-radius: 2rem;
  font-size: 0.875rem;
  font-weight: 500;
}

.estado-aprobada {
  background: #e6f7ed;
  /* color: #28a745; */
}

.estado-desaprobada {
  background: #fff1f1;
  /* color: #dc3545; */
}

.estado-en-curso {
  background: #fff8e6;
  /* color: #ffc107; */
}

/* Lista de exámenes */
.exam-list {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.exam-item {
  display: flex;
  gap: 1rem;
  align-items: center;
}

.date-box {
  background: #f8e6ff;
  padding: 0.75rem;
  border-radius: 0.5rem;
  font-weight: 500;
  color: #7c3aed;
  text-align: center;
  min-width: 70px;
  /* Puedes probar también eliminando este min-width */
  white-space: nowrap;
  /* Añadir esta propiedad para evitar que el texto se corte */
}

.exam-info h4 {
  margin: 0;
  font-size: 1rem;
}

.exam-info p {
  margin: 0;
  color: #666;
  font-size: 0.9rem;
}

/* Botones de navegación */
.nav-buttons {
  display: flex;
  gap: 0.5rem;
}

.nav-btn {
  width: 32px;
  height: 32px;
  border-radius: 50%;
  border: none;
  background: #f8f9fd;
  color: #666;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
  transition: background-color 0.2s;
}

.nav-btn:hover {
  background: #eee;
}

/* Responsive */
@media (max-width: 768px) {
  .dashboard {
    padding: 1rem;
  }

  .progress-circle {
    width: 150px;
    height: 150px;
  }

  .reg-card {
    margin: 0;
    width: 100%;
  }
}
</style>