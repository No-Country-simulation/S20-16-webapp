<template>
  <div class="background-container">
    <div class="form-frame">
      <img src="../assets/img/Logotipo1.png" alt="Edulink" class="logo-image" />
      <h4 class="text-center mb-4">Iniciar Sesión</h4>

      <form @submit.prevent="handleLogin">

        <div class="mb-3">
          <label for="email" class="form-label">Email</label>
          <input
            type="email"
            id="email"
            v-model="email"
            class="form-control rounded-input"
            placeholder="&#xf0e0; example@gmail.com"
            required @focus="clearErrorMessage"
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
            required @focus="clearErrorMessage"
          />
        </div>
        <div class="mb-3">
          <p v-if="errorMessage" class="error-message">{{ errorMessage }}</p>
          <label for="forgot-password" class="form-label forgot-password">¿Olvidaste tu contraseña?</label>
        </div>
        <div>
          <button 
            type="submit" 
            :class="buttonClass" 
            class="btn w-100 rounded-button"          >
            Ingresar
          </button>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
import { mapActions } from "vuex";

export default {
  data() {
    return {
      email: "",
      password: "",
      errorMessage: "",
    };
  },
  computed: {
    buttonClass() {
      return this.email && this.password ? "btn-active" : "btn-inactive";
    }
  },
methods: {
    ...mapActions(["login"]),
    async handleLogin() {
      try {
        const usuario = await this.login({ email: this.email, password: this.password });

        // Redirección después del login
        if (usuario.numero_inicio === 0) {       
          this.$router.push("/passform"); // Si es el primer inicio, redirige a PassForm
        } else {
          this.$router.push("/dashboard"); // Si no, va al Dashboard
        }
      } catch (error) {
        console.error('Error de inicio de sesión:', error);  // Log del error
        
        this.errorMessage = "Los datos ingresados no son correctos. Inténtalo de nuevo.";

      }      
    },
    clearErrorMessage() {
    this.errorMessage = "";
  }
  }
};
</script>
