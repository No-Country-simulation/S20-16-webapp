<template>
  <div class="dashboard">
    <!-- Sidebar -->
    <Sidebar :isSidebarVisible="isSidebarVisible" />

    <!-- Contenido principal -->
    <div class="content" :class="{ 'content-expanded': isSidebarVisible && !isMobile }">
      <!-- Fila 1 -->
      <div class="row mb-4">
        <div class="col-md-5">
          <h2>{{ getAlumno?.carrera || 'Carrera no disponible' }}</h2>
        </div>
        <div class="col-md-6 d-flex justify-content-end">
          <div class="student-info">
            <!-- <img :src="getAlumno?.foto || '../assets/img/estudiante2.jpg'" alt="Foto Estudiante" class="student-photo" /> -->
            
            <img src="../assets/img/estudiante2.jpg" alt="Foto Estudiante" class="student-photo" />


            <h2>{{ getAlumno?.nombre || 'Estudiante' }}</h2>
          </div>
        </div>
      </div>

      <!-- Fila 2 -->
      <div class="row mb-4">
        <div class="col-md-7">
          <div class="frame">
            <h3>Progreso Académico</h3>
            <div class="row">
              <div class="col-md-6 chart">[Gráfico Aquí]</div>
              <div class="col-md-6 subframes">
                <div class="subframe cursadas">
                  <i class="fa fa-check-circle text-success"></i>
                  <h5>Materias Aprobadas</h5>
                  <p>{{ getMateriasAprobadas.length }}</p>
                </div>
                <div class="subframe pendientes">
                  <i class="fa fa-clock text-danger"></i>
                  <h5>Materias Pendientes</h5>
                  <p>{{ getMateriasPendientes.length }}</p>
                </div>
              </div>
            </div>
          </div>
        </div>

        <div class="col-md-5">
          <!-- Inscripción a Materias -->
          <div class="frame centered-text">
            <h3 class="title-blue">Inscripción a Materias</h3>
            <p class="date-range">
              {{ getFechaInicioInscripcionMaterias ? formatDate(getFechaInicioInscripcionMaterias) : 'N/A' }}
            </p>
          </div>

          <!-- Inscripción a Exámenes -->
          <div class="frame centered-text">
            <h3 class="title-blue">Inscripción a Exámenes</h3>
            <p class="date-range">
              {{ getFechaInicioInscripcionExamenes ? formatDate(getFechaInicioInscripcionExamenes) : 'N/A' }}
            </p>
          </div>

          <!-- Comienzo de Clases -->
          <div class="frame centered-text">
            <h3 class="title-blue">Comienzo de Clases</h3>
            <p class="date-range">
              {{ getFechaComienzoClases ? formatDate(getFechaComienzoClases) : 'N/A' }}
            </p>
          </div>
        </div>
      </div>

      <!-- Fila 3: Primer conjunto de tabs -->
      <div class="row">
        <div class="col-md-6">
          <div class="frame">
            <h2>{{ activeTab1 === 'tab1-home' ? 'Primer Año' : 'Segundo Año' }}</h2>
            <div class="arrow-navigation d-flex justify-content-end">
              <button @click="changeTab1('prev')">&lt;</button>
              <button @click="changeTab1('next')">&gt;</button>
            </div>
            <div class="tab-content">
              <div v-show="activeTab1 === 'tab1-home'">
                <table class="materias-table">
                  <tbody>
                    <tr v-for="materia in getMateriasPrimerAnio" :key="materia.nombre">
                      <td>{{ materia.nombre }}</td>
                      <td class="align-right">
                        <span :class="['styled-status', getEstadoClass(materia.estado)]">
                          {{ materia.estado }}
                        </span>
                      </td>
                    </tr>
                  </tbody>
                </table>
              </div>
              <div v-show="activeTab1 === 'tab1-profile'">
                <table class="materias-table">
                  <tbody>
                    <tr v-for="materia in getMateriasSegundoAnio" :key="materia.nombre">
                      <td>{{ materia.nombre }}</td>
                      <td class="align-right">
                        <span :class="['styled-status', getEstadoClass(materia.estado)]">
                          {{ materia.estado }}
                        </span>
                      </td>
                    </tr>
                  </tbody>
                </table>
              </div>
            </div>
          </div>
        </div>

        <div class="col-md-6">
          <div class="frame">
            <h2>{{ activeTab2 === 'tab2-home' ? 'Mis Materias Actuales' : 'Próximos Exámenes' }}</h2>
            <div class="arrow-navigation d-flex justify-content-end">
              <button @click="changeTab2('prev')">&lt;</button>
              <button @click="changeTab2('next')">&gt;</button>
            </div>
            <div class="tab-content">
              <div v-show="activeTab2 === 'tab2-home'">
                <table class="materias-table">
                  <thead>
                    <tr>
                      <th>Materia</th>
                      <th>Estado</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr v-for="materia in getMateriasEnCurso" :key="materia.nombre">
                      <td>{{ materia.nombre }}</td>
                      <td>{{ materia.horario || 'Horario no disponible' }}</td>
                    </tr>
                  </tbody>
                </table>
              </div>
              <div v-show="activeTab2 === 'tab2-profile'">
                <table class="materias-table">
                  <thead>
                    <tr>
                      <th>Materia</th>
                      <th>Fecha</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr v-for="examen in getProximosExamenes" :key="examen.materia">
                      <td>{{ examen.materia }}</td>
                      <td>{{ examen.fechaExamen }}</td>
                    </tr>
                  </tbody>
                </table>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Sidebar from '../components/Sidebar.vue';
import { mapGetters, mapActions } from 'vuex';

export default {
  components: { Sidebar },
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
      console.log('Fecha de inscripción a materias:', this.getFechasInscripcion.inscripcionMaterias);
      return this.getFechasInscripcion?.inscripcionMaterias || 'N/A';
    },

    getFechaInicioInscripcionExamenes() {
      console.log('Fecha de inscripción a exámenes:', this.getFechasInscripcion.inscripcionExamenes);
      return this.getFechasInscripcion?.inscripcionExamenes || 'N/A';
    },

    getFechaComienzoClases() {
      console.log('Fecha de comienzo de clases:', this.getFechasInscripcion.comienzoClases);
      return this.getFechasInscripcion?.comienzoClases || 'N/A';
    },

    getMateriasPrimerAnio() {
      return this.getMaterias.filter(materia => materia.anio === 1);
    },
    getMateriasSegundoAnio() {
      return this.getMaterias.filter(materia => materia.anio === 2);
    },
    getMateriasEnCurso() {
      return this.getMaterias.filter((materia) => materia.estado === 'EnCurso');
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
    console.log('Fechas agrupadas:', this.getFechasInscripcion); // Verifica los datos

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
      this.activeTab1 = tabs[(currentIndex + (direction === 'prev' ? -1 : 1) + tabs.length) % tabs.length];
    },
    changeTab2(direction) {
      const tabs = ['tab2-home', 'tab2-profile'];
      const currentIndex = tabs.indexOf(this.activeTab2);
      this.activeTab2 = tabs[(currentIndex + (direction === 'prev' ? -1 : 1) + tabs.length) % tabs.length];
    },
    formatDate(date) {
      if (!date) return '';
      const d = new Date(date);
      return d.toLocaleDateString();  // Formato simple de fecha
    },
  },
};
</script>


<style scoped>
/* Estilos para la estructura general del dashboard */
.dashboard {
  display: flex;
  height: 100vh;
  width: 100%;
  overflow: auto;
  background-color: #f9f5fd;
  margin-left: 50px;
  padding: 1.5rem 1rem 0;
}

.content {
  flex-grow: 1;
  padding: 1rem;
  transition: margin-left 0.3s;
}

.content-expanded {
  margin-left: 250px;
}

.student-info {
  display: flex;
  align-items: center;
}

.student-photo {
  width: 60px;
  height: 60px;
  border-radius: 50%;
  margin-right: 10px;
}

.centered-text {
  text-align: center;
}

.title-blue {
  color: #0056b3;
  font-weight: bold;
  margin-bottom: 0.5rem;
}

.date-range {
  font-size: 1.3rem;
}

.frame {
  padding: 20px;
  border: 1px solid #aaa;
  border-radius: 20px;
  margin-bottom: 20px;
  background-color: #fff;
  /* Fondo blanco */
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  /* Agrega una ligera sombra */
}

.frame.centered-text {
  text-align: center;
}

.frame .title-blue {
  margin-bottom: 0;
}

.subframes {
  display: flex;
  flex-direction: column;
  /* Apilar los elementos en columna */
  justify-content: space-between;
  margin-top: 20px;
}

.subframe {
  padding: 10px;
  background-color: #f4f4f9;
  border-radius: 10px;
  text-align: center;
  margin-bottom: 10px;
  /* Espacio entre los subframes */
}


.subframe i {
  font-size: 30px;
  margin-bottom: 10px;
}

.subframe h5 {
  margin: 0;
  font-weight: bold;
  color: #333;
}

.subframe p {
  font-size: 1.2rem;
}

.arrow-navigation button {
  width: 20px;
  height: 20px;
  border: 2px solid #0f0f0f;
  /* Borde azul */
  background-color: #0f0f0f;
  /* Fondo azul permanente */
  border-radius: 50%;
  /* Hace el círculo */
  display: flex;
  justify-content: center;
  align-items: center;
  font-size: 18px;
  /* Tamaño del texto */
  color: #fff;
  /* Color blanco para la flecha */
  cursor: pointer;
  margin: 0 5px;
  /* Espaciado entre botones */
}

.materias-table {
  width: 100%;
  border-collapse: collapse;
}

.materias-table th,
.materias-table td {
  padding: 10px;
  text-align: left;
  border-bottom: 1px solid #ddd;
}

.materias-table th {
  background-color: #f2f2f2;
}

.styled-status {
  display: inline-block;
  padding: 6px 12px;
  border-radius: 12px;
  font-size: 14px;
  font-weight: bold;
  color: #fff;
  text-align: center;
  min-width: 100px;
}

/* Colores específicos por estado */
.estado-aprobada {
  background-color: #28a745;
}

.estado-desaprobada {
  background-color: #dc3545;
}

.estado-en-curso {
  background-color: #ffc107;
  color: #000;
}

/* Alineación del estado a la derecha */
.align-right {
  text-align: right;
}

.custom-tabs li {
  display: inline-block;
  margin-right: 15px;
}

.custom-tabs li a {
  text-decoration: none;
  color: #0056b3;
  font-weight: bold;
}

.custom-tabs li a.active {
  color: #0056b3;
  border-bottom: 2px solid #0056b3;
}
</style>
