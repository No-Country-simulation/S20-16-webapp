<template>
    <div class="dashboard">
      <div class="content" :class="{
        'ms-sidebar': isSidebarVisible && !isMobile,  // Control de visibilidad de la barra lateral
        'content-expanded': isSidebarVisible && !isMobile // Expansión de contenido
      }">
        <Header :carrera="alumno?.carrera" :nombre="alumno?.nombre" />
  
        <!-- Comprobantes Section -->
        <div class="comprobantes-container">
          <!-- Título y botón de descarga -->
          <div class="d-flex justify-content-between align-items-center mb-4">
            <h1>Comprobantes</h1>
            <button class="btn btn-primary">
              <i class="bi bi-download me-2"></i>
              Descargar
            </button>
          </div>
  
          <!-- Barra de búsqueda y filtros -->
          <div class="row mb-4">
            <div class="col">
              <div class="input-group">
                <span class="input-group-text">
                  <i class="bi bi-search"></i>
                </span>
                <input type="text" class="form-control" placeholder="Buscar" v-model="searchQuery" />
                <button class="btn btn-outline-secondary" type="button">
                  <i class="bi bi-funnel"></i>
                </button>
              </div>
            </div>
          </div>
  
          <!-- Tabla de comprobantes -->
          <div class="card mb-4">
            <div class="table-responsive">
              <table class="table table-hover mb-0">
                <thead>
                  <tr>
                    <th width="50">
                      <div class="form-check">
                        <input
                          class="form-check-input"
                          type="checkbox"
                          :checked="selectAll"
                          @change="toggleSelectAll"
                        />
                      </div>
                    </th>
                    <th>Materia/Examen</th>
                    <th>
                      Fecha
                      <i class="bi bi-chevron-down ms-1"></i>
                    </th>
                    <th>Categoría</th>
                  </tr>
                </thead>
                <tbody>
                  <tr
                    v-for="item in filteredItems"
                    :key="item.id"
                    :class="{ 'table-active': selectedItems.includes(item.id) }"
                  >
                    <td>
                      <div class="form-check">
                        <input
                          class="form-check-input"
                          type="checkbox"
                          :checked="selectedItems.includes(item.id)"
                          @change="toggleSelect(item.id)"
                        />
                      </div>
                    </td>
                    <td>{{ item.nombre }}</td>
                    <td>{{ item.fecha }}</td>
                    <td>{{ item.categoria }}</td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
  
          <!-- Paginación -->
          <nav aria-label="Page navigation">
            <ul class="pagination justify-content-center">
              <li class="page-item" :class="{ disabled: currentPage === 1 }">
                <button class="page-link">
                  <i class="bi bi-chevron-left"></i>
                </button>
              </li>
              <li
                v-for="page in totalPages"
                :key="page"
                class="page-item"
                :class="{ active: currentPage === page }"
              >
                <button class="page-link" @click="currentPage = page">
                  {{ page }}
                </button>
              </li>
              <li class="page-item" :class="{ disabled: currentPage === totalPages }">
                <button class="page-link">
                  <i class="bi bi-chevron-right"></i>
                </button>
              </li>
            </ul>
          </nav>
        </div>
      </div>
    </div>
  </template>
  
  <script setup>
  import { ref, computed, onMounted, watch } from 'vue';
  import { useStore } from 'vuex';
  import Header from '../components/Header.vue';
  
  const store = useStore();
  const alumno = ref(null);
  const materias = ref([]);
  
  onMounted(async () => {
      try {
          await store.dispatch('fetchAlumnoData');
          await store.dispatch('fetchMateriasData');
          alumno.value = store.getters['getAlumno'] || {};
          materias.value = store.getters['getMaterias'] || [];
      } catch (error) {
          console.error('Error cargando datos:', error);
      }
  });
  
  watch(() => store.getters['getMaterias'], (newMaterias) => {
      materias.value = newMaterias || [];
  });
  
  const isMobile = ref(false);
  const isSidebarVisible = ref(true);
  const searchQuery = ref('');
  const currentPage = ref(1);
  const selectedItems = ref([]);
  
  const filteredItems = computed(() => {
      return materias.value.filter(item =>
          item.nombre?.toLowerCase().includes(searchQuery.value.toLowerCase())
      );
  });
  
  const totalPages = computed(() => Math.ceil(filteredItems.value.length / 10));
  
  const selectAll = computed(() => selectedItems.value.length === filteredItems.value.length);
  
  const toggleSelectAll = () => {
      selectedItems.value = selectAll.value ? [] : filteredItems.value.map(item => item.id);
  };
  
  const toggleSelect = (id) => {
      const index = selectedItems.value.indexOf(id);
      if (index === -1) {
          selectedItems.value.push(id);
      } else {
          selectedItems.value.splice(index, 1);
      }
  };
  
  const handleResize = () => {
    isMobile.value = window.innerWidth <= 768;
    isSidebarVisible.value = !isMobile.value;
  };
  onMounted(() => {
    handleResize();
    window.addEventListener('resize', handleResize);
  });
  </script>
  
  <style scoped>
  /* Agrega aquí el estilo CSS necesario para hacer la página igual al primer diseño */
  .dashboard {
    display: flex;
    flex-direction: column;
    height: 100vh;
    width: 100%;
    background-color: #f9f5fd;
    padding: 2rem;
    overflow: auto;
  }
  
  .content {
    display: flex;
  flex-direction: column;
  transition: margin-left 0.3s ease-in-out;
  flex-grow: 1;
  max-width: 1400px; /* Establecer un máximo ancho */
  margin: 0 auto; /* Centrado */
  }

  .header {
  max-width: 900px; /* Igual que el content-wrapper */
  width: 100%;  
  padding: 1rem 1.5rem; /* Mantiene un diseño compacto */  
  margin: 0 auto 1rem; /* Centrado y con separación inferior */
}
  
.header {
  max-width: 900px; /* Igual que el content-wrapper */
  width: 100%;  
  padding: 1rem 1.5rem; /* Mantiene un diseño compacto */  
  margin: 0 auto 1rem; /* Centrado y con separación inferior */
}
  .ms-sidebar {
    margin-left: 350px; /* Ajusta este valor al ancho de tu sidebar */
  }
  
  .comprobantes-container {
    background-color: white;
    border-radius: 1rem;
    padding: 1.5rem;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.05);
    margin-bottom: 1rem;
  }
  
  .comprobantes-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-bottom: 1.5rem;
  }
  
  .search-bar {
    margin-bottom: 1.5rem;
  }
  
  .card {
    background-color: white;
    border-radius: 12px;
    overflow: hidden;
    margin-bottom: 24px;
    border: 1px solid #e0e0e0;
  }
  
  .table th {
    font-weight: 500;
    color: #666;
  }
  
  .table-responsive table {
    width: 100%;
    border-collapse: collapse;
  }
  
  th, td {
    padding: 16px;
    border-bottom: 1px solid #e0e0e0;
  }
  
  .pagination .page-item .page-link {
    background-color: #4339f2;
    color: white;
    border: none;
  }
  
  .pagination .page-item.active .page-link {
    background-color: #7c3aed;
  }
  
  @media (max-width: 768px) {
    .dashboard {
      padding: 1rem;
    }
  
    .comprobantes-container {
      margin-left: 0;
      padding: 1rem;
    }
  }
  </style>
  