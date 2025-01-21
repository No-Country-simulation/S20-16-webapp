// store.js
import { createStore } from 'vuex';
import axios from 'axios';

const store = createStore({
  state: {
    user: null,
    numero_inicio: null,
    dashboardData: [] // Estado para los datos del Dashboard
  },
  mutations: {
    setUser(state, user) {
      state.user = user;
      state.numero_inicio = user.numero_inicio;
    },
    setDashboardData(state, data) {
      state.dashboardData = data;
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
          commit('setUser', storedCredentials);
          return storedCredentials;
        } else {
          throw new Error('Credenciales incorrectas');
        }
      } catch (error) {
        throw new Error('Error de autenticación: ' + error.message);
      }
    },
    async fetchDashboardData({ commit }) {
      
      try {
        const response = await axios.get('/dashboardData.json'); // Ruta o URL del JSON
        const data = response.data;
        commit('setDashboardData', data); // Almacena los datos en el estado
      } catch (error) {
        console.error('Error al obtener los datos del dashboard: ' + error.message);
      }
    }
  },
  getters: {
    user: (state) => state.user,
    dashboardData: (state) => state.dashboardData
  }
});

export default store;
