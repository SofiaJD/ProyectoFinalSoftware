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
    public class TareasRepository : GenericRepository<Tareas>, ITareasRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TareasRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
