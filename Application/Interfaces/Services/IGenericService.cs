
namespace Application.Interfaces.Services
{
    public interface IGenericService<SaveViewModel, ViewModel>
        where SaveViewModel : class
        where ViewModel : class
    {
        Task AddAsync(SaveViewModel vm);
        Task UpdateAsync(SaveViewModel vm);
        Task DeleteAsync(int id);
        Task<ViewModel> GetByIdAsync(int id);
        Task<List<ViewModel>> GetAllAsync();
    }
}
