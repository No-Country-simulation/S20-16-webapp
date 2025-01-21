<template>
  <div class="sidebar" v-if="isSidebarVisible">
    <!-- Imagen de perfil o ícono -->
    <div class="profile-section text-center mb-4">
      <i class="fas fa-user-circle fa-5x text-white mb-3"></i>  <!-- Ícono de perfil -->
      <h5 class="text-white">User Name</h5>  <!-- Nombre debajo de la imagen -->
    </div>

    <!-- Panel de navegación -->
    <ul class="nav flex-column">
      <li class="nav-item">
        <a href="#" class="nav-link">Dashboard</a>
      </li>
      <li class="nav-item">
        <a href="#" class="nav-link">Settings</a>
      </li>
      <li class="nav-item">
        <a href="#" class="nav-link">Logout</a>
      </li>
    </ul>
  </div>
</template>

<script>
export default {
  props: {
    isSidebarVisible: Boolean
  }
};
</script>

<style scoped>
.sidebar {
  background-color: #007bff;
  color: white;
  height: 100vh;
  padding: 1rem;
  border-radius: 10px;
  position: fixed;
  top: 0;
  left: 0;
  width: 250px;
  z-index: 10;
}

.sidebar .nav-link {
  color: white;
}

.sidebar .nav-link:hover {
  color: #f8f9fa;
}

.profile-section i {
  font-size: 80px;  /* Tamaño grande para el ícono */
}

.profile-section h5 {
  font-size: 16px;
  margin-top: 10px;
}
</style>
