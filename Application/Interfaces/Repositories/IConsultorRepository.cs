using Domain.Entities;

namespace Application.Interfaces.Repositories
{
    public interface IConsultorRepository : IGenericRepository<Consultor>
    {
        Task<string> GetConsultorNombre(int consultorId);
    }
}
