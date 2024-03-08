using Application.Interfaces.Repositorios;
using Application.Interfaces.Servicios;
using Application.ViewModels.Usuarios;
using Domain.Entities;

namespace Application.Services
{
    public class UsuarioServices : IUsuariosServices
    {
        private readonly IUsuariosRepository _usuariosRepository;

        public UsuarioServices(IUsuariosRepository usuariosRepository)
        {
            _usuariosRepository = usuariosRepository;
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

            await _usuariosRepository.AddAsync(usuarios);
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
    }
}
