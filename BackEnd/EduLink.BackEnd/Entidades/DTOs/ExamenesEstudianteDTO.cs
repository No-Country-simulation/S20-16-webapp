using EduLink.BackEnd.Entidades.EntidadesDBB;

namespace EduLink.BackEnd.Entidades.DTOs
{
    public class ExamenesEstudianteDTO
    {
        public int EstudianteId { get; set; }
        //Que este con la condicion de desaprobadas o
        //Ausente, checkear que la correlativa este aprobada
        public List<Examen> ExamenesRendibles { get; set; }

    }
}
