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

        public async Task<string> GetConsultorNombre(int consultorId)
        {
            // Aquí debes obtener el consultor en base al ID proporcionado.
            var consultor = await _dbContext.Consultores.FindAsync(consultorId);

            // Verificamos si el consultor existe
            if (consultor != null)
            {
                return consultor.Nombre;
            }
            else
            {
                // Maneja el caso donde el consultor no existe
                throw new ArgumentException("El consultor especificado no existe.");
            }
        }
    }
}
