using EduLink.Entidades.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Entidades.EntidadesBDD
{
    public class EstudianteMateria
    {

        public int EstudianteMateriaId { get; set; }
        public int EstudianteId { get; set; }
        public int MateriaId { get; set; }
        public string AnioCursado { get; set; }
        public EstadoMateriaEnum EstadoMateria { get; set; }
        public Estudiante Estudiante { get; set; } // Propiedad de navegación
        public Materia Materia { get; set; } // Propiedad de navegación
    }
}
