using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ViewModels.Proyectos;

namespace Application.Interfaces.Services
{
    public interface IProyectosService : IGenericService<SaveProyectosViewModel, ProyectosViewModel> 
    {
    }
}
