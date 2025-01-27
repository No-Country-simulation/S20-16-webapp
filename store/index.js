import { createStore } from 'vuex';
import axios from 'axios';

const store = createStore({
  state: {
    user: null,
    alumno: null,
    materias: [],
    historialMaterias: [],
    proximosExamenes: [],
    fechasInscripcion: {
      inscripcionMaterias: null,
      inscripcionExamenes: null,
      comienzoClases: null,
    },
    error: null,
  },
  mutations: {
    setUser(state, user) {
      state.user = user;
      state.numero_inicio = user.numero_inicio;
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
      console.log('Fechas agrupadas:', state.fechasInscripcion); // Para depuración
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
    },
  },
  actions: {
    async login({ commit, dispatch }, credentials) {
      try {
        const response = await axios.get('../public/credenciales.json');
        const storedCredentials = response.data;

        if (
          storedCredentials.email === credentials.email &&
          storedCredentials.password === credentials.password
        ) {
          commit('setUser', storedCredentials);
          await dispatch('fetchAlumnoData', storedCredentials.numero_inicio);
          commit('clearError');
          return storedCredentials;
        } else {
          throw new Error('Credenciales incorrectas');
        }
      } catch (error) {
        commit('setError', `Error de autenticación: ${error.message}`);
        throw error;
      }
    },
    async fetchAlumnoData({ commit }, userId) {
      try {
        const response = await axios.get(`/public/alumno_1.json`);
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
  },
});

export default store;
