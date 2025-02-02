<template>
  <div class="background-container">
    <div class="form-frame">
      <img src="../assets/img/Logotipo1.png" alt="Edulink" class="logo-image" />
      <h2 class="text-center mb-4">Actualización de contraseña requerida</h2>
      <p>Por motivos de seguridad, es necesario que cambies la contraseña asignada por defecto</p>

      <form @submit.prevent="handlePasswordChange">
        <div class="mb-3">
          <label for="current-password" class="form-label">Ingresa tu contraseña actual</label>
          <input type="password" id="current-password" v-model="currentPassword" class="form-control rounded-input"
            placeholder="&#x1F512;" required />
        </div>
        <div class="mb-3">
          <label for="new-password" class="form-label">Elige tue nueva contraseña</label>
          <input type="password" id="new-password" v-model="newPassword" class="form-control rounded-input"
            placeholder="&#x1F512;" required />
          <label for="mensaje" class="form-label">Debe tener al menos 8 caracteres, una letra mayúscula y un
            número</label>
        </div>
        <div class="mb-3">
          <label for="confirm-password" class="form-label">Confirma tu nueva Contraseña</label>
          <input type="password" id="confirm-password" v-model="confirmPassword" class="form-control rounded-input"
            placeholder="&#x1F512;" required />
        </div>
        <div>
          <button type="submit" :class="buttonClass" class="btn w-100 rounded-button">
            Actualizar Contraseña
          </button>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
import { mapActions } from 'vuex';

export default {
  data() {
    return {
      currentPassword: '',
      newPassword: '',
      confirmPassword: ''
    };
  },
  computed: {
    buttonClass() {
      return this.currentPassword && this.newPassword && this.confirmPassword ? 'btn-active' : 'btn-inactive';
    }
  },
  methods: {
    ...mapActions(['updatePassword']),
    async handlePasswordChange() {
      if (this.newPassword !== this.confirmPassword) {
        alert('Las contraseñas no coinciden.');
        return;
      }
      const passwordData = { currentPassword: this.currentPassword, newPassword: this.newPassword };
      try {
        await this.updatePassword(passwordData);
        alert('Contraseña actualizada correctamente.');
        this.$router.push('/dashboard');
      } catch (error) {
        console.error('Error:', error.message);
        alert('Error al actualizar la contraseña.');
      }
    }
  }
};
</script>
<style scoped>
.logo-image {
  width: 150px;
  /* Ajusta el tamaño de la imagen según tus necesidades */
  height: auto;
  margin-bottom: 1.5rem;
  /* Espaciado entre la imagen y el título */
  margin-left: 10rem;
}

button:hover {
  background-color: #150ca6;
}
</style>
