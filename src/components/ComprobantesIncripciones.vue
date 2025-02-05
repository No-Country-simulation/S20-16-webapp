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
          <h3>Comprobantes</h3>
          <button class="btn btn-primary" @click="showModal">
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

    <!-- Modal de confirmación -->
    <div v-if="isModalVisible" class="modal-backdrop" @click="closeModal">
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
              <h2>Descarga Exitosa</h2>
            </div>
            <button class="close-button" @click="closeModal" aria-label="Cerrar">
              <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                <line x1="18" y1="6" x2="6" y2="18"></line>
                <line x1="6" y1="6" x2="18" y2="18"></line>
              </svg>
            </button>
          </div>
          <div class="modal-body">
            <p>Documentación enviada a su correo electrónico.</p>
          </div>
          <div class="modal-footer">
            <button @click="closeModal" class="btn btn-confirmar">Entendido</button>
          </div>
        </div>
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
const isModalVisible = ref(false);

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

const showModal = () => {
  isModalVisible.value = true;
};

const closeModal = () => {
  isModalVisible.value = false;
};

onMounted(() => {
  handleResize();
  window.addEventListener('resize', handleResize);
});
</script>

<style scoped>
.dashboard {
  display: flex;
  flex-direction: column;
  height: 100vh;
  width: 100%;
  background-color: #f9f5fd;
  padding: 1.5rem;
  overflow: auto;
}

.content {
  display: flex;
  flex-direction: column;
  transition: margin-left 0.3s ease-in-out;
  flex-grow: 1;
  max-width: 1000px; /* Aumento del máximo ancho del contenido */
  width: 100%; /* Para asegurarnos de que ocupe todo el ancho disponible */
}

.header {
  max-width: 900px; /* Aumento del máximo ancho para el header */
  width: 100%;    
  margin: 0 auto 1rem; /* Centrado y con separación inferior */
}

.ms-sidebar {
  margin-left: 280px; /* Ajusta este valor al ancho de tu sidebar */
}

.comprobantes-container {
  background-color: white;
  border-radius: 12px; /* Cambio a 12px para que coincida con el primer componente */
  padding: 1.5rem; /* Reducido a 1.5rem para dar un estilo más compacto */
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.05); /* Cambio en la sombra */
  margin-bottom: 1rem;
}

.table-container {
  border: 1px solid #e0e0e0;
  border-radius: 10px; /* Reducido para mayor compactación */
  overflow: hidden;
}

.table {
  font-size: 14px; /* Reducido el tamaño de fuente para coincidir con el primero */
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
  padding: 16px; /* Mismo tamaño de padding que en el primer componente */
  border-bottom: 1px solid #e0e0e0;
}

.pagination {
  gap: 3px; /* Agregado un espacio más pequeño entre las páginas */
}

.page-item .page-link {
  padding: 0.4rem 0.6rem; /* Compactado */
  background-color: #4339f2;
  color: white;
  border: none;
}

.page-item.active .page-link {
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

  .search-container {
    flex-direction: column;
    gap: 0.8rem;
  }

  .input-group {
    width: 100%;
    max-width: none;
  }
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

.modal-content h2 {
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
  justify-content: flex-end;
}

.btn-confirmar {
  background-color: #4040ff;
  color: white;
  border: none;
  padding: 12px 24px;
  border-radius: 8px;
  font-size: 16px;
  font-weight: 500;
  cursor: pointer;
  transition: background-color 0.2s;
}

.btn-confirmar:hover {
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
</style>