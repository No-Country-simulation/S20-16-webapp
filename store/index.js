import { createStore } from 'vuex';
import axios from 'axios';

const store = createStore({
  state: {
    user: null,
    numero_inicio: null // Agrega numero_inicio al estado
  },
  mutations: {
    setUser(state, user) {
      state.user = user;
      state.numero_inicio = user.numero_inicio; // Almacena numero_inicio también
    }
  },
  actions: {
    async login({ commit }, credentials) {
      try {
        const response = await axios.get('/credenciales.json');
        const storedCredentials = response.data;
        
        if (
          storedCredentials.email === credentials.email &&
          storedCredentials.password === credentials.password
        ) {
          commit('setUser', storedCredentials); // Guarda usuario y numero_inicio
          return storedCredentials; // Retorna las credenciales (incluyendo numero_inicio)
        } else {
          throw new Error('Credenciales incorrectas');
        }
      } catch (error) {
        throw new Error('Error de autenticación: ' + error.message);
      }
    }
  }
});

export default store;

