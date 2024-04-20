using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IProyectosRepository : IGenericRepository<Proyectos>
    {
        Task<string> GetProyectoNombreByTareaIdAsync(int tareaId);
        Task<Dictionary<int, string>> GetProyectoNombresByIDsAsync(List<int> proyectoIds);
        Task<string> GetProyectoNombreByAsignacionIdAsync(int asignacionId);
    }
}
