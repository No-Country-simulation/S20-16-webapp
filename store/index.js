import { createStore } from 'vuex';
import axios from 'axios';

const store = createStore({
  state: {
    user: JSON.parse(localStorage.getItem('user')) || null, // Recuperar usuario desde localStorage si existe
    alumno: null,
    materias: [],
    materiasSeleccionadas: [], // Aquí almacenamos las materias seleccionadas
    historialMaterias: [],
    proximosExamenes: [],
    fechasInscripcion: {
      inscripcionMaterias: null,
      inscripcionExamenes: null,
      comienzoClases: null,
    },
    error: null,
    lastVisitedRoute: null, 
  },
  mutations: {
    setUser(state, user) {
      state.user = user;
      state.numero_inicio = user.numero_inicio;
      localStorage.setItem('user', JSON.stringify(user)); // Guardamos el usuario en localStorage
    },

    setLastVisitedRoute(state, route) {
      state.lastVisitedRoute = route;
    },
    setAlumno(state, alumno) {
      if (!alumno || typeof alumno !== 'object') {
        state.alumno = null;
        state.materias = [];
        state.historialMaterias = [];
        state.proximosExamenes = [];
        state.fechasInscripcion = {
          inscripcionMaterias: null,
          inscripcionExamenes: null,
          comienzoClases: null,
        };
        return;
      }

      state.alumno = {
        nombre: alumno.nombreEstudiante || 'Estudiante',
        foto: alumno.fotoEstudiante || '/ruta/default.png',
        proximosExamenes: alumno.proximosExamenes || [],
        ...alumno,
      };

      state.materias = Array.isArray(alumno.materias) ? alumno.materias : [];
      state.historialMaterias = Array.isArray(alumno.historialMaterias)
        ? alumno.historialMaterias
        : [];
      state.proximosExamenes = Array.isArray(alumno.proximosExamenes)
        ? alumno.proximosExamenes
        : [];
    },
    setFechasInscripcion(state, fechas) {
      console.log('Fechas recibidas:', fechas); // Para depuración
      state.fechasInscripcion = {
        inscripcionMaterias: fechas?.materias?.inicio || null,
        inscripcionExamenes: fechas?.examenes?.inicio || null,
        comienzoClases: fechas?.comienzoClases || null,
      };
      
    },
    setError(state, error) {
      state.error = error;
    },
    clearError(state) {
      state.error = null;
    },
    clearUser(state) {
      state.user = null;
      state.alumno = null;
      state.materias = [];
      state.historialMaterias = [];
      state.proximosExamenes = [];
      state.fechasInscripcion = {
        inscripcionMaterias: null,
        inscripcionExamenes: null,
        comienzoClases: null,
      };
      localStorage.removeItem('user'); // Eliminamos el usuario de localStorage al hacer logout
    },
    // Mutaciones para gestionar las materias seleccionadas
    setMateriasSeleccionadas(state, materias) {
      state.materiasSeleccionadas = materias;
    },
    agregarMateriaSeleccionada(state, materia) {
      if (!state.materiasSeleccionadas.some(m => m.id === materia.id)) {
        state.materiasSeleccionadas.push(materia);
      }
    },
    eliminarMateriaSeleccionada(state, materiaId) {
      state.materiasSeleccionadas = state.materiasSeleccionadas.filter(m => m.id !== materiaId);
    },
  },
  actions: {
    async login({ commit, dispatch }, credentials) {
      try {
        const response = await axios.get('../public/credenciales.json');
        //const response = await axios.get('/credenciales.json');
    
        const storedCredentials = response.data;
    
        // Buscar el usuario en el array de credenciales
        const user = storedCredentials.find(
          (user) => user.email === credentials.email && user.password === credentials.password
        );
    
        if (user) {
          commit('setUser', user);
          console.log("entro");
          await dispatch('fetchAlumnoData', user.numero_inicio);
          commit('clearError');
          return user;
        } else {
          throw new Error('Credenciales incorrectas');
        }
      } catch (error) {
        console.error(error); // Imprime el error completo
        commit('setError', `Error de autenticación: ${error.message}`);
        throw error;
      }
    },

    setLastVisitedRoute({ commit }, route) {
      commit('setLastVisitedRoute', route);
    },

    async fetchAlumnoData({ commit }, userId) {
      try {
        const response = await axios.get(`/public/alumno_1.json`);
        //const response = await axios.get(`/alumno_1.json`);
        if (!response.data || !response.data.alumno) {
          throw new Error('El JSON no contiene datos válidos del alumno.');
        }

        const alumnoData = response.data.alumno;
        commit('setAlumno', alumnoData);

        // Ajustar y cargar fechas de inscripción del alumno
        const fechasInscripcion = {
          materias: alumnoData.inscripcion?.materias || {},
          examenes: alumnoData.inscripcion?.examenes || {},
          comienzoClases: alumnoData.inscripcion?.comienzoClases || null,
        };

        commit('setFechasInscripcion', fechasInscripcion);
        commit('clearError');
      } catch (error) {
        console.error('[fetchAlumnoData] Error al cargar datos del alumno:', error.message);
        if (error.response) {
          commit('setError', `Error del servidor: ${error.response.status}`);
        } else if (error.request) {
          commit('setError', 'No se pudo conectar con el servidor.');
        } else {
          commit('setError', 'Error al procesar los datos del alumno.');
        }
        throw error;
      }
    },
    logout({ commit }) {
      commit('clearUser');
      commit('clearError');
    },
    // Acción para cargar las materias seleccionadas desde el localStorage
    cargarMateriasSeleccionadas({ commit }) {
      const storedMaterias = localStorage.getItem('materiasSeleccionadas');
      if (storedMaterias) {
        commit('setMateriasSeleccionadas', JSON.parse(storedMaterias)); // Recuperar las materias seleccionadas del localStorage
      }
    },
    // Acción para agregar materia seleccionada
    agregarMateriaSeleccionada({ commit, state }, materia) {
      commit('agregarMateriaSeleccionada', materia);
      // Sincronizar con localStorage
      localStorage.setItem('materiasSeleccionadas', JSON.stringify(state.materiasSeleccionadas));
    },
    // Acción para eliminar materia seleccionada
    eliminarMateriaSeleccionada({ commit, state }, materiaId) {
      commit('eliminarMateriaSeleccionada', materiaId);
      // Sincronizar con localStorage
      localStorage.setItem('materiasSeleccionadas', JSON.stringify(state.materiasSeleccionadas));
    },
  },
  getters: {
    getAlumno: (state) => state.alumno || {},
    getNombreAlumno: (state) => state.alumno?.nombre || 'Estudiante',
    getFotoAlumno: (state) => state.alumno?.foto || '../src/assets/img/estuduante2.jpg',
    getFechasInscripcion: (state) => state.fechasInscripcion,
    getMaterias: (state) => state.materias || [],
    getMateriasPorAnio: (state) => {
      const materiasPorAnio = {
        primerAnio: [],
        segundoAnio: [],
        tercerAnio: [],
      };

      state.materias.forEach((materia) => {
        if (materia.anio === 1) {
          materiasPorAnio.primerAnio.push(materia);
        } else if (materia.anio === 2) {
          materiasPorAnio.segundoAnio.push(materia);
        } else if (materia.anio === 3) {
          materiasPorAnio.tercerAnio.push(materia);
        }
      });

      return materiasPorAnio;
    },
    getMateriasSeleccionadas: (state) => state.materiasSeleccionadas,
    getMateriasAprobadas: (state) =>
      state.materias.filter((materia) => materia.estado === 'Aprobada'),
    getMateriasEnCurso: (state) =>
      state.materias.filter((materia) => materia.estado === 'EnCurso'),
    getMateriasDesaprobadas: (state) =>
      state.materias.filter((materia) => materia.estado === 'Desaprobada'),
    getHistorialMaterias: (state) => state.historialMaterias || [],
    getProximosExamenes: (state) => state.proximosExamenes || [],
    getError: (state) => state.error || null,
    getMateriaByName: (state) => (nombre) =>
      state.materias.find((materia) => materia.nombre === nombre) || null,
    isAuthenticated: (state) => !!state.user, // Si hay usuario, devuelve true
  },
});

export default store;
