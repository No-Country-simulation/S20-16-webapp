using EduLink.BackEnd.Entidades.Enums;

namespace EduLink.BackEnd.Entidades.EntidadesDBB
{
    public class EstudianteExamen
    {

        public int EstudianteExamenId { get; set; }
        public int EstudianteId { get; set; }
        public int ExamenId { get; set; }
        public decimal Nota { get; set; }
        public string EstadoExamen { get; set; }

        public Estudiante Estudiante { get; set; } // Propiedad de navegación
        public Examen Examen { get; set; } // Propiedad de navegación
    }
}
