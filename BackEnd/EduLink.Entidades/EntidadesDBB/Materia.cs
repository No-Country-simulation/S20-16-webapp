using EduLink.Entidades.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EduLink.Entidades.EntidadesBDD
{
    public class Materia
    {

        public int MateriaId { get; set; }
        public MateriaEnum NombreMateria { get; set; }
        public int AnioDeLaMateria { get; set; }
        public int Cuatrimestre { get; set; }
        public DateOnly Fecha { get; set; }// ver si está bien
        public int Correlativa { get; set; }

        public int ModuloId { get; set; }
        public Modulo Modulo { get; set; }

        public ICollection<EstudianteMateria> EstudiantesMaterias { get; set; }
        public ICollection<Examen> Examenes { get; set; }
    }
}
