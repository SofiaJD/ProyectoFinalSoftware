using Application.Interfaces.Repositories;
using Application.Interfaces.Repositorios;
using Application.Interfaces.Services;
using Application.ViewModels.Tareas;
using Application.ViewModels.Usuarios;
using Domain.Entities;

namespace Application.Services
{
    public class TareasServices : ITareasService
    {
        private readonly ITareasRepository _tareasRepository;

        public TareasServices(ITareasRepository tareasRepository)
        {
            _tareasRepository = tareasRepository;
        }

        public async Task AddAsync(SaveTareasViewModel vm)
        {
            Tareas tareas = new();
            tareas.Id = vm.Id;
            tareas.Nombre = vm.Nombre;
            tareas.Descripcion = vm.Descripcion;
            tareas.FechaInicio = vm.FechaInicio;
            tareas.FechaFin = vm.FechaFin;
            tareas.Estado = vm.Estado;
            tareas.Proyecto.Id = vm.ProyectoID;

            await _tareasRepository.AddAsync(tareas);
        }

        public async Task DeleteAsync(int id)
        {
            var tareas = await _tareasRepository.GetByIdAsync(id);
            await _tareasRepository.DeleteAsync(tareas);
        }

        public async Task<List<TareasViewModel>> GetAllAsync()
        {
            var tareasList = await _tareasRepository.GetAllAsync();

            return tareasList.Select(tareas => new TareasViewModel
            {
                Id = tareas.Id,
                Nombre = tareas.Nombre,
                Descripcion = tareas.Descripcion,
                FechaInicio = tareas.FechaInicio,
                FechaFin = tareas.FechaFin,
                Estado = tareas.Estado,
                ProyectoNombre= tareas.Proyecto.Nombre
            }).ToList();
        }

        public async Task<TareasViewModel> GetByIdAsync(int id)
        {
            var tareas = await _tareasRepository.GetByIdAsync(id);

            TareasViewModel vm = new();
            vm.Id = tareas.Id;
            vm.Nombre = tareas.Nombre;
            vm.Descripcion = tareas.Descripcion;
            vm.FechaInicio = tareas.FechaInicio;
            vm.FechaFin = tareas.FechaFin;
            vm.Estado = tareas.Estado;
            vm.ProyectoNombre = tareas.Proyecto.Nombre;

            return vm;
        }

        public async Task UpdateAsync(SaveTareasViewModel vm)
        {
            Tareas tareas = new();
            tareas.Id = vm.Id;
            tareas.Nombre = vm.Nombre;
            tareas.Descripcion = vm.Descripcion;
            tareas.FechaInicio = vm.FechaInicio;
            tareas.FechaFin = vm.FechaFin;
            tareas.Estado = vm.Estado;
            tareas.ProyectoID = vm.ProyectoID;

            await _tareasRepository.UpdateAsync(tareas);
        }
    }
}
