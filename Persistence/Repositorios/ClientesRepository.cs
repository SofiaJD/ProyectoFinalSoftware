using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Persistence.Contextos;

namespace Persistence.Repositorios
{
    public class ClientesRepository : GenericRepository<Clientes>, IClientesRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ClientesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
