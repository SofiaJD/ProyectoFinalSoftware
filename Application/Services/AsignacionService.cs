using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.ViewModels.Asignacion;
using Domain.Entities;

namespace Application.Services
{
    public class AsignacionService : IAsignacionService
    {
        private readonly IAsignacionRepository _asignacionRepository;

        public AsignacionService(IAsignacionRepository asignacionRepository)
        {
            _asignacionRepository = asignacionRepository;
        }

        public async Task AsignarConsultorAProyectoAsync(SaveAsignacionViewModel vm)
        {
            var asignacion = new Asignacion
            {
                ConsultorID = vm.ConsultorID,
                ProyectoID = vm.ProyectoID,
                FechaAsignacion = vm.FechaAsignacion
            };
            await _asignacionRepository.AsignarConsultorAProyectoAsync(asignacion);
        }

        public async Task ActualizarAsignacionAsync(SaveAsignacionViewModel vm)
        {
            var asignacion = await _asignacionRepository.GetByIdAsync(vm.Id);
            if (asignacion == null)
                throw new Exception("Asignación no encontrada");

            asignacion.ConsultorID = vm.ConsultorID;
            asignacion.ProyectoID = vm.ProyectoID;
            asignacion.FechaAsignacion = vm.FechaAsignacion;

            await _asignacionRepository.ActualizarAsignacionAsync(asignacion);
        }

        public async Task EliminarAsignacionAsync(int asignacionId)
        {
            var asignacion = await _asignacionRepository.GetByIdAsync(asignacionId);
            if (asignacion == null)
                throw new Exception("Asignación no encontrada");

            await _asignacionRepository.EliminarAsignacionAsync(asignacion);
        }

        public async Task<List<AsignacionViewModel>> ListarAsignacionesPorConsultorAsync(int consultorId)
        {
            var asignaciones = await _asignacionRepository.ListarAsignacionesPorConsultorAsync(consultorId);
            return asignaciones.Select(a => new AsignacionViewModel
            {
                Id = a.Id,
                ConsultorID = a.ConsultorID,
                ProyectoID = a.ProyectoID,
                FechaAsignacion = a.FechaAsignacion
            }).ToList();
        }

        public async Task<List<AsignacionViewModel>> ListarAsignacionesPorProyectoAsync(int proyectoId)
        {
            var asignaciones = await _asignacionRepository.ListarAsignacionesPorProyectoAsync(proyectoId);
            return asignaciones.Select(a => new AsignacionViewModel
            {
                Id = a.Id,
                ConsultorID = a.ConsultorID,
                ProyectoID = a.ProyectoID,
                FechaAsignacion = a.FechaAsignacion
            }).ToList();
        }
        public async Task<AsignacionViewModel> GetByIdAsync(int id)
        {
            var asignacion = await _asignacionRepository.GetByIdAsync(id);

            if (asignacion == null)
                return null;

            return new AsignacionViewModel
            {
                Id = asignacion.Id,
                ConsultorID = asignacion.ConsultorID,
                ProyectoID = asignacion.ProyectoID,
                FechaAsignacion = asignacion.FechaAsignacion
            };
        }
    }
}
