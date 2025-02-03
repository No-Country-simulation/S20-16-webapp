using EduLink.BackEnd.Datos;
using EduLink.BackEnd.Datos.Interfaces;
using EduLink.BackEnd.Datos.Repositorio;
using Microsoft.EntityFrameworkCore;
using TPShoes.Datos;

namespace TPShoes.IoC
{
    public static class DI
    {
        public static void ConfigurarServicios
            (IServiceCollection servicios, IConfiguration configuration)
        {

            //todos los repositorios

            servicios.AddScoped<IRepositorioEstudiante,
               RepositorioEstudiante>();
            

            //UnityOfWork
            servicios.AddScoped<IUnitOfWork, UnitOfWork>();

            servicios.AddDbContext<DBContext>(opciones =>
            {
                var connectionString = configuration.GetConnectionString("MyConn");
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException("La cadena de conexión no puede ser nula o vacía.");
                }
                opciones.UseMySQL(connectionString);
            });
            //servicios.AddDbContext<DBContext>(optiones =>
            //{
            //    optiones.UseMySQL(configuration.GetConnectionString("MyConn"));
            //});


        }

    }

}
