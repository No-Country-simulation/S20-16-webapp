<script setup>
import { ref, computed, onMounted, watch } from "vue";
import { useStore } from "vuex";
import { useRoute } from "vue-router";
import Sidebar from "@/components/Sidebar.vue";

const store = useStore();
const route = useRoute();

const isAuthenticated = computed(() => store.getters.isAuthenticated);
const isLoginPage = computed(() => route.name === "Login");

// Estado del sidebar: visible en pantallas grandes, oculto en pequeñas
const isSidebarVisible = ref(window.innerWidth > 768);

// Función para manejar el tamaño de la pantalla y ocultar el sidebar en móviles
const updateSidebarVisibility = () => {
  isSidebarVisible.value = window.innerWidth > 768;
};

// Detectar cambios en el tamaño de la pantalla
window.addEventListener("resize", updateSidebarVisibility);

// Mostrar el sidebar si el usuario está autenticado y no está en el login
watch(isAuthenticated, (newVal) => {
  if (newVal && !isLoginPage.value) {
    updateSidebarVisibility();
  }
});

// Lógica para abrir/cerrar el sidebar en móviles
const toggleSidebar = () => {
  isSidebarVisible.value = !isSidebarVisible.value;
};

onMounted(() => {
  updateSidebarVisibility();
});
</script>

<template>
  <div class="app-container">
    <!-- Botón para abrir/cerrar el sidebar en móviles -->
    <button v-if="isAuthenticated && !isSidebarVisible" class="menu-button" @click="toggleSidebar">
      ☰
    </button>

    <!-- Sidebar: Se muestra si el usuario está autenticado -->
    <Sidebar v-if="isAuthenticated" :isSidebarVisible="isSidebarVisible" />

    <div class="content">
      <RouterView />
    </div>
  </div>
</template>

<style lang="scss">
@import "@/styles/_main.scss";

// .app-container {
//   display: flex;
// }

// .content {
//   flex-grow: 1;
//   padding: 20px;
// }

/* Botón para abrir/cerrar el sidebar en móviles */
.menu-button {
  position: fixed;
  top: 15px;
  left: 15px;
  background: #0d6efd;
  color: white;
  border: none;
  padding: 10px 15px;
  font-size: 20px;
  border-radius: 5px;
  cursor: pointer;
  z-index: 1000;
}
</style>
