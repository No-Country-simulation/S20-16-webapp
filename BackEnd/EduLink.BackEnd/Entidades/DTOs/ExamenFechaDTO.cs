using EduLink.BackEnd.Entidades.Enums;

namespace EduLink.BackEnd.Entidades.DTOs
{
    public class ExamenFechaDTO
    {

        public string NombreMateria { get; set; }
        public TimeSpan HoraComienzo { get; set; }
        public DateTime FechaExamen { get; set; }
    }
}
