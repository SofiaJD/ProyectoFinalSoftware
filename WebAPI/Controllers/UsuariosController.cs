using Application.Interfaces.Servicios;
using Application.Services;
using Application.ViewModels.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Application.Helpers;
using WebAPI.Middlewares;
using Microsoft.EntityFrameworkCore;


namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosServices _usuariosServices;
        private readonly ValidateUserSession _validateUserSession;

        public UsuariosController(IUsuariosServices usuariosServices, ValidateUserSession validateUserSession)
        {
            _usuariosServices = usuariosServices;
            _validateUserSession = validateUserSession;
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

                HttpContext.Session.Set("Usuario", usuarioVm);
                return Ok(usuarioVm);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error al intentar iniciar sesión: {ex.Message}");
            }
        }
        [HttpPost("verify")]
        public async Task<IActionResult> VerifyUser([FromBody] UsuarioTokenViewModel viewModel)
        {
            try
            {
                if (string.IsNullOrEmpty(viewModel.Token))
                {
                    return BadRequest("Se requiere un token válido.");
                }

                var usuario = await _usuariosServices.VerifyUser(viewModel.Token);
                return Ok(usuario);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var result = await _usuariosServices.ForgotPasswordAsync(email);
            return result ? Ok("Puedes restablecer tu contraseña.") : BadRequest("Usuario no encontrado.");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            var result = await _usuariosServices.ResetPasswordAsync(resetPasswordViewModel);
            return result ? Ok("Contraseña restablecida.") : BadRequest("Token inválido o expirado.");
        }
    }
}

