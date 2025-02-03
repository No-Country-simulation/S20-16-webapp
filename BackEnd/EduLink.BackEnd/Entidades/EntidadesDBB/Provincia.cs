using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.BackEnd.Entidades.EntidadesDBB
{
    public class Provincia
    {
        public int ProvinciaId { get; set; }
        public string NombreProvincia { get; set; }
        public ICollection<Ciudad> Ciudades { get; set; }
    }
}
