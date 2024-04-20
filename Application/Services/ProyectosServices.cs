using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Application.Interfaces.Repositorios;
using Application.Interfaces.Services;
using Application.ViewModels.Proyectos;
using Application.ViewModels.Usuarios;
using Domain.Entities;

namespace Application.Services
{
    public class ProyectosServices : IProyectosService
    {
        private readonly IProyectosRepository _proyectosRepository;
        private readonly IClientesRepository _clientesRepository;
        public ProyectosServices(IProyectosRepository proyectosRepository, IClientesRepository clientesRepository)
        {
            _proyectosRepository = proyectosRepository;
            _clientesRepository = clientesRepository;
        }

        public async Task AddAsync(SaveProyectosViewModel vm)
        {
            var clienteExistente = await _clientesRepository.GetByIdAsync(vm.ClienteID);
            if (clienteExistente == null)
            {
                throw new Exception("El ClienteID proporcionado no existe en la base de datos.");
            }

            // Crear una nueva instancia de Proyectos y asignar los valores del ViewModel
            Proyectos proyecto = new Proyectos
            {
                Nombre = vm.Nombre,
                Descripcion = vm.Descripcion,
                FechaInicio = vm.FechaInicio,
                FechaFin = vm.FechaFin,
                Estado = vm.Estado,
                ClienteID = vm.ClienteID // Asignar el ClienteID proporcionado
            };

            // Agregar el proyecto a la base de datos
            await _proyectosRepository.AddAsync(proyecto);
        }

        public async Task DeleteAsync(int id)
        {
            var usuarios = await _proyectosRepository.GetByIdAsync(id);
            await _proyectosRepository.DeleteAsync(usuarios);
        }

        public async Task<List<ProyectosViewModel>> GetAllAsync()
        {
            var proyectosList = await _proyectosRepository.GetAllAsync();

            var proyectosViewModelList = new List<ProyectosViewModel>();

            foreach (var proyecto in proyectosList)
            {
                var cliente = await _clientesRepository.GetByIdAsync(proyecto.ClienteID);
                var proyectoViewModel = new ProyectosViewModel
                {
                    Id = proyecto.Id,
                    Descripcion = proyecto.Descripcion,
                    Nombre = proyecto.Nombre,
                    FechaInicio = proyecto.FechaInicio,
                    FechaFin = proyecto.FechaFin,
                    Estado = proyecto.Estado,
                    NombreCliente = cliente?.Nombre // Usamos la navegación a cliente para obtener el nombre
                };

                proyectosViewModelList.Add(proyectoViewModel);
            }

            return proyectosViewModelList;
        }

        public async Task<ProyectosViewModel> GetByIdAsync(int id)
        {
            var proyecto = await _proyectosRepository.GetByIdAsync(id);

            if (proyecto == null)
            {
                return null; 
            }

            var cliente = await _clientesRepository.GetByIdAsync(proyecto.ClienteID);

            var proyectoViewModel = new ProyectosViewModel
            {
                Id = proyecto.Id,
                Nombre = proyecto.Nombre,
                Descripcion = proyecto.Descripcion,
                FechaInicio = proyecto.FechaInicio,
                FechaFin = proyecto.FechaFin,
                Estado = proyecto.Estado,
                NombreCliente = cliente?.Nombre 
            };

            return proyectoViewModel;
        }

        public async Task UpdateAsync(SaveProyectosViewModel vm)
        {
            {
                // Obtener el proyecto existente desde la base de datos
                var proyectoExistente = await _proyectosRepository.GetByIdAsync(vm.Id);

                if (proyectoExistente == null)
                {
                    // Manejar el caso en el que el proyecto no existe
                    throw new Exception("El proyecto no existe.");
                }

                // Actualizar las propiedades del proyecto existente
                proyectoExistente.Nombre = vm.Nombre;
                proyectoExistente.Descripcion = vm.Descripcion;
                proyectoExistente.FechaInicio = vm.FechaInicio;
                proyectoExistente.FechaFin = vm.FechaFin;
                proyectoExistente.Estado = vm.Estado;
                proyectoExistente.ClienteID = vm.ClienteID; // Asignar directamente el Id del cliente

                // Marcar la entidad como modificada y guardar los cambios
                await _proyectosRepository.UpdateAsync(proyectoExistente);
            }
        }
    }
}
