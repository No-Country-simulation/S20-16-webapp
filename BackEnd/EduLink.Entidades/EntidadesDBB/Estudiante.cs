using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Entidades.EntidadesBDD
{
    public class Estudiante
    {
        public int EstudianteId { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string DNI { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }

        public int CiudadId { get; set; }
        public Ciudad Ciudad { get; set; }

        public ICollection<EstudianteMateria> EstudiantesMaterias { get; set; }
        public ICollection<EstudianteExamen> EstudiantesExamenes { get; set; }
    }
}
