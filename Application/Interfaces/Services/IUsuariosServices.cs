using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Services;
using Application.ViewModels.Usuarios;
using Domain.Entities;

namespace Application.Interfaces.Servicios
{
    public interface IUsuariosServices : IGenericService<SaveUsuariosViewModel, UsuariosViewModel>
    {
        Task<UsuariosViewModel> Login(LoginViewModel loginVm);
    }
}
