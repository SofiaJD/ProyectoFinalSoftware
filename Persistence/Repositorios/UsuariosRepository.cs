using Application.Helpers;
using Application.Interfaces.Repositories;
using Application.Interfaces.Repositorios;
using Application.ViewModels.Usuarios;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contextos;

namespace Persistence.Repositorios
{
    public class UsuariosRepository : GenericRepository<Usuarios>, IUsuariosRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UsuariosRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task AddAsync(Usuarios entity)
        {
            entity.Password = PasswordEncryptation.ComputeSha256Hash(entity.Password);  
            await base.AddAsync(entity);
        }

        public async Task<Usuarios> LoginAsync(LoginViewModel loginVm)
        {
            string EncryptPassword = PasswordEncryptation.ComputeSha256Hash(loginVm.Password);
            Usuarios usuario = await _dbContext.Set<Usuarios>()
                .FirstOrDefaultAsync(usuario => usuario.Email == loginVm.Email && usuario.Password == EncryptPassword);
            return usuario;
        }
    }
}
