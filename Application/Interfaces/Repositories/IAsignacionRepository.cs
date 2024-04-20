using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ViewModels.Asignacion;
using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IAsignacionRepository 
    {
        Task<Asignacion> GetByIdAsync(int consultorId, int proyectoId);
        Task AsignarConsultorAProyectoAsync(Asignacion asignacion);
        Task ActualizarAsignacionAsync(Asignacion asignacion);
        Task EliminarAsignacionAsync(Asignacion asignacion);
        Task<List<Asignacion>> ListarAsignacionesPorConsultorAsync(int consultorId);
        Task<List<Asignacion>> ListarAsignacionesPorProyectoAsync(int proyectoId);
    }
}
