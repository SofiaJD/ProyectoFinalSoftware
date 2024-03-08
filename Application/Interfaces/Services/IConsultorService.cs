using Application.ViewModels.Consultor;

namespace Application.Interfaces.Services
{
    public interface IConsultorService : IGenericService<SaveConsultorViewModel, ConsultorViewModel>
    {
        Task<bool> IsEmailRegisteredAsync(string email);
        Task<bool> IsTelefonoRegisteredAsync(string telefono);
        Task<bool> IsEmailRegisteredForOtherConsultorAsync(int id, string email);
        Task<bool> IsTelefonoRegisteredForOtherConsultorAsync(int id, string telefono);
    }
}
