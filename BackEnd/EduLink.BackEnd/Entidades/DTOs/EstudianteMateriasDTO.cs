using EduLink.BackEnd.Entidades.Enums;

namespace EduLink.BackEnd.Entidades.DTOs
{
    public class EstudianteMateriasDTO
    {
        public int MateriaId { get; set; }
        public string NombreMateria { get; set; }
        public int AnioDeLaMateria { get; set; }
        public string EstadoMateria { get; set; }
        public string DiaSemana { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFinal { get; set; }

    }
}
