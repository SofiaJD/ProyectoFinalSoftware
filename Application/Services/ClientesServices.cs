using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Application.Interfaces.Repositorios;
using Application.Interfaces.Services;
using Application.ViewModels.Clientes;
using Application.ViewModels.Usuarios;
using Domain.Entities;

namespace Application.Services
{
    public class ClientesServices : IClientesService
    {
        private readonly IClientesRepository _clientesRepository;
        
        public ClientesServices(IClientesRepository clientesRepository)
        {
            _clientesRepository = clientesRepository;
        }

        public async Task AddAsync(SaveClientesViewModel vm)
        {
            Clientes clientes = new();
            clientes.Id = vm.Id;
            clientes.Nombre = vm.Nombre;
            clientes.Email = vm.Email;
            clientes.Contacto = vm.Contacto;
            clientes.Telefono = vm.Telefono;

            await _clientesRepository.AddAsync(clientes);
        }

        public async Task DeleteAsync(int id)
        {
            var clientes = await _clientesRepository.GetByIdAsync(id);
            await _clientesRepository.DeleteAsync(clientes);
        }

        public async Task<List<ClientesViewModel>> GetAllAsync()
        {
            var clientesList = await _clientesRepository.GetAllAsync();

            return clientesList.Select(clientes => new ClientesViewModel
            {
                Id = clientes.Id,
                Nombre = clientes.Nombre,
                Email = clientes.Email,
                Telefono = clientes.Telefono,
                Contacto = clientes.Contacto
            }).ToList();
        }

        public async Task<ClientesViewModel> GetByIdAsync(int id)
        {
            var clientes = await _clientesRepository.GetByIdAsync(id);

            ClientesViewModel vm = new();
            vm.Id = clientes.Id;
            vm.Nombre = clientes.Nombre;
            vm.Contacto = clientes.Contacto;
            vm.Email = clientes.Email;
            vm.Telefono = clientes.Telefono;

            return vm;
        }

        public async Task UpdateAsync(SaveClientesViewModel vm)
        {
            Clientes clientes = new();
            clientes.Id = vm.Id;
            clientes.Nombre = vm.Nombre;
            clientes.Contacto = vm.Contacto;
            clientes.Email = vm.Email;
            clientes.Telefono = vm.Telefono;

            await _clientesRepository.UpdateAsync(clientes);
        }
    }
}
