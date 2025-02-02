<template>
  <div class="modal-backdrop" v-if="isVisible" @click="closeModal">
    <div class="modal-container" @click.stop>
      <!-- Contenido del modal -->
      <div class="modal-content">
        <!-- Icono de información y título -->
        <div class="modal-header">
          <div class="header-content">
            <div class="info-icon">
              <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
                <circle cx="12" cy="12" r="10"></circle>
                <line x1="12" y1="16" x2="12" y2="12"></line>
                <line x1="12" y1="8" x2="12.01" y2="8"></line>
              </svg>
            </div>
            <h2>¿Necesitás ayuda?</h2>
          </div>
          <button class="close-button" @click="closeModal" aria-label="Cerrar">
            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round">
              <line x1="18" y1="6" x2="6" y2="18"></line>
              <line x1="6" y1="6" x2="18" y2="18"></line>
            </svg>
          </button>
        </div>

        <!-- Cuerpo del modal -->
        <div class="modal-body">
          <p>Por favor, envianos un email a <strong>soporte@instituto.edu.ar</strong>.</p>
          <p>Resolveremos tus dudas lo antes posible.</p>
        </div>

        <!-- Botón de acción -->
        <div class="modal-footer">
          <button class="action-button" @click="handleConfirm">
            Entendido
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, onUnmounted } from 'vue'
import { useRouter } from 'vue-router'
import { useStore } from 'vuex'

const router = useRouter()
const store = useStore()
const isVisible = ref(false)

// Mostrar el modal cuando se monta el componente
onMounted(() => {
  isVisible.value = true
  // Prevenir scroll del body cuando el modal está abierto
  document.body.style.overflow = 'hidden'
})

onUnmounted(() => {
  // Restaurar scroll del body cuando se desmonta el componente
  document.body.style.overflow = 'auto'
})

const closeModal = () => {
  isVisible.value = false
  redirectToLastPage()
}

const handleConfirm = () => {
  closeModal()
}

const redirectToLastPage = () => {
  // Pequeño delay para la animación
  setTimeout(() => {
    const lastRoute = store.state.lastVisitedRoute
    if (lastRoute) {
      router.push(lastRoute)
    } else {
      router.push({ name: 'Dashboard' })
    }
  }, 150)
}
</script>

<style scoped>
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
  border: 2px dashed #4040ff;
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

h2 {
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

.modal-body p:first-child {
  margin-bottom: 8px;
}

.modal-footer {
  display: flex;
  justify-content: center;
}

.action-button {
  background-color: #4040ff;
  color: white;
  border: none;
  padding: 12px 32px;
  border-radius: 8px;
  font-size: 16px;
  font-weight: 500;
  cursor: pointer;
  transition: background-color 0.2s;
  min-width: 200px;
}

.action-button:hover {
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
  from {
    opacity: 0;
  }
  to {
    opacity: 1;
  }
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