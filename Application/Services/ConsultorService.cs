using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.ViewModels.Consultor;
using Domain.Entities;

namespace Application.Services
{
    public class ConsultorService : IConsultorService 
    {
        private readonly IConsultorRepository _consultorRepository;

        public ConsultorService(IConsultorRepository consultorRepository)
        {
            _consultorRepository = consultorRepository;
        }

        public async Task AddAsync(SaveConsultorViewModel vm)
        {
            Consultor consultor = new();
            consultor.Id = vm.Id;
            consultor.Nombre = vm.Nombre;
            consultor.Email = vm.Email;
            consultor.Telefono = vm.Telefono;
            consultor.Especialidad = vm.Especialidad;


            await _consultorRepository.AddAsync(consultor);
        }
        public async Task DeleteAsync(int id)
        {
            var consultor = await _consultorRepository.GetByIdAsync(id);
            await _consultorRepository.DeleteAsync(consultor);
        }

        public async Task<List<ConsultorViewModel>> GetAllAsync()
        {
            var consultorList = await _consultorRepository.GetAllAsync();

            return consultorList.Select(consultor => new ConsultorViewModel
            {
                Id = consultor.Id,
                Nombre = consultor.Nombre,
                Email = consultor.Email,
                Telefono = consultor.Telefono,
                Especialidad = consultor.Especialidad,
            }).ToList();
        }

        public async Task<ConsultorViewModel> GetByIdAsync(int id)
        {
            var consultor = await _consultorRepository.GetByIdAsync(id);

            ConsultorViewModel vm = new();
            vm.Id = consultor.Id;
            vm.Nombre = consultor.Nombre;
            vm.Email = consultor.Email;
            vm.Telefono = consultor.Telefono;
            vm.Especialidad = consultor.Especialidad;

            return vm; ;
        }

        public async Task UpdateAsync(SaveConsultorViewModel vm)
        {
            Consultor consultor = new();
            consultor.Id = vm.Id;
            consultor.Nombre = vm.Nombre;
            consultor.Email = vm.Email;
            consultor.Telefono = vm.Telefono;
            consultor.Especialidad = vm.Especialidad;

            await _consultorRepository.UpdateAsync(consultor);
        }

        public async Task<bool> IsEmailRegisteredAsync(string email)
        {
            var consultores = await _consultorRepository.GetAllAsync();
            return consultores.Any(c => c.Email == email);
        }

        public async Task<bool> IsTelefonoRegisteredAsync(string telefono)
        {
            var consultores = await _consultorRepository.GetAllAsync();
            return consultores.Any(c => c.Telefono == telefono);
        }

        public async Task<bool> IsEmailRegisteredForOtherConsultorAsync(int id, string email)
        {
            var consultores = await _consultorRepository.GetAllAsync();
            return consultores.Any(c => c.Id != id && c.Email == email);
        }

        public async Task<bool> IsTelefonoRegisteredForOtherConsultorAsync(int id, string telefono)
        {
            var consultores = await _consultorRepository.GetAllAsync();
            return consultores.Any(c => c.Id != id && c.Telefono == telefono);
        }
    }
}
