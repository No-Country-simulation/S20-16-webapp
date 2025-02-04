<template>

  <div class="dashboard">
    <div class="content" :class="{
      'ms-sidebar': isSidebarVisible && !isMobile,  // Aplica ms-sidebar solo si la barra lateral está visible y no es móvil
      'content-expanded': isSidebarVisible && !isMobile  // Este es el efecto de expansión cuando la barra está visible
       }">
      <!-- Header -->
      <Header :carrera="getAlumno?.carrera" :nombre="getAlumno?.nombre" />
      <!-- Fila 2 -->
      <div class="row g-4 mb-4">
        <div class="col-md-7">
          <div class="frame">
            <h3 class="mb-4">¡Vas por el 60% de la carrera!</h3>
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
                        stroke-dasharray="60, 100" />
                      <defs>
                        <linearGradient id="gradient">
                          <stop offset="0%" stop-color="#4339F2" />
                          <stop offset="100%" stop-color="#7C3AED" />
                        </linearGradient>
                      </defs>
                    </svg>
                    <div class="progress-text">60%</div>
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
              <template v-if="activeTab2 === 'tab2-profile'">
                <div v-for="examen in getProximosExamenes" :key="examen.materia" class="exam-item">
                  <div class="date-box">{{ examen.fechaExamen }}</div>
                  <div class="exam-info">
                    <h4>{{ examen.materia }}</h4>
                    <p>{{ examen.horario }}</p>
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
      return this.getMaterias.filter(materia => materia.estado === 'EnCurso' || materia.estado === 'Desaprobada');
    },
    getEstadoClass() {
      return (estado) => ({
        'estado-aprobada': estado === 'Aprobada',
        'estado-desaprobada': estado === 'Desaprobada',
        'estado-en-curso': estado === 'EnCurso',
      });
    },
  },

  mounted() {
    this.isMobile = window.innerWidth <= 768;
    this.isSidebarVisible = !this.isMobile;
    window.addEventListener('resize', this.handleResize);
    this.fetchAlumnoData();
  },
  beforeUnmount() {
    window.removeEventListener('resize', this.handleResize);
  },
  methods: {
    ...mapActions(['fetchAlumnoData']),
    handleResize() {
      this.isMobile = window.innerWidth <= 768;
      this.isSidebarVisible = !this.isMobile;
    },
    changeTab1(direction) {
      const tabs = ['tab1-home', 'tab1-profile'];
      const currentIndex = tabs.indexOf(this.activeTab1);
      const nextIndex = (currentIndex + (direction === 'prev' ? -1 : 1) + tabs.length) % tabs.length;
      this.activeTab1 = tabs[nextIndex];
    },
    changeTab2(direction) {
      const tabs = ['tab2-home', 'tab2-profile'];
      const currentIndex = tabs.indexOf(this.activeTab2);
      const nextIndex = (currentIndex + (direction === 'prev' ? -1 : 1) + tabs.length) % tabs.length;
      this.activeTab2 = tabs[nextIndex];
    },
    formatDate(date) {
      if (!date) return '';
      const d = new Date(date);
      return d.toLocaleDateString();
    },
  },
};
</script>

<style scoped>
.dashboard {
  display: flex;
  height: 100vh;
  width: 100%;
  overflow: auto;
  background-color: #f9f5fd;
  margin-left: 50px;
  padding: 2rem;
}

.header {
  max-width: 900px; /* Igual que el content-wrapper */
  width: 100%;  
  padding: 1rem 1.5rem; /* Mantiene un diseño compacto */  
  margin: 0 auto 1rem; /* Centrado y con separación inferior */
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
  color: #28a745;
}

.estado-desaprobada {
  background: #fff1f1;
  color: #dc3545;
}

.estado-en-curso {
  background: #fff8e6;
  color: #ffc107;
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