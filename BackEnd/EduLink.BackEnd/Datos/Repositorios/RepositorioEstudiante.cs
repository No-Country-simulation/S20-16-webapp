using EduLink.BackEnd.Datos.Interfaces;
using EduLink.BackEnd.Entidades.DTOs;
using EduLink.BackEnd.Entidades.EntidadesDBB;
using EduLink.BackEnd.Entidades.Enums;

namespace EduLink.BackEnd.Datos.Repositorio
{
    public class RepositorioEstudiante : IRepositorioEstudiante
    {

        private readonly DBContext _db;

        public RepositorioEstudiante(DBContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
        public Estudiante BuscarPorEmailYContraseña(LoginDTO login)
        {
            Estudiante estudiante = _db.Estudiantes.FirstOrDefault(e => e.Email == login.Email && e.Contraseña == login.Contraseña);
            if (estudiante == null)
            {
                return null;
            }

            return estudiante;
        }

        public bool EsContraseñaIgualADNI(Estudiante estudiante)
        {
            return estudiante?.Contraseña == estudiante?.DNI;
        }

        public EstudianteCompletoDTO ObtenerInformacionCompleta(int estudianteId)
        {
            var estudianteCompleto = _db.Estudiantes
                .FirstOrDefault(e => e.EstudianteId == estudianteId);

            if (estudianteCompleto == null)
            {
                throw new KeyNotFoundException($"No se encontró el estudiante con ID {estudianteId}");
            }

            // Obtener las materias del estudiante
            var historialMaterias = GetHistorialEstudiantesMaterias(estudianteCompleto);

            return new EstudianteCompletoDTO
            {
                EstudianteId = estudianteCompleto.EstudianteId,
                FotoEstudiante = "María.jpg",
                NombreEstudiante = estudianteCompleto.Nombres,
                CantidaMateriasAprobadas = GetCantidaMateriasAprobadas(estudianteCompleto),
                CantidaExamenesAprobados = GetCantidaExamenesAprobados(estudianteCompleto),
                CantidaMateriasTotales = 24,
                ProximosExamenes = GetProximosExmamanes(estudianteCompleto),
                HistorialMaterias = historialMaterias
            };
        }

        private List<EstudianteMateriasDTO> GetHistorialEstudiantesMaterias(Estudiante estudianteCompleto)
        {
            var materias = _db.EstudiantesMaterias
                              .Where(em => em.EstudianteId == estudianteCompleto.EstudianteId)
                              .Join(_db.Materias, em => em.MateriaId, m => m.MateriaId, (em, m) => new { em, m })
                              .Join(_db.Modulos, em_m => em_m.m.ModuloId, mod => mod.ModuloId, (em_m, mod) => new
                              {
                                  em_m.em.MateriaId,
                                  em_m.m.NombreMateria,  
                                  em_m.m.AnioDeLaMateria,
                                  em_m.em.EstadoMateria,
                                  em_m.m.Fecha,  
                                  Modulo = mod
                              })
                              .ToList();

            return materias.Select(m => new EstudianteMateriasDTO
            {
                MateriaId = m.MateriaId,
                NombreMateria = m.NombreMateria,
                AnioDeLaMateria = m.AnioDeLaMateria,
                EstadoMateria = m.EstadoMateria,
                DiaSemana = DateOnly.FromDateTime(m.Fecha).ToString("dddd"),  
                HoraInicio = m.Modulo.HoraSalida,
                HoraFinal = m.Modulo.HoraEntrada
            }).ToList();
        }

        private int GetCantidaMateriasAprobadas(Estudiante estudianteCompleto)
        {
            return _db.EstudiantesMaterias
                      .Count(em => em.EstudianteId == estudianteCompleto.EstudianteId && em.EstadoMateria == "Aprobado");
        }

        private int GetCantidaExamenesAprobados(Estudiante estudianteCompleto)
        {
            return _db.EstudiantesExamenes
                      .Count(em => em.EstudianteId == estudianteCompleto.EstudianteId && em.EstadoExamen == "Aprobado");
        }


        private List<ExamenFechaDTO> GetProximosExmamanes(Estudiante estudianteCompleto)
        {
            var examenes = _db.EstudiantesExamenes
                              .Where(e => e.EstadoExamen == "Inscripto" &&
                                          e.EstudianteId == estudianteCompleto.EstudianteId)
                              .Join(_db.Examenes, ee => ee.ExamenId, e => e.ExamenId, (ee, e) => new { ee, e })
                              .Join(_db.Materias, combined => combined.e.MateriaId, m => m.MateriaId, (combined, m) => new
                              {
                                  combined.ee.ExamenId,
                                  combined.e.MateriaId,
                                  m.NombreMateria,
                                  combined.e.FechaExamen,  // Mantener como DateTime
                                  combined.e.HoraComienzo
                              })
                              .ToList();

            return examenes.Select(e => new ExamenFechaDTO
            {
                NombreMateria = e.NombreMateria,
                FechaExamen =e.FechaExamen,  
                HoraComienzo = e.HoraComienzo

            }).ToList();
        }
    }
}
