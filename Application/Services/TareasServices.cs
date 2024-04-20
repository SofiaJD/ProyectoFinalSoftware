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
        private readonly IProyectosRepository _proyectosRepository;

        public TareasServices(ITareasRepository tareasRepository, IProyectosRepository proyectosRepository)
        {
            _tareasRepository = tareasRepository;
            _proyectosRepository = proyectosRepository;
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

            // Obtener el proyecto al que pertenece la tarea
            var proyecto = await _proyectosRepository.GetByIdAsync(vm.ProyectoID);
            if (proyecto == null)
            {
                // Manejar el caso donde el proyecto no existe
                throw new Exception("El proyecto especificado no existe.");
            }

            // Asignar el proyecto a la tarea
            tareas.Proyecto = proyecto;

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

            // Obtener todos los IDs de proyecto únicos
            var proyectoIds = tareasList.Select(t => t.ProyectoID).Distinct().ToList();

            // Obtener los nombres de proyecto correspondientes a los IDs
            var proyectoNombres = await _proyectosRepository.GetProyectoNombresByIDsAsync(proyectoIds);

            return tareasList.Select(tarea => new TareasViewModel
            {
                Id = tarea.Id,
                Nombre = tarea.Nombre,
                Descripcion = tarea.Descripcion,
                FechaInicio = tarea.FechaInicio,
                FechaFin = tarea.FechaFin,
                Estado = tarea.Estado,
                ProyectoNombre = proyectoNombres.ContainsKey(tarea.ProyectoID) ? proyectoNombres[tarea.ProyectoID] : null
            }).ToList();
        }

        public async Task<TareasViewModel> GetByIdAsync(int id)
        {
            var tareas = await _tareasRepository.GetByIdAsync(id);


            if (tareas == null)
            {
                // Manejar el caso donde la tarea no existe
                throw new Exception("La tarea especificada no existe.");
            }

            // Obtener el nombre del proyecto asociado a la tarea
            string nombreProyecto = await _proyectosRepository.GetProyectoNombreByTareaIdAsync(id);

            TareasViewModel vm = new TareasViewModel
            {
                Id = tareas.Id,
                Nombre = tareas.Nombre,
                Descripcion = tareas.Descripcion,
                FechaInicio = tareas.FechaInicio,
                FechaFin = tareas.FechaFin,
                Estado = tareas.Estado,
                ProyectoNombre = nombreProyecto
            };

            return vm;
        }

        public async Task UpdateAsync(SaveTareasViewModel vm)
        {
            // Obtener la tarea existente desde la base de datos
            var tareaExistente = await _tareasRepository.GetByIdAsync(vm.Id);

            if (tareaExistente == null)
            {
                // Manejar el caso en el que la tarea no existe
                throw new Exception("La tarea no existe.");
            }

            // Actualizar las propiedades de la tarea existente
            tareaExistente.Nombre = vm.Nombre;
            tareaExistente.Descripcion = vm.Descripcion;
            tareaExistente.FechaInicio = vm.FechaInicio;
            tareaExistente.FechaFin = vm.FechaFin;
            tareaExistente.Estado = vm.Estado;
            tareaExistente.ProyectoID = vm.ProyectoID;

            // Marcar la entidad como modificada y guardar los cambios
            await _tareasRepository.UpdateAsync(tareaExistente);
        }
    }
}
