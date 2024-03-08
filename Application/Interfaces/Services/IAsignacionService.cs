using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.ViewModels.Asignacion;

namespace Application.Interfaces.Services
{
    public interface IAsignacionService
    {
        Task<AsignacionViewModel> GetByIdAsync(int id);
        Task AsignarConsultorAProyectoAsync(SaveAsignacionViewModel vm);
        Task ActualizarAsignacionAsync(SaveAsignacionViewModel vm);
        Task EliminarAsignacionAsync(int asignacionId);
        Task<List<AsignacionViewModel>> ListarAsignacionesPorConsultorAsync(int consultorId);
        Task<List<AsignacionViewModel>> ListarAsignacionesPorProyectoAsync(int proyectoId);
    }
}
