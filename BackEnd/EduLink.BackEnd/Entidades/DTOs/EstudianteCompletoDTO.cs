using EduLink.BackEnd.Entidades.EntidadesDBB;

namespace EduLink.BackEnd.Entidades.DTOs
{
    public class EstudianteCompletoDTO
    {
        public int EstudianteId { get; set; }
        public string FotoEstudiante { get; set; }
        public string NombreEstudiante { get; set; }
        public int CantidaMateriasAprobadas { get; set; }
        public int CantidaExamenesAprobados { get; set; }
        public int CantidaMateriasTotales { get; set; }
        public List<ExamenFechaDTO> ProximosExamenes { get; set; }     
        public List<EstudianteMateriasDTO> HistorialMaterias { get; set; }
    }
}
