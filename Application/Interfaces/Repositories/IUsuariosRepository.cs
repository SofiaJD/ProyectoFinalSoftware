using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Application.ViewModels.Usuarios;
using Domain.Entities;

namespace Application.Interfaces.Repositorios
{
    public interface IUsuariosRepository : IGenericRepository<Usuarios>
    {
        Task<Usuarios> LoginAsync(LoginViewModel loginVm);

    }
}
