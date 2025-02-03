using EduLink.BackEnd.Entidades.EntidadesDBB;

namespace EduLink.BackEnd.Entidades.DTOs
{
    public class MateriasEstudianteDTO
    {
        public int EstudianteId { get; set; }
        //Que este con la condicion de desaprobadas 
        //Desaprobado o SinCursar, checkear que la correlativa este aprobada
        public List<Materia> MateriasCursables { get; set; }
    }
}
