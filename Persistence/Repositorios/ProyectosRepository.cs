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
    public class ProyectosRepository : GenericRepository<Proyectos>, IProyectosRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProyectosRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
