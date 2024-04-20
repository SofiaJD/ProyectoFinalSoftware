using Application.Interfaces.Services;
using Application.ViewModels.Usuarios;
using Domain.Entities;


namespace Application.Interfaces.Servicios
{
    public interface IUsuariosServices : IGenericService<SaveUsuariosViewModel, UsuariosViewModel>
    {
        Task<UsuariosViewModel> Login(LoginViewModel loginVm);
        Task<Usuarios> VerifyUser(string Token);
        Task<bool> ForgotPasswordAsync(string email);
        Task<bool> ResetPasswordAsync(ResetPasswordViewModel resetPasswordViewModel);


    }
}

