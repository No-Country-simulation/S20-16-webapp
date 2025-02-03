using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.BackEnd.Entidades.EntidadesDBB
{
    public class Modulo
    {
        public int ModuloId { get; set; }
        public TimeSpan HoraEntrada { get; set; }
        public TimeSpan HoraSalida { get; set; }
        public ICollection<Materia> Materias { get; set; }
    }
}
