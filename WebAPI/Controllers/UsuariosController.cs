using Application.Interfaces.Servicios;
using Application.Services;
using Application.ViewModels.Usuarios;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosServices _usuariosServices;

        public UsuariosController(IUsuariosServices usuariosServices)
        {
            _usuariosServices = usuariosServices;
        }

        [HttpPost("registro")]
        public async Task<IActionResult> Registro([FromBody] SaveUsuariosViewModel vm)
        {
            try
            {
                await _usuariosServices.AddAsync(vm);
                return Ok("Usuario registrado exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al registrar usuario: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginVm)
        {
            try
            {
                var usuarioVm = await _usuariosServices.Login(loginVm);

                if (usuarioVm == null)
                {
                    return Unauthorized("Credenciales inválidas");
                }

                return Ok(usuarioVm);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al intentar iniciar sesión: {ex.Message}");
            }
        }
    }
}

