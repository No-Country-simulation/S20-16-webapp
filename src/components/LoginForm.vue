<template>
  <div class="background-container">
    <div class="form-frame">
      <h1 class="edulink">Edulink</h1>
      <h4 class="text-center mb-4">Iniciar Sesión</h4>
<<<<<<< Updated upstream

      <form @submit.prevent="handleLogin">

=======
      <form @submit.prevent="handleLogin">
>>>>>>> Stashed changes
        <div class="mb-3">
          <label for="email" class="form-label">Email</label>
          <input
            type="email"
            id="email"
            v-model="email"
            class="form-control rounded-input"
            placeholder="&#xf0e0; example@gmail.com"
            required
          />
        </div>
        <div class="mb-3">
          <label for="password" class="form-label">Contraseña</label>
          <input 
            type="password" 
            id="password" 
            v-model="password" 
            class="form-control rounded-input" 
            placeholder="&#x1F512;"
            required
          />
        </div>
        <div class="mb-3">
          <label for="forgot-password" class="form-label forgot-password">¿Olvidaste tu contraseña?</label>
        </div>
        <div>
          <button 
            type="submit" 
            :class="buttonClass" 
            class="btn w-100 rounded-button"
          >
            Ingresar
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
      email: '',
      password: ''   
    };
  },
  computed: {
    buttonClass() {
      return this.email && this.password ? 'btn-active' : 'btn-inactive';
    },
    // Acceso a numero_inicio desde el estado de Vuex
    numero_inicio() {
      return this.$store.state.numero_inicio; 
    }
  },
  methods: {
    ...mapActions(['login']),
    async handleLogin() {
      const credentials = { email: this.email, password: this.password };
      try {
        // La respuesta contiene las credenciales, incluyendo numero_inicio
        const response = await this.login(credentials); 

        // Redirección usando numero_inicio desde el estado de Vuex
        if (this.numero_inicio === 0) {
          this.$router.push('/PassForm'); // Redirige si es el primer inicio de sesión
        } else {
          this.$router.push('/dashboard'); // Redirige al dashboard si ya no es el primer inicio
        }
      } catch (error) {
        console.error('Error:', error.message); // Para depurar el error
        alert('Error de inicio de sesión. Verifica tus credenciales.');
      }
    }
  }
};
</script>
