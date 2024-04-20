using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.ViewModels.Asignacion;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AsignacionService : IAsignacionService
    {
        private readonly IAsignacionRepository _asignacionRepository;
        private readonly IConsultorRepository _consultorRepository;
        private readonly IProyectosRepository _proyectosRepository;



        public AsignacionService(IAsignacionRepository asignacionRepository, IConsultorRepository consultorRepository, IProyectosRepository proyectosRepository)
        {
            _asignacionRepository = asignacionRepository;
            _consultorRepository = consultorRepository;
            _proyectosRepository = proyectosRepository;
        }

        public async Task AsignarConsultorAProyectoAsync(SaveAsignacionViewModel vm)
        {
            using (var connection = new SqlConnection("server=DESKTOP-QA2RG00;Database=ProyectoFinalSoftware;TrustServerCertificate=true;Trusted_Connection=true;MultipleActiveResultSets=true"))
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("AsignarConsultorAProyecto", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IdConsultor", vm.IdConsultor);
                    command.Parameters.AddWithValue("@ProyectoID", vm.ProyectoID);
                    command.Parameters.AddWithValue("@FechaAsignacion", vm.FechaAsignacion);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task ActualizarAsignacionAsync(SaveAsignacionViewModel vm)
        {
            var asignacion = await _asignacionRepository.GetByIdAsync(vm.IdConsultor, vm.ProyectoID);
            if (asignacion == null)
                throw new Exception("Asignación no encontrada");

            asignacion.IdConsultor = vm.IdConsultor;
            asignacion.ProyectoID = vm.ProyectoID;
            asignacion.FechaAsignacion = vm.FechaAsignacion;

            await _asignacionRepository.ActualizarAsignacionAsync(asignacion);
        }

        public async Task EliminarAsignacionAsync(int consultorId, int proyectoId)
        {
            var asignacion = await _asignacionRepository.GetByIdAsync(consultorId, proyectoId);
            if (asignacion == null)
                throw new Exception("Asignación no encontrada");

            await _asignacionRepository.EliminarAsignacionAsync(asignacion);
        }

        public async Task<List<AsignacionViewModel>> ListarAsignacionesPorConsultorAsync(int consultorId)
        {
            var asignaciones = await _asignacionRepository.ListarAsignacionesPorConsultorAsync(consultorId);

            // Esperamos el resultado de la tarea y obtenemos el nombre del proyecto
            var consultorNombre = await _consultorRepository.GetConsultorNombre(consultorId);

            return asignaciones.Select(a => new AsignacionViewModel
            {
                Id = a.Id,
                ConsultorNombre = consultorNombre,
                FechaAsignacion = a.FechaAsignacion
            }).ToList();
        }

        public async Task<List<AsignacionViewModel>> ListarAsignacionesPorProyectoAsync(int proyectoId)
        {
            var asignaciones = await _asignacionRepository.ListarAsignacionesPorProyectoAsync(proyectoId);

            // Esperamos el resultado de la tarea y obtenemos el nombre del proyecto
            var proyectoNombre = await _proyectosRepository.GetProyectoNombreByAsignacionIdAsync(proyectoId);

            return asignaciones.Select(a => new AsignacionViewModel
            {
                Id = a.Id,
                ProyectoNombre = proyectoNombre, // Asignamos el nombre del proyecto obtenido
                FechaAsignacion = a.FechaAsignacion
            }).ToList();
        }

        public async Task<AsignacionViewModel> GetByIdAsync(int consultorId, int proyectoId)
        {
            var asignacion = await _asignacionRepository.GetByIdAsync(consultorId, proyectoId);
            var proyectoNombre = await _proyectosRepository.GetProyectoNombreByAsignacionIdAsync(proyectoId);
            var consultorNombre = await _consultorRepository.GetConsultorNombre(consultorId);

            if (asignacion == null)
                return null;

            return new AsignacionViewModel
            {
                Id = asignacion.Id,
                ConsultorNombre = consultorNombre,
                ProyectoNombre = proyectoNombre,
                FechaAsignacion = asignacion.FechaAsignacion
            };
        }
    }
}
