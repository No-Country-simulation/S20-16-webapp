using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Entidades.EntidadesBDD
{
    public class Ciudad
    {
        public int CiudadId { get; set; }
        public string NombreCiudad { get; set; }

        public string ProvinciaId { get; set; }
        public Provincia Provincia { get; set; }

        public ICollection<Estudiante> Estudiantes{ get; set; }
    }
}
