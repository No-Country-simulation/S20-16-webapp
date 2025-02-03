using EduLink.BackEnd.Datos.Interfaces;
using EduLink.BackEnd.Datos.Repositorio;
using EduLink.BackEnd.Entidades.DTOs;
using EduLink.BackEnd.Entidades.EntidadesDBB;
using Microsoft.AspNetCore.Mvc;

namespace EduLink.BackEnd.APIs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IRepositorioEstudiante _repositorioLogin;

        public LoginController(IRepositorioEstudiante repositorioLogin)
        {
            _repositorioLogin = repositorioLogin ?? throw new ApplicationException("Dependencies not set");
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginDTO login)
        {
            if (string.IsNullOrEmpty(login.Email) || string.IsNullOrEmpty(login.Contrase�a))
            {
                return BadRequest("Email y Contrase�a son requeridos.");
            }

            Estudiante estudiante = _repositorioLogin.BuscarPorEmailYContrase�a(login);
            if (estudiante == null)
            {
                return Unauthorized("Email o Contrase�a incorrectos.");
            }
           
            
            var primeIngreso = _repositorioLogin.EsContrase�aIgualADNI(estudiante);

            if (primeIngreso)
            {
                var estudianteLogeado = new EstudianteDTO
                {
                    EstudianteId = estudiante.EstudianteId,
                    Email = estudiante.Email,
                    Contrase�a = estudiante.Contrase�a,
                    PrimeIngreso = true
                };
                return Ok(estudianteLogeado);
            }
            else
            {
                var estudianteCompleto = _repositorioLogin.ObtenerInformacionCompleta(estudiante.EstudianteId);
                return Ok(estudianteCompleto);
            }
        }
    }
}
