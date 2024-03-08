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
    public class ConsultorRepository : GenericRepository<Consultor>, IConsultorRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ConsultorRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
