using Application.DTOs.Email;
using Application.Helpers;
using Application.Interfaces.Repositorios;
using Application.Interfaces.Services;
using Application.Interfaces.Servicios;
using Application.ViewModels.Usuarios;
using Domain.Entities;
using System.Security.Cryptography;

namespace Application.Services
{
    public class UsuarioServices : IUsuariosServices
    {
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly IEmailService _emailService;

        public UsuarioServices(IUsuariosRepository usuariosRepository, IEmailService emailService)
        {
            _usuariosRepository = usuariosRepository;
            _emailService = emailService;
        }


        public async Task DeleteAsync(int id)
        {
            var usuarios = await _usuariosRepository.GetByIdAsync(id);
            await _usuariosRepository.DeleteAsync(usuarios);
        }

        public async Task<UsuariosViewModel> Login(LoginViewModel loginVm)
        {
            Usuarios usuario = await _usuariosRepository.LoginAsync(loginVm);
            
            if (usuario == null)
            {
                return null;
            }

            UsuariosViewModel usuarioVm = new();
            usuarioVm.Id = usuario.Id;
            usuarioVm.NombreUsuario = usuario.NombreUsuario;
            usuarioVm.Nombre = usuario.Nombre;
            usuarioVm.Apellido = usuario.Apellido;
            usuarioVm.Email = usuario.Email;
            usuarioVm.Telefono = usuario.Telefono;
            usuarioVm.Password = usuario.Password;
            usuarioVm.RolUsuario = usuario.RolUsuario;

            return usuarioVm;
        }

        public async Task UpdateAsync(SaveUsuariosViewModel vm)
        {
            Usuarios usuarios = new();
            usuarios.Id = vm.Id;
            usuarios.NombreUsuario = vm.NombreUsuario;
            usuarios.Nombre = vm.Nombre;
            usuarios.Apellido = vm.Apellido;
            usuarios.Email = vm.Email;
            usuarios.Telefono = vm.Telefono;
            usuarios.Password = vm.Password;
            usuarios.RolUsuario = vm.RolUsuario;

            await _usuariosRepository.UpdateAsync(usuarios);
        }

        public async Task AddAsync(SaveUsuariosViewModel vm)
        {
            Usuarios usuarios = new();
            usuarios.Id = vm.Id;
            usuarios.NombreUsuario = vm.NombreUsuario;
            usuarios.Nombre = vm.Nombre;
            usuarios.Apellido = vm.Apellido;
            usuarios.Email = vm.Email;
            usuarios.Telefono = vm.Telefono;
            usuarios.Password = vm.Password;
            usuarios.RolUsuario = vm.RolUsuario;
            usuarios.VerificationToken = CreateRandomToken();


            await _usuariosRepository.AddAsync(usuarios);
            await _emailService.SendAsync(new EmailRequest
            {
                To = usuarios.Email,
                Subject = "Bienvenido al sistema de QDPROPEEP",
                Body = $"<!DOCTYPE html>\r\n<html lang=\"es\">\r\n<head>\r\n<meta charset=\"UTF-8\">\r\n<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n<title>Bienvenido al sistema de QDPROPEEP</title>\r\n</head>\r\n<body style=\"font-family: Arial, sans-serif; background-color: #f5f5f5; padding: 20px;\">\r\n\r\n<h1 style=\"color: #333; text-align: center;\">Bienvenido a Nuestro portal web de gestión de proyectos</h1>\r\n<p style=\"color: #666; text-align: center;\">¡Hola, {usuarios.Nombre}! Explora en nuestra web para descubrir todas nuestras funcionalidades.</p>\r\n\r\n</body>\r\n</html>\r\n"
            });
        }


        public async Task<UsuariosViewModel> GetByIdAsync(int id)
        {
            var usuarios = await _usuariosRepository.GetByIdAsync(id);

            UsuariosViewModel vm = new();
            vm.Id = usuarios.Id;
            vm.NombreUsuario = usuarios.NombreUsuario;
            vm.Nombre = usuarios.Nombre;
            vm.Apellido = usuarios.Apellido;
            vm.Email = usuarios.Email;
            vm.Telefono = usuarios.Telefono;
            vm.RolUsuario = usuarios.RolUsuario;

            return vm;
        }

        public async Task<List<UsuariosViewModel>> GetAllAsync()
        {
            var usuariosList = await _usuariosRepository.GetAllAsync();

            return usuariosList.Select(usuarios => new UsuariosViewModel
            {
                Id = usuarios.Id,
                NombreUsuario = usuarios.NombreUsuario,
                Nombre = usuarios.Nombre,
                Apellido = usuarios.Apellido,
                Email = usuarios.Email,
                Telefono = usuarios.Telefono,
                RolUsuario = usuarios.RolUsuario,
            }).ToList();
        }

        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(4));
        }

        public async Task<Usuarios> VerifyUser(string Token)
        {
            var usuario = await _usuariosRepository.GetByVerificationTokenAsync(Token);

            if (usuario == null)
            {
                throw new InvalidOperationException("Token incorrecto");
            }


            await _usuariosRepository.SaveChangesAsync();

            return usuario; // Devuelve el usuario verificado
        }

        public async Task<bool> ForgotPasswordAsync(string email)
        {
            var usuario = await _usuariosRepository.GetUserByEmailAsync(email);

            if (usuario == null)
            {
                return false;
            }

            // Generar un token para restablecer la contraseña
            var token = CreateRandomToken();

            // Guardar el token en la base de datos
            usuario.PasswordResetToken = token;
            usuario.ResetTokenExpires = DateTime.Now.AddHours(1).ToString();

            await _usuariosRepository.SaveChangesAsync();

            try
            {
                // Configurar el correo electrónico
                var emailRequest = new EmailRequest
                {
                    To = email,
                    Subject = "Restablecer contraseña",
                    Body = $"<!DOCTYPE html>\r\n<html lang=\"es\">\r\n<head>\r\n<meta charset=\"UTF-8\">\r\n<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n<title>Restablecer contraseña</title>\r\n</head>\r\n<body style=\"font-family: Arial, sans-serif; background-color: #f5f5f5; padding: 20px;\">\r\n\r\n<h1 style=\"color: #333; text-align: center;\">Restablecer contraseña</h1>\r\n<p style=\"color: #666; text-align: center;\">Utiliza este token para restablecer tu contraseña: {token}</p>\r\n\r\n</body>\r\n</html>\r\n"
                };

                // Enviar el correo electrónico
                await _emailService.SendAsync(emailRequest);
            }
            catch (Exception ex)
            {
                // Manejar cualquier error al enviar el correo electrónico
                Console.WriteLine($"Error al enviar el correo electrónico: {ex.Message}");
                // En caso de error al enviar el correo, puedes considerar revertir los cambios en la base de datos
                return false;
            }

            return true;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordViewModel resetPasswordViewModel)
        {
            var usuario = await _usuariosRepository.GetByResetTokenAsync(resetPasswordViewModel.Token);

            if (usuario == null )
            {
                return false;
            }

            // Encriptar la nueva contraseña
            usuario.Password = PasswordEncryptation.ComputeSha256Hash(resetPasswordViewModel.Password);

            // Limpiar los campos de token de restablecimiento de contraseña
            usuario.PasswordResetToken = null;
            usuario.ResetTokenExpires = null;

            await _usuariosRepository.SaveChangesAsync();

            return true;
        }
    }
}
