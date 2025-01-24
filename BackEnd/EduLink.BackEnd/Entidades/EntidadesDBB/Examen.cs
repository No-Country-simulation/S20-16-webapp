using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.BackEnd.Entidades.EntidadesDBB
{
    public class Examen
    {
        public int ExamenId { get; set; }
        public int MateriaId { get; set; }
        public Materia Materia { get; set; }
        public TimeSpan HoraComienzo { get; set; }
        public DateOnly FechaExamen { get; set; }
        public ICollection<EstudianteExamen> EstudiantesExamenes { get; set; }
    }
}
