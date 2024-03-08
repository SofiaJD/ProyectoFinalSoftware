using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ViewModels.Clientes;
using Domain.Entities;

namespace Application.Interfaces.Services
{
    public interface IClientesService : IGenericService<SaveClientesViewModel, ClientesViewModel>
    {

    }
}
