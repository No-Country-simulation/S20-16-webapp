using EduLink.Entidades.Enums;

namespace EduLink.Entidades.EntidadesBDD
{
    public class EstudianteExamen
    {

        public int EstudianteExamenId { get; set; }
        public int EstudianteId { get; set; }
        public int ExamenId { get; set; }
        public double Nota { get; set; }
        public EstadoExamenEnum EstadoExamen { get; set; }

        public Estudiante Estudiante { get; set; } // Propiedad de navegación
        public Examen Examen { get; set; } // Propiedad de navegación
    }
}
