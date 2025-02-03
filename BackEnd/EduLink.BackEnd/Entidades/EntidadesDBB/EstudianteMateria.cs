using EduLink.BackEnd.Entidades.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.BackEnd.Entidades.EntidadesDBB
{
    public class EstudianteMateria
    {

        public int EstudianteMateriaId { get; set; }
        public int EstudianteId { get; set; }
        public int MateriaId { get; set; }
        public string AnioCursado { get; set; }
        public string EstadoMateria { get; set; }
        public Estudiante Estudiante { get; set; } // Propiedad de navegación
        public Materia Materia { get; set; } // Propiedad de navegación
    }
}
