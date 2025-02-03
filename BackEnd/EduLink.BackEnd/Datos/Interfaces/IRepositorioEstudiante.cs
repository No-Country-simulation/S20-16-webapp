using EduLink.BackEnd.Entidades.DTOs;
using EduLink.BackEnd.Entidades.EntidadesDBB;

namespace EduLink.BackEnd.Datos.Interfaces
{
    public interface IRepositorioEstudiante
    {
        Estudiante BuscarPorEmailYContraseña(LoginDTO login);
        bool EsContraseñaIgualADNI(Estudiante estudiante);
        EstudianteCompletoDTO ObtenerInformacionCompleta(int estudianteId);

    }
}
