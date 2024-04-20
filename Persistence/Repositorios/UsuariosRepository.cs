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
            if (_dbContext.Usuarios.Any(u => u.Email == entity.Email))
            {
                throw new InvalidOperationException("Ya existe un usuario con este correo electrónico.");
            }

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

        public async Task<Usuarios> GetUserByEmailAsync(string email)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Usuarios> GetByVerificationTokenAsync(string token)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.VerificationToken == token);
        }

        public async Task<Usuarios> GetByResetTokenAsync(string token)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(u => u.PasswordResetToken == token);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
