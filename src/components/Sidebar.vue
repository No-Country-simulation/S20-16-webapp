<template>
    <aside :class="['sidebar', { 'sidebar-hidden': !isSidebarVisible }]">
        <div class="sidebar-header text-center">
            <img src="../assets/img/Logotipo1.png" alt="Edulink" class="logo-image">
            <label class="text-muted">Nombre de la Institución</label>
        </div>
        <nav>
            <ul class="nav flex-column">
                <li class="nav-item">
                    <a class="nav-link" :class="{ active: activeMenu === 'home' }" href="#" @click="setActive('home')">
                        <i class="fas fa-home me-2"></i> Home
                    </a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" :class="{ active: activeMenu === 'inscripciones' }" href="#" @click="setActive('inscripciones')">
                        <i class="fas fa-edit me-2"></i> Inscripciones
                    </a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" :class="{ active: activeMenu === 'cancelar' }" href="#" @click="setActive('cancelar')">
                        <i class="fas fa-times-circle me-2"></i> Cancelar Inscripciones
                    </a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" :class="{ active: activeMenu === 'comprobantes' }" href="#" @click="setActive('comprobantes')">
                        <i class="fas fa-file-invoice me-2"></i> Comprobantes
                    </a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" :class="{ active: activeMenu === 'ayuda' }" href="#" @click="setActive('ayuda')">
                        <i class="fas fa-question-circle me-2"></i> Ayuda
                    </a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" :class="{ active: activeMenu === 'cerrar' }" href="#" @click="setActive('cerrar')">
                        <i class="fas fa-sign-out-alt me-2"></i> Cerrar Sesión
                    </a>
                </li>
            </ul>
        </nav>
    </aside>
</template>

<script>
export default {
    props: {
        isSidebarVisible: {
            type: Boolean,
            required: true,
        },
    },
    data() {
        return {
            activeMenu: 'home',
        };
    },
    methods: {
        setActive(menu) {
            this.activeMenu = menu;
        },
    },
};
</script>

<style scoped>
.sidebar {
  width: 250px;
  padding: 1rem;
  height: 100vh; /* Asegura que cubra toda la altura */
  position: fixed;
  left: 1.5rem;
  top: 1.5rem;
  border-radius: 20px;
  border-color: black;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
  background-color: #f8f9fa;
  transition: transform 0.3s ease;
  transform: translateX(0); /* Visible por defecto */
}

.sidebar-hidden {
  transform: translateX(-100%); /* Oculta completamente a la izquierda */
}

.nav-link.active {
    background-color: #0d6efd; /* Fondo azul */
    color: white !important;   /* Texto blanco */
    font-weight: bold;
    border-radius: 5px;        /* Suavizar bordes */
}

.nav-link {
    color: black;              /* Texto negro cuando no está activo */
}

.nav-link:hover {
    color: #0d6efd;
}
</style>
