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

        public ProyectosServices(IProyectosRepository proyectosRepository)
        {
            _proyectosRepository = proyectosRepository;
        }

        public async Task AddAsync(SaveProyectosViewModel vm)
        {
            Proyectos proyectos = new();
            proyectos.Id = vm.Id;
            proyectos.Nombre = vm.Nombre;
            proyectos.Descripcion = vm.Descripcion;
            proyectos.FechaInicio = vm.FechaInicio;
            proyectos.FechaFin = vm.FechaFin;
            proyectos.Estado = vm.Estado;
            proyectos.ClienteID = vm.ClienteID;

            await _proyectosRepository.AddAsync(proyectos);
        }

        public async Task DeleteAsync(int id)
        {
            var usuarios = await _proyectosRepository.GetByIdAsync(id);
            await _proyectosRepository.DeleteAsync(usuarios);
        }

        public async Task<List<ProyectosViewModel>> GetAllAsync()
        {
            var proyectosList = await _proyectosRepository.GetAllAsync();

            return proyectosList.Select(proyectos => new ProyectosViewModel
            {
                Id = proyectos.Id,
                Descripcion = proyectos.Descripcion,
                Nombre = proyectos.Nombre,
                FechaInicio = proyectos.FechaInicio,
                FechaFin = proyectos.FechaFin,
                Estado = proyectos.Estado,
                NombreCliente = proyectos.Cliente.Nombre
            }).ToList();
        }

        public async Task<ProyectosViewModel> GetByIdAsync(int id)
        {
            var proyectos = await _proyectosRepository.GetByIdAsync(id);

            ProyectosViewModel vm = new();
            vm.Id = proyectos.Id;
            vm.Nombre = proyectos.Nombre;
            vm.Descripcion = proyectos.Descripcion;
            vm.FechaInicio= proyectos.FechaInicio;
            vm.FechaFin = proyectos.FechaFin;
            vm.Estado = proyectos.Estado;
            vm.NombreCliente = proyectos.Cliente.Nombre;

            return vm;
        }

        public async Task UpdateAsync(SaveProyectosViewModel vm)
        {
            Proyectos proyectos = new();
            proyectos.Id = vm.Id;
            proyectos.Nombre = vm.Nombre;
            proyectos.Descripcion = vm.Descripcion;
            proyectos.FechaInicio = vm.FechaInicio;
            proyectos.FechaFin = vm.FechaFin;
            proyectos.Estado = vm.Estado;
            proyectos.Cliente.Id = vm.ClienteID;

            await _proyectosRepository.UpdateAsync(proyectos);
        }
    }
}
