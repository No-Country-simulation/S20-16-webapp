<template>
  <div class="dashboard">
    <div class="content" :class="{ 'ms-sidebar': isSidebarVisible && !isMobile, 'content-expanded': isSidebarVisible && !isMobile }">
      <Header :carrera="alumno?.carrera" :nombre="alumno?.nombre" />

      <div class="content-wrapper">
        <!-- Título y botón -->
        <div class="d-flex justify-content-between align-items-center mb-4">
          <h1 class="h2 mb-0">Cancelar inscripciones</h1>
          <button class="btn btn-primary px-4">
            <i class="bi bi-x-lg me-2"></i> Cancelar
          </button>
        </div>

        <!-- Barra de búsqueda -->
        <div class="search-container mb-4">
          <div class="input-group">
            <span class="input-group-text border-end-0 bg-white">
              <i class="bi bi-search text-muted"></i>
            </span>
            <input type="text" class="form-control border-start-0" placeholder="Buscar" v-model="searchQuery">
          </div>
          <button class="btn btn-filter ms-2">
            <i class="bi bi-sliders2"></i>
          </button>
        </div>

        <!-- Tabla -->
        <div class="table-container">
          <table class="table table-hover">
            <thead>
              <tr>
                <th scope="col" width="50">
                  <div class="form-check">
                    <input class="form-check-input" type="checkbox" :checked="selectAll" @change="toggleSelectAll">
                  </div>
                </th>
                <th scope="col">Materia/Examen</th>
                <th scope="col">Fecha <i class="bi bi-chevron-down ms-1"></i></th>
                <th scope="col">Categoría</th>
              </tr>
            </thead>
            <tbody>
              <tr v-for="item in filteredItems" :key="item.id" :class="{ 'selected-row': selectedItems.includes(item.id) }">
                <td>
                  <div class="form-check">
                    <input class="form-check-input" type="checkbox" :checked="selectedItems.includes(item.id)" @change="toggleSelect(item.id)">
                  </div>
                </td>
                <td>{{ item.nombre }}</td>
                <td>{{ item.fecha }}</td>
                <td>{{ item.categoria }}</td>
              </tr>
            </tbody>
          </table>
        </div>

        <!-- Paginación -->
        <nav class="mt-4">
          <ul class="pagination justify-content-center">
            <li class="page-item" :class="{ disabled: currentPage === 1 }">
              <button class="page-link"><i class="bi bi-chevron-left"></i></button>
            </li>
            <li v-for="page in totalPages" :key="page" class="page-item" :class="{ active: currentPage === page }">
              <button class="page-link" @click="currentPage = page">{{ page }}</button>
            </li>
            <li class="page-item" :class="{ disabled: currentPage === totalPages }">
              <button class="page-link"><i class="bi bi-chevron-right"></i></button>
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
.dashboard {
  display: flex;
  height: 100vh;
  width: 100%;
  overflow: auto;
  background-color: #f9f5fd;
  padding: 1.5rem;
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
  margin-left: 220px; /* Reducido de 250px a 220px */
}

.content-wrapper {
  background: white;
  border-radius: 12px;
  padding: 1.5rem; /* Reducido de 2rem a 1.5rem */
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.05);
  max-width: 900px; /* Controla el ancho máximo */
  margin: 0 auto;
}

.search-container {
  display: flex;
  align-items: center;
}

.input-group {
  max-width: 350px; /* Reducido de 400px a 350px */
}

.btn-filter {
  background: white;
  border: 1px solid #e0e0e0;
  color: #666;
  padding: 0.4rem 0.7rem; /* Un poco más compacto */
  border-radius: 6px;
}

.table-container {
  border: 1px solid #e0e0e0;
  border-radius: 10px; /* Reducido para mayor compactación */
  overflow: hidden;
}

.table {
  font-size: 14px; /* Letra más pequeña */
}

.pagination {
  gap: 3px;
}

.page-link {
  padding: 0.4rem 0.6rem; /* Compacto */
}

@media (max-width: 768px) {
  .content-wrapper {
    padding: 1rem;
  }
  
  .search-container {
    flex-direction: column;
    gap: 0.8rem;
  }

  .input-group {
    width: 100%;
    max-width: none;
  }
}
</style>
