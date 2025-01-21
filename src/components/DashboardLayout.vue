<template>
  <div class="dashboard">
    <!-- Sidebar -->
    <Sidebar :isSidebarVisible="isSidebarVisible" />

    <!-- Contenido principal -->
    <div class="content" :class="{ 'content-expanded': isSidebarVisible }">

      <!-- Fila 1 -->
      <div class="row mb-4">
        <!-- Fila 1 Columna 1: Carrera -->
        <div class="col-md-5">
          <h1>{{ carrera }}</h1>
        </div>

        <!-- Fila 1 Columna 2: Foto Estudiante -->
        <div class="col-md-6">
          <div class="student-info d-flex justify-content-end align-items-center">
            <img :src="fotoEstudiante" alt="Foto Estudiante" class="student-photo mr-3" />
            <h2>{{ nombreEstudiante }}</h2>
          </div>
        </div>


      </div>

      <!-- Fila 2 -->
      <div class="row mb-4">
        <!-- Fila 2 Columna 1: Gráfico y Materias -->
        <div class="col-md-6">
          <div class="frame">
            <h3>Gráfico</h3>
            <div class="chart">
              <!-- Aquí va el gráfico -->
              [Gráfico Aquí]
            </div>
            <div class="subframes">
              <div class="subframe">
                <h4>Materias Cursadas</h4>
                <!-- Lista de materias cursadas -->
                <ul>
                  <li>Matemáticas</li>
                  <li>Física</li>
                  <li>Programación</li>
                </ul>
              </div>
              <div class="subframe">
                <h4>Materias Pendientes</h4>
                <!-- Lista de materias pendientes -->
                <ul>
                  <li>Redes</li>
                  <li>Base de Datos</li>
                </ul>
              </div>
            </div>
          </div>
        </div>

        <!-- Fila 2 Columna 2: 3 Frames -->
        <div class="col-md-6">
          <div class="frame">
            <h3>Frame 1</h3>
            <h2>Subtítulo 1</h2>
          </div>
          <div class="frame">
            <h3>Frame 2</h3>
            <h2>Subtítulo 2</h2>
          </div>
          <div class="frame">
            <h3>Frame 3</h3>
            <h2>Subtítulo 3</h2>
          </div>
        </div>
      </div>

      <!-- Fila 3: Tabs -->
      <div class="row">
        <!-- Fila 3 Columna 1: Tab 1 -->
        <div class="col-md-6">
          <div class="frame">
            <!-- Cambiar el título dinámicamente según la pestaña activa -->
            <h2>{{ activeTab === 'tab2-home' ? 'Primer año' : 'Segundo año' }}</h2>

            <!-- Contenedor para las flechas, alineadas a la derecha -->
            <div class="arrow-navigation">
              <button class="btn btn-link prev-arrow" @click="changeTab('prev')">
                &lt;
              </button>
              <button class="btn btn-link next-arrow" @click="changeTab('next')">
                &gt;
              </button>
            </div>

            <div class="tab-content" id="myTab2Content">
              <div class="tab-pane fade" :class="{ 'show active': activeTab === 'tab2-home' }" id="tab2-home"
                role="tabpanel" aria-labelledby="tab2-home-tab">
                <p>Contenido del primer tab de la segunda columna.</p>
              </div>
              <div class="tab-pane fade" :class="{ 'show active': activeTab === 'tab2-profile' }" id="tab2-profile"
                role="tabpanel" aria-labelledby="tab2-profile-tab">
                <p>Contenido del segundo tab de la segunda columna.</p>
              </div>
            </div>
          </div>
        </div>



        <!-- Fila 3 Columna 2: Tab 2 -->
        <div class="col-md-6">
          <div class="frame">
            <h3>Tab 2</h3>
            <ul class="nav nav-tabs custom-tabs" id="myTab2" role="tablist">
              <li class="nav-item" role="presentation">
                <a class="nav-link active" id="tab2-home-tab" data-bs-toggle="tab" href="#tab2-home" role="tab"
                  aria-controls="tab2-home" aria-selected="true">Tus materias</a>
              </li>
              <li class="nav-item" role="presentation">
                <a class="nav-link" id="tab2-profile-tab" data-bs-toggle="tab" href="#tab2-profile" role="tab"
                  aria-controls="tab2-profile" aria-selected="false">Próximos examenes finales</a>
              </li>
            </ul>
            <div class="tab-content" id="myTab2Content">
              <div class="tab-pane fade show active" id="tab2-home" role="tabpanel" aria-labelledby="tab2-home-tab">
                <p>Contenido del primer tab de la segunda columna.</p>
              </div>
              <div class="tab-pane fade" id="tab2-profile" role="tabpanel" aria-labelledby="tab2-profile-tab">
                <p>Contenido del segundo tab de la segunda columna.</p>
              </div>
            </div>
          </div>
        </div>

      </div>
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
      isMobile: false, // Detecta si el dispositivo es móvil
      carrera: 'Ingeniería en Sistemas',
      nombreEstudiante: 'Juan Pérez',
      fotoEstudiante: 'https://via.placeholder.com/150',
      activeTab: 'tab2-home',  // Establecer el tab por defecto
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
    },
    changeTab(direction) {
      if (direction === 'prev') {
        // Cambiar al tab anterior
        this.activeTab = this.activeTab === 'tab2-home' ? 'tab2-profile' : 'tab2-home';
      } else if (direction === 'next') {
        // Cambiar al siguiente tab
        this.activeTab = this.activeTab === 'tab2-home' ? 'tab2-profile' : 'tab2-home';
      }
    },
  }
};
</script>

<style scoped>
.dashboard {
  display: flex;
  height: 100vh;
  /* Asegura que la altura sea del 100% de la pantalla */
  width: 100%;
  /* Asegura que ocupe todo el ancho */
  max-width: 100%;
  /* Para evitar que el contenido tenga un límite de ancho */
  overflow: hidden;
  /* Evita el desbordamiento */
  padding: 0;
  /* Elimina márgenes o rellenos adicionales */
}

.content {
  flex-grow: 1;
  padding: 1rem;
  transition: margin-left 0.3s ease;
  margin-left: 0;
  /* Cuando el sidebar está oculto no hay margen izquierdo */
}

.content-expanded {
  margin-left: 250px;
  /* Cuando el sidebar está visible, el contenido se mueve 250px a la derecha */
}

.student-info {
  display: flex;
  justify-content: flex-end;
  /* Alinea todo a la derecha */
  align-items: center;
  /* Asegura que la imagen y el texto estén a la misma altura */
}

.student-photo {
  width: 60px;
  /* Tamaño de la foto */
  height: 60px;
  border-radius: 50%;
  /* Hace la imagen circular */
  margin-right: 10px;
  /* Espacio entre la foto y el nombre */
}

h2 {
  margin: 0;
  /* Elimina márgenes innecesarios */
}



.frame {
  padding: 20px;
  border: 1px solid #ccc;
  margin-bottom: 20px;
}

.chart {
  height: 200px;
  background-color: #f5f5f5;
  margin-bottom: 10px;
}

.subframes {
  display: flex;
  justify-content: space-between;
}

.subframe {
  width: 48%;
}

.nav-tabs .nav-link {
  cursor: pointer;
}

.custom-tabs {
  margin-top: 0;
  /* Eliminar el margen superior del contenedor de tabs */
}

.custom-tabs .nav-tabs {
  margin-top: 0;
  /* Eliminar el margen superior de las pestañas */
}

.custom-tabs .nav-link {
  border-bottom: 2px solid transparent;
  /* Línea base transparente */
  color: #000;
  /* Color del texto de las pestañas */
  padding: 10px 15px;
  /* Espaciado adecuado para las pestañas */
}

.custom-tabs .nav-link.active {
  border-bottom: 2px solid #007bff;
  /* Línea visible debajo del tab activo */
  color: #007bff;
  /* Cambia el color del texto en el tab activo */
  font-weight: bold;
  /* Resalta el texto del tab activo */
}

.custom-tabs .nav-link:hover {
  border-bottom: 2px solid #0056b3;
  /* Línea de hover (cuando el ratón pasa sobre el tab) */
  color: #0056b3;
  /* Cambia el color del texto cuando está en hover */
}

.arrow-navigation {
  display: flex;
  justify-content: flex-end;
  /* Alinea las flechas hacia la derecha */
  margin-top: 10px;
  /* Espacio entre el título y las flechas */
}

.arrow-navigation button {
  font-size: 20px;
  /* Tamaño de los símbolos */
  color: #007bff;
  /* Color de las flechas */
  background-color: transparent;
  /* Sin fondo */
  border: 2px solid #007bff;
  /* Borde azul */
  border-radius: 50%;
  /* Borde circular */
  padding: 8px;
  /* Padding pequeño */
  margin-left: 10px;
  /* Espacio entre las flechas */
  cursor: pointer;
  transition: background-color 0.3s, border-color 0.3s;
  width: 35px;
  /* Establecer un ancho fijo */
  height: 35px;
  /* Establecer una altura fija */
  display: flex;
  justify-content: center;
  align-items: center;
  font-weight: bold;
  /* Asegura que las flechas sean un poco más visibles */
  text-decoration: none;
  /* Elimina el subrayado */
}

.arrow-navigation button:hover {
  background-color: #007bff;
  /* Color de fondo al pasar el ratón */
  color: white;
  /* Cambiar color de la flecha cuando está activo */
  border-color: #0056b3;
  /* Cambiar el borde al pasar el ratón */
}

.arrow-navigation button:focus {
  outline: none;
  /* Elimina el contorno por defecto */
}



@media (max-width: 768px) {
  .content {
    margin-left: 0;
  }
}
</style>
